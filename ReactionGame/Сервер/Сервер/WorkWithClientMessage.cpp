#include <winsock2.h>
#include "Globals.h"
#include "Main.h"
#include <time.h>
#include <stdio.h>
#include <stdlib.h>

clock_t GameTime;

void WorkWithClientMessage(LPVOID ClientSocket)
{
	// ������� � �������� ��������� ������, � ������� ����� ��������� ��������� �� �������
	char Message[DEFAULT_BUFFER];
	srand(time(0)); // �������������� ������������

	// ��������� ��������� �� �������, ���� ���� ����������
	while (recv((SOCKET)ClientSocket, Message, DEFAULT_BUFFER, 0) != SOCKET_ERROR)
	{
		if (!GameInProgress)
		{
			// ���������, ��� ������ �������� �����
			
			PlayersReady[PlayersCount] = 1;
			PlayersCount++;
			if (PlayersCount > 1)
			{	// ���� ���-�� ���������� ������ ������, �� ������� ������������ ������, ����� ������� ��������� ���� ����������
				PlayersReady = (LPBYTE)realloc(PlayersReady, PlayersCount);
				PlayersEndGame = (LPBYTE)realloc(PlayersEndGame, PlayersCount);
				Clients = (SOCKET*)realloc(Clients, PlayersCount);
			}
			Clients[PlayersCount-1] = (SOCKET)ClientSocket; // ���������� ����� ��������������� ������ � ������
			// ���� ���������� �����, ����� ��� ������ ������
			if (AllReady(PlayersReady))
			{
				GameInProgress = true;
				GameTime = clock();
				// ��� ��� ���� ���� �� ����� ������� ������ ����, � ����
				// ������ ������� ����� � ��������, �� ���������� ������ �����
				// ��������� ���������� ���� �������
				// ���������� ���������� ���� �������
				for (int i = 0; i < PlayersCount; i++)
				{
					char Msg[DEFAULT_BUFFER]; // ��������� � ������������ �������
					char tmp[DEFAULT_BUFFER];
					strcpy(tmp, Message);
					int Count = 0;
					//for (int i = 0; i < DEFAULT_BUFFER - 1; i++)
					//	Count += Message[i + 1];
					Count = Message[1];
					ZeroMemory(Msg, DEFAULT_BUFFER);
					// ��������� ���������					
					for (int i = 0; i < Count; i++)
					{
						char Temp[DEFAULT_BUFFER]; // ��������� � ������������ �������
						int Width = 20 + rand() % 800;
						int Height = 20 + rand() % 300;
						itoa(Width, Temp, 10);
						strcat(Msg, Temp);
						Msg[strlen(Msg)] = ' ';

						char ResHeight[DEFAULT_BUFFER]; // ��������� � ������������ �������
						ZeroMemory(ResHeight, DEFAULT_BUFFER);
						itoa(Height, ResHeight, 10);
						strcat(Msg, ResHeight);
						Msg[strlen(Msg)] = ' ';
					}					
					send(Clients[i], Msg, strlen(Msg) + 1, 0); // ���������� ��������� �������
				}					
			}
		}
		else
		{
			// ���� ���� ��� � �������� � ���-�� �� ��������
			// (������� � ����� 1 � ����� � else),
			// �� �� ������ � ������� 0 ��� ������ � ���������� ��� �����(�������������� ����������)
			PlayersEndGame[PlayersEndGameB] = 0; // ������ ���� 0 - ����� �������� ����
			PlayersEndGameB++; // ��������� � ����������, ��� ��� ���� ����� �������� ����
			if (PlayersEndGameB == PlayersCount) // ���� ��� ������ ��������� ����
			{	// �������� ������� ��������� ����, �������� ������ ����������
				// ������ PlayersEndGame �� ��������� ���� ��� ������ ���� �������� ������
				ZeroMemory(PlayersReady, sizeof(int));
				ZeroMemory(&PlayersEndGameB, sizeof(int));
				ZeroMemory(&PlayersCount, sizeof(int));
				GameInProgress = false;
			}			
			clock_t CurPlayertime = clock() - GameTime;			
			double t = (double)CurPlayertime / CLOCKS_PER_SEC;
			// ������� �������� ������� � ������� �������
			char TMessage[5];
			ZeroMemory(TMessage, 5);
			sprintf(TMessage, "%f", t);
			// ���������� ������� ���������
			send((SOCKET)ClientSocket, TMessage, 5, 0);
		}
		Sleep(20);
	}
	if (PlayersCount > 0)
		PlayersCount--; // ��� ���������� ������ ��������� ��� ���������
}