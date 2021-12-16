#include <stdio.h>
#include <conio.h>
#include <windows.h>
#include <string.h>
#include "head.h"

void vvod_bemol(int c)
{
	FILE* file;
	FILE* rezerv;
	char str[50];
	char* buffer;
	int chislo;
	char k;
	int z; // ������ �� scanf
	switch (c)
	{
	case(3):
		system("cls");
		file = fopen("Spisok bemol.txt","r");
		if(!file)
		{
			printf("������ � ������ ����� Spisok bemol.txt\n");
			printf("������ � ���� ����������� ����������\n");
			system("pause");
			system("cls");
			return 0;
		}
		rezerv = fopen("tempfile","w");
		while(!feof(file))
		{
			buffer = fgets(str,sizeof(str),file);
			if (buffer==NULL) break;
			fputs(buffer,rezerv);
		}
		fclose(rezerv);
		fclose(file);
		buffer = (char*)malloc(1024); // �������� ������ ��� �������� ��������
		file = fopen("Spisok bemol.txt","w");
		do
		{
			printf("--------------------------------------------------------------------------------");
			printf("\t\t������������ ������ � ���� Spisok bemol.txt\n");
			printf("--------------------------------------------------------------------------------");
			printf("\n������ �������� ��� ��������\n");
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
			k = getch();
			system("cls");
		} while(k!='*');
		free(buffer);
		rezerv = fopen("tempfile","r");
		while(!feof(rezerv))
		{
			buffer = fgets(str,sizeof(str),rezerv);
			if (buffer==NULL) break;
			fputs(buffer,file);
		}
		fclose(rezerv);
		remove ("tempfile");
		fclose(file);
		break;
	case(4):
		system("cls");
		buffer = (char*)malloc(1024); // �������� ������ ��� �������� ��������
		file = fopen("Spisok bemol.txt","a");
		fseek(file,0,SEEK_END);
		if(!ftell(file))
		{
			fclose(file);
			printf("������ � ������ ����� Spisok bemol.txt\n");
			printf("������ � ���� ����������� ����������\n");
			system("pause");
			system("cls");
			return 0;
		}
		do
		{
			printf("������ �������� ��� ��������\n");
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
			k = getch();
			system("cls");
		} while(k!='*');
		fclose(file);
		free(buffer);
		break;
	}
	printf("--------------------------------------------------------------------------------");
	printf("������ � ���� ���� ������� ���������\n");
	printf("--------------------------------------------------------------------------------\n");
	system("pause");
}