#include "CLI_Menu.h"

int main()
{
	setlocale(LC_ALL, "Russian");
	CLI_Menu* cli_Menu = new CLI_Menu();
	cli_Menu->WaitForUserCmd();
	delete(cli_Menu);
}