//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit1.h"
#include "Unit2.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TForm1 *Form1;
bool check_the_word_in_sub_alphabet(char,char*,int);
//---------------------------------------------------------------------------
__fastcall TForm1::TForm1(TComponent* Owner)
	: TForm(Owner)
{

}
//---------------------------------------------------------------------------
void __fastcall TForm1::FormCreate(TObject *Sender)
{
	Form1->polygramm = 1; // Разрядность k-граммы при запуске программы
	Form1->click_counter = 0; // Разрядность k-граммы при запуске программы
	Form1->count = 0; // кол-во k-грамм при запуске программы
	Form1->current_decode_percent = 0;
	Form1->change_group[0] = 0; // Изначально заменяеме между собой символы (2 самые часто встречаемые буквы)
	Form1->change_group[1] = 1;
	Form1->right_words = 0;
	// Производим загрузку настроек
	Application->CreateForm(__classid(TForm2), &Form2); // Вызываем функцию загрузки настроек
	Form2->Free();
	FILE* f0;
	if(lowercase_letters_of_the_current_language[0] == 'а') f0 = fopen("dictionary_rus.txt","r");
	else if(lowercase_letters_of_the_current_language[0] == 'a') f0 = fopen("dictionary_eng.txt","r");
	char c;
	fscanf(f0,"%d",&Form1->dictionary_count);
	dictionary_count++;
	dictionary_words = new AnsiString[Form1->dictionary_count];
	fscanf(f0,"%c",&c);
	for(int i = 0; i < Form1->dictionary_count; i++)
	{
		char str[30];
		fgets(str,sizeof(str),f0);
		dictionary_words[i] = str;
        dictionary_words[i].Delete(dictionary_words[i].Length(),1);
	}
	fclose(f0);
}
//---------------------------------------------------------------------------
void __fastcall TForm1::N2Click(TObject *Sender)
{
	if(!OpenDialog1->Execute()) return;
	else
	{
		Memo1->Lines->LoadFromFile(OpenDialog1->FileName);
		Button1->Enabled = true;
	}
}
//---------------------------------------------------------------------------
void __fastcall TForm1::N3Click(TObject *Sender)
{
	if(!SaveDialog1->Execute()) return;
	else Memo1->Lines->SaveToFile(SaveDialog1->FileName);
}
//---------------------------------------------------------------------------
void __fastcall TForm1::N4Click(TObject *Sender)
{
	Application->CreateForm(__classid(TForm2), &Form2);
	Form2->ShowModal();
	Form2->Free();
}
//---------------------------------------------------------------------------

