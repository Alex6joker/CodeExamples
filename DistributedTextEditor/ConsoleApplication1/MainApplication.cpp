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
	// ������� � ������ ������� (������������ ������ �� ����� �����������), ���������� � ���������� ��������� (�� ������� �� ������� ������)
	hEventEndDataDownload = CreateEvent(NULL, FALSE, FALSE, NULL);
	hEventEndWorkDeleteClient = CreateEvent(NULL, FALSE, FALSE, NULL); // ������� ����� ������ � ������������ ���������� ������ ��� ��������
	hEventWorkWithMessageList[0] = CreateEvent(NULL, TRUE, TRUE, NULL);
	hEventWorkWithMessageList[1] = CreateEvent(NULL, TRUE, TRUE, NULL);
	hEventIAmOwner = CreateEvent(NULL, FALSE, FALSE, NULL);
	hEventEndWorkWithMsgFormClient = CreateEvent(NULL, TRUE, TRUE, NULL);
	hEventConnectNewUserToTokenRing = CreateEvent(NULL, FALSE, FALSE, NULL); // �� ����� ������� ����� ����� ����������� ������� ������� � ������
	// ���������� ���������� WinSock
	WinSockConnect();
	
	// �������� ��� ���������� � ��� ��� IP-������
	int adress_count;
	struct in_addr* addr = GetHostNameAndAddresses(&adress_count);
	
	// ��������, ����� IP ����� ������������
	unsigned int number = ChooseMyIP(adress_count, addr);
	strcpy(My_IP, inet_ntoa(addr[number]));

	// �������� arp �������, ������� �������� � ���� ��� IP �������, ��������� � ������
	int num_of_IPs = ARP_TABLE_SIZE;
	char** PC_IP_in_LVS = GetArpTable(&num_of_IPs);
	// ��������� ��������� �����, ����� ��������� ��������� �� ����������� �� ������
	// ������� ����� �������
	//						�� TCP/UPD	TCP		  0 ��������
	TokenNetServerSocket = socket(AF_INET, SOCK_STREAM, 0);
	sin_Server.sin_family = AF_INET; // ��� ������
	sin_Server.sin_addr.s_addr = 0; // IP-����� ������� (��� �������� ������� ����� 0)
	sin_Server.sin_port = htons(DEFAULT_PORT); // ����� ����� �������
	DWORD iResult = bind(TokenNetServerSocket, (sockaddr*)&sin_Server, sizeof(sin_Server)); // ���������� ���������� ������ � ������� sin
	if (iResult == SOCKET_ERROR) {
		wprintf(L"failed binding server with error: %d\n", WSAGetLastError());
		WSACleanup();
		return 1;
	}
	CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)WaitNewClients, (LPVOID)TokenNetServerSocket, 0, 0); // ������� ����� �������� �������� �������� �������
	

	// ������������ ����������� �� ���� ����������� � ����
	sockaddr_in sin; // ��������(�����) ������������ ������
	memset((sockaddr_in*)&sin, 0, sizeof(sin));
	// ������� ����� �������
	//						�� TCP/UPD	TCP		  0 ��������
	TokenNetClientSocket = socket(AF_INET, SOCK_STREAM, 0);

	char* Server_IP = (char*)malloc(30);
	UserQuestions(num_of_IPs, PC_IP_in_LVS, &sin, TokenNetClientSocket, Server_IP);
	if (ConnectOrDisconnectMeFromTokenRing(TokenNetClientSocket, My_IP, BEGIN_CONNECT_NEW_USER, '0') == SOCKET_ERROR)
	{ 
		printf("�������� ������� � �������� � ����");
		// ������� ��������� ������
		_Token_Ring *temp = new _Token_Ring; //�������� ������ ��� ������ ��������
		strcpy(temp->IP, inet_ntoa(addr[number])); //���������� IP ����� ������� �������
		Token_Ring = temp;
		temp->next = temp;
		// C������ ������������� ������ � ����
		// ������������� ��������� ������� ��� ������
		strcpy(Token.Owner_IP, My_IP); //���������� IP ��������� ���������� ������
		Token.Priority = 0; // ����� � ������ ���������
		Token.Token_bit = '1'; // ����� �� ����� ������������
		strcpy(Token.Owner_IP, My_IP); // ��������, ��� �� �������� ��������������� ����������� �������
		CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)SendTokenToTokenRing, 0, 0, 0); // ������� ����� ������� ������ �� ������
	}
	free(Server_IP);

	CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)DocumentWork, NULL, NULL, NULL);// ������� ����� ��� ������ � ������
	WaitForSingleObject(hEscapeEvent, INFINITE); // ����, ���� ������������ �������� �����
	system("cls");
	printf("���������� ���������");
	WaitForSingleObject(hEventIAmOwner, INFINITE); // ������ ���������, ��� �� ������� ��������
	if (strcmp(My_IP,Token_Ring->next->IP)) // ���� ���� �� ����
	{ 
		reconnect();
		ConnectOrDisconnectMeFromTokenRing(TokenNetClientSocket, inet_ntoa(addr[number]), DELETE_USER, '1');
		WaitForSingleObject(hEventEndWorkDeleteClient, INFINITE); // ������ ���������, ��� ��������� ���� �����������
	}
	// �� ��������� ������ ��������� DLL WinSock
	// ��������� SOCKET
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
	WaitForSingleObject(hEventEndWorkWithMsgFormClient, INFINITE); // ���� ����� ������ � ����������

	WSACleanup();
	free(addr);
	// ������� ������
	struct _Token_Ring *temp = Token_Ring;
	if (Token_Ring != NULL) // ���� ��� ���� �������������� ������
	{
		while (temp) // ������������� ������ ������� � �����
		{ // ���� �� ������ ����, �������������� Token_Ring
			temp = Token_Ring->next; // ������ ������ ��������� �������, ���� ����� ����������
			free(temp); // ����������� ������ ���������� ����
			if (!Token_Ring) temp = Token_Ring;
			else temp = NULL;
		}
	}
	for (int i = 0; i < num_of_IPs; i++)
	{
		free(PC_IP_in_LVS[i]);
	}
	CloseHandle(hEventConnectNewUserToTokenRing); // ������� ����������� ������� �������� � ������
	CloseHandle(hEscapeEvent); // ������� ������ �� ��������� (������� escape)
	CloseHandle(hEventWorkWithMessageList[2]); // ������� ������������� ������ �� ������� ���������
	CloseHandle(hstd);
	CloseHandle(hEventEndWorkWithMsgFormClient); // ������� ����� ������ � ����������
	CloseHandle(hEventEndWorkDeleteClient);
	CloseHandle(hEventEndDataDownload); // ������� ��������� �������� ���������� �����
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
		printf("�������� �� ����� ��������� �� ������ ����������? Y/N: ");
		scanf("%c", &Answer);
	}
	if (Answer == 'Y' || Answer == 'y') // ���� ����� ��������� �������� �� ������ ����������
	{ 
		Answer = 0;
		while (Answer != 'Y' && Answer != 'N' && Answer != 'y' && Answer != 'n')
		{
			fflush(stdin);
			printf("������ �� �� IP ������� Y/N: ");
			scanf("%c", &Answer);
		}
		if (Answer == 'Y' || Answer == 'y') // ���� ������������ ����� IP �������
		{
			char Server_IP[20];
			printf("������� IP �������: ");
			scanf("%s", Server_IP);
			// ����������� 
			sin->sin_family = AF_INET; // ��� ������
			sin->sin_addr.s_addr = inet_addr(Server_IP); // IP-����� ������� (���� �������� ������� ����� 0)
			sin->sin_port = htons(DEFAULT_PORT); // ����� ����� �������
			DWORD ret = connect(TokenNetClientSocket, (sockaddr*)sin, sizeof(*sin));
			while (ret == SOCKET_ERROR)
			{
				printf("��������� ������������ � ������� �� ������� IP\n������� ������ IP �������: ");
				scanf("%s", Server_IP);
				sin->sin_addr.s_addr = inet_addr(Server_IP); // IP-����� �������
				ret = connect(TokenNetClientSocket, (sockaddr*)sin, sizeof(*sin));
			}
			strcpy(Server, Server_IP);
		}
		else
		{
			printf("����� ������������ ��������� �����\n����������� ����� ���������� � ������ ���������\n");
			printf("��� ����� ������ ��������� �����\n");
			// ������� ������������ �� ���� IP �� ARP �������, ���� �� ��������� ������������
			// ���� �� ������� �� � ������ - ������ ��� ���������� ��������� �� �� ����� ����������
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
						sin->sin_family = AF_INET; // ��� ������
						sin->sin_addr.s_addr = inet_addr(IPs[i]); // IP-����� ������� (���� �������� ������� ����� 0)
						sin->sin_port = htons(DEFAULT_PORT); // ����� ����� �������
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
	// ���������� ������� �� ������, ��� ���������� ����������� ��������� ������
	char* IP_send = (char*)malloc(30);
	IP_send[0] = MsgType;
	IP_send[1] = '0';
	IP_send[2] = ConnectOrDisconnect; // �������������/�������� ������������
	IP_send[3] = '\0';
	strcat(IP_send, Adress);
	DWORD iClientResult = send(TokenNetClientSocket, IP_send, strlen(IP_send) + 1, 0); // �������� ������� �� ������ (����� �������, ���� ������ ��� ������)
	free(IP_send);
	return iClientResult;
}

void WinSockConnect()
{
	WSADATA wsaData; // ������ � ������ ���������� WinSock

	// ���������� ����� ������ ���������� ��� ������ ������� MAKEWORD(lowbyte, highbyte)
	WORD wVersionRequested = MAKEWORD(2, 2);

	int err = WSAStartup(wVersionRequested, &wsaData);
	if (err != 0) {
		// ��� ������ ������� ������������, ��� DLL � ������ ������� ���������� �� ����������
		printf("WSAStartup failed with error: %d\n", err);
		return;
	}
	if (LOBYTE(wsaData.wVersion) != 2 || HIBYTE(wsaData.wVersion) != 2) {
		// ������� ������������, ��� �� ����� DLL, �������� � �������������
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
	printf("������� ����� � ������ IP ��� ������:");
	scanf("%d", &number);
	while (number >= adress_count)
	{
		printf("������� ����� � ������ IP ��� ������:");
		scanf("%d", &number);
	}
	return number;
}