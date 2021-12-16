#include <stdio.h>
#include <malloc.h>
#include "Create_struct_header.h"
#include "Sort_akk.h"
#include <limits.h>
#include <string.h>

void sort_diez_stupen()
{
	struct muz_akk* tmp;  // Отсортированная таблица
	struct muz_akk* tmp1; // Временный указатель на голову(выход из внешнего цикла)
	struct muz_akk* tmp2; // Временный указатель на голову(выход из внутреннего цикла)
	struct muz_akk* tmp3; // Сохраняем значение tmp->next
	struct muz_akk* tmp4; // Хранит начальное положение головы tmp
	int max,min;
	int k = 3; // Входной параметр для функций chtenie_s_file_diez/bemol
	int i,j,z;
	int sort_chislo; // Храним числа для сортировки
	char* sort_str;	// Храним строки для сортировки
	char c;
	printf("--------------------------------------------------------------------------------");
	printf("Выберите тип сортировки: По возврастанию(1) По убыванию(2)\n");
	printf("--------------------------------------------------------------------------------\n");
	do
	{
		c = getch();
		__asm
		{
			xor ecx,ecx
			mov cl,c
			sub ecx,0x30
			xor	eax,eax
			add eax,ecx
			mov i,eax
		}
	}while(i!=1 && i!=2);
	j = 0;
	max = INT_MIN;
	min = INT_MAX;
	tmp = chtenie_s_file_diez(k); // Результат функции - голова последней строки того, что можно было считать из файла
	if (tmp==error) 
	{
		printf("Сортировка таблицы невозможна\n");
		return;
	}
	tmp1=tmp;
	tmp2=tmp;
	tmp4=tmp;
	sort_str = (char*)malloc(50);
	if(i==1)
	{
		//.............Определяем минимальное значение в структуре и ставим его на первое место
		while(tmp1!=NULL)
		{
			while(tmp2!=NULL)
			{
				if(tmp2->stupen < min) 
				{
					min = tmp2->stupen;
					strcpy(sort_str,tmp2->name);
					strcpy(tmp2->name,tmp->name);
					strcpy(tmp->name,sort_str);
					strcpy(sort_str,tmp2->osn_ton);
					strcpy(tmp2->osn_ton,tmp->osn_ton);
					strcpy(tmp->osn_ton,sort_str);
					strcpy(sort_str,tmp2->tonal);
					strcpy(tmp2->tonal,tmp->tonal);
					strcpy(tmp->tonal,sort_str);
					strcpy(sort_str,tmp2->razr_name);
					strcpy(tmp2->razr_name,tmp->razr_name);
					strcpy(tmp->razr_name,sort_str);
					strcpy(sort_str,tmp2->gamma);
					strcpy(tmp2->gamma,tmp->gamma);
					strcpy(tmp->gamma,sort_str);
					sort_chislo = tmp2->frequency;
					tmp2->frequency = tmp->frequency;
					tmp->frequency = sort_chislo;
					sort_chislo = tmp2->stupen;
					tmp2->stupen = tmp->stupen;
					tmp->stupen = sort_chislo;
				}
				tmp2 = tmp2->next;
			}
			tmp1 = tmp1->next;
			tmp2 = tmp;
		}
		tmp = tmp->next; // Уходим с самого меньшего по величине элемента
		tmp3 = tmp; // Сохраняем значение tmp->next
		tmp1=tmp; // Возвращаем начальное положение указателей
		tmp2=tmp;
		//............Сортируем таблицу
		while(tmp1!=NULL)
		{
			while(tmp2!=NULL)
			{
				if(tmp2->stupen >= tmp->stupen)
				{
					max = tmp2->stupen;
					strcpy(sort_str,tmp2->name);
					strcpy(tmp2->name,tmp->name);
					strcpy(tmp->name,sort_str);
					strcpy(sort_str,tmp2->osn_ton);
					strcpy(tmp2->osn_ton,tmp->osn_ton);
					strcpy(tmp->osn_ton,sort_str);
					strcpy(sort_str,tmp2->tonal);
					strcpy(tmp2->tonal,tmp->tonal);
					strcpy(tmp->tonal,sort_str);
					strcpy(sort_str,tmp2->razr_name);
					strcpy(tmp2->razr_name,tmp->razr_name);
					strcpy(tmp->razr_name,sort_str);
					strcpy(sort_str,tmp2->gamma);
					strcpy(tmp2->gamma,tmp->gamma);
					strcpy(tmp->gamma,sort_str);
					sort_chislo = tmp2->frequency;
					tmp2->frequency = tmp->frequency;
					tmp->frequency = sort_chislo;
					sort_chislo = tmp2->stupen;
					tmp2->stupen = tmp->stupen;
					tmp->stupen = sort_chislo;
				}
				tmp2 = tmp2->next;
			}
			tmp = tmp->next; // Переходим к следующему элементу структуры
			tmp1 = tmp1->next;
			tmp2 = tmp3;
		}
	}
	if(i==2)
	{
		//.............Определяем максимальное значение в структуре и ставим его на первое место
		while(tmp1!=NULL)
		{
			while(tmp2!=NULL)
			{
				if(tmp2->stupen > max) 
				{
					max = tmp2->stupen;
					strcpy(sort_str,tmp2->name);
					strcpy(tmp2->name,tmp->name);
					strcpy(tmp->name,sort_str);
					strcpy(sort_str,tmp2->osn_ton);
					strcpy(tmp2->osn_ton,tmp->osn_ton);
					strcpy(tmp->osn_ton,sort_str);
					strcpy(sort_str,tmp2->tonal);
					strcpy(tmp2->tonal,tmp->tonal);
					strcpy(tmp->tonal,sort_str);
					strcpy(sort_str,tmp2->razr_name);
					strcpy(tmp2->razr_name,tmp->razr_name);
					strcpy(tmp->razr_name,sort_str);
					strcpy(sort_str,tmp2->gamma);
					strcpy(tmp2->gamma,tmp->gamma);
					strcpy(tmp->gamma,sort_str);
					sort_chislo = tmp2->frequency;
					tmp2->frequency = tmp->frequency;
					tmp->frequency = sort_chislo;
					sort_chislo = tmp2->stupen;
					tmp2->stupen = tmp->stupen;
					tmp->stupen = sort_chislo;
				}
				tmp2 = tmp2->next;
			}
			tmp1 = tmp1->next;
			tmp2 = tmp;
		}
		tmp = tmp->next; // Уходим с самого большого элемента
		tmp3 = tmp; // Сохраняем значение tmp->next
		tmp1=tmp; // Возвращаем начальное положение указателей
		tmp2=tmp;
		//............Сортируем таблицу
		while(tmp1!=NULL)
		{
			while(tmp2!=NULL)
			{
				if(tmp2->stupen < tmp->stupen)
				{
					max = tmp2->stupen;
					strcpy(sort_str,tmp2->name);
					strcpy(tmp2->name,tmp->name);
					strcpy(tmp->name,sort_str);
					strcpy(sort_str,tmp2->osn_ton);
					strcpy(tmp2->osn_ton,tmp->osn_ton);
					strcpy(tmp->osn_ton,sort_str);
					strcpy(sort_str,tmp2->tonal);
					strcpy(tmp2->tonal,tmp->tonal);
					strcpy(tmp->tonal,sort_str);
					strcpy(sort_str,tmp2->razr_name);
					strcpy(tmp2->razr_name,tmp->razr_name);
					strcpy(tmp->razr_name,sort_str);
					strcpy(sort_str,tmp2->gamma);
					strcpy(tmp2->gamma,tmp->gamma);
					strcpy(tmp->gamma,sort_str);
					sort_chislo = tmp2->frequency;
					tmp2->frequency = tmp->frequency;
					tmp->frequency = sort_chislo;
					sort_chislo = tmp2->stupen;
					tmp2->stupen = tmp->stupen;
					tmp->stupen = sort_chislo;
				}
				tmp2 = tmp2->next;
			}
			tmp = tmp->next; // Переходим к следующему элементу структуры
			tmp1 = tmp1->next;
			tmp2 = tmp3;
		}
	}
	free(sort_str); // Освобождаем память под строку
	tmp=tmp4;// Возвращаемся к самому первому элементу
	printf("--------------------------------------------------------------------------------");
	printf("Сортировка проведена успешно\nЖелаете ли вывести отсортированную структуру на экран? (1) ДА (0) НЕТ\n");
	printf("--------------------------------------------------------------------------------");
	do
	{
		c = getch();
		__asm
		{
			xor ecx,ecx
			mov cl,c
			sub ecx,0x30
			xor	eax,eax
			add eax,ecx
			mov z,eax
		}
	}while(z!=1 && z!=0);
	system("cls");
	if(z==1) // Если пользователь дал согласие на вывод
	{
		printf("--------------------------------------------------------------------------------");
		printf("|Аккорд\t|Тон\t|Тональность\t|Частота(ГЦ)\t|Разрешение\t|Гамма\t|Ступень");
		printf("--------------------------------------------------------------------------------");
		while(tmp!=NULL)
		{
			printf("%s\t%s\t%s\t\t%d\t\t%s\t\t%s\t%d\n",tmp->name,tmp->osn_ton,tmp->tonal,tmp->frequency,tmp->razr_name,tmp->gamma,tmp->stupen);
			tmp = tmp->next;
		}
		printf("--------------------------------------------------------------------------------");
		printf("\n");
	}
	printf("--------------------------------------------------------------------------------");
	printf("Требуется ли сохранить отсортированную таблицу? (1) ДА (0) НЕТ\n");
	printf("--------------------------------------------------------------------------------\n");
	do
	{
		c = getch();
		__asm
		{
			xor ecx,ecx
			mov cl,c
			sub ecx,0x30
			xor	eax,eax
			add eax,ecx
			mov z,eax
		}
	}while(z!=1 && z!=0);
	system("cls");
	if(z==1) // Если пользователь дал согласие на сохранение
	{
		printf("Возможно сохранение в новый файл (1) или с заменой старой структуры(0)\n");
		printf("--------------------------------------------------------------------------------");
		do
		{
			c = getch();
			__asm
			{
				xor ecx,ecx
				mov cl,c
				sub ecx,0x30
				xor	eax,eax
				add eax,ecx
				mov z,eax
			}
		}while(z!=1 && z!=0);
		system("cls");
		save_sort_diez(z,tmp4);
	}
}