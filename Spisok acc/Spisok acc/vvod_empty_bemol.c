#include <stdio.h>
#include <conio.h>
#include <windows.h>
#include <string.h>

void vvod_empty_bemol()
{
	FILE* file;
	char *buffer;
	int chislo;
	int z; // ������ �� scanf
	char c;
	buffer = (char*)malloc(1024); // �������� ������ ��� �������� ��������
	file = fopen("Spisok bemol.txt","w");
	do
	{
	system("cls");
	printf("--------------------------------------------------------------------------------");
	printf("������� ������ � ���� Spisok bemol.txt\n");
	printf("--------------------------------------------------------------------------------");
	printf("������ �������� ��� ��������\n");
	printf("--------------------------------------------------------------------------------");
	printf("������� �������� �������: ");
	fflush(stdin);
	gets(buffer);
	while((strlen(buffer)>6) || (prov_probel(buffer)==0)) // ������ ������ ������������ ����� ��� ������ ������� � ��������
	{
		printf("������� ������� �������� �������, ������������ ����� - 6 ��� ������ ������� �������\n������� �������� �������: ");
		fflush(stdin);
		gets(buffer);
	}
	fprintf(file,"%s ", buffer);
	printf("--------------------------------------------------------------------------------");
	printf("������� �������� ��� �������: ");
	fflush(stdin);
	gets(buffer);
	while((strlen(buffer)>2) || (prov_probel(buffer)==0))
	{
		printf("������� �������� ��� ������, �� ������� �������� �� ���� ����\n������� �������� ��� �������: ");
		fflush(stdin);
		gets(buffer);
	}
	fprintf(file,"%s ", buffer);
	printf("--------------------------------------------------------------------------------");
	printf("������� ����������� �������: ");
	fflush(stdin);
	gets(buffer);
	while((strlen(buffer)>6) || (prov_probel(buffer)==0))
	{
		printf("������� ������� �������� �����������, ������������ ����� ����������� ������� - 6\n������� ����������� �������: ");
		fflush(stdin);
		gets(buffer);
	}
	fprintf(file,"%s ", buffer);
	printf("--------------------------------------------------------------------------------");
	printf("������� ������� �������(��): ");
	z = scanf("%5d",&chislo);
	fflush(stdin);
	while((chislo>1365) || (z==0)|| (chislo<=0)) // ���� ������� ������� ������� ��� ������� �� �����
	{
		printf("������� ���� 1365 �� ��� ������� �� �����\n������� ������� �������(��): ");
		fflush(stdin);
		z = scanf("%5d",&chislo);
	}
	fprintf(file,"%d ", chislo);
	printf("--------------------------------------------------------------------------------");
	printf("������� ���������� �������(NR, ���� �� ���������): ");
	fflush(stdin);
	gets(buffer);
	while((strlen(buffer)>6) || (prov_probel(buffer)==0))
	{
		printf("������� ������� �������� ����������, ������������ ����� ���������� ������� - 6\n������� ���������� �������(NR, ���� �� ���������): ");
		fflush(stdin);
		gets(buffer);
	}
	fprintf(file,"%s ", buffer);
	printf("--------------------------------------------------------------------------------");
	printf("������� ����� �������: ");
	fflush(stdin);
	gets(buffer);
	while((strlen(buffer)>4) || (prov_probel(buffer)==0))
	{
		printf("������� ������� �������� �����, ������������ ����� ����� - 4\n������� ����� �������: ");
		fflush(stdin);
		gets(buffer);
	}
	fprintf(file,"%s ", buffer);
	printf("--------------------------------------------------------------------------------");
	printf("������� ������� ������� � �����������: ");
	z = scanf("%2d",&chislo);
	fflush(stdin);
	while((chislo>7) || (z==0)|| (chislo<=0))
	{
		printf("������ �� ����� ������ �� ������� ���� ������� ��� ������� �� �����\n������� ������� ������� � �����������: ");
		fflush(stdin);
		z = scanf("%2d",&chislo);
	}
	fprintf(file,"%d\n", chislo);
	printf("--------------------------------------------------------------------------------");
	printf("������ ��� ���� ������ (����� �������) ��� ���������� ������(*)? ");
	c = getch();
	system("cls");
	} while(c!='*');
	free(buffer);
	fclose(file);
}