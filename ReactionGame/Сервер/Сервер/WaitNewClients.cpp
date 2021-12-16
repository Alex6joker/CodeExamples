#include <winsock2.h>
#include <ws2tcpip.h>
#include <stdio.h>
#include "Main.h"

void WaitNewClients(LPVOID ServerSocket)
{
	while (1)
	{
		listen((SOCKET)ServerSocket, SOMAXCONN); // ������������ ������� ���������
		sockaddr_in sin_new_client; // �������� ����������������� ����������� ������
		memset((sockaddr_in*)&sin_new_client, 0, sizeof(sin_new_client)); // �������� ��������� ������� ������
		SOCKET Client = accept((SOCKET)ServerSocket, (sockaddr*)&sin_new_client, 0); // �������� �������
		// ������� ����� �������� �������� �������� �������
		if (Client != SOCKET_ERROR)
			HANDLE hThreadWorkWithClientMessage = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)WorkWithClientMessage, (LPVOID)Client, 0, 0);
		Sleep(0);
	}
}