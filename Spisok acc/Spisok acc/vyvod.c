#include <stdio.h>
#include <conio.h>
#include "Create_struct_header.h"

void vyvod(int c)
{
	char simv;
	FILE* f0;
	int m;
	printf("--------------------------------------------------------------------------------");
	printf("\t\t\tВы желаете вывести базу аккордов\n");
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
		simv = getch();
		__asm
		{
			xor ecx,ecx
				mov cl,simv
				sub ecx,0x30
				xor eax,eax
				add eax,ecx
				mov m,eax
		}
	}while(m!=1 && m!=2 && m!=3);
	switch(m)
	{
	case(1) :
		{
			system("cls");
			chtenie_s_file_diez(c);
			break;
		}
	case(2) :
		{
			system("cls");
			chtenie_s_file_bemol(c);
			break;
		}
	case(3) :
		{
			system("cls");
			break;
		}
	}
}