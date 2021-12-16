#include "Calculator.h"

Calculator::Calculator()
{

}

Calculator::~Calculator()
{

}

void Calculator::Calculate()
{
	std::cout << "Расчет платы и премий сотрудникам.\n";
	std::cout << "Должность\tИмя\tФамилия\t\tЗарплата\tПремия(%)\tЗарплата+Премия\n";
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
				<< it->second->GetSalary() << "\t" << "\t"
				<< it->second->GetPercentedPremiumCoeficientString() << "\t" << "\t"
				<< (it->second->GetSalary() + (it->second->GetSalary() * it->second->GetPremiumCoeficient())) << std::endl;
		}
		it++;
	}

	system("pause");
}

Groups* Calculator::GetAccountGroups()
{
	return Settings::GetInstanse()->GetGroupsList();
}