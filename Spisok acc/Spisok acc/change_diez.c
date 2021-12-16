#include <stdio.h>
#include <conio.h>
#include "Create_struct_header.h"

void change_diez()
{
	FILE* akk;
	FILE* rezerv;
	struct muz_akk z;
	char* buffer;
	char change_name[255];
	int chislo;
	int i; // Флаг поиска
	int j; // Слежка за fscanf
	int z1; // Слежка за scanf
	buffer=(char*)malloc(1024);
	printf("--------------------------------------------------------------------------------");
	printf("\tПо природе своей каждый аккорд имеет из уникального только название\n");
	printf("--------------------------------------------------------------------------------");
	printf("\nВведите название аккорда, данные о котором хотите изменить: ");
	fflush(stdin);
	gets(change_name);
	while((strlen(change_name)>6) || (prov_probel(change_name)==0)) // Строка больше максимальной длины или строка введена с пробелом
	{
		printf("Слишком длинное название аккорда, максимальная длина - 6 или строка содежит пробелы\nПовторно введите название аккорда, данные о котором хотите изменить: : ");
		fflush(stdin);
		gets(change_name);
	}
	fflush(stdin);
	i = 0; 
	akk = fopen("Spisok #.txt","r");
	if(!akk)
	{
		printf("Ошибка в чтении файла Spisok #.txt\n");
		return 0;
	}
	rezerv = fopen("tempfile","w");
	while(!feof(akk))
	{
		j = fscanf(akk,"%s%s%s%d%s%s%d", z.name,z.osn_ton,z.tonal,&(z.frequency),z.razr_name,z.gamma,&(z.stupen));
		if (j!=7) break; // Пока есть правильно заполненные данные в файле
		if ((proverka_slov(z.name,change_name)==1) && (i==0))  // Если название совпадает (В музыке у каждого аккорда свое уникальное название - единственный критерий поиска)
		{
			i++;
			fclose(rezerv);
			rezerv = fopen("tempfile","a");
			system("cls");
			printf("--------------------------------------------------------------------------------");
			printf("\t\t   Аккорд с таким названием найден!\n");
			printf("--------------------------------------------------------------------------------");
			printf("Данные вводятся без пробелов\n");
			printf("--------------------------------------------------------------------------------");
			printf("Введите название нового аккорда: ");
			fflush(stdin);
			gets(buffer);
			while((strlen(buffer)>6) || (prov_probel(buffer)==0)) // Строка больше максимальной длины или строка введена с пробелом
			{
				printf("Слишком длинное название аккорда, максимальная длина - 6 или строка содежит пробелы\nВведите название нового аккорда: ");
				fflush(stdin);
				gets(buffer);
			}
			fprintf(rezerv,"%s ", buffer);
			printf("\n--------------------------------------------------------------------------------");
			printf("Введите основной тон нового аккорда: ");
			fflush(stdin);
			gets(buffer);
			while((strlen(buffer)>2) || (prov_probel(buffer)==0))
			{
				printf("Введите основной тон заново, он состоит максимум из двух букв\nВведите основной тон нового аккорда: ");
				fflush(stdin);
				gets(buffer);
			}
			fprintf(rezerv,"%s ", buffer);
			printf("\n--------------------------------------------------------------------------------");
			printf("Введите тональность нового аккорда: ");
			fflush(stdin);
			gets(buffer);
			while((strlen(buffer)>6) || (prov_probel(buffer)==0))
			{
				printf("Слишком длинное название тональности, максимальная длина тональности аккорда - 6\nВведите тональность нового аккорда: ");
				fflush(stdin);
				gets(buffer);
			}
			fprintf(rezerv,"%s ", buffer);
			printf("\n--------------------------------------------------------------------------------");
			printf("Введите частоту аккорда(ГЦ): ");
			z1 = scanf("%5d",&chislo);
			fflush(stdin);
			while((chislo>1365) || (z1==0)|| (chislo<=0)) // Если слишком большая чистота или введено не число
			{
				printf("Частота выше 1365 Гц или введено не число\nВведите частоту аккорда(ГЦ): ");
				fflush(stdin);
				z1 = scanf("%5d",&chislo);
			}
			fprintf(rezerv,"%d ", chislo);
			printf("\n--------------------------------------------------------------------------------");
			printf("Введите разрешение нового аккорда(NR, если не требуется): ");
			fflush(stdin);
			gets(buffer);
			while((strlen(buffer)>6) || (prov_probel(buffer)==0))
			{
				printf("Слишком длинное название разрешения, максимальная длина разрешения аккорда - 6\nВведите разрешение нового аккорда(NR, если не требуется): ");
				fflush(stdin);
				gets(buffer);
			}
			fprintf(rezerv,"%s ", buffer);
			printf("\n--------------------------------------------------------------------------------");
			printf("Введите гамму нового аккорда: ");
			fflush(stdin);
			gets(buffer);
			while((strlen(buffer)>4) || (prov_probel(buffer)==0))
			{
				printf("Слишком длинное название гаммы, максимальная длина гаммы - 4\nВведите гамму нового аккорда: ");
				fflush(stdin);
				gets(buffer);
			}
			fprintf(rezerv,"%s ", buffer);
			printf("\n--------------------------------------------------------------------------------");
			printf("Введите ступень нового аккорда в тональности: ");
			z1 = scanf("%2d",&chislo);
			fflush(stdin);
			while((chislo>7) || (z1==0)|| (chislo<=0))
			{
				printf("Аккорд не может стоять на ступени выше седьмой или введено не число\nВведите ступень нового аккорда в тональности: ");
				fflush(stdin);
				z1 = scanf("%2d",&chislo);
			}
			fprintf(rezerv,"%d\n", chislo);
			break;
		}
		fprintf(rezerv,"%s %s %s %d %s %s %d\n", z.name,z.osn_ton,z.tonal,z.frequency,z.razr_name,z.gamma,z.stupen); // Записываем остальные данные во временный файл
	}
	if (i==1){
		while(!feof(akk))
		{
			j = fscanf(akk,"%s%s%s%d%s%s%d", z.name,z.osn_ton,z.tonal,&(z.frequency),z.razr_name,z.gamma,&(z.stupen));
			if (j!=7) break;
			fprintf(rezerv,"%s %s %s %d %s %s %d\n", z.name,z.osn_ton,z.tonal,z.frequency,z.razr_name,z.gamma,z.stupen);
		}
	}
	fclose(akk);
	fclose(rezerv);
	if (i==0)
	{
		system("cls");
		printf("--------------------------------------------------------------------------------");
		printf("\t\t   К сожалению, аккорд таким именем не найден\n");
		printf("--------------------------------------------------------------------------------");
		system("pause");
		system("cls");
		return;
	}
	free(buffer);
	akk = fopen("Spisok #.txt","w");
	rezerv = fopen("tempfile","r");
	buffer = (char*)malloc(30);
	while(!feof(rezerv))				// Запись из временного файла в основной
	{
		if (fgets(buffer,sizeof(buffer),rezerv)==NULL) break;
		fputs(buffer,akk);	
	}
	free(buffer);
	fclose(akk);
	fclose(rezerv);
	remove ("tempfile");
	system("cls");
	printf("--------------------------------------------------------------------------------");
	printf("\t\tЗапись изменений успешно проведена\n");
	printf("--------------------------------------------------------------------------------");
	system("pause");
	system("cls");
}