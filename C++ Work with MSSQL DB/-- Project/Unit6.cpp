//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit6.h"
#include "Unit1.h"
#include <string.h>
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TForm6 *Form6;
const char separator[]=" \.,";
AnsiString selected;
//---------------------------------------------------------------------------
__fastcall TForm6::TForm6(TComponent* Owner)
	: TForm(Owner)
{
}
//---------------------------------------------------------------------------
void __fastcall TForm6::FormActivate(TObject *Sender)
{
	Form1->DBGrid2->DataSource->DataSet = NULL;
	Form6->Left = (Screen->Width/3);
	Form6->Top = (Screen->Height/8);
	ADOQuery1->Connection = Form1->ADOConnection1;
	ADOQuery1->SQL->Clear();
	ComboBox1->Clear();  ComboBox1->Text = "";  Label1->Caption = "";
	ComboBox2->Clear();  ComboBox2->Text = "";  Label2->Caption = "";
	ComboBox3->Clear();  ComboBox3->Text = "";  Label3->Caption = "";
	Label1->Width = 155; Label2->Width = 155;  Label3->Width = 155; Label4->Width = 155;
	Label10->Width = 155; Label4->Caption = "";
	Edit1->Text = "";
	Edit2->Text = "";
	Edit3->Text = "";
	Edit4->Text = "";
	if (!CompareText(AnsiString("Добавить нового артиста"),AnsiString(Form1->ComboBox3->Text)))
	{
		Edit1->Visible = true;  Label1->Visible = true;
		Edit2->Visible = true;  Label2->Visible = true;
		Edit3->Visible = true;  Label3->Visible = true;
		Edit4->Visible = true;  Label4->Visible = true;
		Button1->Visible = true;
		Button1->Caption = "Добавить артиста";
		Label1->Caption = "Имя артиста";   Label2->Caption = "Отчество артиста";
		Label3->Caption = "Фамилия артиста"; Label4->Caption = "Дата рождения";
		Label4->Width = 100;
		Label10->Caption = "Процедура на добавление нового артиста"; Label10->Top = 20;
		Label10->Left = 50; Edit1->Width = 120;
		Label1->Top = Label10->Top+40; Label1->Left = Label10->Left+20;
		Edit1->Left = 50; Edit1->Top = Label1->Top+20;
		Label2->Top = Edit1->Top+30; Label2->Left = Label1->Left;
		Edit2->Left = Edit1->Left; Edit2->Top = Label2->Top+20;
		Label3->Top = Edit2->Top+30; Label3->Left = Label2->Left;
		Edit3->Left = Edit2->Left; Edit3->Top = Label3->Top+20;
		Label4->Top = Edit3->Top+30; Label4->Left = Label3->Left;
		Edit4->Left = Edit3->Left; Edit4->Top = Label4->Top+20;
		Button1->Top = Edit4->Top + 50; Button1->Left = 60;
		Button1->Width = Edit4->Width - 20;
		Form6->Width = 250; Form6->Height = Button1->Top+120;
	}
	else if (!CompareText(AnsiString("Изменить импресарио у данного артиста"),AnsiString(Form1->ComboBox3->Text)))
	{
		ADOQuery1->SQL->Text = "SELECT Name,Middle_Name,Family FROM Artist";
		ADOQuery1->Open();
		ADOQuery1->First();
		ComboBox1->Visible = true;  Label1->Visible = true;
		ComboBox2->Visible = true;  Label2->Visible = true;
		ComboBox3->Visible = true;  Label3->Visible = true;
		while(!ADOQuery1->Eof)
		{
			ComboBox1->Items->Add(AnsiString(ADOQuery1->FieldByName("Name")->AsString)+" "+AnsiString(ADOQuery1->FieldByName("Middle_Name")->AsString)+" "+AnsiString(ADOQuery1->FieldByName("Family")->AsString));
			ADOQuery1->Next();
		}
		ADOQuery1->SQL->Clear();
		ADOQuery1->SQL->Text = "SELECT Name,Middle_Name,Family FROM Impresario";
		ADOQuery1->Open();
		ADOQuery1->First();
		while(!ADOQuery1->Eof)
		{
			ComboBox2->Items->Add(AnsiString(ADOQuery1->FieldByName("Name")->AsString)+" "+AnsiString(ADOQuery1->FieldByName("Middle_Name")->AsString)+" "+AnsiString(ADOQuery1->FieldByName("Family")->AsString));
			ComboBox3->Items->Add(AnsiString(ADOQuery1->FieldByName("Name")->AsString)+" "+AnsiString(ADOQuery1->FieldByName("Middle_Name")->AsString)+" "+AnsiString(ADOQuery1->FieldByName("Family")->AsString));
			ADOQuery1->Next();
		}
		Button1->Visible = true;
		Button1->Caption = "Сменить импресарио";
		Label1->Caption = "ФИО артиста";   Label2->Caption = "ФИО старого импресарио";
		Label3->Caption = "ФИО нового импресарио";
		Label10->Caption = "Процедура на смену старого импресарио"; Label10->Top = 20;
		Label10->Left = 50;
		Label1->Top = Label10->Top+40; Label1->Left = Label10->Left+20;
		ComboBox1->Width = 200; ComboBox2->Width = 200; ComboBox3->Width = 200;
		ComboBox1->Left = 50; ComboBox1->Top = Label1->Top+20;
		Label2->Top = ComboBox1->Top+30; Label2->Left = Label1->Left;
		ComboBox2->Left = ComboBox1->Left; ComboBox2->Top = Label2->Top+20;
		Label3->Top = ComboBox2->Top+30; Label3->Left = Label2->Left;
		ComboBox3->Left = ComboBox2->Left; ComboBox3->Top = Label3->Top+20;
		Button1->Top = ComboBox3->Top + 50; Button1->Left = 60;
		Button1->Width = ComboBox3->Width - 20;
		Form6->Width = 320; Form6->Height = Button1->Top+120;
	}
	else if (!CompareText(AnsiString("Вывод артистов, работающих в данном жанре"),AnsiString(Form1->ComboBox3->Text)))
	{
		ADOQuery1->SQL->Text = "SELECT Name FROM Genre";
		ADOQuery1->Open();
		ADOQuery1->First();
		ComboBox1->Visible = true;  Label1->Visible = true;
		while(!ADOQuery1->Eof)
		{
			ComboBox1->Items->Add(ADOQuery1->FieldByName("Name")->AsString);
			ADOQuery1->Next();
		}
		Button1->Visible = true;
		Button1->Caption = "Найти артистов";
		Label10->Caption = "Процедура на выборку артистов, работающих в данном жанре";
		Label10->Top = 20;
		Label10->Left = 50;
		Label1->Top = Label10->Top+40; Label1->Left = Label10->Left+20;
		ComboBox1->Left = 50; ComboBox1->Top = Label1->Top+20;
		Button1->Top = ComboBox1->Top + 50; Button1->Left = 60;
		Button1->Width = ComboBox1->Width - 20;
		Form6->Width = 320; Form6->Height = Button1->Top+120;
	}
	else if (!CompareText(AnsiString("Вывод филармоний данного типа"),AnsiString(Form1->ComboBox4->Text)))
	{
		Edit1->Visible = true;  Label1->Visible = true;
		Button1->Visible = true;
		ADOQuery1->SQL->Clear();
		ADOQuery1->SQL->Text = "SELECT ORDINAL_POSITION,COLUMN_NAME,DATA_TYPE,CHARACTER_MAXIMUM_LENGTH,IS_NULLABLE,COLUMN_DEFAULT FROM INFORMATION_SCHEMA.COLUMNS WHERE (TABLE_NAME = '"+Form1->ComboBox1->Text+"') ORDER BY ORDINAL_POSITION ASC;";
		ADOQuery1->Open();
		ADOQuery1->First();
		selected = Form1->DBGrid1->DataSource->DataSet->FieldByName(ADOQuery1->FieldByName("COLUMN_NAME")->AsString)->AsAnsiString;
		ADOQuery1->SQL->Clear();
		ADOQuery1->SQL->Text = "SELECT * FROM ["+Form1->ComboBox1->Text+"] WHERE [Название филармонии] = '"+selected+"'";
		ADOQuery1->Open();
		ADOQuery1->First();
		Edit1->Text = ADOQuery1->FieldByName("Название филармонии")->AsString;
		Button1->Caption = "Принять изменения";
		Label1->Caption = "Название филармонии";
		Label10->Caption = "Смена названия филармонии"; Label10->Top = 20;
		Label10->Left = 50;
		Label1->Top = Label10->Top+40; Label1->Left = Label10->Left+20;
		Edit1->Left = 50; Edit1->Top = Label1->Top+20; Edit1->Width = 200;
		Button1->Top = Edit1->Top + 50; Button1->Left = 60;
		Button1->Width = Edit1->Width + 20;
		Form6->Width = 320; Form6->Height = Button1->Top+120;
	}
	else if (!CompareText(AnsiString("Вывод списка мероприятий"),AnsiString(Form1->ComboBox4->Text)))
	{
		Edit1->Visible = true;  Label1->Visible = true;
		Edit2->Visible = true;  Label2->Visible = true;
		Edit3->Visible = true;  Label3->Visible = true;
		Edit4->Visible = true;  Label4->Visible = true;
		Button1->Visible = true;
		ADOQuery1->SQL->Clear();
		ADOQuery1->SQL->Text = "SELECT ORDINAL_POSITION,COLUMN_NAME,DATA_TYPE,CHARACTER_MAXIMUM_LENGTH,IS_NULLABLE,COLUMN_DEFAULT FROM INFORMATION_SCHEMA.COLUMNS WHERE (TABLE_NAME = '"+Form1->ComboBox1->Text+"') ORDER BY ORDINAL_POSITION ASC;";
		ADOQuery1->Open();
		ADOQuery1->First();
		selected = Form1->DBGrid1->DataSource->DataSet->FieldByName(ADOQuery1->FieldByName("COLUMN_NAME")->AsString)->AsAnsiString;
		ADOQuery1->SQL->Clear();
		ADOQuery1->SQL->Text = "SELECT * FROM ["+Form1->ComboBox1->Text+"] WHERE [Название мероприятия] = '"+selected+"'";
		ADOQuery1->Open();
		ADOQuery1->First();
		Edit1->Text = ADOQuery1->FieldByName("Название мероприятия")->AsString;
		Edit2->Text = ADOQuery1->FieldByName("Дата проведения")->AsString;
		Edit3->Text = ADOQuery1->FieldByName("Награды")->AsString;
		Edit4->Text = ADOQuery1->FieldByName("Ожидаемое число зрителей")->AsString;
		Button1->Caption = "Принять изменения";
		Label1->Caption = "Название мероприятия";
		Label2->Caption = "Дата проведения";
		Label3->Caption = "Награды";
		Label4->Caption = "Ожидаемое число зрителей";
		Label10->Caption = "Смена информации о мероприятии";
		Label10->Top = 20; Label10->Left = 50; Label4->Width = 220;
		Label1->Top = Label10->Top+40; Label1->Left = Label10->Left+20;
		Edit1->Left = 50; Edit1->Top = Label1->Top+20; Edit1->Width = 200;
		Label2->Top = Edit1->Top+30; Label2->Left = Label1->Left;
		Edit2->Left = Edit1->Left; Edit2->Top = Label2->Top+20;
		Label3->Top = Edit2->Top+30; Label3->Left = Label2->Left;
		Edit3->Left = Edit2->Left; Edit3->Top = Label3->Top+20;
		Label4->Top = Edit3->Top+30; Label4->Left = Label3->Left - 40;
		Edit4->Left = Edit3->Left; Edit4->Top = Label4->Top+20;
		Button1->Top = Edit4->Top + 50; Button1->Left = 60;
		Button1->Width = Edit4->Width + 20;
		Form6->Width = 320; Form6->Height = Button1->Top+120;
	}
}
//---------------------------------------------------------------------------
void __fastcall TForm6::Button1Click(TObject *Sender)
{
	if (!CompareText(AnsiString("Добавить нового артиста"),AnsiString(Form1->ComboBox3->Text)))
	{
		ADOQuery1->SQL->Text = "exec addition '"+Edit1->Text+"','"+Edit2->Text+"','"+Edit3->Text+"','"+Edit4->Text+"'";
		ADOQuery1->ExecSQL();
		Form1->ADOTable2->TableName = "Artist";
		Form1->ADOTable2->Open();
	}
	else if (!CompareText(AnsiString("Изменить импресарио у данного артиста"),AnsiString(Form1->ComboBox3->Text)))
	{
		ADOQuery1->SQL->Clear();
		AnsiString full_text = ComboBox1->Text;
		AnsiString art[3],old_imp[3],new_imp[3];
		art[0] = strtok(full_text.c_str(),separator);
		art[1] = strtok(0,separator);
		art[2] = strtok(0,separator);
		full_text = ComboBox2->Text;
		old_imp[0] = strtok(full_text.c_str(),separator);
		old_imp[1] = strtok(0,separator);
		old_imp[2] = strtok(0,separator);
		full_text = ComboBox3->Text;
		new_imp[0] = strtok(full_text.c_str(),separator);
		new_imp[1] = strtok(0,separator);
		new_imp[2] = strtok(0,separator);
		ADOQuery1->SQL->Text = "exec upd '"+art[0]+"','"+art[1]+"','"+art[2]+"','"+old_imp[0]+"','"+old_imp[1]+"','"+old_imp[2]+"','"+new_imp[0]+"','"+new_imp[1]+"','"+new_imp[2]+"'";
		ADOQuery1->ExecSQL();
	}
	else if (!CompareText(AnsiString("Вывод артистов, работающих в данном жанре"),AnsiString(Form1->ComboBox3->Text)))
	{
		ADOQuery1->SQL->Clear();
		ADOQuery1->SQL->Text = "exec viborka_genre '"+ComboBox1->Text+"'";
		ADOQuery1->Open();
		Form1->DBGrid2->DataSource->DataSet = (Form6->ADOQuery1);
	}
	else if (!CompareText(AnsiString("Вывод филармоний данного типа"),AnsiString(Form1->ComboBox4->Text)))
	{
		ADOQuery1->SQL->Clear();
		ADOQuery1->SQL->Text = "UPDATE [Вывод филармоний данного типа] SET [Название филармонии] = '"+Edit1->Text+"' WHERE [Название филармонии] = '"+selected+"'";
		ADOQuery1->ExecSQL();
	}
	else if (!CompareText(AnsiString("Вывод списка мероприятий"),AnsiString(Form1->ComboBox4->Text)))
	{
		ADOQuery1->SQL->Clear();
		ADOQuery1->SQL->Text = "UPDATE [Вывод списка мероприятий] SET [Название мероприятия] = '"+Edit1->Text+"', [Дата проведения] = '"+Edit2->Text+"', [Награды] = '"+Edit3->Text+"', [Ожидаемое число зрителей] = '"+Edit4->Text+"' WHERE [Название мероприятия] = '"+selected+"'";
		ADOQuery1->ExecSQL();
	}
}
//---------------------------------------------------------------------------
void __fastcall TForm6::FormClose(TObject *Sender, TCloseAction &Action)
{
		Form1->ComboBox3->Text = "";
		Form1->ComboBox4->Text = "";
		Button1->Visible = false;
		ComboBox1->Visible = false;  Label1->Visible = false;  Edit1->Visible = false;
		ComboBox2->Visible = false;  Label2->Visible = false;  Edit2->Visible = false;
		ComboBox3->Visible = false;  Label3->Visible = false;  Edit3->Visible = false;
		Label4->Visible = false;  Edit4->Visible = false;
}
//---------------------------------------------------------------------------
