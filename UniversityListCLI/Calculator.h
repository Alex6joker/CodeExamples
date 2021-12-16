#pragma once
#ifndef CALCULATOR_H
#define CALCULATOR_H
#include "Groups.h"
#include "Settings.h"

class Calculator
{
private:
	Groups* GetAccountGroups();

public:
	Calculator();
	~Calculator();
	void Calculate();
};

#endif