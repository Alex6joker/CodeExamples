#include <stdio.h>
#include <winsock2.h>
#include <ws2tcpip.h>
#include <windows.h>
#include <stdlib.h>
#include <conio.h>
#include "TokenRingGlobals.h"
#include "TokenRingDefines.h"
#include "TokenRingStructs.h"
#include "TokenRingFuncs.h"

#pragma comment (lib, "ws2_32.lib")
#pragma comment (lib, "ole32.lib")
#pragma comment (lib, "Iphlpapi.lib")


int main()
{
	SetConsoleCP(1251);
	SetConsoleOutputCP(1251);
	hEscapeEvent = CreateEvent(NULL, FALSE, FALSE, NULL);
	// Событие с ручным сбросом (сбрасывается только во время перестройки), изначально в сигнальном состоянии (со списком не ведется работа)
	hEventEndDataDownload = CreateEvent(NULL, FALSE, FALSE, NULL);
	hEventEndWorkDeleteClient = CreateEvent(NULL, FALSE, FALSE, NULL); // Событие конца работы с перестройкой маркерного кольца при удалении
	hEventWorkWithMessageList[0] = CreateEvent(NULL, TRUE, TRUE, NULL);
	hEventWorkWithMessageList[1] = CreateEvent(NULL, TRUE, TRUE, NULL);
	hEventIAmOwner = CreateEvent(NULL, FALSE, FALSE, NULL);
	hEventEndWorkWithMsgFormClient = CreateEvent(NULL, TRUE, TRUE, NULL);
	hEventConnectNewUserToTokenRing = CreateEvent(NULL, FALSE, FALSE, NULL); // По этому событию будем ждать подключения второго клиента к кольцу
	// Подключаем библиотеку WinSock
	WinSockConnect();
	
	// Получаем имя компьютера и все его IP-адреса
	int adress_count;
	struct in_addr* addr = GetHostNameAndAddresses(&adress_count);
	
	// Выбираем, какой IP хотим использовать
	unsigned int number = ChooseMyIP(adress_count, addr);
	strcpy(My_IP, inet_ntoa(addr[number]));

	// Получаем arp таблицу, которая содержит в себе все IP станций, связанных с данной
	int num_of_IPs = ARP_TABLE_SIZE;
	char** PC_IP_in_LVS = GetArpTable(&num_of_IPs);
	// Запускаем серверную часть, чтобы принимать сообщения от предыдущего по кольцу
	// Создаем сокет клиента
	//						По TCP/UPD	TCP		  0 протокол
	TokenNetServerSocket = socket(AF_INET, SOCK_STREAM, 0);
	sin_Server.sin_family = AF_INET; // Тип адреса
	sin_Server.sin_addr.s_addr = 0; // IP-адрес сервера (при создании сервера можно 0)
	sin_Server.sin_port = htons(DEFAULT_PORT); // Номер порта сервера
	DWORD iResult = bind(TokenNetServerSocket, (sockaddr*)&sin_Server, sizeof(sin_Server)); // Связывание созданного сокета с адресом sin
	if (iResult == SOCKET_ERROR) {
		wprintf(L"failed binding server with error: %d\n", WSAGetLastError());
		WSACleanup();
		return 1;
	}
	CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)WaitNewClients, (LPVOID)TokenNetServerSocket, 0, 0); // Создаем поток ожидания клиентов даннного сервера
	

	// Отбабатываем подключение ко всем компьютерам в сети
	sockaddr_in sin; // Свойства(адрес) создаваемого сокета
	memset((sockaddr_in*)&sin, 0, sizeof(sin));
	// Создаем сокет клиента
	//						По TCP/UPD	TCP		  0 протокол
	TokenNetClientSocket = socket(AF_INET, SOCK_STREAM, 0);

	char* Server_IP = (char*)malloc(30);
	UserQuestions(num_of_IPs, PC_IP_in_LVS, &sin, TokenNetClientSocket, Server_IP);
	if (ConnectOrDisconnectMeFromTokenRing(TokenNetClientSocket, My_IP, BEGIN_CONNECT_NEW_USER, '0') == SOCKET_ERROR)
	{ 
		printf("Создание маркера и отправка в сеть");
		// Создаем маркерное кольцо
		_Token_Ring *temp = new _Token_Ring; //Выделяем память для нового элемента
		strcpy(temp->IP, inet_ntoa(addr[number])); //Записываем IP своей рабочей станции
		Token_Ring = temp;
		temp->next = temp;
		// Cоздаем принудительно маркер в сети
		// Устанавливаем начальные начения для токена
		strcpy(Token.Owner_IP, My_IP); //Записываем IP владельца созданного токена
		Token.Priority = 0; // Токен с пустой операцией
		Token.Token_bit = '1'; // Токен не будет удерживаться
		strcpy(Token.Owner_IP, My_IP); // Указывем, что мы являемся первоначальными владельцами маркера
		CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)SendTokenToTokenRing, 0, 0, 0); // Создаем поток посылки токена по кольцу
	}
	free(Server_IP);

	CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)DocumentWork, NULL, NULL, NULL);// Создаем поток для работы с файлом
	WaitForSingleObject(hEscapeEvent, INFINITE); // Ждем, пока пользователь пожелает выйти
	system("cls");
	printf("Выключение программы");
	WaitForSingleObject(hEventIAmOwner, INFINITE); // Должны дождаться, что мы владеем маркером
	if (strcmp(My_IP,Token_Ring->next->IP)) // Если были не одни
	{ 
		reconnect();
		ConnectOrDisconnectMeFromTokenRing(TokenNetClientSocket, inet_ntoa(addr[number]), DELETE_USER, '1');
		WaitForSingleObject(hEventEndWorkDeleteClient, INFINITE); // Должны дождаться, что маркерная сеть перестроена
	}
	// По окончанию работы отключаем DLL WinSock
	// Закрываем SOCKET
	iResult = closesocket(TokenNetServerSocket);
	if (iResult == SOCKET_ERROR) {
		wprintf(L"close failed with error: %d\n", WSAGetLastError());
		WSACleanup();
		return 1;
	}
	iResult = closesocket(TokenNetClientSocket);
	if (iResult == SOCKET_ERROR) {
		wprintf(L"close failed with error: %d\n", WSAGetLastError());
		WSACleanup();
		return 1;
	}
	WaitForSingleObject(hEventEndWorkWithMsgFormClient, INFINITE); // Ждем конца работы с сообщением

	WSACleanup();
	free(addr);
	// Удаляем кольцо
	struct _Token_Ring *temp = Token_Ring;
	if (Token_Ring != NULL) // Если уже есть сформированное кольцо
	{
		while (temp) // просматриваем список начиная с корня
		{ // пока не найдем узел, предшествующий Token_Ring
			temp = Token_Ring->next; // Корнем станет следующий элемент, если такой существует
			free(temp); // освобождаем память удаляемого узла
			if (!Token_Ring) temp = Token_Ring;
			else temp = NULL;
		}
	}
	for (int i = 0; i < num_of_IPs; i++)
	{
		free(PC_IP_in_LVS[i]);
	}
	CloseHandle(hEventConnectNewUserToTokenRing); // Событие подключения второго абонента к кольцу
	CloseHandle(hEscapeEvent); // Событие выхода из программы (нажатие escape)
	CloseHandle(hEventWorkWithMessageList[2]); // Событие незавершенной работы со списком сообщений
	CloseHandle(hstd);
	CloseHandle(hEventEndWorkWithMsgFormClient); // Событие конца работы с сообщением
	CloseHandle(hEventEndWorkDeleteClient);
	CloseHandle(hEventEndDataDownload); // Событие окончания загрузки содержания файла
	FileBuffer = (char*)realloc(FileBuffer, 2);
	free(FileBuffer);
	return 0;
}

