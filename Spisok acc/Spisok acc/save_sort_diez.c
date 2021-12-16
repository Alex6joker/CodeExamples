#include <stdio.h>
#include <string.h>
#include <malloc.h>
#include "Create_struct_header.h"

void save_sort_diez(int z, struct muz_akk* tmp4)
{
	
	FILE* akk;
	char* file_name;
	file_name = (char*)malloc(256); // ������������ ����� ����� � NTFS - 256 ������.
	if(z==0)  strcpy(file_name,"Spisok #.txt"); // ���� ������ ������� � ���� ������ ���������
	else // ���� ������ ������� � ����� ����
	{
		printf("--------------------------------------------------------------------------------");
		printf("������� �������� �����: ");
		fflush(stdin);
		gets(file_name);
		printf("--------------------------------------------------------------------------------");
	}
	if(z==0)
	{
		akk = fopen(file_name,"r");// ��������� ������������� �����
		if(!akk) 
		{
			printf("����� ��������� �� ���������� ��� ��������� ������ ��������\n������ ����������\n");
			system("pause");
			return;
		}
		fclose(akk);
	}
	akk = fopen(file_name,"w");
	while(tmp4!=NULL)
	{
		fprintf(akk,"%s %s %s %d %s %s %d\n", tmp4->name,tmp4->osn_ton,tmp4->tonal,tmp4->frequency,tmp4->razr_name,tmp4->gamma,tmp4->stupen);
		tmp4 = tmp4->next;
	}
	fclose(akk);
	free(file_name);
	printf("������ � ���� ��������� �������\n");
	system("pause");
	system("cls");
}