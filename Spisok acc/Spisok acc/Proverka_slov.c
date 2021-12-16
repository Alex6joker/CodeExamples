#include <stdio.h>
#include <conio.h>

int proverka_slov(char name1[], char name2[])
{
	int i;
	i = 0;
	if(strlen(name1)!=strlen(name2)) return 0; // ≈сли длина слов не совпадает, то уже возвращаем ложь(0)
	while(name1[i]==name2[i] && i<strlen(name2)) // ѕока совпадают буквы и не конец слова считаем кол-во совпадений
		i++;
	if (i!=strlen(name2)) return 0; // ≈сли кол-во совпавших букв не соответсвует длине второго слова, то возвращаем ложь(0)
	else return 1; // ≈сли все проверки пройдены, то возвращаем истину(1)
}