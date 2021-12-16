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
		printf("Ошибка чтения файла Spisok #.txt\n");
		return error;
	}
	while(!feof(akk))
	{
		struct muz_akk* next_akk; //Указатель на новую структуру
		next_akk = (struct muz_akk*)malloc(sizeof(struct muz_akk)); // Выделяем память под новую структуру
		next_akk->next = head; // Определяем голову на следующий элемент
		next_akk->prev = tail; // Определяем голову на предыдущий элемент
		if (head) head->prev = next_akk; // Определяем на текущий узел
		head = next_akk;  // Определяем голову на новый узел
		if(!tail) tail = head; // Если не было элемента в структуре, то ставим голову на 1-ый элемент
		i = fscanf(akk,"%s%s%s%d%s%s%d", next_akk->name, next_akk->osn_ton, next_akk->tonal, &(next_akk->frequency), next_akk->razr_name, next_akk->gamma, &(next_akk->stupen));
		if (i!=7) // Если было отсканировано меньше семи эл-тов(Некорректно заполненная таблица) то игнорируем место сбоя и переходим к следующей строке
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