void __fastcall TForm1::Button1Click(TObject *Sender)
{
	if(!Form1->click_counter)
	{
		// Считываем из файла k-граммы в порядке убывания частоты
		FILE* f0;
		if(lowercase_letters_of_the_current_language[0] == 'а') f0 = fopen("rus_frequency.txt","r");
		else if(lowercase_letters_of_the_current_language[0] == 'a') f0 = fopen("eng_frequency.txt","r");
		char c;
		// Пропускаем первые две строки
		do fscanf(f0,"%c",&c); while(c!='\n');
		do fscanf(f0,"%c",&c); while(c!='\n');
		// Теперь count - количество k-грамм
		fscanf(f0,"%d",&Form1->count); // Считываем количество k-грамм в файле частот
		// Выделяем память под k-граммы
		k_gramms = new char*[Form1->count]; // Выделяем место под кол-во k-грамм
		for(int i = 0; i < Form1->count; i++)
			k_gramms[i] = new char[Form1->polygramm];
		do fscanf(f0,"%c",&c); while(c!='\n'); // Выходим непостредственно к k-граммам
		for(int i = 0; i < Form1->count; i++) // Считываем все k-граммы
		{
			for(int j = 0; j < Form1->polygramm; j++)
			{   // Читаем саму k-грамму (сочетание букв)
				fscanf(f0,"%c",&k_gramms[i][j]);
			}
			// Выходим непостредственно к следующей k-грамме
			do fscanf(f0,"%c",&c); while(c!='\n' && !feof(f0));
		}
		fclose(f0);
		// Выделяем память под нужное количество анализируемых k-грамм
		text_k_gramms = new frequency_of_k_gramms_in_text[Form1->count];
		Form1->click_counter++;
	}
	// Расшифровка исходного закодированного текста до тех пор, пока не будет достигнут нужный процент
	clock_t time = clock();
	int current_percent_is_not_changed = 0; // Счетчик того, сколько раз не изменялся текущий процент расшифровки
	for(int iteration_counter = 0; Form1->current_decode_percent <= Form1->final_percent; iteration_counter++)
	{
		if(iteration_counter) // Если уже был получен первичный открытый текст, то проводим замену фиксированных символов
		{
			char* temp_str = new char[Form1->polygramm]; // Будем хранить в переменной старый символ
			for(int i = 0; i < Form1->polygramm; i++)
				temp_str[i] = k_gramms[Form1->change_group[0]][i];
			strcpy(k_gramms[Form1->change_group[0]],k_gramms[Form1->change_group[1]]);
			strcpy(k_gramms[Form1->change_group[1]],temp_str);
			delete [] temp_str;
		}
		// Загружаем в строковую переменную текст из поля Memo1
		char* text = ((AnsiString)Memo1->Lines->Text).c_str();
		// Считываем из текста соответствующие k-граммы
		// Сохраняем адрес начала текста
		char* text_begin = text;
		int text_k_gramms_count = 0; // Кол-во уже внесенных в список k-грамм из текста
		char* analyzing_k_gramm = new char[Form1->polygramm]; // Строка для чтения k_gramm из исходного текста
		while(text[0]!='\0')
		{
			strncpy(analyzing_k_gramm,text,Form1->polygramm); // Копируем в строку анализируемый k_gramm
			// Переводим все заглавные буквы в строчные согласно таблице ANSII
			for(int i = 0; i < Form1->polygramm; i++) // До конца размера k-граммы
				if(analyzing_k_gramm[i] >= upcase_letters_of_the_current_language[0] && analyzing_k_gramm[i] <= upcase_letters_of_the_current_language[1])
//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!только для русского текста!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
					analyzing_k_gramm[i] += 32;
			// Определяем, являются ли все буквы k-граммы буквами русского\английского алфавита
			// Если буквы соотвествуют рассматриваемому языку, записываем их в таблицу
			if(match_the_language(analyzing_k_gramm,Form1->polygramm))
				text_k_gramms_count = Find_and_write_k_gramm(analyzing_k_gramm,text_k_gramms,k_gramms,Form1->count,text_k_gramms_count);
			text++;
		}
		// Сортировка полученной таблицы по убыванию частоты
		for(int i = 0; i < text_k_gramms_count; i++)
			for(int j = 0; j < text_k_gramms_count; j++)
				if(text_k_gramms[i].frequency > text_k_gramms[j].frequency)
					{
						AnsiString old_name = text_k_gramms[j].k_gramm;
						int old_frequency = text_k_gramms[j].frequency;
						text_k_gramms[j].frequency = text_k_gramms[i].frequency;
						text_k_gramms[i].frequency = old_frequency;
						text_k_gramms[j].k_gramm = text_k_gramms[i].k_gramm;
						text_k_gramms[i].k_gramm = old_name;
					}
		// Замена k-грамм в тексте на k-граммы частотной статистики языка
		// Восстанавливаем исходный текст
		text = text_begin;
		while(text[0]!='\0')
		{
			// Считываем из текста k-грамму
			strncpy(analyzing_k_gramm,text,Form1->polygramm); // Копируем в строку анализируемый k_gramm
			for(int i = 0; i < Form1->polygramm; i++) // До конца размера k-граммы
				if (analyzing_k_gramm[i] >= upcase_letters_of_the_current_language[0] && analyzing_k_gramm[i] <= upcase_letters_of_the_current_language[1])
//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!только для русского текста!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
					analyzing_k_gramm[i] += 32;
			// Проверка, есть ли считанная k-грамма в отсортированной структуре данных (необходима ли этой k-грамме замена)
			for(int i = 0; i < Form1->count; i++)
			{
				if(!strcmp(analyzing_k_gramm,text_k_gramms[i].k_gramm.c_str()))
				{   // Заменяем k-грамму в тексте
					for(int j = 0; j < Form1->polygramm; j++)
						text[j] = k_gramms[i][j];
					break;
				}
			}
			text++;
		}
		// Восстанавливаем исходный текст
		AnsiString str;
		str.Insert(text_begin,1);
		AnsiString str2;
		str2.Insert(str,1);
		// Новый процент расшифровки текста с переставленным ключом
		float new_decode_percent = get_decode_percent(str.c_str());
		// Если же процент расшифровки увеличился, значит результат улучшился, сохраняем перестановку букв в частотном анализе
		if(new_decode_percent > Form1->current_decode_percent)
		{
			Form1->current_decode_percent = new_decode_percent;
			current_percent_is_not_changed = 0;
			if(iteration_counter)
				Memo1->Lines->Text = str2;
		}
		else // Иначе текущую перестановку не применяем
		{
			current_percent_is_not_changed++;
			char* temp_str = new char[Form1->polygramm]; // Будем хранить в переменной старый символ
			for(int i = 0; i < Form1->polygramm; i++)
			temp_str[i] = k_gramms[Form1->change_group[0]][i];
			strcpy(k_gramms[Form1->change_group[0]],k_gramms[Form1->change_group[1]]);
			strcpy(k_gramms[Form1->change_group[1]],temp_str);
			delete [] temp_str;
		}
		if(iteration_counter)
		{
			Form1->change_group[0]++;
			Form1->change_group[1]++;
		}
		Memo2->Lines->Text = str2;
		if(!iteration_counter) // Копируем первичный открытый текст
			Memo1->Lines->Text = Memo2->Lines->Text;
		if((iteration_counter+1)%Form1->count == 0)
		{
			Form1->change_group[0] = 0;
			Form1->change_group[1] = 1;
			iteration_counter++;
		}
		Form1->right_words = 0;
		if(current_percent_is_not_changed == Form1->count) break;
		// Очистка памяти
		delete [] analyzing_k_gramm;
		//delete [] text_k_gramms;
		//for(int i = 0; i < count; i++)
		//	delete [] k_gramms[i];
		//delete [] k_gramms;
	}
	time = clock() - time;
	if(current_percent_is_not_changed == 0) Memo3->Lines->Text = "Требуемая точность достигнута\n";
	else Memo3->Lines->Text = "Требуемая точность не была достигнута\n";
	Memo3->Lines->Text = Memo3->Lines->Text + "Процент букв, совпавших со словарем: ";
	Memo3->Lines->Text = Memo3->Lines->Text + current_decode_percent + "%";
	Memo3->Lines->Text = Memo3->Lines->Text + "\nВремя выполнения: " + (double)time/CLOCKS_PER_SEC+ "сек.";
	FILE* f0 = fopen("NewKey.txt","w");
	for(int i = 0; i < Form1->count; i++)
		for(int j = 0; j < Form1->polygramm; j++)
			fprintf(f0,"%c",k_gramms[i][j]);
	fclose(f0);
}
//---------------------------------------------------------------------------
// Функция поиска k-граммы в таблице k-грамм и записи в новую либо уже имеющуюся ячейку
int TForm1::Find_and_write_k_gramm(char* analyzing_k_gramm, frequency_of_k_gramms_in_text* text_k_gramms,char** k_gramms,int count,int text_k_gramms_count)
{   // Проверяем, есть ли данная k-грамма в списке анализируемых k-грамм
	bool in_k_gramms = false;
	for(int i = 0; i < count; i++)
		// Если есть равенство, значит можно проверять дальше
		if(!strcmp(analyzing_k_gramm,k_gramms[i]))
		{
			in_k_gramms = true;
			break;
		}
	// Иначе смысла дальнейшей проверки нет, уходим из функции
	if(!in_k_gramms) return text_k_gramms_count;
	// Проверяем, есть ли данная k-грамма в таблице с уже подсчитанными частотами k-грамм
	bool in_text_k_gramms = false;
	int i; // Должны запомнить последнюю ячейку, если k-грамма новая (для записи)
	for(i = 0; i < text_k_gramms_count; i++) // Проверяем, до пустой ячейки
		if(!strcmp(analyzing_k_gramm,text_k_gramms[i].k_gramm.c_str())) // Если есть совпадение, то прибавляем счетчик на 1
		{
			text_k_gramms[i].frequency++;
			in_text_k_gramms = true;
			break;
		}
	if(!in_text_k_gramms) // Если элемента в таблице не было, то записываем в последнюю свободную ячейку
	{
		text_k_gramms[i].k_gramm = analyzing_k_gramm;
		text_k_gramms[i].frequency = 1;
        text_k_gramms_count++; // Увеличиваем счетчик записанных в таблицу k-грамм из текста
	}
	return text_k_gramms_count;
}
float TForm1::get_decode_percent(char* text) // 2-й параметр для проверки
{
	// Считываем из текста слова (лексемы) до тех пор, пока это возможно
	char* separator = " \.-,\r\n\t:;";
	text = strtok(text,separator);
	int max_word_equality = 0; // Максимальное число совпадений
	float percent = 0; // Конечный предполагаемый процент расшифровки
	int i; // Счетчик цикла (общее число лексем)
	for(i = 0; text; i++) // пока есть следующая лексема
	{   // Определяем, являются ли все буквы лексемы буквами русского\английского алфавита
		if(!match_the_language(text,strlen(text)))
		{
            i--;
			text =  strtok(0,separator);
			continue;
		}
		for(int j = 0; j < Form1->dictionary_count; j++) // Проходим по всем словам в словаре
		{
			// Разное число букв - берем из словаря следующее слово, иначе считаем число совпадений
			if(strlen(text) != dictionary_words[j].Length()) continue;
			else  // Считаем число совпадений
			{
				int word_equality = 0;
				for(int k = 0; k < strlen(text); k++)
					if(text[k] == dictionary_words[j][k+1])
						{
							word_equality++;
						}
				if(word_equality > max_word_equality) max_word_equality = word_equality;
			}
		}
		// Максимальное число совпадений делим на длину лексемы, это будет доля истинных букв в расшифровывающемся тексте
		percent += (float)max_word_equality/strlen(text);
		text =  strtok(0,separator);
        max_word_equality = 0;
	}
	percent *= 100;
	percent /= i;
	return percent;
}
bool TForm1::match_the_language(char* analyzing_k_gramm, int count) // Определяем, являются ли все буквы k-граммы буквами русского\английского алфавита
{
	for(int i = 0; i < count; i++) // До конца размера k-граммы
	if(!(analyzing_k_gramm[i] >= lowercase_letters_of_the_current_language[0] && analyzing_k_gramm[i] <= lowercase_letters_of_the_current_language[1]))
	{
		return false;
	}
	return true;
}
bool check_the_word_in_sub_alphabet(char word,char* sub_alphabet, int num)
{   // Проверяем, есть ли уже эта буква в подалфавите
	for(int i = 0; i < num; i++)
		if(sub_alphabet[i] == word) return true;
	return false;
}
void __fastcall TForm1::N7Click(TObject *Sender)
{
	clock_t time = clock();
	// Попытка доведения результата до 100%
	// Формируем подалфавит, состоящий из букв, которые не совпадают со словами из словаря
	// Загружаем в строковую переменную текст из поля Memo1
	char* text = ((AnsiString)Memo1->Lines->Text).c_str();
	// Считываем из текста слова (лексемы) до тех пор, пока это возможно
	char* separator = " \.-,\r\n\t:;";
	char* sub_alphabet = new char[Form1->count]; // Подалфавит, который состоит из букв, не совпавших с буквами из словаря
	char* temp_sub_alphabet = new char[Form1->count]; // Подалфавит, который состоит из букв, не совпавших с буквами из словаря
	int number_of_symbols_in_sub_alphabet = 0;
	text = strtok(text,separator);
	int min_word_nonequality = pow(2,8); // Минимальное число совпадений
	for(int i = 0; text; i++) // пока есть следующая лексема
	{   // Определяем, являются ли все буквы лексемы буквами русского\английского алфавита
		int n = 0; // Число букв в временном подалфавите
		if(!match_the_language(text,strlen(text)))
		{
            i--;
			text =  strtok(0,separator);
			continue;
		}
		for(int j = 0; j < Form1->dictionary_count; j++) // Проходим по всем словам в словаре
		{
			// Разное число букв - берем из словаря следующее слово, иначе считаем число совпадений
			if(strlen(text) != strlen(dictionary_words[j].c_str())) continue;
			else  // Считаем число несовпадений
			{
				if(strcmp(text,dictionary_words[j].c_str()))
				{
					int word_nonequality = 0; // Число несоответствий с текщим словом из словаря
					for(int k = 0; k < strlen(text); k++)
						if(text[k] != dictionary_words[j][k+1])
							word_nonequality++;
					// Если данное число несовпадений наименьшее, то записываем новый минимум, и все буквы несовпавшего слова
					if(word_nonequality <= min_word_nonequality)
					{
						min_word_nonequality = word_nonequality;
						// Теперь временный подалфавит содержит все несовпавшие буквы
						for(int k = 0; k < strlen(text); k++)
							if(text[k] != dictionary_words[j][k+1])
							{
									temp_sub_alphabet[n] = text[k];
									n++;
									temp_sub_alphabet[n] = '\0';
							}
					}
				}
				else
				{
					n = 0;
					temp_sub_alphabet[n] = '\0';
					min_word_nonequality = 0;
					break;
				}
			}
			n = 0;
		}
		// С получившегося массива с минимальными несовпадениями переносим все в результирующий подалфавит
		for(int i = 0; i < min_word_nonequality; i++)
			if(!check_the_word_in_sub_alphabet(temp_sub_alphabet[i],sub_alphabet,number_of_symbols_in_sub_alphabet))
			{
				sub_alphabet[number_of_symbols_in_sub_alphabet] = temp_sub_alphabet[i];
				number_of_symbols_in_sub_alphabet++;
            }
		// Максимальное число совпадений делим на длину лексемы, это будет доля истинных букв в расшифровывающемся тексте
		text =  strtok(0,separator);
		min_word_nonequality = pow(2,8);
		// Как правило, первые четыре буквы являются теми, что осталось переставить
		if(number_of_symbols_in_sub_alphabet >= 4) break;
	}
	// Производим перестановку символов с полным перебором подалфавита
	bool get_better_percent = false;
	for(int i = 0; i < number_of_symbols_in_sub_alphabet && (!get_better_percent); i++)
	{
		for(int j = 0; j < number_of_symbols_in_sub_alphabet; j++)
			if(i!=j)
			{
				// Загружаем в строковую переменную текст из поля Memo1
				char* text = ((AnsiString)Memo1->Lines->Text).c_str();
				// Сохраняем адрес начала текста
				char* text_begin = text;
				for(int k = 0; text[0]!='\0'; k++)
				{
					// Заменяем k-грамму в тексте
					if(text[k] == sub_alphabet[i])
						text[k] = sub_alphabet[j];
					else if (text[k] == sub_alphabet[j])
						text[k] = sub_alphabet[i];
					text++;
				}
				// Восстанавливаем исходный текст
				AnsiString str = text_begin;
				// Новый процент расшифровки текста с переставленным ключом
				float new_decode_percent = get_decode_percent(text_begin);
				// Если же процент расшифровки увеличился, значит результат улучшился, сохраняем перестановку букв в частотном анализе
				if(new_decode_percent > Form1->current_decode_percent)
				{
					Form1->current_decode_percent = new_decode_percent;
					Memo1->Lines->Text = str;
					get_better_percent = true;
					break;
				}
				Memo2->Lines->Text = str;
			}
	}
	time = clock() - time;
	if(get_better_percent) Memo3->Lines->Text = "Точность была улучшена\n";
	else Memo3->Lines->Text = "Не удалось улучшить результат\n";
	Memo3->Lines->Text = Memo3->Lines->Text + "Процент букв, совпавших со словарем: ";
	Memo3->Lines->Text = Memo3->Lines->Text + current_decode_percent + "%";
	Memo3->Lines->Text = Memo3->Lines->Text + "\nВремя выполнения: " + (double)time/CLOCKS_PER_SEC+ "сек.";
}
//---------------------------------------------------------------------------


