#pragma once
#ifndef ACCOUNT_H
#define ACCOUNT_H
#include <vector>
#include <string>

class Account
{
private:
	std::string name;
	std::string surname;

public:
	Account(std::vector<std::string>);
	~Account();
	std::vector<std::string> GetInfo();
};

#endif