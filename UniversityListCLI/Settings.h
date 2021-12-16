#pragma once
#ifndef SETTINGS_H
#define SETTINGS_H
#include <vector>
#include <string>
#include <fstream>
#include <iostream>
#include <mutex>
#include "Groups.h"

class Settings
{
private:
	Settings();
	static Settings* instanse;
	Groups* groups;
	void WrongSettingsFileStructure();
	void ReadAndWriteConcreteGroupAccountsInfo(std::fstream&, std::string, std::string);
	void ReadAndWriteConcreteGroupSettingsInfo(std::fstream&, std::string, std::string);
	void ReadAndCreateConcreteGroup(std::fstream&, std::string);
	static std::mutex syncMtx;

public:
	~Settings();
	void LoadSettings();
	Groups* GetGroupsList();
	static Settings* GetInstanse();
};

#endif