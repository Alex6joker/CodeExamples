#include <stdio.h>
#include <conio.h>
#include <string.h>

int del_verx_diez(int k)
{
	int i;
	FILE* akk;
	FILE* rezerv;
	char str[50];
	char* buffer;
	i = 0; // ������� �����
	akk = fopen("Spisok #.txt","r");
	fseek(akk,0,SEEK_END);
	if(!ftell(akk))
	{
		printf("������ � ������ ����� Spisok #.txt\n");
		return 1;
	}
	fseek(akk,0,SEEK_SET);
	while(!feof(akk))
	{
		buffer = fgets(str,sizeof(str),akk);
		if (buffer==NULL) break;
		i++;
	}
	fclose(akk);
	if (k>=i) //�������� �� ����������� ����� ��������� ����� � ����� ����� ������� ������������
	{
		printf("������� ������������ ���-�� ��������� �����\n");
		printf("�� ������� ����� ������� ����� ���������� ����� �� �������\n");
		getch();
		return 1;
	}
	akk = fopen("Spisok #.txt","r");
	if(!akk)
	{
		printf("������ � ������ ����� Spisok #.txt");
		return 0;
	}
	rezerv = fopen("tempfile","w");
	while(!feof(akk))					// ������ ������ ����� �� ��������� ����
	{
		buffer = fgets(str,sizeof(str),akk);
		if (k!=0)
		{
			k--;
			continue;
		}
		if (buffer==NULL) break;
		fputs(buffer,rezerv);
	}
	fclose(akk);
	fclose(rezerv);
	akk = fopen("Spisok #.txt","w");
	rezerv = fopen("tempfile","r");
	while(!feof(rezerv))				// ������ �� ���������� �����
	{
		buffer = fgets(str,sizeof(str),rezerv);
		if (buffer==NULL) break;
		fputs(buffer,akk);
	}
	fclose(rezerv);
	remove("tempfile");
	fclose(akk);
	return 0;
}