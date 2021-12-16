#include "Settings.h"

Settings* Settings::instanse;
std::mutex Settings::syncMtx;

Settings::Settings()
{

}

Settings::~Settings()
{

}


Settings* Settings::GetInstanse()
{
	// Owner only can unlock mutex
	// in multi-threading app version only one thread create instanse, others get created instanse
	if (instanse == nullptr)
	{
		syncMtx.lock();
		if (instanse == nullptr)
			instanse = new Settings();
		syncMtx.unlock();
	}	
	return instanse;
}

void Settings::LoadSettings()
{
	std::fstream fs;
	std::string line;

	// Temporary path for x64/x86 versions compatible
	//fs.open(".\\Settings\\Settings.ini", std::fstream::in);
	fs.open("Settings.ini", std::fstream::in);

	if (fs.is_open())
	{
		while (std::getline(fs, line))
		{
			try
			{
				if (!line.compare("[GROUPS]"))
				{

					// Read Groups Count
					std::getline(fs, line);
					std::int32_t groupsCount = std::stoi(line);

					for (std::int32_t groupNumber = 0; groupNumber < groupsCount; groupNumber++)
					{
						ReadAndCreateConcreteGroup(fs, line);
					}
				}

			}
			catch (...)
			{
				WrongSettingsFileStructure();
			}
		}
	}

	fs.close();
}

void Settings::ReadAndCreateConcreteGroup(std::fstream& fs, std::string line)
{
	// Read GROUP Identificator
	std::getline(fs, line);
	if (!line.compare("[GROUP]"))
	{
		std::string concreteGroupName;
		std::getline(fs, line);
		concreteGroupName = line;

		// Create concrete group
		if (groups == NULL)
			groups = new Groups();
		groups->AddGroup(concreteGroupName);

		ReadAndWriteConcreteGroupSettingsInfo(fs, line, concreteGroupName);
		ReadAndWriteConcreteGroupAccountsInfo(fs, line, concreteGroupName);
	}
}

void Settings::ReadAndWriteConcreteGroupSettingsInfo(std::fstream& fs, std::string line, std::string groupName)
{
	// Read and add concrete group info
	std::getline(fs, line);
	if (!line.compare("[GROUP_SETTINGS]"))
	{
		// Read Settings Groups Count
		std::getline(fs, line);
		std::int32_t groupsSettingsCount = std::stoi(line);

		// Salary
		std::getline(fs, line);
		groups->AddGroupSalary(groupName, std::stoi(line));

		// Premium Coeficient
		std::getline(fs, line);
		groups->AddGroupPremiumCoeficient(groupName, std::stoi(line));
	}
}

void Settings::ReadAndWriteConcreteGroupAccountsInfo(std::fstream& fs, std::string line, std::string groupName)
{
	// Read and add concrete group accounts info
	std::getline(fs, line);
	if (!line.compare("[GROUP_ACCOUNTS]"))
	{
		// Read Accounts Group Count
		std::getline(fs, line);
		std::int32_t groupsAccountsCount = std::stoi(line);

		for (std::int32_t groupsAccountsNumber = 0; groupsAccountsNumber < groupsAccountsCount; groupsAccountsNumber++)
		{
			// Read Accounts Group Count
			std::getline(fs, line);
			if (!line.compare("[ACCOUNT]"))
			{
				std::vector<std::string> concreteAccountInfo;
				std::vector<std::string>::iterator it = concreteAccountInfo.begin();

				// Name
				std::getline(fs, line);
				concreteAccountInfo.insert(it, line);

				// Surname
				std::getline(fs, line);
				it = concreteAccountInfo.end();
				concreteAccountInfo.insert(it, line);

				groups->AddGroupAccountInfo(groupName, concreteAccountInfo);
			}
		}
	}
}

void Settings::WrongSettingsFileStructure()
{
	system("cls");
	std::cout << "Wrong settings file structure";
}

Groups* Settings::GetGroupsList()
{
	return groups;
}