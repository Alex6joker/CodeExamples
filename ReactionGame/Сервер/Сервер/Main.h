#include <windows.h>
#ifndef _Main
#define _Main


#define DEFAULT_PORT 5050
#define DEFAULT_BUFFER 128

void WinSockConnect(); // ��������� ��������� ���������� WinSock
void StartServer();		// ��������� ������ �������(�������� ���������� ������, ������ ������� �������� ��������)
bool AllReady(LPBYTE);
void WaitNewClients(LPVOID);
void WorkWithClientMessage(LPVOID);

#pragma comment (lib, "ws2_32.lib")
#pragma comment (lib, "ole32.lib")
#pragma comment (lib, "Iphlpapi.lib")

#endif