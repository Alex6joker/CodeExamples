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
		listen((SOCKET)TokenNetServerSocket, SOMAXCONN); // ������������ ������� ���������
		SOCKET Client = accept((SOCKET)TokenNetServerSocket, &new_client, 0); // �������� �������
		// ������� ����� �������� �������� �������� �������
		if (Client != SOCKET_ERROR) HANDLE hThreadWorkWithClientMessage = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)WorkWithClientMessage, (LPVOID)Client, 0, 0);
		Sleep(0);
	}
}

void reconnect()
{
	sockaddr_in name;
	closesocket(TokenNetClientSocket); //  ����������� �����
	TokenNetClientSocket = socket(AF_INET, SOCK_STREAM, 0);
	name.sin_family = AF_INET; // ��� ������
	name.sin_addr.s_addr = inet_addr(Token_Ring->next->IP); // IP-����� ������� (��� �������� ������� ����� 0)
	name.sin_port = htons(DEFAULT_PORT); // ����� ����� �������
	connect(TokenNetClientSocket, (sockaddr*)&name, sizeof(name));
}

void WorkWithClientMessage(LPVOID ClientSocket)
{
	ResetEvent(hEventEndWorkWithMsgFormClient); // �������������, ��� �������� ������ ��� ���������� ����������
	char* Message = (char*)malloc(DEFAULT_BUFFER);
	while (recv((SOCKET)ClientSocket, Message, DEFAULT_BUFFER, 0) == 0) // ��������� ��������� �� �������
		Sleep(100);
	// ������������ ���������� ���������
	int Received_Msg_Priority = Message[0]; // ��������� ��������� ���������
	int Received_Msg_Type = Message[1] - 0x30;    // ��������� ��� ��������� (0 - ���� ������, 1 - ������)
	Message += 2;
	switch (Received_Msg_Priority | Received_Msg_Type)
	{
	case	ACCEPT_AND_SEND_DATA:
	{
		// ��������� ������ �� ����������� � ����������� �������� ����-��� ��� key-���
		char* AnalysingMsg = (char*)malloc(DEFAULT_BUFFER);
		char* begin = AnalysingMsg;
		char Target_IP[20];
		CONSOLE_SCREEN_BUFFER_INFO bi;
		GetConsoleScreenBufferInfo(GetStdHandle(STD_OUTPUT_HANDLE), &bi);
		strcpy(AnalysingMsg, Message);
		AnalysingMsg = strtok(AnalysingMsg, " ");
		strcpy(Target_IP, AnalysingMsg);
		if (!strcmp(My_IP, Target_IP)) // ���� ���� ������ ������ ���� �� ������
		{
			Message -= 2;
			Message[0] = 0;
			Message[1] = '1';
			Message[2] = '\0';
			reconnect();
			DWORD ret = send(TokenNetClientSocket, Message, 3, 0); // �������� ������ ���������� �������� �� ������
			free(AnalysingMsg);
			break;
		}
		// ���������, ��������� ������-���� ������ (0 - ����-���, 1 - key-���)
		AnalysingMsg = strtok(0, " ");
		if (AnalysingMsg[0] - 0x30 == 0)
		{
			// �������� ������� ����������� ��� ���� ����
			AnalysingMsg = strtok(0, " ");
			if (AnalysingMsg[0] == 0x0e)
			{
				AnalysingMsg = strtok(0, " "); // ������ �������
				BACKSPACE_Remake_Text(atoi(AnalysingMsg));
			}
			else if (AnalysingMsg[0] == 0x53)
			{
				AnalysingMsg = strtok(0, " "); // ������ �������
				DELETE_Remake_Text(atoi(AnalysingMsg));
			}
		}
		else
		{
			// �������� ������� ����������� ��� ������� ������
			AnalysingMsg = strtok(0, " "); // ������ ������
			char symbol = AnalysingMsg[0]; // ��������� ������
			AnalysingMsg = strtok(0, " "); // ������ �������
			FileBuffer = Remake_Text(atoi(AnalysingMsg), symbol);
		}
		system("cls");
		printf("%s", FileBuffer);
		SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), bi.dwCursorPosition); // ��������� ������ �� ��� �� �������
		// �������� ��������� ���������� �������� �� ������
		Message -= 2; // ���������� ��������� ��� ��� ��������
		free(begin);
		reconnect();
		DWORD ret = send(TokenNetClientSocket, Message, strlen(Message) + 1, 0); // �������� ������ ���������� �������� �� ������
		break;
	}

	case	SEND_TOKEN:
	{	// �������� ������, ������ ����� ����� ���������� ��������� ������ �� ����
		strcpy(Token.Owner_IP, My_IP); // ��������, ��� �� �������� �����������
		SetEvent(hEventIAmOwner); // ������ �������, ��� �� ��������� (��� �������� ������������, ����� �� ���� ������ �������)
		// ���������, ���� �� �� � ����
		// �������� ������� ��������� �� ���������
		if (Message_List != NULL) // ���� � ������� ���� ��������
		{
			WaitForMultipleObjects(2, hEventWorkWithMessageList, TRUE, INFINITE); // ���� ���� ��� �������� � �������� ��������� ����� ���������
			// �������� ������� ��������� �� ������
			Message -= 2; // ���������� ��������� ��� ��� ��������
			Take_Package_With_The_Highest_Priority(Message);
			if (Message[0] == RESTRUCT_TOKEN_RING) // ���� ������������ ����� ��� ��������� ������ ������������ � ��������� ����������� ������ � �������������� ��� ������
			{
				// ���������������� ������ ����� ������, �������������� ���������� ����� �� ������ ���� ��� ������� ����� ������
				// ��� ����� ������� ���� ����������� ��� ����������������� ������
				sockaddr_in name;
				closesocket(TokenNetClientSocket); //  ����������� �����
				TokenNetClientSocket = socket(AF_INET, SOCK_STREAM, 0);
				name.sin_family = AF_INET; // ��� ������
				name.sin_addr.s_addr = inet_addr(My_IP); // IP-����� ������� (��� �������� ������� ����� 0)
				name.sin_port = htons(DEFAULT_PORT); // ����� ����� �������
				connect(TokenNetClientSocket, (sockaddr*)&name, sizeof(name));
				DWORD ret = send(TokenNetClientSocket, Message, strlen(Message) + 1, 0);
				WaitForSingleObject(hEventConnectNewUserToTokenRing, INFINITE);
			}
			else if (Message[0] == DOWNLOAD_FILE_DATA)
			{
				// ������� ��������
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
				// ���� �� ��������� ����������� ������, �� ������ ���������� ��������� ���������� � ������
				char AnalysingMsg[DEFAULT_BUFFER];
				Message += 2;
				strcpy(AnalysingMsg, Message);
				strtok(AnalysingMsg, " ");
				Message -= 2;
				reconnect();
				DWORD ret = send(TokenNetClientSocket, Message, strlen(Message) + 1, 0);
			}
			// ���� ���������� ������� ������� ����� ������ ���������
			//WaitForSingleObject(DataFrameIsDone, INFINITE);
		}
		else
			Message -= 2; // ���������� ��������� ��� ��� ��������
		Sleep(20);
		strcpy(Token.Owner_IP, ""); // ����������, ��� ������ �� �������� ����������� �������
		char Msg[3];
		Msg[0] = 0;
		Msg[1] = '1';
		Msg[2] = '\0';
		reconnect();
		DWORD ret = send(TokenNetClientSocket, Msg, 3, 0); // �������� ������ ���������� �������� �� ������
		break;
	}
	case	ACCEPT_NEW_TOKEN_RING:
	{
		char* NewTokenRing;
		NewTokenRing = new char[DEFAULT_BUFFER];
		strcpy(NewTokenRing, Message);

		// ���������, ������ �� ���� ������ ������ �� ������
		NewTokenRing = strtok(NewTokenRing, " ");
		if (!strcmp(My_IP, NewTokenRing)) // ���� ����� ���������, �� ������ ������
		{
			Message -= 2; // ����������  ��������� ��� ��� ��������
			if (DeleteInProcessing)
			{
				DeleteInProcessing = FALSE;
				SOCKET  temp_socket;
				temp_socket = socket(AF_INET, SOCK_STREAM, 0);
				sockaddr_in temp_connect;
				temp_connect.sin_family = AF_INET; // ��� ������
				temp_connect.sin_addr.s_addr = inet_addr(Deleting_IP); // IP-����� ������� (��� �������� ������� ����� 0)
				temp_connect.sin_port = htons(DEFAULT_PORT); // ����� ����� �������
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

		// ������� ������ ������
		struct _Token_Ring *temp;
		temp = Token_Ring;
		if (Token_Ring != NULL) // ���� ��� ���� �������������� ������
		{
			while (temp) // ������������� ������ ������� � �����
			{ // ���� �� ������ ����, �������������� Token_Ring
				temp = Token_Ring->next; // ������ ������ ��������� �������, ���� ����� ����������
				free(temp); // ����������� ������ ���������� ����
				if (!Token_Ring) temp = Token_Ring;
				else temp = NULL;
			}
			free(Token_Ring); // ����������� ������ ���������� ����
			Token_Ring = NULL;
		}

		// ��������� ����� ������ ��� ������
		while (NewTokenRing) // ���� ���� ������� (IP)
		{
			struct _Token_Ring *temp, *p;
			temp = (struct _Token_Ring*)malloc(sizeof(_Token_Ring));
			if (Token_Ring == NULL) // ���� ������ ��� ������ ������� ��� �� ���� ������������
			{
				strcpy(temp->IP, NewTokenRing); //���������� IP ����� ������� �������
				Token_Ring = temp;
				temp->next = temp;
			}
			else
			{
				temp = Token_Ring;
				_Token_Ring* temp2 = (struct _Token_Ring*)malloc(sizeof(_Token_Ring));
				while (temp->next != Token_Ring) temp = temp->next;
				p = temp->next; // ���������� ��������� �� ��������� �������
				temp->next = temp2; // ���������� ���� ��������� �� �����������
				strcpy(temp2->IP, NewTokenRing); // ���������� ���� ������ ������������ ����
				temp2->next = p; // ��������� ���� ��������� �� ��������� �������
			}
			NewTokenRing = strtok(0, " ");
		}
		delete[] NewTokenRing;

		// ���� ���� ��������� � ������
		while (strcmp(My_IP, Token_Ring->IP))
		{
			Token_Ring = Token_Ring->next;
		}
		// ����������� ���������� ����� �� ������� �������
		sockaddr_in name;
		int n_l;
		n_l = sizeof(name);
		ZeroMemory(&name, sizeof(name));
		DWORD ret;
		ret = getsockname(TokenNetClientSocket, (sockaddr*)&name, &n_l);
		char string[300];
		strcpy(string, inet_ntoa(name.sin_addr));
		if (strcmp(inet_ntoa(name.sin_addr), Token_Ring->next->IP)) // ���� ������ ���������
		{
			closesocket(TokenNetClientSocket); //  ����������� �����
			TokenNetClientSocket = socket(AF_INET, SOCK_STREAM, 0);
			name.sin_family = AF_INET; // ��� ������
			name.sin_addr.s_addr = inet_addr(Token_Ring->next->IP); // IP-����� ������� (��� �������� ������� ����� 0)
			name.sin_port = htons(DEFAULT_PORT); // ����� ����� �������
			ret = connect(TokenNetClientSocket, (sockaddr*)&name, sizeof(name));
		}
		// ���������� ������ �������� ������ �� ������
		Message -= 2; // ����������  ��������� ��� ��� ��������
		ret = send(TokenNetClientSocket, Message, strlen(Message) + 1, 0); // �������� ������� �� ������ (����� �������, ���� ������ ��� ������)
		break;
	}
	case	DOWNLOAD_FILE_DATA:
	{
		char* File = (char*)malloc(DEFAULT_BUFFER);
		char* File_begin = File;
		strcpy(File, Message);
		Message -= 2; // ����������  ��������� ��� ��� ��������
		while (File[0] != ' ') // ������� �� ������, �������� ����� IP
			File++;
		File++;

		int CurrentOffset = atoi(File);

		while (File[0] != ' ') // ������� �� ������, �������� ����� IP
			File++;
		File++;
		// ��������� ����� � �������� ��� � ������
		FileBuffer = (char*)realloc(FileBuffer, DEFAULT_BUFFER + CurrentOffset); // �������� ������ ������
		if (CurrentOffset == 0)
			strcpy(FileBuffer, File);
		else
			strcat(FileBuffer, File);

		// ���� ��������� ������ ������� � ���, ��� ����� ������� ��������� ����� �����, �� ���������� ����� �����
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
		// ��� ��������� IP ������� ������ ������������� ��������� ������
		struct _Token_Ring *temp, *p;
		// Message[0] - ������ ������ (0 - �������� ������ ������������, 1 - �������)
		if (Message[0] == '0') // ���� ��������� �������� ������������ � ������
		{
			Message++; // ��������� � IP
			temp = (struct _Token_Ring*)malloc(sizeof(_Token_Ring));
			p = Token_Ring->next; // ���������� ��������� �� ��������� �������
			Token_Ring->next = temp; // ���������� ���� ��������� �� �����������
			strcpy(temp->IP, Message); // ���������� ���� ������ ������������ ����
			temp->next = p; // ��������� ���� ��������� �� ��������� �������
		}
		else // ����� ��������� ������� ��������� �� ������
		{
			Message++; // ��������� � IP
			temp = Token_Ring;
			while (strcmp(temp->next->IP, Message)) // ������������� ������ ������� � �����
			{	// ���� �� ������ ����, �������������� ���� � ������ IP
				temp = temp->next;
			}
			p = temp->next;
			temp->next = p->next;
			free(p);
		}
		// ���������� ������������� ������ �������
		// ��������� ���� ������ ��� �������� ������ ������ �������
		temp = Token_Ring; // ���������� ������� ����� ������
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

		// ������ ��� ���������
		// Message[0] - ������ ������ (0 - �������� ������ ������������, 1 - �������)
		Message--;

		DWORD ret;
		SOCKET  temp_socket;
		temp_socket = socket(AF_INET, SOCK_STREAM, 0);
		sockaddr_in temp_connect;
		temp_connect.sin_family = AF_INET; // ��� ������

		if (Message[0] == '0')
		{
			Message++;
			temp_connect.sin_addr.s_addr = inet_addr(Message); // IP-����� ������� (��� �������� ������� ����� 0)
		}
		else
		{
			Message++;
			temp_connect.sin_addr.s_addr = inet_addr(Token_Ring->next->IP); // IP-����� ������� (��� �������� ������� ����� 0)
		}

		temp_connect.sin_port = htons(DEFAULT_PORT); // ����� ����� �������
		ret = closesocket((SOCKET)ClientSocket);
		ret = connect((SOCKET)temp_socket, (sockaddr*)&temp_connect, sizeof(temp_connect));
		if (ret == SOCKET_ERROR) {
			wprintf(L"connect failed with error: %d\n", WSAGetLastError());
			WSACleanup();
			return;
		}
		ret = send(temp_socket, NewTokenRing, strlen(NewTokenRing) + 1, 0);
		ret = closesocket(temp_socket);
		Message -= 3; // ����������  ��������� ��� ��� ��������
		break;
	}
	case CREATE_TOKEN:
	{
		// ������������� ��������� ������� ��� ������
		strcpy(Token.Owner_IP, My_IP); //���������� IP ��������� ���������� ������
		Token.Priority = 0; // ����� � ������ ���������
		Token.Token_bit = '1'; // ����� �� ����� ������������
		break;
	}
	case BEGIN_CONNECT_NEW_USER:
	{
		Message -= 2; // ����������  ��������� ��� ��� ��������
		Message[0] = RESTRUCT_TOKEN_RING;
		Add_To_Msg_List(Message); // ��������� ��������� � ������� ���������
		break;
	}
	case DELETE_USER:
	{
		Message++;
		strcpy(Deleting_IP, Message);
		Message -= 3; // ����������  ��������� ��� ��� ��������
		Message[0] = RESTRUCT_TOKEN_RING;
		DeleteInProcessing = TRUE;
		Add_To_Msg_List(Message); // ��������� ��������� � ������� ���������
		break;
	}
	case END_OF_DELETION:
	{
		SetEvent(hEventEndWorkDeleteClient);
		Message -= 2; // ���������� ��������� ��� ��� ��������
		break;
	}
	case WANT_TO_GET_FILEBUFFER:
	{
		Message -= 2; // ���������� ��������� ��� ��� ��������
		Message[0] = DOWNLOAD_FILE_DATA;
		Add_To_Msg_List(Message); // ��������� ��������� � ������� ���������
		break;
	}	
	default: // ��������� ������ ��� ������ ������� �����������
		Message -= 2; // ���������� ��������� ��� ��� ��������
		SendTokenToTokenRing(0);
	}
	closesocket((SOCKET)ClientSocket);
	free(Message);
	SetEvent(hEventEndWorkWithMsgFormClient); // �������������, ��� ��������� ������ ��� ���������� ����������
}

