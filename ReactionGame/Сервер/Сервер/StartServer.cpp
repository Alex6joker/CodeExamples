#include <winsock2.h>
#include <ws2tcpip.h>
#include <stdio.h>
#include "Main.h"

void StartServer()
{
	SOCKET ServerSocket = socket(AF_INET, SOCK_STREAM, 0); // Создаем сокет
	sockaddr_in sin_Server; // Свойства создаваемого сокета

	if (ServerSocket == SOCKET_ERROR) // Проверяем на ошибку создания сокета
	{
		WSACleanup();
		return;
	}

	memset((sockaddr_in*)&sin_Server, 0, sizeof(sin_Server)); // Обнуляем структуру свойств сокета
	sin_Server.sin_family = AF_INET; // Тип адреса
	sin_Server.sin_addr.s_addr = 0; // IP-адрес сервера (при создании сервера можно 0)
	sin_Server.sin_port = htons(DEFAULT_PORT); // Номер порта сервера
	// Связывание сокета с адресом
	if (bind(ServerSocket, (sockaddr*)&sin_Server, sizeof(sin_Server)) == SOCKET_ERROR) // Если bind сервера провалился
	{
		printf("Failed binding server with error: %d\nUnload WinSock libriary\n", WSAGetLastError());
		WSACleanup();
		return;
	}
	else
	{
		printf("Binding server successful\nWait for clients...\n");
		// Создаем поток ожидания клиентов даннного сервера
		CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)WaitNewClients, (LPVOID)ServerSocket, 0, 0);
	}
}