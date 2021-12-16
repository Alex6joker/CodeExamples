//---------------------------------------------------------------------------

#include <vcl.h>
#include <string.h>
#pragma hdrstop

#include "Unit2.h"
#include "Unit1.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
const char separator[]=" \.,";
TForm2 *Form2;
TEdit*** edit;  // Массивы для указателей на конструкторы объектов
TLabel*** label;
TComboBox*** ComboBox;
int COUNT[4]; // Всего нужных полей 1 (иначе 3 (ФИО))
int i = 1;
int N = 0;
int ComboBoxCount = 0;
AnsiString table[4]; // Имена связанных таблиц
AnsiString str[4][3];   // Столбцы с информацией в связанных табицах
//---------------------------------------------------------------------------
__fastcall TForm2::TForm2(TComponent* Owner)
	: TForm(Owner)
{
}
// Добавление данных SQL INSERT
void __fastcall TForm2::FormActivate(TObject *Sender)
{
	Form2->Left = (Screen->Width/3);
	Form2->Top = (Screen->Height/8);
	ADOQuery1->Connection = Form1->ADOConnection1;
	ADOQuery1->SQL->Clear();
	// Выполняем запрос, который возратит информацию о столбцах таблицы
	ADOQuery1->SQL->Text = "SELECT ORDINAL_POSITION,COLUMN_NAME,DATA_TYPE,CHARACTER_MAXIMUM_LENGTH,IS_NULLABLE,COLUMN_DEFAULT FROM INFORMATION_SCHEMA.COLUMNS WHERE (TABLE_NAME = '"+Form1->ComboBox1->Text+"') ORDER BY ORDINAL_POSITION ASC;";
	ADOQuery1->Open();
	ADOQuery1->First();
	while(!ADOQuery1->Eof) // Расчет числа столбцов для записи
	{
		N++;
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
	edit = new TEdit **[N-ComboBoxCount];
	for (int i=0;i<N-ComboBoxCount;i++)
	{
		edit[i] = new TEdit *[0];
		for (int j = 0; j < 1; j++)
		edit[i][j] = new TEdit(Form2);
	}
	label = new TLabel **[N];
	for (int i=0;i<N;i++)
	{
		label[i] = new TLabel *[0];
		for (int j = 0; j < 1; j++)
		label[i][j] = new TLabel(Form2);
	}
	ComboBox = new TComboBox **[ComboBoxCount];
	for (int i=0;i<ComboBoxCount;i++)
	{
		ComboBox[i] = new TComboBox *[0];
		for (int j = 0; j < 1; j++)
		ComboBox[i][j] = new TComboBox(Form2);
	}
	i = 0;
	int CurrentBox = 0;
	while(!ADOQuery1->Eof)
	{
		if(i<N-ComboBoxCount)
		{
			edit[i][0]->Parent = Form2;
			label[i][0]->Parent = Form2;
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
			ComboBox[CurrentBox][0]->Parent = Form2;
			label[i+CurrentBox][0]->Parent = Form2;
			ComboBox[CurrentBox][0]->Top = (50 * (i+CurrentBox+1));
			label[i+CurrentBox][0]->Top = (50 * (i+CurrentBox+1))-15;
			ComboBox[CurrentBox][0]->Height = 32;
			label[i+CurrentBox][0]->Height = 14;
			ComboBox[CurrentBox][0]->Width = 150;
			label[i+CurrentBox][0]->Width = 150;
			ComboBox[CurrentBox][0]->Left = 50;
			label[i+CurrentBox][0]->Left = 30;
			ComboBox[CurrentBox][0]->Width = 210;
			ADOQuery1->Next();
			CurrentBox++;
		}
	}
	Form2->Height = (50 * (N+1)) + 20 + 50;
	Form2->Width = 50 + 150 + 100;
	Form2->Button1->Left = Form2->Width/4-40;
	Form2->Button2->Left = Form2->Width/4+80;
	Form2->Button1->Top = (50 * (N+1));
	Form2->Button2->Top = (50 * (N+1));
	ADOQuery2->Connection = Form1->ADOConnection1;
	ADOQuery2->SQL->Clear();
	ADOQuery2->SQL->Text = "SELECT COUNT(*) as count FROM "+Form1->ComboBox1->Text+"";
	ADOQuery2->Open();
	ADOQuery2->First();
	CurrentBox = 0;
	COUNT[CurrentBox] = ADOQuery2->FieldByName("count")->AsInteger+1;
	edit[0][0]->Text = COUNT[CurrentBox];
	edit[0][0]->Enabled = 0;
	ADOQuery2->SQL->Clear();
	ADOQuery2->SQL->Text = "exec fk_search '"+Form1->ComboBox1->Text+"'";
	ADOQuery2->Open();
	ADOQuery2->First();
	ADOQuery3->Connection = Form1->ADOConnection1;
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
void __fastcall TForm2::FormClose(TObject *Sender, TCloseAction &Action)
{
		Close_Form();
}
//---------------------------------------------------------------------------
void Close_Form()
{
	// Уничтожение данных формы
	int CurrentBox = 0;
	for (int i = 0; i < N; i++)
	{
		for (int j = 0; j < 1; j++)
		{
			if (i<N-ComboBoxCount) delete edit[i][j];
			else
			{
				delete ComboBox[CurrentBox][j];
			}
			delete label[i][j];
		}
		if (i<N-ComboBoxCount) delete edit[i];
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
	N = 0;
	ComboBoxCount = 0;
}
void __fastcall TForm2::Button2Click(TObject *Sender)
{
	   Close_Form();
	   Form2->Close();
}
//---------------------------------------------------------------------------
void __fastcall TForm2::Button1Click(TObject *Sender)
{
	ADOQuery1->First();
	i = 0;
	int CurrentBox = 0;
	// Выполняем конгатенацию строк всех полей для занесения в поле VALUE SQL запроса
	AnsiString VALUE;
	while(!ADOQuery1->Eof)
	{
		if(i<N-ComboBoxCount)
		{
			AnsiString s = AnsiString(ADOQuery1->FieldByName("DATA_TYPE")->AsString);
			// Если переменная числовая, то переводим строку в число
			if(!CompareText(s,AnsiString("int")))
			{
				int z = edit[i][0]->Text.ToInt();
				VALUE = VALUE + z;
			}
			// Если переменная строковая или дата-время, то оставляем ее в первоначальном виде
			else if (!CompareText(s,AnsiString("char")) || !CompareText(s,AnsiString("varchar")) || (!CompareText(s,AnsiString("datetime")))) VALUE = VALUE + "'"+edit[i][0]->Text+"'";
			i++;
		}
		else
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
			CurrentBox++;
		}
		ADOQuery1->Next();
		if(!ADOQuery1->Eof) VALUE = VALUE + ",";
	}
	ADOQuery2->SQL->Clear();
	ADOQuery2->SQL->Text = "INSERT INTO "+Form1->ComboBox1->Text+" VALUES ("+VALUE+")";
	ADOQuery2->ExecSQL();
	Close_Form();
	Form2->Close();
	// Если какая-нибудь таблица уже открыта, то закрываем таблицу
	if(Form1->ADOTable1->TableName != "")
	{
		Form1->ADOTable1->Close();
	}
	Form1->ADOTable1->TableName = Form1->ComboBox1->Text;
	Form1->ADOTable1->Open();
}
//---------------------------------------------------------------------------
