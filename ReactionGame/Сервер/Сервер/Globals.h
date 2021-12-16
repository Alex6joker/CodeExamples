#include <windows.h>
#ifndef _Globals
#define _Globals

extern LPBYTE PlayersReady; // Кол-во игроков, готовых к игре
extern LPBYTE PlayersEndGame; // Кол-во игроков, закончивших игру
extern int PlayersCount; // Кол-во игроков
extern bool GameInProgress; // Флаг того, что игра уже начата(если не начата, то 1 - готов к игре, если начата, то 1 - игра звершена)
extern int PlayersEndGameB; // Кол-во игроков, завершивших игру, пока она в процессе(нужна для заполнения нулями PlayersEndGame)
extern SOCKET* Clients;	// Массив подключенных к серверу сокетов

#endif