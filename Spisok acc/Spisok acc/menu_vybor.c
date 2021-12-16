#include <stdio.h>
#include <conio.h>
#include "head.h"
#include <windows.h>

int menu_vybor()
{
	char c;
	int i;
	do{
		FILE* menu;
		char* estr;
		char str[50];
		menu = fopen("menu_vybor.txt","r");
		while(!feof(menu))
		{
			estr=fgets(str,sizeof(str),menu);
			printf(estr);
		}
		printf("\n");
		fclose(menu);
		do
		{
			c = getch();
			__asm
			{
				xor ecx,ecx
				mov cl,c
				sub	ecx,0x30
				xor eax,eax
				add eax,ecx
				mov i,eax
			}
		}while(i!=1 && i!=2 && i!=3 && i!=4 && i!=5 && i!=6 && i!=7 && i!=8 && i!=9);
		switch (i)
		{
		case(1):
			system("cls");
			vyvod(i);
			break;
		case(2):
			system("cls");
			vyvod(i);
			break;
		case(3):
			system("cls");
			change_akk();
			break;
		case(4):
			system("cls");
			vvod_akk();
			break;
		case(5):
			system("cls");
			del_akk();
			break;
		case(6):
			system("cls");
			sortirovka();
			break;
		case(7) :
			system("cls");
			proverka_file();
			break;
		case(8) :
			system("cls");
			reset_file();
			break;
		case(9) :
			system("cls");
			return 0;
			break;
		}
		printf("--------------------------------------------------------------------------------");
		printf("\t\t  Требуются ли еще действия с таблицей?\n");
		printf("--------------------------------------------------------------------------------");
		printf("\n1. Продолжить работу с таблицей\n");
		printf("2. Выход из программы\n");
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
		}while(i!=1 && i!=2);
		system("cls");
	}while(i==1);
}