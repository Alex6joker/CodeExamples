#include <stdio.h>
#include <conio.h>
#include "Change_akk.h"

void change_akk()
{
	int i;
	char c;
	printf("--------------------------------------------------------------------------------");
	printf("\t    Здесь вы можете изменить данные об уже записанном в БД аккорде\n");
	printf("--------------------------------------------------------------------------------");
	printf("\n\t\t\tВыберите систему аккордов\n");
	printf("\n\t\t\t---------------------------\n");
	printf("\t\t\t\t1. Диез #\n");
	printf("\t\t\t---------------------------\n");
	printf("\n\t\t\t---------------------------\n");
	printf("\t\t\t\t2. Бемоль\n");
	printf("\t\t\t---------------------------\n");
	printf("\n\t\t\t---------------------------\n");
	printf("\t\t\t\t3. Выход в меню\n");
	printf("\t\t\t---------------------------\n");
	do
	{
		c = getch();
		__asm
		{
			xor ecx,ecx
			mov cl,c
			sub ecx,0x30
			xor eax,eax
			add eax,ecx
			mov i,eax
		}
	}while(i!=1 && i!=2 && i!=3);
		switch(i)
	{
	case 1:
		system("cls");
		change_diez();
		break;
	case 2:
		system("cls");
		change_bemol();
		break;
	case 3:
		system("cls");
		break;
	}
}