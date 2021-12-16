#include <stdio.h>
#include <conio.h>
#include <windows.h>
#include "head.h"

void main()
{
	SetConsoleCP(1251);
    SetConsoleOutputCP(1251);
	privetstvie();
	if (gl_menu_vybor()==3) return;
	menu_vybor();
}