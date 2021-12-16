#include "Groups.h"

Groups::Groups()
{
	it = groups.begin();
}

Groups::~Groups()
{
	groups.clear();
}

void Groups::AddGroup(std::string addedGroupName)
{
	groups.insert(it, std::pair<std::string, Group*>(addedGroupName, new Group(addedGroupName)));
}

void Groups::AddGroupSalary(std::string groupName, std::int32_t salaryInfo)
{
	GetGroupByGroupName(groupName)->SetSalary(salaryInfo);
}

void Groups::AddGroupPremiumCoeficient(std::string groupName, std::int32_t premiumCoeficient)
{
	GetGroupByGroupName(groupName)->SetPremiumCoeficient(premiumCoeficient);
}

void Groups::AddGroupAccountInfo(std::string groupName, std::vector<std::string> accountInfo)
{
	GetGroupByGroupName(groupName)->SetNewAccountInfo(accountInfo);
}

std::map<std::string, Group*> Groups::GetGroups()
{
	return groups;
}

Group* Groups::GetGroupByGroupName(std::string searchedGroupName)
{
	return groups.at(searchedGroupName);
}