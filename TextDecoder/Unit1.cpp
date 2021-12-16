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
	Form1->polygramm = 1; // ����������� k-������ ��� ������� ���������
	Form1->click_counter = 0; // ����������� k-������ ��� ������� ���������
	Form1->count = 0; // ���-�� k-����� ��� ������� ���������
	Form1->current_decode_percent = 0;
	Form1->change_group[0] = 0; // ���������� ��������� ����� ����� ������� (2 ����� ����� ����������� �����)
	Form1->change_group[1] = 1;
	Form1->right_words = 0;
	// ���������� �������� ��������
	Application->CreateForm(__classid(TForm2), &Form2); // �������� ������� �������� ��������
	Form2->Free();
	FILE* f0;
	if(lowercase_letters_of_the_current_language[0] == '�') f0 = fopen("dictionary_rus.txt","r");
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
		// ��������� �� ����� k-������ � ������� �������� �������
		FILE* f0;
		if(lowercase_letters_of_the_current_language[0] == '�') f0 = fopen("rus_frequency.txt","r");
		else if(lowercase_letters_of_the_current_language[0] == 'a') f0 = fopen("eng_frequency.txt","r");
		char c;
		// ���������� ������ ��� ������
		do fscanf(f0,"%c",&c); while(c!='\n');
		do fscanf(f0,"%c",&c); while(c!='\n');
		// ������ count - ���������� k-�����
		fscanf(f0,"%d",&Form1->count); // ��������� ���������� k-����� � ����� ������
		// �������� ������ ��� k-������
		k_gramms = new char*[Form1->count]; // �������� ����� ��� ���-�� k-�����
		for(int i = 0; i < Form1->count; i++)
			k_gramms[i] = new char[Form1->polygramm];
		do fscanf(f0,"%c",&c); while(c!='\n'); // ������� ���������������� � k-�������
		for(int i = 0; i < Form1->count; i++) // ��������� ��� k-������
		{
			for(int j = 0; j < Form1->polygramm; j++)
			{   // ������ ���� k-������ (��������� ����)
				fscanf(f0,"%c",&k_gramms[i][j]);
			}
			// ������� ���������������� � ��������� k-������
			do fscanf(f0,"%c",&c); while(c!='\n' && !feof(f0));
		}
		fclose(f0);
		// �������� ������ ��� ������ ���������� ������������� k-�����
		text_k_gramms = new frequency_of_k_gramms_in_text[Form1->count];
		Form1->click_counter++;
	}
	// ����������� ��������� ��������������� ������ �� ��� ���, ���� �� ����� ��������� ������ �������
	clock_t time = clock();
	int current_percent_is_not_changed = 0; // ������� ����, ������� ��� �� ��������� ������� ������� �����������
	for(int iteration_counter = 0; Form1->current_decode_percent <= Form1->final_percent; iteration_counter++)
	{
		if(iteration_counter) // ���� ��� ��� ������� ��������� �������� �����, �� �������� ������ ������������� ��������
		{
			char* temp_str = new char[Form1->polygramm]; // ����� ������� � ���������� ������ ������
			for(int i = 0; i < Form1->polygramm; i++)
				temp_str[i] = k_gramms[Form1->change_group[0]][i];
			strcpy(k_gramms[Form1->change_group[0]],k_gramms[Form1->change_group[1]]);
			strcpy(k_gramms[Form1->change_group[1]],temp_str);
			delete [] temp_str;
		}
		// ��������� � ��������� ���������� ����� �� ���� Memo1
		char* text = ((AnsiString)Memo1->Lines->Text).c_str();
		// ��������� �� ������ ��������������� k-������
		// ��������� ����� ������ ������
		char* text_begin = text;
		int text_k_gramms_count = 0; // ���-�� ��� ��������� � ������ k-����� �� ������
		char* analyzing_k_gramm = new char[Form1->polygramm]; // ������ ��� ������ k_gramm �� ��������� ������
		while(text[0]!='\0')
		{
			strncpy(analyzing_k_gramm,text,Form1->polygramm); // �������� � ������ ������������� k_gramm
			// ��������� ��� ��������� ����� � �������� �������� ������� ANSII
			for(int i = 0; i < Form1->polygramm; i++) // �� ����� ������� k-������
				if(analyzing_k_gramm[i] >= upcase_letters_of_the_current_language[0] && analyzing_k_gramm[i] <= upcase_letters_of_the_current_language[1])
//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!������ ��� �������� ������!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
					analyzing_k_gramm[i] += 32;
			// ����������, �������� �� ��� ����� k-������ ������� ��������\����������� ��������
			// ���� ����� ������������ ���������������� �����, ���������� �� � �������
			if(match_the_language(analyzing_k_gramm,Form1->polygramm))
				text_k_gramms_count = Find_and_write_k_gramm(analyzing_k_gramm,text_k_gramms,k_gramms,Form1->count,text_k_gramms_count);
			text++;
		}
		// ���������� ���������� ������� �� �������� �������
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
		// ������ k-����� � ������ �� k-������ ��������� ���������� �����
		// ��������������� �������� �����
		text = text_begin;
		while(text[0]!='\0')
		{
			// ��������� �� ������ k-������
			strncpy(analyzing_k_gramm,text,Form1->polygramm); // �������� � ������ ������������� k_gramm
			for(int i = 0; i < Form1->polygramm; i++) // �� ����� ������� k-������
				if (analyzing_k_gramm[i] >= upcase_letters_of_the_current_language[0] && analyzing_k_gramm[i] <= upcase_letters_of_the_current_language[1])
//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!������ ��� �������� ������!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
					analyzing_k_gramm[i] += 32;
			// ��������, ���� �� ��������� k-������ � ��������������� ��������� ������ (���������� �� ���� k-������ ������)
			for(int i = 0; i < Form1->count; i++)
			{
				if(!strcmp(analyzing_k_gramm,text_k_gramms[i].k_gramm.c_str()))
				{   // �������� k-������ � ������
					for(int j = 0; j < Form1->polygramm; j++)
						text[j] = k_gramms[i][j];
					break;
				}
			}
			text++;
		}
		// ��������������� �������� �����
		AnsiString str;
		str.Insert(text_begin,1);
		AnsiString str2;
		str2.Insert(str,1);
		// ����� ������� ����������� ������ � �������������� ������
		float new_decode_percent = get_decode_percent(str.c_str());
		// ���� �� ������� ����������� ����������, ������ ��������� ���������, ��������� ������������ ���� � ��������� �������
		if(new_decode_percent > Form1->current_decode_percent)
		{
			Form1->current_decode_percent = new_decode_percent;
			current_percent_is_not_changed = 0;
			if(iteration_counter)
				Memo1->Lines->Text = str2;
		}
		else // ����� ������� ������������ �� ���������
		{
			current_percent_is_not_changed++;
			char* temp_str = new char[Form1->polygramm]; // ����� ������� � ���������� ������ ������
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
		if(!iteration_counter) // �������� ��������� �������� �����
			Memo1->Lines->Text = Memo2->Lines->Text;
		if((iteration_counter+1)%Form1->count == 0)
		{
			Form1->change_group[0] = 0;
			Form1->change_group[1] = 1;
			iteration_counter++;
		}
		Form1->right_words = 0;
		if(current_percent_is_not_changed == Form1->count) break;
		// ������� ������
		delete [] analyzing_k_gramm;
		//delete [] text_k_gramms;
		//for(int i = 0; i < count; i++)
		//	delete [] k_gramms[i];
		//delete [] k_gramms;
	}
	time = clock() - time;
	if(current_percent_is_not_changed == 0) Memo3->Lines->Text = "��������� �������� ����������\n";
	else Memo3->Lines->Text = "��������� �������� �� ���� ����������\n";
	Memo3->Lines->Text = Memo3->Lines->Text + "������� ����, ��������� �� ��������: ";
	Memo3->Lines->Text = Memo3->Lines->Text + current_decode_percent + "%";
	Memo3->Lines->Text = Memo3->Lines->Text + "\n����� ����������: " + (double)time/CLOCKS_PER_SEC+ "���.";
	FILE* f0 = fopen("NewKey.txt","w");
	for(int i = 0; i < Form1->count; i++)
		for(int j = 0; j < Form1->polygramm; j++)
			fprintf(f0,"%c",k_gramms[i][j]);
	fclose(f0);
}
//---------------------------------------------------------------------------
// ������� ������ k-������ � ������� k-����� � ������ � ����� ���� ��� ��������� ������
int TForm1::Find_and_write_k_gramm(char* analyzing_k_gramm, frequency_of_k_gramms_in_text* text_k_gramms,char** k_gramms,int count,int text_k_gramms_count)
{   // ���������, ���� �� ������ k-������ � ������ ������������� k-�����
	bool in_k_gramms = false;
	for(int i = 0; i < count; i++)
		// ���� ���� ���������, ������ ����� ��������� ������
		if(!strcmp(analyzing_k_gramm,k_gramms[i]))
		{
			in_k_gramms = true;
			break;
		}
	// ����� ������ ���������� �������� ���, ������ �� �������
	if(!in_k_gramms) return text_k_gramms_count;
	// ���������, ���� �� ������ k-������ � ������� � ��� ������������� ��������� k-�����
	bool in_text_k_gramms = false;
	int i; // ������ ��������� ��������� ������, ���� k-������ ����� (��� ������)
	for(i = 0; i < text_k_gramms_count; i++) // ���������, �� ������ ������
		if(!strcmp(analyzing_k_gramm,text_k_gramms[i].k_gramm.c_str())) // ���� ���� ����������, �� ���������� ������� �� 1
		{
			text_k_gramms[i].frequency++;
			in_text_k_gramms = true;
			break;
		}
	if(!in_text_k_gramms) // ���� �������� � ������� �� ����, �� ���������� � ��������� ��������� ������
	{
		text_k_gramms[i].k_gramm = analyzing_k_gramm;
		text_k_gramms[i].frequency = 1;
        text_k_gramms_count++; // ����������� ������� ���������� � ������� k-����� �� ������
	}
	return text_k_gramms_count;
}
float TForm1::get_decode_percent(char* text) // 2-� �������� ��� ��������
{
	// ��������� �� ������ ����� (�������) �� ��� ���, ���� ��� ��������
	char* separator = " \.-,\r\n\t:;";
	text = strtok(text,separator);
	int max_word_equality = 0; // ������������ ����� ����������
	float percent = 0; // �������� �������������� ������� �����������
	int i; // ������� ����� (����� ����� ������)
	for(i = 0; text; i++) // ���� ���� ��������� �������
	{   // ����������, �������� �� ��� ����� ������� ������� ��������\����������� ��������
		if(!match_the_language(text,strlen(text)))
		{
            i--;
			text =  strtok(0,separator);
			continue;
		}
		for(int j = 0; j < Form1->dictionary_count; j++) // �������� �� ���� ������ � �������
		{
			// ������ ����� ���� - ����� �� ������� ��������� �����, ����� ������� ����� ����������
			if(strlen(text) != dictionary_words[j].Length()) continue;
			else  // ������� ����� ����������
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
		// ������������ ����� ���������� ����� �� ����� �������, ��� ����� ���� �������� ���� � ������������������ ������
		percent += (float)max_word_equality/strlen(text);
		text =  strtok(0,separator);
        max_word_equality = 0;
	}
	percent *= 100;
	percent /= i;
	return percent;
}
bool TForm1::match_the_language(char* analyzing_k_gramm, int count) // ����������, �������� �� ��� ����� k-������ ������� ��������\����������� ��������
{
	for(int i = 0; i < count; i++) // �� ����� ������� k-������
	if(!(analyzing_k_gramm[i] >= lowercase_letters_of_the_current_language[0] && analyzing_k_gramm[i] <= lowercase_letters_of_the_current_language[1]))
	{
		return false;
	}
	return true;
}
bool check_the_word_in_sub_alphabet(char word,char* sub_alphabet, int num)
{   // ���������, ���� �� ��� ��� ����� � �����������
	for(int i = 0; i < num; i++)
		if(sub_alphabet[i] == word) return true;
	return false;
}
void __fastcall TForm1::N7Click(TObject *Sender)
{
	clock_t time = clock();
	// ������� ��������� ���������� �� 100%
	// ��������� ����������, ��������� �� ����, ������� �� ��������� �� ������� �� �������
	// ��������� � ��������� ���������� ����� �� ���� Memo1
	char* text = ((AnsiString)Memo1->Lines->Text).c_str();
	// ��������� �� ������ ����� (�������) �� ��� ���, ���� ��� ��������
	char* separator = " \.-,\r\n\t:;";
	char* sub_alphabet = new char[Form1->count]; // ����������, ������� ������� �� ����, �� ��������� � ������� �� �������
	char* temp_sub_alphabet = new char[Form1->count]; // ����������, ������� ������� �� ����, �� ��������� � ������� �� �������
	int number_of_symbols_in_sub_alphabet = 0;
	text = strtok(text,separator);
	int min_word_nonequality = pow(2,8); // ����������� ����� ����������
	for(int i = 0; text; i++) // ���� ���� ��������� �������
	{   // ����������, �������� �� ��� ����� ������� ������� ��������\����������� ��������
		int n = 0; // ����� ���� � ��������� �����������
		if(!match_the_language(text,strlen(text)))
		{
            i--;
			text =  strtok(0,separator);
			continue;
		}
		for(int j = 0; j < Form1->dictionary_count; j++) // �������� �� ���� ������ � �������
		{
			// ������ ����� ���� - ����� �� ������� ��������� �����, ����� ������� ����� ����������
			if(strlen(text) != strlen(dictionary_words[j].c_str())) continue;
			else  // ������� ����� ������������
			{
				if(strcmp(text,dictionary_words[j].c_str()))
				{
					int word_nonequality = 0; // ����� �������������� � ������ ������ �� �������
					for(int k = 0; k < strlen(text); k++)
						if(text[k] != dictionary_words[j][k+1])
							word_nonequality++;
					// ���� ������ ����� ������������ ����������, �� ���������� ����� �������, � ��� ����� ������������ �����
					if(word_nonequality <= min_word_nonequality)
					{
						min_word_nonequality = word_nonequality;
						// ������ ��������� ���������� �������� ��� ����������� �����
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
		// � ������������� ������� � ������������ �������������� ��������� ��� � �������������� ����������
		for(int i = 0; i < min_word_nonequality; i++)
			if(!check_the_word_in_sub_alphabet(temp_sub_alphabet[i],sub_alphabet,number_of_symbols_in_sub_alphabet))
			{
				sub_alphabet[number_of_symbols_in_sub_alphabet] = temp_sub_alphabet[i];
				number_of_symbols_in_sub_alphabet++;
            }
		// ������������ ����� ���������� ����� �� ����� �������, ��� ����� ���� �������� ���� � ������������������ ������
		text =  strtok(0,separator);
		min_word_nonequality = pow(2,8);
		// ��� �������, ������ ������ ����� �������� ����, ��� �������� �����������
		if(number_of_symbols_in_sub_alphabet >= 4) break;
	}
	// ���������� ������������ �������� � ������ ��������� �����������
	bool get_better_percent = false;
	for(int i = 0; i < number_of_symbols_in_sub_alphabet && (!get_better_percent); i++)
	{
		for(int j = 0; j < number_of_symbols_in_sub_alphabet; j++)
			if(i!=j)
			{
				// ��������� � ��������� ���������� ����� �� ���� Memo1
				char* text = ((AnsiString)Memo1->Lines->Text).c_str();
				// ��������� ����� ������ ������
				char* text_begin = text;
				for(int k = 0; text[0]!='\0'; k++)
				{
					// �������� k-������ � ������
					if(text[k] == sub_alphabet[i])
						text[k] = sub_alphabet[j];
					else if (text[k] == sub_alphabet[j])
						text[k] = sub_alphabet[i];
					text++;
				}
				// ��������������� �������� �����
				AnsiString str = text_begin;
				// ����� ������� ����������� ������ � �������������� ������
				float new_decode_percent = get_decode_percent(text_begin);
				// ���� �� ������� ����������� ����������, ������ ��������� ���������, ��������� ������������ ���� � ��������� �������
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
	if(get_better_percent) Memo3->Lines->Text = "�������� ���� ��������\n";
	else Memo3->Lines->Text = "�� ������� �������� ���������\n";
	Memo3->Lines->Text = Memo3->Lines->Text + "������� ����, ��������� �� ��������: ";
	Memo3->Lines->Text = Memo3->Lines->Text + current_decode_percent + "%";
	Memo3->Lines->Text = Memo3->Lines->Text + "\n����� ����������: " + (double)time/CLOCKS_PER_SEC+ "���.";
}
//---------------------------------------------------------------------------


void __fastcall TForm1::N21Click(TObject *Sender)
{
	// ������� ��������� ���������� �� 100% ������ �2
	// ��������� ����������, ��������� �� ����, ������� �� ��������� �� ������� �� �������
	// ��������� � ��������� ���������� ����� �� ���� Memo1
	clock_t time = clock();
	char* text = ((AnsiString)Memo1->Lines->Text).c_str();
	char* nonequal_words = new char[40];
	char* nonequal_dictionary_words = new char[40];
	int words_counter = 0;
	// ��������� �� ������ ����� (�������) �� ��� ���, ���� ��� ��������
	char* separator = " \.-,\r\n\t:;";
	AnsiString text_begin = ((AnsiString)Memo1->Lines->Text).c_str();
	int lexemes = 1;
	text =  strtok(text,separator);
	while(text)
	{
		lexemes++;
		text = strtok(0,separator);
	}
	randomize(); // ���������� ��������������� ������������������
	bool pozitive_lexeme = false; // ������� �������� ��� ������ ���� (������������� ����� � � ��� ���� ������)
	int get_lexeme = rand()%lexemes; // ����� ��������� �������
	text = ((AnsiString)Memo1->Lines->Text).c_str(); // ��������������� ������������ �����
	int current_lexeme = 0;
	text =  strtok(text,separator);
	while(!pozitive_lexeme)
	{
		while(current_lexeme != get_lexeme) // ���� �� ������ �������
		{
			text = strtok(0,separator);
			current_lexeme++;
		}
		while(!match_the_language(text,strlen(text))) // ���� ������� �� ������������� �����
		{
			text = text_begin.c_str(); // ��������������� ������������ �����
			get_lexeme = rand()%lexemes; // ����� ������ ��������� �������
			text =  strtok(text,separator);
			current_lexeme = 0;
			while(current_lexeme != get_lexeme) // ���� �� ������ �������
			{
				text = strtok(0,separator);
				current_lexeme++;
			}
        }
		int min_word_nonequality = pow(2,8);
		// ���������� �� ������� ������� � ������� ������� �� ��������� ����
        for(int j = 0; j < Form1->dictionary_count; j++) // �������� �� ���� ������ � �������
		{
			// ������ ����� ���� - ����� �� ������� ��������� �����, ����� ������� ����� ����������
			if(strlen(text) != strlen(dictionary_words[j].c_str())) continue;
			else  // ������� ����� ������������
			{
				if(strcmp(text,dictionary_words[j].c_str()))
				{
					int word_nonequality = 0; // ����� �������������� � ������ ������ �� �������
					for(int k = 0; k < strlen(text); k++)
						if(text[k] != dictionary_words[j][k+1])
							word_nonequality++;
					// ���� ������ ����� ������������ ����������, �� ���������� ����� �������, � ��� ����� ������������ �����
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
			get_lexeme = rand()%lexemes; // ����� ������ ��������� �������
			text = text_begin.c_str(); // ��������������� ������������ �����
		}
		else pozitive_lexeme = true;
	}
	// ����� ������� ����� �������� �� ������ � �������� ������ ����� � �������� ����� ����� � ������� �������
	// ���������� ������������ �������� � ������ ��������� �����������
	bool get_better_percent = false;
	text = ((AnsiString)Memo1->Lines->Text).c_str(); // ��������������� ������������ �����
	char* text_begin_2 = text;
	for(int k = 0; text[0]!='\0'; k++)
	{
		// �������� k-������ � ������
		if(text[k] == nonequal_words[0])
			text[k] = nonequal_dictionary_words[0];
		else if (text[k] == nonequal_dictionary_words[0])
			text[k] = nonequal_words[0];
		text++;
	}
	// ����� ������� ����������� ������ � �������������� ������
	float new_decode_percent = get_decode_percent(text_begin_2);
	// ���� �� ������� ����������� ����������, ������ ��������� ���������, ��������� ������������ ���� � ��������� �������
	if(new_decode_percent > Form1->current_decode_percent)
	{
		Form1->current_decode_percent = new_decode_percent;
		Memo1->Lines->Text = text_begin;
		get_better_percent = true;
	}
	Memo2->Lines->Text = text_begin;
	time = clock() - time;
	if(get_better_percent) Memo3->Lines->Text = "�������� ���� ��������\n";
	else Memo3->Lines->Text = "�� ������� �������� ���������\n";
	Memo3->Lines->Text = Memo3->Lines->Text + "������� ����, ��������� �� ��������: ";
	Memo3->Lines->Text = Memo3->Lines->Text + current_decode_percent + "%";
	Memo3->Lines->Text = Memo3->Lines->Text + "\n����� ����������: " + (double)time/CLOCKS_PER_SEC+ "���.";
}
//---------------------------------------------------------------------------

