#include <stdio.h>
#include <conio.h>

void vvod_akk_start()
{
	int i;
	char c;
	printf("--------------------------------------------------------------------------------");
	printf("\t\t\tВыберите систему аккордов\n");
	printf("--------------------------------------------------------------------------------\n");
	printf("\n\t\t\t---------------------------\n");
	printf("\t\t\t\t1. Диез #\n");
	printf("\t\t\t---------------------------\n");
	printf("\n\t\t\t---------------------------\n");
	printf("\t\t\t\t2. Бемоль\n");
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
	}while(i!=1 && i!=2);
	switch(i)
	{
	case 1:
		system("cls");
		vvod_diez(i);
		break;
	case 2:
		i = 3;
		system("cls");
		vvod_bemol(i);
		break;
	}
}