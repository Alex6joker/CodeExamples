#include <stdio.h>
#include <winsock2.h>
#include <ws2tcpip.h>
#include <windows.h>
#include <stdlib.h>
#include "TokenRingGlobals.h"
#include "TokenRingDefines.h"
#include "TokenRingStructs.h"
#include "TokenRingFuncs.h"

void WaitNewClients(LPVOID TokenNetServerSocket)
{
	while (1)
	{
		listen((SOCKET)TokenNetServerSocket, SOMAXCONN); // Прослушиваем очередь сообщений
		SOCKET Client = accept((SOCKET)TokenNetServerSocket, &new_client, 0); // Ожиданем клиента
		// Создаем поток ожидания клиентов даннного сервера
		if (Client != SOCKET_ERROR) HANDLE hThreadWorkWithClientMessage = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)WorkWithClientMessage, (LPVOID)Client, 0, 0);
		Sleep(0);
	}
}

void reconnect()
{
	sockaddr_in name;
	closesocket(TokenNetClientSocket); //  Отсоединяем сокет
	TokenNetClientSocket = socket(AF_INET, SOCK_STREAM, 0);
	name.sin_family = AF_INET; // Тип адреса
	name.sin_addr.s_addr = inet_addr(Token_Ring->next->IP); // IP-адрес сервера (при создании сервера можно 0)
	name.sin_port = htons(DEFAULT_PORT); // Номер порта сервера
	connect(TokenNetClientSocket, (sockaddr*)&name, sizeof(name));
}

