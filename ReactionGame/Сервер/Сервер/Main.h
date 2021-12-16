#include <windows.h>
#ifndef _Main
#define _Main


#define DEFAULT_PORT 5050
#define DEFAULT_BUFFER 128

void WinSockConnect(); // Процедура загркузки библиотеки WinSock
void StartServer();		// Процедура старта сервера(создание серверного сокета, запуск функции ожидания клиентов)
bool AllReady(LPBYTE);
void WaitNewClients(LPVOID);
void WorkWithClientMessage(LPVOID);

#pragma comment (lib, "ws2_32.lib")
#pragma comment (lib, "ole32.lib")
#pragma comment (lib, "Iphlpapi.lib")

#endif