//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit2.h"
#include "Unit1.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TForm2 *Form2;
//---------------------------------------------------------------------------
__fastcall TForm2::TForm2(TComponent* Owner)
	: TForm(Owner)
{
	FILE* f0 = fopen("settings.ini","r"); // Загрузка настроек из файла
	while(!feof(f0))
	{
		int i = 1;
		AnsiString current_point; // Строка содержит текущий пункт настройки
		char c;
		do
		{
			fscanf(f0,"%c",&c); // Читаем символы
			current_point.Insert(&c,i);
			i++;
		}while(c!='=');
		current_point.Insert('\0',i-1);
		char* text = current_point.c_str();
		i = 1;
		if(!strcmp(text,"LANGUAGE"))
		{
			current_point = ""; // Очищаем строку
            char c;
			do
			{
				fscanf(f0,"%c",&c); // Читаем символы
				current_point.Insert(&c,i);
				i++;
			}while(c!='\n' && c!='\0' && !feof(f0));
			current_point.Insert('\0',i-1);
			char* text = current_point.c_str();
			if(!strcmp(text,"ENG")) ComboBox1->Text = "Английский";
			else if(!strcmp(text,"RUS")) ComboBox1->Text = "Русский";
		}
		else if(!strcmp(text,"UPCASE1"))
		{
			char c;
			fscanf(f0,"%c",&c); // Читаем символы
			Form1->upcase_letters_of_the_current_language[0] = c;
			fscanf(f0,"%c",&c); // Читаем символ \n
		}
		else if(!strcmp(text,"UPCASE2"))
		{
			char c;
			fscanf(f0,"%c",&c); // Читаем символы
			Form1->upcase_letters_of_the_current_language[1] = c;
			fscanf(f0,"%c",&c); // Читаем символ \n
		}
		else if(!strcmp(text,"LOWERCASE1"))
		{
			char c;
			fscanf(f0,"%c",&c); // Читаем символы
			Form1->lowercase_letters_of_the_current_language[0] = c;
			fscanf(f0,"%c",&c); // Читаем символ \n
		}
		else if(!strcmp(text,"LOWERCASE2"))
		{
			char c;
			fscanf(f0,"%c",&c); // Читаем символы
			Form1->lowercase_letters_of_the_current_language[1] = c;
			fscanf(f0,"%c",&c); // Читаем символ \n
		}
		else if(!strcmp(text,"DECODE_PERCENT"))
		{
			current_point = ""; // Очищаем строку
			char c;
			i = 1;
			do
			{
				fscanf(f0,"%c",&c); // Читаем символы
				current_point.Insert(&c,i);
				i++;
			}while(c!='\n' && c!='\0' && !feof(f0));
			current_point.Insert('\0',i-1);
			char* text = current_point.c_str();
			Form1->final_percent = StrToInt(text);
			Edit1->Text = text;
		}
	}
}
//---------------------------------------------------------------------------
void __fastcall TForm2::Edit1Change(TObject *Sender)
{
	if(Edit1->Text == "") return;
	try
	{
		if(StrToInt(Edit1->Text) > 100) Edit1->Text = Edit1->Text.Delete(Edit1->Text.Length(),1); // Удаляем лишний символ
    }
	catch(...)
	{
		Edit1->Text = Edit1->Text.Delete(Edit1->Text.Length(),1); // Удаляем лишний символ
    }
}
//---------------------------------------------------------------------------
void __fastcall TForm2::Button1Click(TObject *Sender)
{
	FILE* f0 = fopen("settings.ini","w"); // Загрузка настроек из файла
	if(!AnsiCompareText("Русский",ComboBox1->Text))
	{
		fprintf(f0,"%s%s%c","LANGUAGE=","RUS",'\n');
		fprintf(f0,"%s%s%c","UPCASE1=","А",'\n');
		fprintf(f0,"%s%s%c","UPCASE2=","Я",'\n');
		fprintf(f0,"%s%s%c","LOWERCASE1=","а",'\n');
		fprintf(f0,"%s%s%c","LOWERCASE2=","я",'\n');
	}
	else if(!AnsiCompareText("Английский",ComboBox1->Text))
	{
		fprintf(f0,"%s%s%c","LANGUAGE=","ENG",'\n');
		fprintf(f0,"%s%s%c","UPCASE1=","A",'\n');
		fprintf(f0,"%s%s%c","UPCASE2=","Z",'\n');
		fprintf(f0,"%s%s%c","LOWERCASE1=","a",'\n');
		fprintf(f0,"%s%s%c","LOWERCASE2=","z",'\n');
	}
	fprintf(f0,"%s%d","DECODE_PERCENT=",StrToInt(Edit1->Text));
	Form1->final_percent = Edit1->Text.ToDouble();
	fclose(f0);
	Form2->Close();
}
//---------------------------------------------------------------------------

void __fastcall TForm2::Button2Click(TObject *Sender)
{
	Form2->Close();
}
//---------------------------------------------------------------------------

