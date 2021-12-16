#include <stdio.h>
#include <conio.h>
#include "Create_struct_header.h"

void vyvod_opred(int c)
{
	FILE* f0;
	char str[50];
	char* estr;
	int m;
	f0 = fopen("System.txt","r");
	while(!feof(f0))
	{
		estr = fgets(str,sizeof(str),f0);
		printf(estr);
	}
	printf("\n");
	scanf("%d",&m);
	switch(m)
	{
	case(1) :
		{
			system("cls");
			chtenie_s_file_diez(head,tail,c);
			break;
		}
	case(2) :
		{
			system("cls");
			chtenie_s_file_bemol(head,tail,c);
			break;
		}
	}
}