#include <stdio.h>
#include <conio.h>
#include "Vvod_akk.h"

void vvod_akk()
{
	int i;
	char c;
	printf("--------------------------------------------------------------------------------");
	printf("\t\t   Здесь вы можете внести новую информацию в базу данных\n");
	printf("--------------------------------------------------------------------------------");
	printf("\n\t\t   Выберите, куда будет добавлена запись\n");
	printf("\n\t\t\t---------------------------\n");
	printf("\t\t\t\t1. В начало списка\n");
	printf("\t\t\t---------------------------\n");
	printf("\n\t\t\t---------------------------\n");
	printf("\t\t\t\t2. В конец списка\n");
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
			xor	eax,eax
			add eax,ecx
			mov i,eax
		}
	}while(i!=1 && i!=2 && i!=3);
	switch(i)
	{
	case 1:
		system("cls");
		vvod_akk_start();
		break;
	case 2:
		system("cls");
		vvod_akk_end();
		break;
	case 3:
		system("cls");
		break;
	}
	system("cls");
}