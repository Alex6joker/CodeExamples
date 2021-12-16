#include <stdio.h>
#include <malloc.h>
#include "Create_struct_header.h"
#include "Sort_akk.h"
#include <limits.h>
#include <string.h>

void sort_diez_stupen()
{
	struct muz_akk* tmp;  // ��������������� �������
	struct muz_akk* tmp1; // ��������� ��������� �� ������(����� �� �������� �����)
	struct muz_akk* tmp2; // ��������� ��������� �� ������(����� �� ����������� �����)
	struct muz_akk* tmp3; // ��������� �������� tmp->next
	struct muz_akk* tmp4; // ������ ��������� ��������� ������ tmp
	int max,min;
	int k = 3; // ������� �������� ��� ������� chtenie_s_file_diez/bemol
	int i,j,z;
	int sort_chislo; // ������ ����� ��� ����������
	char* sort_str;	// ������ ������ ��� ����������
	char c;
	printf("--------------------------------------------------------------------------------");
	printf("�������� ��� ����������: �� ������������(1) �� ��������(2)\n");
	printf("--------------------------------------------------------------------------------\n");
	do
	{
		c = getch();
		__asm
		{
			xor ecx,ecx
			mov cl,c
			sub ecx,0x30
			xor	eax,eax
			add eax,ecx
			mov i,eax
		}
	}while(i!=1 && i!=2);
	j = 0;
	max = INT_MIN;
	min = INT_MAX;
	tmp = chtenie_s_file_diez(k); // ��������� ������� - ������ ��������� ������ ����, ��� ����� ���� ������� �� �����
	if (tmp==error) 
	{
		printf("���������� ������� ����������\n");
		return;
	}
	tmp1=tmp;
	tmp2=tmp;
	tmp4=tmp;
	sort_str = (char*)malloc(50);
	if(i==1)
	{
		//.............���������� ����������� �������� � ��������� � ������ ��� �� ������ �����
		while(tmp1!=NULL)
		{
			while(tmp2!=NULL)
			{
				if(tmp2->stupen < min) 
				{
					min = tmp2->stupen;
					strcpy(sort_str,tmp2->name);
					strcpy(tmp2->name,tmp->name);
					strcpy(tmp->name,sort_str);
					strcpy(sort_str,tmp2->osn_ton);
					strcpy(tmp2->osn_ton,tmp->osn_ton);
					strcpy(tmp->osn_ton,sort_str);
					strcpy(sort_str,tmp2->tonal);
					strcpy(tmp2->tonal,tmp->tonal);
					strcpy(tmp->tonal,sort_str);
					strcpy(sort_str,tmp2->razr_name);
					strcpy(tmp2->razr_name,tmp->razr_name);
					strcpy(tmp->razr_name,sort_str);
					strcpy(sort_str,tmp2->gamma);
					strcpy(tmp2->gamma,tmp->gamma);
					strcpy(tmp->gamma,sort_str);
					sort_chislo = tmp2->frequency;
					tmp2->frequency = tmp->frequency;
					tmp->frequency = sort_chislo;
					sort_chislo = tmp2->stupen;
					tmp2->stupen = tmp->stupen;
					tmp->stupen = sort_chislo;
				}
				tmp2 = tmp2->next;
			}
			tmp1 = tmp1->next;
			tmp2 = tmp;
		}
		tmp = tmp->next; // ������ � ������ �������� �� �������� ��������
		tmp3 = tmp; // ��������� �������� tmp->next
		tmp1=tmp; // ���������� ��������� ��������� ����������
		tmp2=tmp;
		//............��������� �������
		while(tmp1!=NULL)
		{
			while(tmp2!=NULL)
			{
				if(tmp2->stupen >= tmp->stupen)
				{
					max = tmp2->stupen;
					strcpy(sort_str,tmp2->name);
					strcpy(tmp2->name,tmp->name);
					strcpy(tmp->name,sort_str);
					strcpy(sort_str,tmp2->osn_ton);
					strcpy(tmp2->osn_ton,tmp->osn_ton);
					strcpy(tmp->osn_ton,sort_str);
					strcpy(sort_str,tmp2->tonal);
					strcpy(tmp2->tonal,tmp->tonal);
					strcpy(tmp->tonal,sort_str);
					strcpy(sort_str,tmp2->razr_name);
					strcpy(tmp2->razr_name,tmp->razr_name);
					strcpy(tmp->razr_name,sort_str);
					strcpy(sort_str,tmp2->gamma);
					strcpy(tmp2->gamma,tmp->gamma);
					strcpy(tmp->gamma,sort_str);
					sort_chislo = tmp2->frequency;
					tmp2->frequency = tmp->frequency;
					tmp->frequency = sort_chislo;
					sort_chislo = tmp2->stupen;
					tmp2->stupen = tmp->stupen;
					tmp->stupen = sort_chislo;
				}
				tmp2 = tmp2->next;
			}
			tmp = tmp->next; // ��������� � ���������� �������� ���������
			tmp1 = tmp1->next;
			tmp2 = tmp3;
		}
	}
	if(i==2)
	{
		//.............���������� ������������ �������� � ��������� � ������ ��� �� ������ �����
		while(tmp1!=NULL)
		{
			while(tmp2!=NULL)
			{
				if(tmp2->stupen > max) 
				{
					max = tmp2->stupen;
					strcpy(sort_str,tmp2->name);
					strcpy(tmp2->name,tmp->name);
					strcpy(tmp->name,sort_str);
					strcpy(sort_str,tmp2->osn_ton);
					strcpy(tmp2->osn_ton,tmp->osn_ton);
					strcpy(tmp->osn_ton,sort_str);
					strcpy(sort_str,tmp2->tonal);
					strcpy(tmp2->tonal,tmp->tonal);
					strcpy(tmp->tonal,sort_str);
					strcpy(sort_str,tmp2->razr_name);
					strcpy(tmp2->razr_name,tmp->razr_name);
					strcpy(tmp->razr_name,sort_str);
					strcpy(sort_str,tmp2->gamma);
					strcpy(tmp2->gamma,tmp->gamma);
					strcpy(tmp->gamma,sort_str);
					sort_chislo = tmp2->frequency;
					tmp2->frequency = tmp->frequency;
					tmp->frequency = sort_chislo;
					sort_chislo = tmp2->stupen;
					tmp2->stupen = tmp->stupen;
					tmp->stupen = sort_chislo;
				}
				tmp2 = tmp2->next;
			}
			tmp1 = tmp1->next;
			tmp2 = tmp;
		}
		tmp = tmp->next; // ������ � ������ �������� ��������
		tmp3 = tmp; // ��������� �������� tmp->next
		tmp1=tmp; // ���������� ��������� ��������� ����������
		tmp2=tmp;
		//............��������� �������
		while(tmp1!=NULL)
		{
			while(tmp2!=NULL)
			{
				if(tmp2->stupen < tmp->stupen)
				{
					max = tmp2->stupen;
					strcpy(sort_str,tmp2->name);
					strcpy(tmp2->name,tmp->name);
					strcpy(tmp->name,sort_str);
					strcpy(sort_str,tmp2->osn_ton);
					strcpy(tmp2->osn_ton,tmp->osn_ton);
					strcpy(tmp->osn_ton,sort_str);
					strcpy(sort_str,tmp2->tonal);
					strcpy(tmp2->tonal,tmp->tonal);
					strcpy(tmp->tonal,sort_str);
					strcpy(sort_str,tmp2->razr_name);
					strcpy(tmp2->razr_name,tmp->razr_name);
					strcpy(tmp->razr_name,sort_str);
					strcpy(sort_str,tmp2->gamma);
					strcpy(tmp2->gamma,tmp->gamma);
					strcpy(tmp->gamma,sort_str);
					sort_chislo = tmp2->frequency;
					tmp2->frequency = tmp->frequency;
					tmp->frequency = sort_chislo;
					sort_chislo = tmp2->stupen;
					tmp2->stupen = tmp->stupen;
					tmp->stupen = sort_chislo;
				}
				tmp2 = tmp2->next;
			}
			tmp = tmp->next; // ��������� � ���������� �������� ���������
			tmp1 = tmp1->next;
			tmp2 = tmp3;
		}
	}
	free(sort_str); // ����������� ������ ��� ������
	tmp=tmp4;// ������������ � ������ ������� ��������
	printf("--------------------------------------------------------------------------------");
	printf("���������� ��������� �������\n������� �� ������� ��������������� ��������� �� �����? (1) �� (0) ���\n");
	printf("--------------------------------------------------------------------------------");
	do
	{
		c = getch();
		__asm
		{
			xor ecx,ecx
			mov cl,c
			sub ecx,0x30
			xor	eax,eax
			add eax,ecx
			mov z,eax
		}
	}while(z!=1 && z!=0);
	system("cls");
	if(z==1) // ���� ������������ ��� �������� �� �����
	{
		printf("--------------------------------------------------------------------------------");
		printf("|������\t|���\t|�����������\t|�������(��)\t|����������\t|�����\t|�������");
		printf("--------------------------------------------------------------------------------");
		while(tmp!=NULL)
		{
			printf("%s\t%s\t%s\t\t%d\t\t%s\t\t%s\t%d\n",tmp->name,tmp->osn_ton,tmp->tonal,tmp->frequency,tmp->razr_name,tmp->gamma,tmp->stupen);
			tmp = tmp->next;
		}
		printf("--------------------------------------------------------------------------------");
		printf("\n");
	}
	printf("--------------------------------------------------------------------------------");
	printf("��������� �� ��������� ��������������� �������? (1) �� (0) ���\n");
	printf("--------------------------------------------------------------------------------\n");
	do
	{
		c = getch();
		__asm
		{
			xor ecx,ecx
			mov cl,c
			sub ecx,0x30
			xor	eax,eax
			add eax,ecx
			mov z,eax
		}
	}while(z!=1 && z!=0);
	system("cls");
	if(z==1) // ���� ������������ ��� �������� �� ����������
	{
		printf("�������� ���������� � ����� ���� (1) ��� � ������� ������ ���������(0)\n");
		printf("--------------------------------------------------------------------------------");
		do
		{
			c = getch();
			__asm
			{
				xor ecx,ecx
				mov cl,c
				sub ecx,0x30
				xor	eax,eax
				add eax,ecx
				mov z,eax
			}
		}while(z!=1 && z!=0);
		system("cls");
		save_sort_diez(z,tmp4);
	}
}