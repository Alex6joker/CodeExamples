#include <stdio.h>
#include <ctype.h>

int prov_probel(char* str)
{
	int i,z;
	z = strlen(str);
	for(i=0;;i++)
	{
		if(str[i]==32) return 0; // ���� ������ ����, �� ���������� 0
		if(i==z)return 1; // �������� ��� ������ � �������� � ��� �� ����������
	}
}