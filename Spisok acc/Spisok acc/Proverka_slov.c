#include <stdio.h>
#include <conio.h>

int proverka_slov(char name1[], char name2[])
{
	int i;
	i = 0;
	if(strlen(name1)!=strlen(name2)) return 0; // ���� ����� ���� �� ���������, �� ��� ���������� ����(0)
	while(name1[i]==name2[i] && i<strlen(name2)) // ���� ��������� ����� � �� ����� ����� ������� ���-�� ����������
		i++;
	if (i!=strlen(name2)) return 0; // ���� ���-�� ��������� ���� �� ������������ ����� ������� �����, �� ���������� ����(0)
	else return 1; // ���� ��� �������� ��������, �� ���������� ������(1)
}