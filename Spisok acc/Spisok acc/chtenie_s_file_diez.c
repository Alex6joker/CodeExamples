#include <stdio.h>
#include <conio.h>
#include "Create_struct_header.h"

struct muz_akk* chtenie_s_file_diez(int c)
{
	FILE* akk;
	int i;
	head=NULL;
	tail=NULL;
	akk = fopen("Spisok #.txt","r");
	if(!akk)
	{
		printf("������ ������ ����� Spisok #.txt\n");
		return error;
	}
	while(!feof(akk))
	{
		struct muz_akk* next_akk; //��������� �� ����� ���������
		next_akk = (struct muz_akk*)malloc(sizeof(struct muz_akk)); // �������� ������ ��� ����� ���������
		next_akk->next = head; // ���������� ������ �� ��������� �������
		next_akk->prev = tail; // ���������� ������ �� ���������� �������
		if (head) head->prev = next_akk; // ���������� �� ������� ����
		head = next_akk;  // ���������� ������ �� ����� ����
		if(!tail) tail = head; // ���� �� ���� �������� � ���������, �� ������ ������ �� 1-�� �������
		i = fscanf(akk,"%s%s%s%d%s%s%d", next_akk->name, next_akk->osn_ton, next_akk->tonal, &(next_akk->frequency), next_akk->razr_name, next_akk->gamma, &(next_akk->stupen));
		if (i!=7) // ���� ���� ������������� ������ ���� ��-���(����������� ����������� �������) �� ���������� ����� ���� � ��������� � ��������� ������
		{
			struct muz_akk* temp;
			temp = head->next;
			free(head);
			head = temp;
		}
	}
	fclose(akk);
	switch (c)
	{
	case(1):
		system("cls");
		listing(head,tail);
		break;
	case(2):
		system("cls");
		listing_vybor(head,tail);
		break;
	case(3):
		break;
	}
	return head;
}