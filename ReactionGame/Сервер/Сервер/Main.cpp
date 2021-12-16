#include "Globals.h"
#include "Main.h"
#include <stdio.h>
#include <conio.h>

void ExitThreadFunction(LPVOID);

void main()
{
	SetConsoleCP(1251);
	SetConsoleOutputCP(1251);
	WinSockConnect(); // Подключение библиотеки WinSock

	ZeroMemory(PlayersReady, DEFAULT_BUFFER);
	ZeroMemory(PlayersEndGame, DEFAULT_BUFFER);
	StartServer();

	HANDLE ExitThread = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)ExitThreadFunction, 0, 0, 0);
	WaitForSingleObject(ExitThread, INFINITE); // Ждем, пока пользователь пожелает выйти
}

void ExitThreadFunction(LPVOID lParam)
{
	char Text[DEFAULT_BUFFER];
	ZeroMemory(Text, DEFAULT_BUFFER);
	while (strcmp(Text, "Exit") && strcmp(Text, "exit"))
	{
		printf("Для отключения сервера наберите Exit: ");
		scanf("%s", Text);		
	}		
}