#include <stdio.h>
#include <conio.h>
#include <string.h>

int del_verx_diez(int k)
{
	int i;
	FILE* akk;
	FILE* rezerv;
	char str[50];
	char* buffer;
	i = 0; // Счетчик строк
	akk = fopen("Spisok #.txt","r");
	fseek(akk,0,SEEK_END);
	if(!ftell(akk))
	{
		printf("Ошибка в чтении файла Spisok #.txt\n");
		return 1;
	}
	fseek(akk,0,SEEK_SET);
	while(!feof(akk))
	{
		buffer = fgets(str,sizeof(str),akk);
		if (buffer==NULL) break;
		i++;
	}
	fclose(akk);
	if (k>=i) //Проверка на соотвествие числа удаляемых строк и числа строк реально существующих
	{
		printf("Введено некорректное кол-во удаляемых строк\n");
		printf("По нажатию любой клавиши будет произведен выход из функции\n");
		getch();
		return 1;
	}
	akk = fopen("Spisok #.txt","r");
	if(!akk)
	{
		printf("Ошибка в чтении файла Spisok #.txt");
		return 0;
	}
	rezerv = fopen("tempfile","w");
	while(!feof(akk))					// Запись нужных строк во временный файл
	{
		buffer = fgets(str,sizeof(str),akk);
		if (k!=0)
		{
			k--;
			continue;
		}
		if (buffer==NULL) break;
		fputs(buffer,rezerv);
	}
	fclose(akk);
	fclose(rezerv);
	akk = fopen("Spisok #.txt","w");
	rezerv = fopen("tempfile","r");
	while(!feof(rezerv))				// Запись из временного файла
	{
		buffer = fgets(str,sizeof(str),rezerv);
		if (buffer==NULL) break;
		fputs(buffer,akk);
	}
	fclose(rezerv);
	remove("tempfile");
	fclose(akk);
	return 0;
}