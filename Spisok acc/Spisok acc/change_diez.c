#include <stdio.h>
#include <conio.h>
#include "Create_struct_header.h"

void change_diez()
{
	FILE* akk;
	FILE* rezerv;
	struct muz_akk z;
	char* buffer;
	char change_name[255];
	int chislo;
	int i; // ���� ������
	int j; // ������ �� fscanf
	int z1; // ������ �� scanf
	buffer=(char*)malloc(1024);
	printf("--------------------------------------------------------------------------------");
	printf("\t�� ������� ����� ������ ������ ����� �� ����������� ������ ��������\n");
	printf("--------------------------------------------------------------------------------");
	printf("\n������� �������� �������, ������ � ������� ������ ��������: ");
	fflush(stdin);
	gets(change_name);
	while((strlen(change_name)>6) || (prov_probel(change_name)==0)) // ������ ������ ������������ ����� ��� ������ ������� � ��������
	{
		printf("������� ������� �������� �������, ������������ ����� - 6 ��� ������ ������� �������\n�������� ������� �������� �������, ������ � ������� ������ ��������: : ");
		fflush(stdin);
		gets(change_name);
	}
	fflush(stdin);
	i = 0; 
	akk = fopen("Spisok #.txt","r");
	if(!akk)
	{
		printf("������ � ������ ����� Spisok #.txt\n");
		return 0;
	}
	rezerv = fopen("tempfile","w");
	while(!feof(akk))
	{
		j = fscanf(akk,"%s%s%s%d%s%s%d", z.name,z.osn_ton,z.tonal,&(z.frequency),z.razr_name,z.gamma,&(z.stupen));
		if (j!=7) break; // ���� ���� ��������� ����������� ������ � �����
		if ((proverka_slov(z.name,change_name)==1) && (i==0))  // ���� �������� ��������� (� ������ � ������� ������� ���� ���������� �������� - ������������ �������� ������)
		{
			i++;
			fclose(rezerv);
			rezerv = fopen("tempfile","a");
			system("cls");
			printf("--------------------------------------------------------------------------------");
			printf("\t\t   ������ � ����� ��������� ������!\n");
			printf("--------------------------------------------------------------------------------");
			printf("������ �������� ��� ��������\n");
			printf("--------------------------------------------------------------------------------");
			printf("������� �������� ������ �������: ");
			fflush(stdin);
			gets(buffer);
			while((strlen(buffer)>6) || (prov_probel(buffer)==0)) // ������ ������ ������������ ����� ��� ������ ������� � ��������
			{
				printf("������� ������� �������� �������, ������������ ����� - 6 ��� ������ ������� �������\n������� �������� ������ �������: ");
				fflush(stdin);
				gets(buffer);
			}
			fprintf(rezerv,"%s ", buffer);
			printf("\n--------------------------------------------------------------------------------");
			printf("������� �������� ��� ������ �������: ");
			fflush(stdin);
			gets(buffer);
			while((strlen(buffer)>2) || (prov_probel(buffer)==0))
			{
				printf("������� �������� ��� ������, �� ������� �������� �� ���� ����\n������� �������� ��� ������ �������: ");
				fflush(stdin);
				gets(buffer);
			}
			fprintf(rezerv,"%s ", buffer);
			printf("\n--------------------------------------------------------------------------------");
			printf("������� ����������� ������ �������: ");
			fflush(stdin);
			gets(buffer);
			while((strlen(buffer)>6) || (prov_probel(buffer)==0))
			{
				printf("������� ������� �������� �����������, ������������ ����� ����������� ������� - 6\n������� ����������� ������ �������: ");
				fflush(stdin);
				gets(buffer);
			}
			fprintf(rezerv,"%s ", buffer);
			printf("\n--------------------------------------------------------------------------------");
			printf("������� ������� �������(��): ");
			z1 = scanf("%5d",&chislo);
			fflush(stdin);
			while((chislo>1365) || (z1==0)|| (chislo<=0)) // ���� ������� ������� ������� ��� ������� �� �����
			{
				printf("������� ���� 1365 �� ��� ������� �� �����\n������� ������� �������(��): ");
				fflush(stdin);
				z1 = scanf("%5d",&chislo);
			}
			fprintf(rezerv,"%d ", chislo);
			printf("\n--------------------------------------------------------------------------------");
			printf("������� ���������� ������ �������(NR, ���� �� ���������): ");
			fflush(stdin);
			gets(buffer);
			while((strlen(buffer)>6) || (prov_probel(buffer)==0))
			{
				printf("������� ������� �������� ����������, ������������ ����� ���������� ������� - 6\n������� ���������� ������ �������(NR, ���� �� ���������): ");
				fflush(stdin);
				gets(buffer);
			}
			fprintf(rezerv,"%s ", buffer);
			printf("\n--------------------------------------------------------------------------------");
			printf("������� ����� ������ �������: ");
			fflush(stdin);
			gets(buffer);
			while((strlen(buffer)>4) || (prov_probel(buffer)==0))
			{
				printf("������� ������� �������� �����, ������������ ����� ����� - 4\n������� ����� ������ �������: ");
				fflush(stdin);
				gets(buffer);
			}
			fprintf(rezerv,"%s ", buffer);
			printf("\n--------------------------------------------------------------------------------");
			printf("������� ������� ������ ������� � �����������: ");
			z1 = scanf("%2d",&chislo);
			fflush(stdin);
			while((chislo>7) || (z1==0)|| (chislo<=0))
			{
				printf("������ �� ����� ������ �� ������� ���� ������� ��� ������� �� �����\n������� ������� ������ ������� � �����������: ");
				fflush(stdin);
				z1 = scanf("%2d",&chislo);
			}
			fprintf(rezerv,"%d\n", chislo);
			break;
		}
		fprintf(rezerv,"%s %s %s %d %s %s %d\n", z.name,z.osn_ton,z.tonal,z.frequency,z.razr_name,z.gamma,z.stupen); // ���������� ��������� ������ �� ��������� ����
	}
	if (i==1){
		while(!feof(akk))
		{
			j = fscanf(akk,"%s%s%s%d%s%s%d", z.name,z.osn_ton,z.tonal,&(z.frequency),z.razr_name,z.gamma,&(z.stupen));
			if (j!=7) break;
			fprintf(rezerv,"%s %s %s %d %s %s %d\n", z.name,z.osn_ton,z.tonal,z.frequency,z.razr_name,z.gamma,z.stupen);
		}
	}
	fclose(akk);
	fclose(rezerv);
	if (i==0)
	{
		system("cls");
		printf("--------------------------------------------------------------------------------");
		printf("\t\t   � ���������, ������ ����� ������ �� ������\n");
		printf("--------------------------------------------------------------------------------");
		system("pause");
		system("cls");
		return;
	}
	free(buffer);
	akk = fopen("Spisok #.txt","w");
	rezerv = fopen("tempfile","r");
	buffer = (char*)malloc(30);
	while(!feof(rezerv))				// ������ �� ���������� ����� � ��������
	{
		if (fgets(buffer,sizeof(buffer),rezerv)==NULL) break;
		fputs(buffer,akk);	
	}
	free(buffer);
	fclose(akk);
	fclose(rezerv);
	remove ("tempfile");
	system("cls");
	printf("--------------------------------------------------------------------------------");
	printf("\t\t������ ��������� ������� ���������\n");
	printf("--------------------------------------------------------------------------------");
	system("pause");
	system("cls");
}