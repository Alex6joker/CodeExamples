#include <stdio.h>
#include <conio.h>
#include <malloc.h>
#include "Create_struct_header.h"

int del_opred_diez()
{
	struct muz_akk z;
	FILE* akk;
	FILE* rezerv;
	char del_name[8];
	char* buffer;
	int j;
	int i;
	printf("--------------------------------------------------------------------------------");
	printf("\t�� ������� ����� ������ ������ ����� �� ����������� ������ ��������\n");
	printf("--------------------------------------------------------------------------------");
	printf("\n������� �������� �������, ������ � ������� ������ �������: ");
	fflush(stdin);
	gets(del_name);
	i = 0;
	buffer = (char*)malloc(60);
	akk = fopen("Spisok #.txt","r");
	fseek(akk,0,SEEK_END);
	if(!ftell(akk))
	{
		printf("������ � ������ ����� Spisok #.txt");
		return 1;
	}
	fseek(akk,0,SEEK_SET);
	rezerv = fopen("tempfile","w");
	while(!feof(akk))
	{
		j = fscanf(akk,"%s%s%s%d%s%s%d", z.name,z.osn_ton,z.tonal,&(z.frequency),z.razr_name,z.gamma,&(z.stupen));
		if (j!=7) break; // ���� ���� ��������� ����������� ������ � �����
		if ((proverka_slov(z.name,del_name)==1) && (i==0)) 
		{
			i++;
			continue;											// ���� ��� ������ ��� ��� �������� ������, �� �� ��� ���������� ��� ������ � �� ������������
		}			
		fprintf(rezerv,"%s %s %s %d %s %s %d\n", z.name,z.osn_ton,z.tonal,z.frequency,z.razr_name,z.gamma,z.stupen); // ���������� ��������� ������ �� ��������� ����
	}
	fclose(akk);
	fclose(rezerv);
	if(i==0)
	{
		system("cls");
		printf("--------------------------------------------------------------------------------");
		printf("\t�� ������� �� ������ �������, ���������������� �������� ������\n");
		printf("--------------------------------------------------------------------------------");
		return 1;
	}
	akk = fopen("Spisok #.txt","w");
	rezerv = fopen("tempfile","r");
	while(!feof(rezerv))
	{
		buffer = fgets(buffer,sizeof(buffer),rezerv);
		if (buffer==NULL) break;
		fputs(buffer,akk);
	}
	free(buffer);
	fclose(akk);
	fclose(rezerv);
	remove ("tempfile");
	return 0;
}