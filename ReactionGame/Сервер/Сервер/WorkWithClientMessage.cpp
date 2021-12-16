#include <winsock2.h>
#include "Globals.h"
#include "Main.h"
#include <time.h>
#include <stdio.h>
#include <stdlib.h>

clock_t GameTime;

void WorkWithClientMessage(LPVOID ClientSocket)
{
	// Создаем и обнуляем строковый массив, в который будут поступать сообщения от клиента
	char Message[DEFAULT_BUFFER];
	srand(time(0)); // автоматическая рандомизация

	// Получение сообщения от клиента, пока есть соединение
	while (recv((SOCKET)ClientSocket, Message, DEFAULT_BUFFER, 0) != SOCKET_ERROR)
	{
		if (!GameInProgress)
		{
			// Указываем, что данный участник готов
			
			PlayersReady[PlayersCount] = 1;
			PlayersCount++;
			if (PlayersCount > 1)
			{	// Если кол-во участников больше одного, то следует перевыделить память, чтобы хранить состояния всех участников
				PlayersReady = (LPBYTE)realloc(PlayersReady, PlayersCount);
				PlayersEndGame = (LPBYTE)realloc(PlayersEndGame, PlayersCount);
				Clients = (SOCKET*)realloc(Clients, PlayersCount);
			}
			Clients[PlayersCount-1] = (SOCKET)ClientSocket; // Записываем сокет подключившегося игрока в список
			// Игра начинается тогда, когда все игроки готовы
			if (AllReady(PlayersReady))
			{
				GameInProgress = true;
				GameTime = clock();
				// Так как лишь одна из нитей заметит начало игры, а нити
				// других игроков будут в ожидании, то достаточно только одной
				// доставить оповещения всем игрокам
				// Отправляем координаты всем игрокам
				for (int i = 0; i < PlayersCount; i++)
				{
					char Msg[DEFAULT_BUFFER]; // Сообщение с координатами объекта
					char tmp[DEFAULT_BUFFER];
					strcpy(tmp, Message);
					int Count = 0;
					//for (int i = 0; i < DEFAULT_BUFFER - 1; i++)
					//	Count += Message[i + 1];
					Count = Message[1];
					ZeroMemory(Msg, DEFAULT_BUFFER);
					// Формируем сообщение					
					for (int i = 0; i < Count; i++)
					{
						char Temp[DEFAULT_BUFFER]; // Сообщение с координатами объекта
						int Width = 20 + rand() % 800;
						int Height = 20 + rand() % 300;
						itoa(Width, Temp, 10);
						strcat(Msg, Temp);
						Msg[strlen(Msg)] = ' ';

						char ResHeight[DEFAULT_BUFFER]; // Сообщение с координатами объекта
						ZeroMemory(ResHeight, DEFAULT_BUFFER);
						itoa(Height, ResHeight, 10);
						strcat(Msg, ResHeight);
						Msg[strlen(Msg)] = ' ';
					}					
					send(Clients[i], Msg, strlen(Msg) + 1, 0); // Отправляем сообщение клиенту
				}					
			}
		}
		else
		{
			// Если игра уже в процессе и кто-то ее завершил
			// (прислал в ответ 1 и попал в else),
			// то мы ставим в массиве 0 для игрока и отправляем ему время(статистическая информация)
			PlayersEndGame[PlayersEndGameB] = 0; // Ставим знак 0 - игрок завершил игру
			PlayersEndGameB++; // Указываем в переменной, что еще один игрок завершил игру
			if (PlayersEndGameB == PlayersCount) // Если все игроки завершили игру
			{	// Начинаем процесс остановки игры, обнуляем память переменных
				// массив PlayersEndGame по окончанию игры уже должен быть заполнен нулями
				ZeroMemory(PlayersReady, sizeof(int));
				ZeroMemory(&PlayersEndGameB, sizeof(int));
				ZeroMemory(&PlayersCount, sizeof(int));
				GameInProgress = false;
			}			
			clock_t CurPlayertime = clock() - GameTime;			
			double t = (double)CurPlayertime / CLOCKS_PER_SEC;
			// Готовим значение времени к отпавке клиенту
			char TMessage[5];
			ZeroMemory(TMessage, 5);
			sprintf(TMessage, "%f", t);
			// Отправляем клиенту результат
			send((SOCKET)ClientSocket, TMessage, 5, 0);
		}
		Sleep(20);
	}
	if (PlayersCount > 0)
		PlayersCount--; // При отключении игрока перестаем его учитывать
}