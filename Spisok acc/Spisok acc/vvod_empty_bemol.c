#include <stdio.h>
#include <conio.h>
#include <windows.h>
#include <string.h>

void vvod_empty_bemol()
{
	FILE* file;
	char *buffer;
	int chislo;
	int z; // Слежка за scanf
	char c;
	buffer = (char*)malloc(1024); // Выделяем память под проверку символов
	file = fopen("Spisok bemol.txt","w");
	do
	{
	system("cls");
	printf("--------------------------------------------------------------------------------");
	printf("Ведется запись в файл Spisok bemol.txt\n");
	printf("--------------------------------------------------------------------------------");
	printf("Данные вводятся без пробелов\n");
	printf("--------------------------------------------------------------------------------");
	printf("Введите название аккорда: ");
	fflush(stdin);
	gets(buffer);
	while((strlen(buffer)>6) || (prov_probel(buffer)==0)) // Строка больше максимальной длины или строка введена с пробелом
	{
		printf("Слишком длинное название аккорда, максимальная длина - 6 или строка содежит пробелы\nВведите название аккорда: ");
		fflush(stdin);
		gets(buffer);
	}
	fprintf(file,"%s ", buffer);
	printf("--------------------------------------------------------------------------------");
	printf("Введите основной тон аккорда: ");
	fflush(stdin);
	gets(buffer);
	while((strlen(buffer)>2) || (prov_probel(buffer)==0))
	{
		printf("Введите основной тон заново, он состоит максимум из двух букв\nВведите основной тон аккорда: ");
		fflush(stdin);
		gets(buffer);
	}
	fprintf(file,"%s ", buffer);
	printf("--------------------------------------------------------------------------------");
	printf("Введите тональность аккорда: ");
	fflush(stdin);
	gets(buffer);
	while((strlen(buffer)>6) || (prov_probel(buffer)==0))
	{
		printf("Слишком длинное название тональности, максимальная длина тональности аккорда - 6\nВведите тональность аккорда: ");
		fflush(stdin);
		gets(buffer);
	}
	fprintf(file,"%s ", buffer);
	printf("--------------------------------------------------------------------------------");
	printf("Введите частоту аккорда(ГЦ): ");
	z = scanf("%5d",&chislo);
	fflush(stdin);
	while((chislo>1365) || (z==0)|| (chislo<=0)) // Если слишком большая чистота или введено не число
	{
		printf("Частота выше 1365 Гц или введено не число\nВведите частоту аккорда(ГЦ): ");
		fflush(stdin);
		z = scanf("%5d",&chislo);
	}
	fprintf(file,"%d ", chislo);
	printf("--------------------------------------------------------------------------------");
	printf("Введите разрешение аккорда(NR, если не требуется): ");
	fflush(stdin);
	gets(buffer);
	while((strlen(buffer)>6) || (prov_probel(buffer)==0))
	{
		printf("Слишком длинное название разрешения, максимальная длина разрешения аккорда - 6\nВведите разрешение аккорда(NR, если не требуется): ");
		fflush(stdin);
		gets(buffer);
	}
	fprintf(file,"%s ", buffer);
	printf("--------------------------------------------------------------------------------");
	printf("Введите гамму аккорда: ");
	fflush(stdin);
	gets(buffer);
	while((strlen(buffer)>4) || (prov_probel(buffer)==0))
	{
		printf("Слишком длинное название гаммы, максимальная длина гаммы - 4\nВведите гамму аккорда: ");
		fflush(stdin);
		gets(buffer);
	}
	fprintf(file,"%s ", buffer);
	printf("--------------------------------------------------------------------------------");
	printf("Введите ступень аккорда в тональности: ");
	z = scanf("%2d",&chislo);
	fflush(stdin);
	while((chislo>7) || (z==0)|| (chislo<=0))
	{
		printf("Аккорд не может стоять на ступени выше седьмой или введено не число\nВведите ступень аккорда в тональности: ");
		fflush(stdin);
		z = scanf("%2d",&chislo);
	}
	fprintf(file,"%d\n", chislo);
	printf("--------------------------------------------------------------------------------");
	printf("Ввести еще один аккорд (Любая клавиша) или прекратить запись(*)? ");
	c = getch();
	system("cls");
	} while(c!='*');
	free(buffer);
	fclose(file);
}