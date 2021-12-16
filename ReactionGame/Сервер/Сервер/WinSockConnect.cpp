#include <winsock2.h>
#include <ws2tcpip.h>
#include <stdio.h>

void WinSockConnect()
{
	WSADATA wsaData; // ������ � ������ ���������� WinSock

	// ���������� ����� ������ ���������� ��� ������ ������� MAKEWORD(lowbyte, highbyte)
	WORD wVersionRequested = MAKEWORD(2, 2);

	int err = WSAStartup(wVersionRequested, &wsaData);
	if (err != 0)
	{
		// ��� ������ ������� ������������, ��� DLL � ������ ������� ���������� �� ����������
		printf("WSAStartup failed with error:\n");
		return;
	}
	if (LOBYTE(wsaData.wVersion) != 2 || HIBYTE(wsaData.wVersion) != 2)
	{
		// ������� ������������, ��� �� ����� DLL, �������� � �������������
		printf("Could not find a usable version of Winsock.dll\n");
		WSACleanup();
		return;
	}
	printf("WSAStartup load successful\n");
}