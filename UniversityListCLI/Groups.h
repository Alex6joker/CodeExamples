#pragma once
#ifndef GROUPS_H
#define GROUPS_H
#include <map>
#include <string>
#include "Group.h"

class Groups
{
private:
	Group* GetGroupByGroupName(std::string);
	std::map<std::string, Group*>::iterator it;
	std::map<std::string, Group*> groups;

public:
	Groups();
	~Groups();

	std::map<std::string, Group*> GetGroups();
	void AddGroup(std::string);
	void AddGroupSalary(std::string, std::int32_t); 
	void AddGroupPremiumCoeficient(std::string, std::int32_t);
	void AddGroupAccountInfo(std::string, std::vector<std::string>);
};

#endif