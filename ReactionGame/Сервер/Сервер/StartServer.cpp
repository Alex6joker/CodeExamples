#include <winsock2.h>
#include <ws2tcpip.h>
#include <stdio.h>
#include "Main.h"

void StartServer()
{
	SOCKET ServerSocket = socket(AF_INET, SOCK_STREAM, 0); // ������� �����
	sockaddr_in sin_Server; // �������� ������������ ������

	if (ServerSocket == SOCKET_ERROR) // ��������� �� ������ �������� ������
	{
		WSACleanup();
		return;
	}

	memset((sockaddr_in*)&sin_Server, 0, sizeof(sin_Server)); // �������� ��������� ������� ������
	sin_Server.sin_family = AF_INET; // ��� ������
	sin_Server.sin_addr.s_addr = 0; // IP-����� ������� (��� �������� ������� ����� 0)
	sin_Server.sin_port = htons(DEFAULT_PORT); // ����� ����� �������
	// ���������� ������ � �������
	if (bind(ServerSocket, (sockaddr*)&sin_Server, sizeof(sin_Server)) == SOCKET_ERROR) // ���� bind ������� ����������
	{
		printf("Failed binding server with error: %d\nUnload WinSock libriary\n", WSAGetLastError());
		WSACleanup();
		return;
	}
	else
	{
		printf("Binding server successful\nWait for clients...\n");
		// ������� ����� �������� �������� �������� �������
		CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)WaitNewClients, (LPVOID)ServerSocket, 0, 0);
	}
}