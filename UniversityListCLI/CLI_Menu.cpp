#include "CLI_Menu.h"

CLI_Menu::CLI_Menu()
{
	calculator = new Calculator();
	settings = Settings::GetInstanse();
	settings->LoadSettings();
}

CLI_Menu::~CLI_Menu()
{
	delete(calculator);
	delete(settings);
}

void CLI_Menu::WaitForUserCmd()
{
	char pressedSymbol = ' ';
	while (pressedSymbol != '3')
	{
		PrintCLIInterface();
		std::cin.get(pressedSymbol);
		std::cin.clear();
		std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
		switch (pressedSymbol)
		{
		case '1':
			PrintGroupsInfo();
			break;
		case '2':
			Calculate();
			break;
		}
	}
}

void CLI_Menu::Calculate()
{
	calculator->Calculate();
}

void CLI_Menu::PrintGroupsInfo()
{
	std::cout << "Вывод данных о сотрудниках.\n";
	std::cout << "Должность\tИмя\tФамилия\t\tЗарплата\t\n";
	std::map<std::string, Group*> groups = Settings::GetInstanse()->GetGroupsList()->GetGroups();
	std::map<std::string, Group*>::reverse_iterator it = groups.rbegin();

	while (it != groups.rend())
	{
		size_t size = it->second->GetAccounts().size();
		for (size_t concreteAccountId = 0; concreteAccountId < size; concreteAccountId++)
		{
			std::cout << it->second->GetGroupName() << "\t"
				<< it->second->GetAccounts()[concreteAccountId]->GetInfo()[0] << "\t"
				<< it->second->GetAccounts()[concreteAccountId]->GetInfo()[1] << "\t" << "\t"
				<< it->second->GetSalary() << "\t" << std::endl;
		}
		it++;
	}

	system("pause");
}

void CLI_Menu::PrintCLIInterface()
{
	system("cls");
	std::cout << "Консольное приложение на С++ (с использованием любых библиотек),\n";
	std::cout << "по расчету суммарных расходов за месяц по заработной плате в университете:\n";
	std::cout << "для условных двух преподавателей и одного лаборанта.\n\n";

	std::cout << "\t1. Просмотр данных о сотрудниках.\n";
	std::cout << "\t2. Расчет заработной платы и премий сотрудникам университета по фиксированным ставкам.\n";
	std::cout << "\t3. Выход.";
	std::cout << std::endl << std::endl;
}