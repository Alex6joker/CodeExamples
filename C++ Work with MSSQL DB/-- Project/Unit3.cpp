//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit1.h"
#include "Unit3.h"
#include <string.h>
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TForm3 *Form3;
const char separator[]=" \.,";
TEdit*** edit;  // Массивы для указателей на конструкторы объектов
TLabel*** label;
TComboBox*** ComboBox;
int COUNT[4]; // Всего нужных полей 1 (иначе 3 (ФИО))
int i = 1;
int N = 0;
int ComboBoxCount = 0;
AnsiString table[4]; // Имена связанных таблиц
AnsiString str[4][3];   // Столбцы с информацией в связанных табицах
int Z;
AnsiString selected_ID;
AnsiString ID_Column;
//---------------------------------------------------------------------------
__fastcall TForm3::TForm3(TComponent* Owner)
	: TForm(Owner)
{
}
// Изменение данных SQL UPDATE
void __fastcall TForm3::FormActivate(TObject *Sender)
{
	Form3->Left = (Screen->Width/3);
	Form3->Top = (Screen->Height/8);
	ADOQuery1->Connection = Form1->ADOConnection1;
	ADOQuery1->SQL->Clear();
	// Выполняем запрос, который возратит информацию о столбцах таблицы
	ADOQuery1->SQL->Text = "SELECT ORDINAL_POSITION,COLUMN_NAME,DATA_TYPE,CHARACTER_MAXIMUM_LENGTH,IS_NULLABLE,COLUMN_DEFAULT FROM INFORMATION_SCHEMA.COLUMNS WHERE (TABLE_NAME = '"+Form1->ComboBox1->Text+"') ORDER BY ORDINAL_POSITION ASC;";
	ADOQuery1->Open();
	ADOQuery1->First();
	selected_ID = Form1->DBGrid1->DataSource->DataSet->FieldByName(ADOQuery1->FieldByName("COLUMN_NAME")->AsString)->AsAnsiString;
	ID_Column = ADOQuery1->FieldByName("COLUMN_NAME")->AsString;
	Z = 0;
	while(!ADOQuery1->Eof) // Расчет числа столбцов для записи
	{
		Z++;
		ADOQuery1->Next();
	}
	ADOQuery1->First();
	while(!ADOQuery1->Eof) // Расчет числа столбцов для записи
	{
		// Подсчет числа столбцов с ID
		if(strstr(AnsiString(ADOQuery1->FieldByName("COLUMN_NAME")->AsString).c_str(),"ID")!=NULL)
		ComboBoxCount ++;
		ADOQuery1->Next();
	}
	ComboBoxCount --; // Не учитываем поле с PK
	ADOQuery1->Open();
	ADOQuery1->First();
	edit = new TEdit **[Z-ComboBoxCount];
	for (int i=0;i<Z-ComboBoxCount;i++)
	{
		edit[i] = new TEdit *[0];
		for (int j = 0; j < 1; j++)
		edit[i][j] = new TEdit(Form3);
	}
	label = new TLabel **[Z];
	for (int i=0;i<Z;i++)
	{
		label[i] = new TLabel *[0];
		for (int j = 0; j < 1; j++)
		label[i][j] = new TLabel(Form3);
	}
	ComboBox = new TComboBox **[ComboBoxCount];
	for (int i=0;i<ComboBoxCount;i++)
	{
		ComboBox[i] = new TComboBox *[0];
		for (int j = 0; j < 1; j++)
		ComboBox[i][j] = new TComboBox(Form3);
	}
	i = 0;
	int CurrentBox = 0;
	while(!ADOQuery1->Eof)
	{
		if(i<Z-ComboBoxCount)
		{
			edit[i][0]->Parent = Form3;
			label[i][0]->Parent = Form3;
			edit[i][0]->Top = (50 * (i+1));
			label[i][0]->Top = (50 * (i+1))-15;
			edit[i][0]->Height = 32;
			label[i][0]->Height = 14;
			edit[i][0]->Width = 150;
			label[i][0]->Width = 150;
			edit[i][0]->Left = 50;
			label[i][0]->Left = 30;
			label[i][0]->Caption = "Поле " + ADOQuery1->FieldByName("COLUMN_NAME")->AsString;
			ADOQuery1->Next();
			i++;
		}
		else
		{
			ComboBox[CurrentBox][0]->Parent = Form3;
			label[i+CurrentBox][0]->Parent = Form3;
			ComboBox[CurrentBox][0]->Top = (50 * (i+CurrentBox+1));
			label[i+CurrentBox][0]->Top = (50 * (i+CurrentBox+1))-15;
			ComboBox[CurrentBox][0]->Height = 32;
			label[i+CurrentBox][0]->Height = 14;
			ComboBox[CurrentBox][0]->Width = 150;
			label[i+CurrentBox][0]->Width = 150;
			ComboBox[CurrentBox][0]->Left = 50;
			label[i+CurrentBox][0]->Left = 30;
			ADOQuery1->Next();
			CurrentBox++;
		}
	}
	Form3->Height = (50 * (Z+1)) + 20 + 50;
	Form3->Width = 50 + 150 + 100;
	Form3->Button1->Left = Form3->Width/4-40;
	Form3->Button2->Left = Form3->Width/4+80;
	Form3->Button1->Top = (50 * (Z+1));
	Form3->Button2->Top = (50 * (Z+1));
	edit[0][0]->Enabled = 0;
	ADOQuery2->Connection = Form1->ADOConnection1;
	ADOQuery2->SQL->Clear();
	ADOQuery2->SQL->Text = "SELECT * FROM "+Form1->ComboBox1->Text+" WHERE "+ID_Column+" = "+selected_ID+"";
	ADOQuery2->Open();
	ADOQuery2->First();
	ADOQuery3->Connection = Form1->ADOConnection1;
	ADOQuery3->SQL->Clear();
	ADOQuery3->SQL->Text = "select COLUMN_NAME from INFORMATION_SCHEMA.columns where TABLE_NAME = '"+Form1->ComboBox1->Text+"'";
	ADOQuery3->Open();
	ADOQuery3->First();
	int n = 0;
	while(!ADOQuery3->Eof && n<Z-ComboBoxCount)
	{
		edit[n][0]->Text = ADOQuery2->FieldByName(ADOQuery3->FieldByName("COLUMN_NAME")->AsString)->AsString;
		n++;
		ADOQuery2->Next();
		ADOQuery3->Next();
	}
	ADOQuery2->SQL->Clear();
	ADOQuery2->SQL->Text = "exec fk_search '"+Form1->ComboBox1->Text+"'";
	ADOQuery2->Open();
	ADOQuery2->First();
	CurrentBox = 0;
	while(!ADOQuery2->Eof) // Заполнение всех ComboBox данными из связанных таблиц
	{
		table[CurrentBox] = ADOQuery2->FieldByName("referenced_table")->AsString;
		ADOQuery3->SQL->Clear();
		ADOQuery3->SQL->Text = "exec get_columns '"+table[CurrentBox]+"'";
		COUNT[CurrentBox] = 0;
		ADOQuery3->Open();
		ADOQuery3->First();
		while(!ADOQuery3->Eof) // Считаем колонки в связанной таблице
		{
			COUNT[CurrentBox]++;
			ADOQuery3->Next();
		}
		if(COUNT[CurrentBox]==1) // Если всего нужных полей 1 (иначе 3 (ФИО))
		{
			str[CurrentBox][0] = ADOQuery3->FieldByName("COLUMN_NAME")->AsString;
			label[i+CurrentBox][0]->Caption = "Поле " + table[CurrentBox];
			ADOQuery3->SQL->Clear();
			ADOQuery3->SQL->Text = "SELECT "+str[CurrentBox][0]+" FROM "+table[CurrentBox];
			ADOQuery3->Open();
			ADOQuery3->First();
			while(!ADOQuery3->Eof)
			{
				ComboBox[CurrentBox][0]->Items->Add(ADOQuery3->FieldByName(str[CurrentBox][0])->AsString);
				ADOQuery3->Next();
			}
		}
		else
		{
			int cur = 0;
			ADOQuery3->First();
			while(!ADOQuery3->Eof)
			{
				str[CurrentBox][cur] = ADOQuery3->FieldByName("COLUMN_NAME")->AsString;
				label[i+CurrentBox][0]->Caption = "Поле ФИО " + table[CurrentBox];
				ADOQuery3->Next();
				cur++;
			}
            ADOQuery3->SQL->Clear();
			ADOQuery3->SQL->Text = "SELECT "+str[CurrentBox][0]+","+str[CurrentBox][1]+","+str[CurrentBox][2]+" FROM "+table[CurrentBox];
			ADOQuery3->Open();
			ADOQuery3->First();
			while(!ADOQuery3->Eof)
			{
				ComboBox[CurrentBox][0]->Items->Add(AnsiString(ADOQuery3->FieldByName(str[CurrentBox][0])->AsString)+" "+AnsiString(ADOQuery3->FieldByName(str[CurrentBox][1])->AsString)+" "+AnsiString(ADOQuery3->FieldByName(str[CurrentBox][2])->AsString));
				ADOQuery3->Next();
			}
		}
		CurrentBox ++;
		ADOQuery2->Next();
	}
}
//---------------------------------------------------------------------------
void __fastcall TForm3::Button1Click(TObject *Sender)
{
	ADOQuery1->First();
	ADOQuery2->Connection = Form1->ADOConnection1;
	i = 0;
	int CurrentBox = 0;
	while(!ADOQuery1->Eof)
	{
		AnsiString VALUE; // Каждый раз у нас новое значение для каждого из столбцов
		AnsiString s = AnsiString(ADOQuery1->FieldByName("DATA_TYPE")->AsString);
		AnsiString column =  AnsiString(ADOQuery1->FieldByName("COLUMN_NAME")->AsString);
		if(i<Z-ComboBoxCount && edit[i][0]->Enabled == true)
		{
			// Если переменная числовая, то переводим строку в число
			if(!CompareText(s,AnsiString("int")))
			{
				int z = edit[i][0]->Text.ToInt();
				VALUE = VALUE + z;
			}
			// Если переменная строковая или дата-время, то оставляем ее в первоначальном виде
			else if (!CompareText(s,AnsiString("char")) || !CompareText(s,AnsiString("varchar")) || (!CompareText(s,AnsiString("datetime")))) VALUE = VALUE + "'"+edit[i][0]->Text+"'";
			ADOQuery2->SQL->Clear();
			ADOQuery2->SQL->Text = "UPDATE "+Form1->ComboBox1->Text+" SET ["+column+"] = "+VALUE+" WHERE "+ ID_Column+ " = "+selected_ID;
			int err = ADOQuery2->ExecSQL();
		}
		else if (i>=Z-ComboBoxCount)
		{
            // Определяем имя поля с ID
			ADOQuery3->SQL->Clear();
			ADOQuery3->SQL->Text = "exec get_IDcolumn "+table[CurrentBox];
			ADOQuery3->Open();
			ADOQuery3->First();
			AnsiString COLUMN = ADOQuery3->FieldByName("COLUMN_NAME")->AsString;
			ADOQuery3->SQL->Clear();
			// Если нужное поле одно
			if(COUNT[CurrentBox]==1)
			{
				ADOQuery3->SQL->Text = "SELECT "+COLUMN+" as ID FROM "+table[CurrentBox]+" WHERE "+str[CurrentBox][0]+" = '"+ComboBox[CurrentBox][0]->Text+"'";
				ADOQuery3->Open();
				ADOQuery3->First();
			}
			// Если нужных полей больше 1
			else
			{
				// Производим разбитие строки на лексемы
				AnsiString c1,c2,c3;
				AnsiString full_str = ComboBox[CurrentBox][0]->Text;
				c1 = strtok(full_str.c_str(),separator);
				c2 = strtok(0,separator);
				c3 = strtok(0,separator);
				ADOQuery3->SQL->Text = "SELECT "+COLUMN+" as ID FROM "+table[CurrentBox]+" WHERE "+str[CurrentBox][0]+" = '"+c1+"' and "+str[CurrentBox][1]+" = '"+c2+"' and "+str[CurrentBox][2]+" = '"+c3+"'";
				ADOQuery3->Open();
				ADOQuery3->First();
			}
			int z = ADOQuery3->FieldByName("ID")->AsInteger;
			VALUE = VALUE + z;
			ADOQuery2->SQL->Clear();
			ADOQuery2->SQL->Text = "UPDATE "+Form1->ComboBox1->Text+" SET "+column+" = "+VALUE+" WHERE "+ ID_Column+ " = "+selected_ID;
			int err = ADOQuery2->ExecSQL();
			CurrentBox++;
		}
		ADOQuery1->Next();
		i++;
	}
	Close_Form3();
	Form3->Close();
	//Если какая-нибудь таблица уже открыта, то закрываем таблицу
	if(Form1->ADOTable1->TableName != "")
	{
		Form1->ADOTable1->Close();
	}
	Form1->ADOTable1->TableName = Form1->ComboBox1->Text;
	Form1->ADOTable1->Open();
}
//---------------------------------------------------------------------------
void __fastcall TForm3::Button2Click(TObject *Sender)
{
	Close_Form3();
	Form3->Close();
}
//---------------------------------------------------------------------------
void Close_Form3()
{
	// Уничтожение данных формы
	int CurrentBox = 0;
	for (int i = 0; i < Z; i++)
	{
		for (int j = 0; j < 1; j++)
		{
			if (i<Z-ComboBoxCount) delete edit[i][j];
			else
			{
				delete ComboBox[CurrentBox][j];
			}
			delete label[i][j];
		}
		if (i<Z-ComboBoxCount) delete edit[i];
		else
		{
			delete ComboBox[CurrentBox];
			CurrentBox ++;
		}
		delete [] label[i];
	}
	delete [] edit;
	delete [] label;
	delete [] ComboBox;
	i = 1;
	Z = 0;
	ComboBoxCount = 0;
}
void __fastcall TForm3::FormClose(TObject *Sender, TCloseAction &Action)
{
		Close_Form3();
}
//---------------------------------------------------------------------------
