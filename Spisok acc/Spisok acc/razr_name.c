#include <stdio.h>
#include <conio.h>
#include "Create_struct_header.h"
#include "Poisk_akk.h"
void razr_name(struct muz_akk* h,struct muz_akk* p)
{
	struct muz_akk* tmp;
	int z;
	char razr_name_akk[10];
	z = 0;
	tmp = h; // �������� ��������� ��������� ������
	printf("������� ���������� ��������� �������: ");
	scanf("%s",razr_name_akk);
	system("cls");
	printf("����� �������� ��� �������, ��������������� �������� ������\n");
	printf("--------------------------------------------------------------------------------");
	printf("|������\t|���\t|�����������\t|�������(��)\t|����������\t|�����\t|�������");
	printf("--------------------------------------------------------------------------------");
	while(h!=NULL)
	{
		if(proverka_slov(razr_name_akk,p->razr_name)==1) {printf("|%s\t|%s\t|%s\t\t|%d\t\t|%s\t\t|%s\t|%d\n",p->name,p->osn_ton,p->tonal,p->frequency,p->razr_name,p->gamma,p->stupen); z++;}
		h=h->next;
		p=p->prev;
	}
	printf("--------------------------------------------------------------------------------");
	if (z==0)
	{
		system("cls");
		printf("�� ������� �� ������ �������, ���������������� �������� ������\n");
	}
	// ������� ������
	h = tmp;  // ��������������� ��������� ��������� ������
	while(h!=NULL) // ���� ���������� ��������� ���������
	{
		struct muz_akk* temp; // ���������� �������� ��������� ��-��� ������
		temp = h->next;
		free(h);
		h = temp;
	}
	printf("\n");
	system("pause");
	system("cls");
}