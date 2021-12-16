#include "Account.h"

Account::Account(std::vector<std::string> accountInfo)
{
	name = accountInfo[0];
	surname = accountInfo[1];
}

Account::~Account()
{

}

std::vector<std::string> Account::GetInfo()
{
	std::vector<std::string> accountInfo;
	std::vector<std::string>::iterator it = accountInfo.begin();

	accountInfo.insert(it, name);
	it = accountInfo.end();
	accountInfo.insert(it, surname);

	return accountInfo;
}