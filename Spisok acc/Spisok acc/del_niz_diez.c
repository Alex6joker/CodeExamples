#include <stdio.h>
#include <conio.h>

int del_niz_diez(int k)
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
		printf("--------------------------------------------------------------------------------");
		printf("\t\t  ������� ������������ ���-�� ��������� �����\n");
		printf("--------------------------------------------------------------------------------");
		printf("\n�� ������� ����� ������� ����� ���������� ����� �� �������");
		getch();
		system("cls");
		return 1;
	}
	akk = fopen("Spisok #.txt","r");
	if(!akk)
	{
		printf("������ � ������ ����� Spisok #.txt");
		return 0;
	}
	rezerv = fopen("tempfile","w");
	while(!feof(akk) && (i-k>0))					// ������ ������ ����� �� ��������� ����
	{
		buffer = fgets(str,sizeof(str),akk);
		if (buffer==NULL) break;
		fputs(buffer,rezerv);
		i--;
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