void WorkWithClientMessage(LPVOID ClientSocket)
{
	ResetEvent(hEventEndWorkWithMsgFormClient); // Сигнализируем, что начинаем работу над полученным сообщением
	char* Message = (char*)malloc(DEFAULT_BUFFER);
	while (recv((SOCKET)ClientSocket, Message, DEFAULT_BUFFER, 0) == 0) // Получение сообщения от клиента
		Sleep(100);
	// Обрабатываем полученное сообщение
	int Received_Msg_Priority = Message[0]; // Считываем приоритет сообщения
	int Received_Msg_Type = Message[1] - 0x30;    // Считываем тип сообщения (0 - кадр данных, 1 - маркер)
	Message += 2;
	switch (Received_Msg_Priority | Received_Msg_Type)
	{
	case	ACCEPT_AND_SEND_DATA:
	{
		// Принимаем данные от отправителя и анализируем получаен скан-код или key-код
		char* AnalysingMsg = (char*)malloc(DEFAULT_BUFFER);
		char* begin = AnalysingMsg;
		char Target_IP[20];
		CONSOLE_SCREEN_BUFFER_INFO bi;
		GetConsoleScreenBufferInfo(GetStdHandle(STD_OUTPUT_HANDLE), &bi);
		strcpy(AnalysingMsg, Message);
		AnalysingMsg = strtok(AnalysingMsg, " ");
		strcpy(Target_IP, AnalysingMsg);
		if (!strcmp(My_IP, Target_IP)) // Если кадр прошел полный круг по кольцу
		{
			Message -= 2;
			Message[0] = 0;
			Message[1] = '1';
			Message[2] = '\0';
			reconnect();
			DWORD ret = send(TokenNetClientSocket, Message, 3, 0); // Передаем маркер следующему абоненту по кольцу
			free(AnalysingMsg);
			break;
		}
		// Проверяем, сообщение какого-вида пришло (0 - скан-код, 1 - key-код)
		AnalysingMsg = strtok(0, " ");
		if (AnalysingMsg[0] - 0x30 == 0)
		{
			// Вызываем функции перестройки для скан кода
			AnalysingMsg = strtok(0, " ");
			if (AnalysingMsg[0] == 0x0e)
			{
				AnalysingMsg = strtok(0, " "); // Читаем позицию
				BACKSPACE_Remake_Text(atoi(AnalysingMsg));
			}
			else if (AnalysingMsg[0] == 0x53)
			{
				AnalysingMsg = strtok(0, " "); // Читаем позицию
				DELETE_Remake_Text(atoi(AnalysingMsg));
			}
		}
		else
		{
			// Вызываем функции перестройки для простых клавиш
			AnalysingMsg = strtok(0, " "); // Читаем символ
			char symbol = AnalysingMsg[0]; // сохраняем символ
			AnalysingMsg = strtok(0, " "); // Читаем позицию
			FileBuffer = Remake_Text(atoi(AnalysingMsg), symbol);
		}
		system("cls");
		printf("%s", FileBuffer);
		SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), bi.dwCursorPosition); // Сохраняем курсор на той же позиции
		// Отправка сообщения следующему абоненту по кольцу
		Message -= 2; // Возвращаем сообщению все его атрибуты
		free(begin);
		reconnect();
		DWORD ret = send(TokenNetClientSocket, Message, strlen(Message) + 1, 0); // Передаем маркер следующему абоненту по кольцу
		break;
	}

	case	SEND_TOKEN:
	{	// Получили маркер, теперь имеем право отправлять различные данные по сети
		strcpy(Token.Owner_IP, My_IP); // Указывем, что мы являемся владельцами
		SetEvent(hEventIAmOwner); // Ставим событие, что мы владельцы (для удаления пользователя, чтобы не было потери маркера)
		// Проверяем, одни ли мы в сети
		// Проверка очереди сообщений на обработку
		if (Message_List != NULL) // Если в очереди есть элементы
		{
			WaitForMultipleObjects(2, hEventWorkWithMessageList, TRUE, INFINITE); // Ждем пока все действия с очередью сообщений будут выполнены
			// Забираем верхнее сообщение из списка
			Message -= 2; // Возвращаем сообщению все его атрибуты
			Take_Package_With_The_Highest_Priority(Message);
			if (Message[0] == RESTRUCT_TOKEN_RING) // Если подключается новый или удаляется старый пользователь и требуется перестроить кольцо и переподключить все сокеты
			{
				// Реконструировать кольцо будет сервер, перенаправляем клиентский сокет на самого себя для посылки кадра данных
				// Сам сокет изменит свое направление при реконструировании кольца
				sockaddr_in name;
				closesocket(TokenNetClientSocket); //  Отсоединяем сокет
				TokenNetClientSocket = socket(AF_INET, SOCK_STREAM, 0);
				name.sin_family = AF_INET; // Тип адреса
				name.sin_addr.s_addr = inet_addr(My_IP); // IP-адрес сервера (при создании сервера можно 0)
				name.sin_port = htons(DEFAULT_PORT); // Номер порта сервера
				connect(TokenNetClientSocket, (sockaddr*)&name, sizeof(name));
				DWORD ret = send(TokenNetClientSocket, Message, strlen(Message) + 1, 0);
				WaitForSingleObject(hEventConnectNewUserToTokenRing, INFINITE);
			}
			else if (Message[0] == DOWNLOAD_FILE_DATA)
			{
				// Находим смещение
				char* FindingOffset = (char*)malloc(strlen(Message));
				strcpy(FindingOffset, Message);
				FindingOffset = strtok(FindingOffset, " ");
				FindingOffset = strtok(0, " ");
				if (FindingOffset)
					SendDataBuffer(atoi(FindingOffset), Message);
				else
					SendDataBuffer(0, Message);
			}
			else
			{ 
				// Если не присходит перестройки кольца, то просто передается сообщение следующему в кольце
				char AnalysingMsg[DEFAULT_BUFFER];
				Message += 2;
				strcpy(AnalysingMsg, Message);
				strtok(AnalysingMsg, " ");
				Message -= 2;
				reconnect();
				DWORD ret = send(TokenNetClientSocket, Message, strlen(Message) + 1, 0);
			}
			// Ждем выполнения полного оборота кадра данных сообщения
			//WaitForSingleObject(DataFrameIsDone, INFINITE);
		}
		else
			Message -= 2; // Возвращаем сообщению все его атрибуты
		Sleep(20);
		strcpy(Token.Owner_IP, ""); // Обозначаем, что больше не являемся владельцами маркера
		char Msg[3];
		Msg[0] = 0;
		Msg[1] = '1';
		Msg[2] = '\0';
		reconnect();
		DWORD ret = send(TokenNetClientSocket, Msg, 3, 0); // Передаем маркер следующему абоненту по кольцу
		break;
	}
	case	ACCEPT_NEW_TOKEN_RING:
	{
		char* NewTokenRing;
		NewTokenRing = new char[DEFAULT_BUFFER];
		strcpy(NewTokenRing, Message);

		// Проверяем, прошел ли кадр полный оборот по кольцу
		NewTokenRing = strtok(NewTokenRing, " ");
		if (!strcmp(My_IP, NewTokenRing)) // Если адрес совпадает, то оборот сделан
		{
			Message -= 2; // Возвращаем  сообщению все его атрибуты
			if (DeleteInProcessing)
			{
				DeleteInProcessing = FALSE;
				SOCKET  temp_socket;
				temp_socket = socket(AF_INET, SOCK_STREAM, 0);
				sockaddr_in temp_connect;
				temp_connect.sin_family = AF_INET; // Тип адреса
				temp_connect.sin_addr.s_addr = inet_addr(Deleting_IP); // IP-адрес сервера (при создании сервера можно 0)
				temp_connect.sin_port = htons(DEFAULT_PORT); // Номер порта сервера
				DWORD ret = connect((SOCKET)temp_socket, (sockaddr*)&temp_connect, sizeof(temp_connect));
				if (ret == SOCKET_ERROR) {
					wprintf(L"connect failed with error: %d\n", WSAGetLastError());
					WSACleanup();
					return;
				}
				char Msg[2];
				Msg[0] = END_OF_DELETION;
				Msg[1] = '0';
				ret = send(temp_socket, Msg, strlen(Msg) + 1, 0);
				ret = closesocket(temp_socket);
			}
			reconnect();
			if (hEventConnectNewUserToTokenRing != NULL)
			{
				SetEvent(hEventConnectNewUserToTokenRing);
			}
			break;
		}

		// Удаляем старое кольцо
		struct _Token_Ring *temp;
		temp = Token_Ring;
		if (Token_Ring != NULL) // Если уже есть сформированное кольцо
		{
			while (temp) // просматриваем список начиная с корня
			{ // пока не найдем узел, предшествующий Token_Ring
				temp = Token_Ring->next; // Корнем станет следующий элемент, если такой существует
				free(temp); // освобождаем память удаляемого узла
				if (!Token_Ring) temp = Token_Ring;
				else temp = NULL;
			}
			free(Token_Ring); // освобождаем память удаляемого узла
			Token_Ring = NULL;
		}

		// Загружаем новые данные для кольца
		while (NewTokenRing) // Пока есть лексемы (IP)
		{
			struct _Token_Ring *temp, *p;
			temp = (struct _Token_Ring*)malloc(sizeof(_Token_Ring));
			if (Token_Ring == NULL) // Если кольца для данной станции еще не было сформировано
			{
				strcpy(temp->IP, NewTokenRing); //Записываем IP своей рабочей станции
				Token_Ring = temp;
				temp->next = temp;
			}
			else
			{
				temp = Token_Ring;
				_Token_Ring* temp2 = (struct _Token_Ring*)malloc(sizeof(_Token_Ring));
				while (temp->next != Token_Ring) temp = temp->next;
				p = temp->next; // сохранение указателя на следующий элемент
				temp->next = temp2; // предыдущий узел указывает на создаваемый
				strcpy(temp2->IP, NewTokenRing); // сохранение поля данных добавляемого узла
				temp2->next = p; // созданный узел указывает на следующий элемент
			}
			NewTokenRing = strtok(0, " ");
		}
		delete[] NewTokenRing;

		// Ищем свое положение в кольце
		while (strcmp(My_IP, Token_Ring->IP))
		{
			Token_Ring = Token_Ring->next;
		}
		// Отсоединяем клиентский сокет от старого сервера
		sockaddr_in name;
		int n_l;
		n_l = sizeof(name);
		ZeroMemory(&name, sizeof(name));
		DWORD ret;
		ret = getsockname(TokenNetClientSocket, (sockaddr*)&name, &n_l);
		char string[300];
		strcpy(string, inet_ntoa(name.sin_addr));
		if (strcmp(inet_ntoa(name.sin_addr), Token_Ring->next->IP)) // Если сервер изменился
		{
			closesocket(TokenNetClientSocket); //  Отсоединяем сокет
			TokenNetClientSocket = socket(AF_INET, SOCK_STREAM, 0);
			name.sin_family = AF_INET; // Тип адреса
			name.sin_addr.s_addr = inet_addr(Token_Ring->next->IP); // IP-адрес сервера (при создании сервера можно 0)
			name.sin_port = htons(DEFAULT_PORT); // Номер порта сервера
			ret = connect(TokenNetClientSocket, (sockaddr*)&name, sizeof(name));
		}
		// Отправляем данные элементу дальше по кольцу
		Message -= 2; // Возвращаем  сообщению все его атрибуты
		ret = send(TokenNetClientSocket, Message, strlen(Message) + 1, 0); // Отправка посылки на сервер (будет успешна, если найден был сервер)
		break;
	}
	case	DOWNLOAD_FILE_DATA:
	{
		char* File = (char*)malloc(DEFAULT_BUFFER);
		char* File_begin = File;
		strcpy(File, Message);
		Message -= 2; // Возвращаем  сообщению все его атрибуты
		while (File[0] != ' ') // Доходим до текста, стоящего после IP
			File++;
		File++;

		int CurrentOffset = atoi(File);

		while (File[0] != ' ') // Доходим до текста, стоящего после IP
			File++;
		File++;
		// Принимаем текст и помещаем его в буффер
		FileBuffer = (char*)realloc(FileBuffer, DEFAULT_BUFFER + CurrentOffset); // Выделяем память меньше
		if (CurrentOffset == 0)
			strcpy(FileBuffer, File);
		else
			strcat(FileBuffer, File);

		// Если последний символ говорит о том, что нужно скачать следующую часть файла, то отправляем пакет снова
		if (FileBuffer[strlen(FileBuffer) - 1] == '1')
		{
			char* Msg = (char*)malloc(30);
			Msg[0] = WANT_TO_GET_FILEBUFFER;
			Msg[1] = '0';
			Msg[2] = '\0';
			strcat(Msg, My_IP);
			Msg += strlen(Msg);
			Msg[0] = ' ';
			Msg[1] = '\0';
			Msg++;
			itoa(strlen(FileBuffer) - 1, Msg, 10);
			Msg -= strlen(My_IP) + 3;
			Add_To_Msg_List(Msg);
			free(Msg);
			FileBuffer[strlen(FileBuffer) - 1] = '\0';
		}
		else
		{
			SetEvent(hEventEndDataDownload);
		}
		free(File_begin);
		break;
	}
		
	case RESTRUCT_TOKEN_RING:
	{
		// При получении IP клиента сервер перестраивает маркерное кольцо
		struct _Token_Ring *temp, *p;
		// Message[0] - символ данных (0 - добавить нового пользователя, 1 - удалить)
		if (Message[0] == '0') // Если требуется добавить пользователя в кольцо
		{
			Message++; // Переходим к IP
			temp = (struct _Token_Ring*)malloc(sizeof(_Token_Ring));
			p = Token_Ring->next; // сохранение указателя на следующий элемент
			Token_Ring->next = temp; // предыдущий узел указывает на создаваемый
			strcpy(temp->IP, Message); // сохранение поля данных добавляемого узла
			temp->next = p; // созданный узел указывает на следующий элемент
		}
		else // Иначе требуется удалить компьютер из кольца
		{
			Message++; // Переходим к IP
			temp = Token_Ring;
			while (strcmp(temp->next->IP, Message)) // просматриваем список начиная с корня
			{	// пока не найдем узел, предшествующий узлу с нужным IP
				temp = temp->next;
			}
			p = temp->next;
			temp->next = p->next;
			free(p);
		}
		// Отправляем перестроенное кольцо клиенту
		// Формируем кадр данных для принятия кольца новому клиенту
		temp = Token_Ring; // Запоминаем текущее звено кольца
		char NewTokenRing[DEFAULT_BUFFER];
		NewTokenRing[0] = 3;
		NewTokenRing[1] = '0';
		NewTokenRing[2] = '\0';
		do
		{
			strcat(NewTokenRing, Token_Ring->IP);
			strcat(NewTokenRing, " ");
			Token_Ring = Token_Ring->next;
		} while (Token_Ring != temp);
		NewTokenRing[strlen(NewTokenRing) - 1] = '\0';

		// Узнаем тип сообщения
		// Message[0] - символ данных (0 - добавить нового пользователя, 1 - удалить)
		Message--;

		DWORD ret;
		SOCKET  temp_socket;
		temp_socket = socket(AF_INET, SOCK_STREAM, 0);
		sockaddr_in temp_connect;
		temp_connect.sin_family = AF_INET; // Тип адреса

		if (Message[0] == '0')
		{
			Message++;
			temp_connect.sin_addr.s_addr = inet_addr(Message); // IP-адрес сервера (при создании сервера можно 0)
		}
		else
		{
			Message++;
			temp_connect.sin_addr.s_addr = inet_addr(Token_Ring->next->IP); // IP-адрес сервера (при создании сервера можно 0)
		}

		temp_connect.sin_port = htons(DEFAULT_PORT); // Номер порта сервера
		ret = closesocket((SOCKET)ClientSocket);
		ret = connect((SOCKET)temp_socket, (sockaddr*)&temp_connect, sizeof(temp_connect));
		if (ret == SOCKET_ERROR) {
			wprintf(L"connect failed with error: %d\n", WSAGetLastError());
			WSACleanup();
			return;
		}
		ret = send(temp_socket, NewTokenRing, strlen(NewTokenRing) + 1, 0);
		ret = closesocket(temp_socket);
		Message -= 3; // Возвращаем  сообщению все его атрибуты
		break;
	}
	case CREATE_TOKEN:
	{
		// Устанавливаем начальные начения для токена
		strcpy(Token.Owner_IP, My_IP); //Записываем IP владельца созданного токена
		Token.Priority = 0; // Токен с пустой операцией
		Token.Token_bit = '1'; // Токен не будет удерживаться
		break;
	}
	case BEGIN_CONNECT_NEW_USER:
	{
		Message -= 2; // Возвращаем  сообщению все его атрибуты
		Message[0] = RESTRUCT_TOKEN_RING;
		Add_To_Msg_List(Message); // Добавляем сообщение в очередь сообщений
		break;
	}
	case DELETE_USER:
	{
		Message++;
		strcpy(Deleting_IP, Message);
		Message -= 3; // Возвращаем  сообщению все его атрибуты
		Message[0] = RESTRUCT_TOKEN_RING;
		DeleteInProcessing = TRUE;
		Add_To_Msg_List(Message); // Добавляем сообщение в очередь сообщений
		break;
	}
	case END_OF_DELETION:
	{
		SetEvent(hEventEndWorkDeleteClient);
		Message -= 2; // Возвращаем сообщению все его атрибуты
		break;
	}
	case WANT_TO_GET_FILEBUFFER:
	{
		Message -= 2; // Возвращаем сообщению все его атрибуты
		Message[0] = DOWNLOAD_FILE_DATA;
		Add_To_Msg_List(Message); // Добавляем сообщение в очередь сообщений
		break;
	}	
	default: // Возникало только при потере маркера приложением
		Message -= 2; // Возвращаем сообщению все его атрибуты
		SendTokenToTokenRing(0);
	}
	closesocket((SOCKET)ClientSocket);
	free(Message);
	SetEvent(hEventEndWorkWithMsgFormClient); // Сигнализируем, что закончили работу над полученным сообщением
}