char* UserQuestions(int num_of_IPs, char** IPs, sockaddr_in* sin, SOCKET TokenNetClientSocket, char* Server)
{
	char Answer = 0;
	while (Answer != 'Y' && Answer != 'N' && Answer != 'y' && Answer != 'n')
	{
		fflush(stdin);
		printf("Запущена ли копия программы на другом компьютере? Y/N: ");
		scanf("%c", &Answer);
	}
	if (Answer == 'Y' || Answer == 'y') // Если копия программы запущена на другом компьютере
	{ 
		Answer = 0;
		while (Answer != 'Y' && Answer != 'N' && Answer != 'y' && Answer != 'n')
		{
			fflush(stdin);
			printf("Знаете ли вы IP сервера Y/N: ");
			scanf("%c", &Answer);
		}
		if (Answer == 'Y' || Answer == 'y') // Если пользователь знает IP сервера
		{
			char Server_IP[20];
			printf("Введите IP сервера: ");
			scanf("%s", Server_IP);
			// Подключение 
			sin->sin_family = AF_INET; // Тип адреса
			sin->sin_addr.s_addr = inet_addr(Server_IP); // IP-адрес сервера (пори создании сервера можно 0)
			sin->sin_port = htons(DEFAULT_PORT); // Номер порта сервера
			DWORD ret = connect(TokenNetClientSocket, (sockaddr*)sin, sizeof(*sin));
			while (ret == SOCKET_ERROR)
			{
				printf("Неудалось подключиться к серверу по данному IP\nВведите другой IP сервера: ");
				scanf("%s", Server_IP);
				sin->sin_addr.s_addr = inet_addr(Server_IP); // IP-адрес сервера
				ret = connect(TokenNetClientSocket, (sockaddr*)sin, sizeof(*sin));
			}
			strcpy(Server, Server_IP);
		}
		else
		{
			printf("Поиск существующих маркерных сетей\nподключение будет совершенно к первой найденной\n");
			printf("Это может занять некоторое время\n");
			// Пробуем подключиться ко всем IP из ARP таблицы, пока не получится подключиться
			// Если не удастся ни к одному - значит нет запущенной программы ни на одном компьютере
			{
				int i = 0;
				do
				{
					char My_IP_Main_Part[9];
					char ARP_IP[9];
					strncpy(My_IP_Main_Part, My_IP, 8);
					strncpy(ARP_IP, IPs[i], 8);
					My_IP_Main_Part[8] = '\0';
					ARP_IP[8] = '\0';
					if (!strcmp(My_IP_Main_Part, ARP_IP))
					{
						sin->sin_family = AF_INET; // Тип адреса
						sin->sin_addr.s_addr = inet_addr(IPs[i]); // IP-адрес сервера (пори создании сервера можно 0)
						sin->sin_port = htons(DEFAULT_PORT); // Номер порта сервера
						DWORD ret = connect(TokenNetClientSocket, (sockaddr*)sin, sizeof(*sin));
						if (ret == 0)
						{
							FirstInNetwork = FALSE;
							strcpy(Server, IPs[i]);
							break;
						}
						else
							FirstInNetwork = TRUE;
					}
					i++;
				} while ((i < num_of_IPs));
			}
		}
	}
	else
	{
		FirstInNetwork = TRUE;
		strcpy(Server, My_IP);
	}
	return Server;
}

