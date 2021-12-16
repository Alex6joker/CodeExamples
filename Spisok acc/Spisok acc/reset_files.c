#include <stdio.h>
#include <conio.h>

void reset_file()
{
	FILE* file;
	FILE* reset_file;
	char* buffer;
	int i,j;
	i = 1;
	j = 1;
	buffer = (char*)malloc(60);
	reset_file = fopen("Initial_Spisok #.txt","r");
	if(!reset_file)
	{
		printf("Ошибка открытия исходного файла\n");
		printf("Восстановление данных для Spisok #.txt невозможно\n");
		system("pause");
		i = 0;
	}
	if(i==1)
	{
		file = fopen("Spisok #.txt","w");
		while(!feof(reset_file))
		{
			if(fgets(buffer,sizeof(buffer),reset_file)==NULL) break;
			fputs(buffer,file);
		}
		fclose(reset_file);
		fclose(file);
		printf("Восстановление данных для Spisok #.txt прошло успешно\n");
	}
	reset_file = fopen("Initial_Spisok bemol.txt","r");
	if(!reset_file)
	{
		printf("Ошибка открытия исходного файла\n");
		printf("Восстановление данных для Spisok bemol.txt невозможно\n");
		system("pause");
		system("cls");
		j = 0;
	}
	if(j==1)
	{
		file = fopen("Spisok bemol.txt","w");
		while(!feof(reset_file))
		{
			if(fgets(buffer,sizeof(buffer),reset_file)==NULL) break;
			fputs(buffer,file);
		}
		fclose(reset_file);
		fclose(file);
		printf("Восстановление данных для Spisok bemol.txt прошло успешно\n");
		system("pause");
	}
	system("cls");
}