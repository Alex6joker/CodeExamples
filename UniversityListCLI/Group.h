#pragma once
#ifndef GROUP_H
#define GROUP_H
#include <vector>
#include <string>
#include "Account.h"

class Group
{
private:
	std::vector<Account*>::iterator it;
	std::vector<Account*> accounts;
	std::string groupName;
	float premiumCoeficient; 
	int32_t percentedPremiumCoeficient;
	int32_t salary;

public:
	Group(std::string);
	~Group();
	void SetSalary(int32_t);
	void SetPremiumCoeficient(int32_t);
	void SetNewAccountInfo(std::vector<std::string>);

	std::vector<Account*> GetAccounts();
	std::string GetGroupName();
	float GetPremiumCoeficient();
	int32_t GetPercentedPremiumCoeficient();
	int32_t GetSalary();
	std::string GetPercentedPremiumCoeficientString();
};

#endif