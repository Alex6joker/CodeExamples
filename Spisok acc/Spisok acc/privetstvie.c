#include <stdio.h>
#include <conio.h>
#include <windows.h>

void privetstvie()
{
	FILE* f0;
	char str[50];
	char* estr;
	HANDLE hstdout;
	hstdout = GetStdHandle(STD_OUTPUT_HANDLE);
	SetConsoleTextAttribute(hstdout,FOREGROUND_RED | BACKGROUND_RED |  BACKGROUND_BLUE | BACKGROUND_GREEN | BACKGROUND_INTENSITY);
	f0=fopen("privetstvie.txt","r");
	while(!feof(f0)) // Пока не конец файла
	{
		estr = fgets(str,sizeof(str),f0);
		printf(estr);
	}
	fclose(f0);
	printf("\n");
	printf("--------------------------------------------------------------------------------");
	printf("\t\t\tНажмте любую клавишу                                    ");
	printf("--------------------------------------------------------------------------------");
	getch();
	system("cls");
}