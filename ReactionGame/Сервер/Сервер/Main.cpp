#include "Globals.h"
#include "Main.h"
#include <stdio.h>
#include <conio.h>

void ExitThreadFunction(LPVOID);

void main()
{
	SetConsoleCP(1251);
	SetConsoleOutputCP(1251);
	WinSockConnect(); // ����������� ���������� WinSock

	ZeroMemory(PlayersReady, DEFAULT_BUFFER);
	ZeroMemory(PlayersEndGame, DEFAULT_BUFFER);
	StartServer();

	HANDLE ExitThread = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)ExitThreadFunction, 0, 0, 0);
	WaitForSingleObject(ExitThread, INFINITE); // ����, ���� ������������ �������� �����
}

void ExitThreadFunction(LPVOID lParam)
{
	char Text[DEFAULT_BUFFER];
	ZeroMemory(Text, DEFAULT_BUFFER);
	while (strcmp(Text, "Exit") && strcmp(Text, "exit"))
	{
		printf("��� ���������� ������� �������� Exit: ");
		scanf("%s", Text);		
	}		
}