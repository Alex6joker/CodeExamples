#include <windows.h>
#include <time.h>

int PlayersCount = 0;
int PlayersEndGameB = 0;
bool GameInProgress = false;
LPBYTE PlayersReady = (LPBYTE)malloc(100); // ���������� ����� ����� ������ ������� ��� �������
LPBYTE PlayersEndGame = (LPBYTE)malloc(100); // ���������� ����� ����� ������ ������� ��� �������
SOCKET* Clients = (SOCKET*)malloc(100); // ���������� ����� ����� ������ ������� ��� �������