void __fastcall TForm1::N21Click(TObject *Sender)
{
	// Попытка доведения результата до 100% способ №2
	// Формируем подалфавит, состоящий из букв, которые не совпадают со словами из словаря
	// Загружаем в строковую переменную текст из поля Memo1
	clock_t time = clock();
	char* text = ((AnsiString)Memo1->Lines->Text).c_str();
	char* nonequal_words = new char[40];
	char* nonequal_dictionary_words = new char[40];
	int words_counter = 0;
	// Считываем из текста слова (лексемы) до тех пор, пока это возможно
	char* separator = " \.-,\r\n\t:;";
	AnsiString text_begin = ((AnsiString)Memo1->Lines->Text).c_str();
	int lexemes = 1;
	text =  strtok(text,separator);
	while(text)
	{
		lexemes++;
		text = strtok(0,separator);
	}
	randomize(); // Генерируем псевдослучайную последовательность
	bool pozitive_lexeme = false; // Лексема пригодна для замены букв (соответствует языку и в ней есть ошибки)
	int get_lexeme = rand()%lexemes; // Берем случайную лексему
	text = ((AnsiString)Memo1->Lines->Text).c_str(); // Восстанавливаем оригинальный текст
	int current_lexeme = 0;
	text =  strtok(text,separator);
	while(!pozitive_lexeme)
	{
		while(current_lexeme != get_lexeme) // Идем до нужной лексемы
		{
			text = strtok(0,separator);
			current_lexeme++;
		}
		while(!match_the_language(text,strlen(text))) // Если лексема не соответствует языку
		{
			text = text_begin.c_str(); // Восстанавливаем оригинальный текст
			get_lexeme = rand()%lexemes; // Берем другую случайную лексему
			text =  strtok(text,separator);
			current_lexeme = 0;
			while(current_lexeme != get_lexeme) // Идем до нужной лексемы
			{
				text = strtok(0,separator);
				current_lexeme++;
			}
        }
		int min_word_nonequality = pow(2,8);
		// Сравниваем со словами словаря и находим минимум не эталонных букв
        for(int j = 0; j < Form1->dictionary_count; j++) // Проходим по всем словам в словаре
		{
			// Разное число букв - берем из словаря следующее слово, иначе считаем число совпадений
			if(strlen(text) != strlen(dictionary_words[j].c_str())) continue;
			else  // Считаем число несовпадений
			{
				if(strcmp(text,dictionary_words[j].c_str()))
				{
					int word_nonequality = 0; // Число несоответствий с текщим словом из словаря
					for(int k = 0; k < strlen(text); k++)
						if(text[k] != dictionary_words[j][k+1])
							word_nonequality++;
					// Если данное число несовпадений наименьшее, то записываем новый минимум, и все буквы несовпавшего слова
					if(word_nonequality <= min_word_nonequality)
					{
						words_counter = 0;
						min_word_nonequality = word_nonequality;
						for(int k = 0; k < strlen(text); k++)
							if(text[k] != dictionary_words[j][k+1])
							{
								nonequal_words[words_counter] = text[k];
								nonequal_dictionary_words[words_counter] = dictionary_words[j][k+1];
								words_counter++;
							}
						nonequal_words[words_counter] = '\0';
						nonequal_dictionary_words[words_counter] = '\0';
					}
				}
			}
		}
		if(words_counter == 0 || strlen(text) == 1)
		{
			pozitive_lexeme = false;
			current_lexeme = 0;
			get_lexeme = rand()%lexemes; // Берем другую случайную лексему
			text = text_begin.c_str(); // Восстанавливаем оригинальный текст
		}
		else pozitive_lexeme = true;
	}
	// Когда лексема взята проходим по тексту и заменяем первые буквы в массивах между собой и считаем процент
	// Производим перестановку символов с полным перебором подалфавита
	bool get_better_percent = false;
	text = ((AnsiString)Memo1->Lines->Text).c_str(); // Восстанавливаем оригинальный текст
	char* text_begin_2 = text;
	for(int k = 0; text[0]!='\0'; k++)
	{
		// Заменяем k-грамму в тексте
		if(text[k] == nonequal_words[0])
			text[k] = nonequal_dictionary_words[0];
		else if (text[k] == nonequal_dictionary_words[0])
			text[k] = nonequal_words[0];
		text++;
	}
	// Новый процент расшифровки текста с переставленным ключом
	float new_decode_percent = get_decode_percent(text_begin_2);
	// Если же процент расшифровки увеличился, значит результат улучшился, сохраняем перестановку букв в частотном анализе
	if(new_decode_percent > Form1->current_decode_percent)
	{
		Form1->current_decode_percent = new_decode_percent;
		Memo1->Lines->Text = text_begin;
		get_better_percent = true;
	}
	Memo2->Lines->Text = text_begin;
	time = clock() - time;
	if(get_better_percent) Memo3->Lines->Text = "Точность была улучшена\n";
	else Memo3->Lines->Text = "Не удалось улучшить результат\n";
	Memo3->Lines->Text = Memo3->Lines->Text + "Процент букв, совпавших со словарем: ";
	Memo3->Lines->Text = Memo3->Lines->Text + current_decode_percent + "%";
	Memo3->Lines->Text = Memo3->Lines->Text + "\nВремя выполнения: " + (double)time/CLOCKS_PER_SEC+ "сек.";
}
//---------------------------------------------------------------------------

