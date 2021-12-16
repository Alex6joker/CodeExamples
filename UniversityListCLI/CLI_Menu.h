#pragma once
#ifndef CLI_MENU_H
#define CLI_MENU_H
#include <iostream>
#include "Settings.h"
#include "Calculator.h"

class CLI_Menu
{
private:
	Calculator* calculator;
	Settings* settings;

	void Calculate();
	void PrintCLIInterface();
	void PrintGroupsInfo();

public:
	CLI_Menu();
	~CLI_Menu();
	void WaitForUserCmd();
};

#endif