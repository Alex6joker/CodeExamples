#include <windows.h>
#ifndef _Globals
#define _Globals

extern LPBYTE PlayersReady; // ���-�� �������, ������� � ����
extern LPBYTE PlayersEndGame; // ���-�� �������, ����������� ����
extern int PlayersCount; // ���-�� �������
extern bool GameInProgress; // ���� ����, ��� ���� ��� ������(���� �� ������, �� 1 - ����� � ����, ���� ������, �� 1 - ���� ��������)
extern int PlayersEndGameB; // ���-�� �������, ����������� ����, ���� ��� � ��������(����� ��� ���������� ������ PlayersEndGame)
extern SOCKET* Clients;	// ������ ������������ � ������� �������

#endif