#include "Main.h"
#include "Globals.h"

// ������� ����������, ��� �� ������ ������
// ��� ����� ������ ���, ��� � ������� ��� ��������
// ������ ���� �� ����� 0
bool AllReady(LPBYTE PlayersReady)
{
	for (int i = 0; i < PlayersCount; i++)
		if (PlayersReady[i] == 0)
			return false;
	return true;
}