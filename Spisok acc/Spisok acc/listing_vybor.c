#include <stdio.h>
#include <conio.h>
#include "Create_struct_header.h"
#include "Poisk_akk.h"
void listing_vybor(struct muz_akk* h,struct muz_akk* p)
{
	char simv;
	int c;
	printf("--------------------------------------------------------------------------------");
	printf("\t\t    Выберите по какому признаку вывести аккорды\n");
	printf("--------------------------------------------------------------------------------");
	printf("\n1. Имя\n2. Основной тон\n3. Тональность\n4. Частота\n5. Разрешение\n6. Гамма\n7. Ступень\n8. Выход");
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
			mov c,eax
		}
	}while(c!=1 && c!=2 && c!=3 && c!=4 && c!=5 && c!=6 && c!=7 && c!=8);
	switch(c)
	{
	case(1):
		system("cls");
		name(h,p);
		break;
	case(2):
		system("cls");
		osn_ton(h,p);
		break;
	case(3):
		system("cls");
		tonal(h,p);
		break;
	case(4):
		system("cls");
		frequency(h,p);
		break;
	case(5):
		system("cls");
		razr_name(h,p);
		break;
	case(6):
		system("cls");
		gamma(h,p);
		break;
	case(7):
		system("cls");
		stupen(h,p);
		break;
	case(8):
		system("cls");
		break;
	}
}