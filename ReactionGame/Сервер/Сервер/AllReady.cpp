#include "Main.h"
#include "Globals.h"

// Функция определяет, все ли игроки готовы
// Это можно задать тем, что в массиве все значения
// должны быть не равны 0
bool AllReady(LPBYTE PlayersReady)
{
	for (int i = 0; i < PlayersCount; i++)
		if (PlayersReady[i] == 0)
			return false;
	return true;
}