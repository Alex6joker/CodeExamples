#include <winsock2.h>
#include <ws2tcpip.h>
#include <stdio.h>

void WinSockConnect()
{
	WSADATA wsaData; // Данные о версии библиотеки WinSock

	// Генерируем слово версии библиотеки при помощи функции MAKEWORD(lowbyte, highbyte)
	WORD wVersionRequested = MAKEWORD(2, 2);

	int err = WSAStartup(wVersionRequested, &wsaData);
	if (err != 0)
	{
		// При ошибке говорим пользователю, что DLL с данной версией библиотеки не существует
		printf("WSAStartup failed with error:\n");
		return;
	}
	if (LOBYTE(wsaData.wVersion) != 2 || HIBYTE(wsaData.wVersion) != 2)
	{
		// Говорим пользователю, что не нашли DLL, уместную к использованию
		printf("Could not find a usable version of Winsock.dll\n");
		WSACleanup();
		return;
	}
	printf("WSAStartup load successful\n");
}