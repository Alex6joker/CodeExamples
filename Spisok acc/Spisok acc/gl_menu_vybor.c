#include <stdio.h>
#include <conio.h>
#include <stdlib.h>

int gl_menu_vybor()
{
	int i;
	char c;
	FILE* gl_menu;
	char str[50];
	char* estr;
	gl_menu = fopen("gl_menu_vybor.txt","r");
	while(!feof(gl_menu))
	{
		estr = fgets(str,sizeof(str),gl_menu);
		printf(estr);
	}
	printf("\n");
	fclose(gl_menu);
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
	system("cls");
	if (i==1) files_size();
	return i;
}