int ConnectOrDisconnectMeFromTokenRing(SOCKET TokenNetClientSocket, char* Adress, int MsgType, char ConnectOrDisconnect)
{
	// Отправляем посылку на сервер, что необходимо перестроить маркерное кольцо
	char* IP_send = (char*)malloc(30);
	IP_send[0] = MsgType;
	IP_send[1] = '0';
	IP_send[2] = ConnectOrDisconnect; // Присоединение/удаление пользователя
	IP_send[3] = '\0';
	strcat(IP_send, Adress);
	DWORD iClientResult = send(TokenNetClientSocket, IP_send, strlen(IP_send) + 1, 0); // Отправка посылки на сервер (будет успешна, если найден был сервер)
	free(IP_send);
	return iClientResult;
}

void WinSockConnect()
{
	WSADATA wsaData; // Данные о версии библиотеки WinSock

	// Генерируем слово версии библиотеки при помощи функции MAKEWORD(lowbyte, highbyte)
	WORD wVersionRequested = MAKEWORD(2, 2);

	int err = WSAStartup(wVersionRequested, &wsaData);
	if (err != 0) {
		// При ошибке говорим пользователю, что DLL с данной версией библиотеки не существует
		printf("WSAStartup failed with error: %d\n", err);
		return;
	}
	if (LOBYTE(wsaData.wVersion) != 2 || HIBYTE(wsaData.wVersion) != 2) {
		// Говорим пользователю, что не нашли DLL, уместной к использованию
		printf("Could not find a usable version of Winsock.dll\n");
		WSACleanup();
		return;
	}
	else
		printf("The Winsock 2.2 dll was found okay\n\n");
}

struct in_addr* GetHostNameAndAddresses(int* adress_count)
{
	char ac[80];
	if (gethostname(ac, sizeof(ac)) == SOCKET_ERROR)
	{
		wprintf(L"Error %d when getting local host name.\n", WSAGetLastError());
	}
	printf("Host name is %s\n", ac);
	struct hostent *phe = gethostbyname(ac);
	if (phe == 0)
	{
		wprintf(L"Yow! Bad host lookup.\n");
	}
	for (*adress_count = 0; phe->h_addr_list[*adress_count] != 0; ++(*adress_count));

	struct in_addr* addr = (struct in_addr*)malloc(sizeof(struct in_addr)*(*adress_count));
	for (int i = 0; phe->h_addr_list[i] != 0; ++i)
	{
		memcpy(&addr[i], phe->h_addr_list[i], sizeof(struct in_addr));
		printf("Address %d:%s\n", i, inet_ntoa(addr[i]));
	}
	return addr;
}

unsigned int ChooseMyIP(int adress_count, struct in_addr* addr)
{
	unsigned int number;
	printf("Введите номер с нужным IP для работы:");
	scanf("%d", &number);
	while (number >= adress_count)
	{
		printf("Введите номер с нужным IP для работы:");
		scanf("%d", &number);
	}
	return number;
}