void SendTokenToTokenRing(LPVOID lParam)
{
	char Message[3];
	Message[0] = Token.Priority;
	Message[1] = '1';
	Message[2] = '\0';
	sockaddr_in name;
	closesocket(TokenNetClientSocket); //  Отсоединяем сокет
	TokenNetClientSocket = socket(AF_INET, SOCK_STREAM, 0);
	name.sin_family = AF_INET; // Тип адреса
	name.sin_addr.s_addr = inet_addr(My_IP); // IP-адрес сервера (при создании сервера можно 0)
	name.sin_port = htons(DEFAULT_PORT); // Номер порта сервера
	DWORD ret = connect(TokenNetClientSocket, (sockaddr*)&name, sizeof(name));
	send((SOCKET)TokenNetClientSocket, Message, 3, 0); // Запускаем маркер циркулировать по кольцу
}

void SendDataBuffer(int Offset, char* Message)
{
	char* Msg = (char*)malloc(DEFAULT_BUFFER);
	Msg[0] = DOWNLOAD_FILE_DATA;
	Msg[1] = '0';
	Msg[2] = '\0';
	Msg += 2;

	Message += 2; // Переходим к IP адресата
	strtok(Message, " ");
	strcat(Msg, Message);

	Msg[strlen(Message)] = ' ';
	Msg[strlen(Message) + 1] = '\0';
	Msg += strlen(Message) + 1;
	itoa(Offset, Msg, 10);
	FileBuffer += Offset;
	// Считаем кол-во цифр в смещении
	int count = 1;
	int temp = Offset;
	while (temp = temp / 10)
		count++;
	Msg[count] = ' ';
	Msg[count + 1] = '\0';
	Msg -= strlen(Message) + 1;

	strncat(Msg, FileBuffer, DEFAULT_BUFFER - 25);
	FileBuffer -= Offset;
	Msg -= 2;
	if (strlen(Msg) >= DEFAULT_BUFFER - 25)
	{
		if (strlen(FileBuffer) > strlen(Msg))
		{
			int lenght = strlen(Msg);
			Msg[lenght] = '1';
			Msg[lenght + 1] = '\0';
		}
	}

	sockaddr_in name;
	closesocket(TokenNetClientSocket); //  Отсоединяем сокет
	SOCKET SendTokenSocket = TokenNetClientSocket = socket(AF_INET, SOCK_STREAM, 0);
	name.sin_family = AF_INET; // Тип адреса
	name.sin_addr.s_addr = inet_addr(Message); // IP-адрес сервера (при создании сервера можно 0)
	name.sin_port = htons(DEFAULT_PORT); // Номер порта сервера
	DWORD ret = connect(SendTokenSocket, (sockaddr*)&name, sizeof(name));
	ret = send((SOCKET)TokenNetClientSocket, Msg, strlen(Msg) + 1, 0); // Отсылаем часть общего файла клиенту

	Message -= 2; // Возвращаем обратно
	free(Msg);
}