void SendTokenToTokenRing(LPVOID lParam)
{
	char Message[3];
	Message[0] = Token.Priority;
	Message[1] = '1';
	Message[2] = '\0';
	sockaddr_in name;
	closesocket(TokenNetClientSocket); //  ����������� �����
	TokenNetClientSocket = socket(AF_INET, SOCK_STREAM, 0);
	name.sin_family = AF_INET; // ��� ������
	name.sin_addr.s_addr = inet_addr(My_IP); // IP-����� ������� (��� �������� ������� ����� 0)
	name.sin_port = htons(DEFAULT_PORT); // ����� ����� �������
	DWORD ret = connect(TokenNetClientSocket, (sockaddr*)&name, sizeof(name));
	send((SOCKET)TokenNetClientSocket, Message, 3, 0); // ��������� ������ ������������� �� ������
}

void SendDataBuffer(int Offset, char* Message)
{
	char* Msg = (char*)malloc(DEFAULT_BUFFER);
	Msg[0] = DOWNLOAD_FILE_DATA;
	Msg[1] = '0';
	Msg[2] = '\0';
	Msg += 2;

	Message += 2; // ��������� � IP ��������
	strtok(Message, " ");
	strcat(Msg, Message);

	Msg[strlen(Message)] = ' ';
	Msg[strlen(Message) + 1] = '\0';
	Msg += strlen(Message) + 1;
	itoa(Offset, Msg, 10);
	FileBuffer += Offset;
	// ������� ���-�� ���� � ��������
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
	closesocket(TokenNetClientSocket); //  ����������� �����
	SOCKET SendTokenSocket = TokenNetClientSocket = socket(AF_INET, SOCK_STREAM, 0);
	name.sin_family = AF_INET; // ��� ������
	name.sin_addr.s_addr = inet_addr(Message); // IP-����� ������� (��� �������� ������� ����� 0)
	name.sin_port = htons(DEFAULT_PORT); // ����� ����� �������
	DWORD ret = connect(SendTokenSocket, (sockaddr*)&name, sizeof(name));
	ret = send((SOCKET)TokenNetClientSocket, Msg, strlen(Msg) + 1, 0); // �������� ����� ������ ����� �������

	Message -= 2; // ���������� �������
	free(Msg);
}