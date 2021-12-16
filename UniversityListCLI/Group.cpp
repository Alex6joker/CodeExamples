#include "Group.h"

Group::Group(std::string nGroupName)
{
	groupName = nGroupName;
	it = accounts.begin();
}

Group::~Group()
{

}

void Group::SetSalary(int32_t salary)
{
	this->salary = salary;
}

void Group::SetPremiumCoeficient(int32_t percentedPremiumCoeficient)
{
	this->percentedPremiumCoeficient = percentedPremiumCoeficient;
	premiumCoeficient = (float)percentedPremiumCoeficient / 100;
}

void Group::SetNewAccountInfo(std::vector<std::string> accountInfo)
{
	accounts.insert(it, new Account(accountInfo));
	it = accounts.end();
}

std::vector<Account*> Group::GetAccounts()
{
	return accounts;
}

std::string Group::GetGroupName()
{
	return groupName;
}

float Group::GetPremiumCoeficient()
{
	return premiumCoeficient;
}

int32_t Group::GetPercentedPremiumCoeficient()
{
	return percentedPremiumCoeficient;
}

std::string Group::GetPercentedPremiumCoeficientString()
{
	std::string percentedPremiumCoeficientString = std::to_string(percentedPremiumCoeficient);
	percentedPremiumCoeficientString.insert(percentedPremiumCoeficientString.end(), '%');
	return percentedPremiumCoeficientString;
}

int32_t Group::GetSalary()
{
	return salary;
}