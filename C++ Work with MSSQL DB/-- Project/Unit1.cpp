//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit1.h"
#include "Unit2.h"
#include "Unit3.h"
#include "Unit4.h"
#include "Unit5.h"
#include "Unit6.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TForm1 *Form1;
//---------------------------------------------------------------------------
__fastcall TForm1::TForm1(TComponent* Owner)
	: TForm(Owner)
{
	Form1->Width = Screen->Width;
	Form1->Height = Screen->Height;
	Label1->Left = 5;
	Label1->Top = 15;
	ComboBox1->Clear();
	// Заполняем выпадающий список таблицами, к которым имеем доступ
	ADOConnection1->GetTableNames(ComboBox1->Items, false);
	DBGrid1->ReadOnly = true;
	DBGrid1->Enabled = false;
	Image1->Enabled = false;
	Image2->Enabled = false;
	Image3->Enabled = false;
	Image4->Enabled = false;
	ComboBox4->Enabled = false;
	// Расположение объектов на форме
	DBGrid1->Width = 900;
	DBGrid1->Height = 350;
	DBGrid1->Left = 0;
	DBGrid1->Top = 40;
	DBGrid2->ReadOnly = true;
	DBGrid2->Enabled = false;
	DBGrid2->Width = 1300;
	DBGrid2->Height = 300;
	DBGrid2->Left = 0;
	DBGrid2->Top = 420;
	Label4->Left = DBGrid1->Width + 60;
	Label4->Top = 50;
	Label4->Caption = "Работа с таблицами";
	Image1->Left = DBGrid1->Width + 80;
	Image1->Top = 100;
	Image3->Left = Image1->Left + 80;
	Image3->Top = Image1->Top;
	Image4->Left = Image3->Left + 80;
	Image4->Top = Image3->Top;
	Label2->Left = Image1->Left + 20;
	Label2->Top = Image1->Top + 180;
	Label5->Left = Image1->Left + 20;
	Label5->Top = Image1->Top + 100;
	ComboBox2->Clear();
	ComboBox2->Items->Add("Вывод импресарио артиста");
	ComboBox2->Items->Add("Выбор артистов работающих в данной филармонии");
	ComboBox2->Items->Add("Вывод организатора данного события");
	ComboBox2->Items->Add("Список судей на мероприятии");
	ComboBox2->Items->Add("Вывод мероприятий данного типа за данный промежуток времени");
	ComboBox2->Left = Label2->Left - 60;
	ComboBox2->Top = Label2->Top + 20;
	Label3->Left = Image1->Left + 20;
	Label3->Top = Image1->Top + 230;
	ComboBox3->Clear();
	ComboBox3->Items->Add("Добавить нового артиста");
	ComboBox3->Items->Add("Изменить импресарио у данного артиста");
	ComboBox3->Items->Add("Вывод артистов, работающих в данном жанре");
	ComboBox3->Left = Label3->Left - 60;
	ComboBox3->Top = Label3->Top + 20;
	ComboBox3->Width = ComboBox2->Width;
	ComboBox4->Clear();
	ComboBox4->Items->Add("Вывод филармоний данного типа");
	ComboBox4->Items->Add("Вывод списка мероприятий");
	ComboBox4->Left = Label5->Left - 60;
	ComboBox4->Top = Label5->Top + 20;
	ComboBox4->Width = ComboBox2->Width;
}
//---------------------------------------------------------------------------
void __fastcall TForm1::ComboBox1Change(TObject *Sender)
{   // Если какая-нибудь таблица уже открыта, то закрываем таблицу
	if(ADOTable1->TableName != "")
	{
		ADOTable1->Close();
	}
	else
	{
		DBGrid1->Enabled = true;
		Image1->Enabled = true;
		Image2->Enabled = true;
		Image3->Enabled = true;
		Image4->Enabled = true;
	}
	ADOTable1->TableName = ComboBox1->Text;
	ADOTable1->Open();
	if(strstr(AnsiString(ComboBox1->Text).c_str(),"Выбор")!=NULL || strstr(AnsiString(ComboBox1->Text).c_str(),"Вывод")!=NULL)
	{
		Image1->Enabled = false;
		Image3->Enabled = false;
		Image4->Enabled = false;
		ComboBox4->Enabled = true;
	}
	else
	{
		Image1->Enabled = true;
		Image3->Enabled = true;
		Image4->Enabled = true;
		ComboBox4->Enabled = false;
    }
}
//---------------------------------------------------------------------------
void __fastcall TForm1::Image1Click(TObject *Sender)
{
	Application->CreateForm(__classid(TForm2), &Form2);
	Form2->ShowModal();
	Form2->Free();
}
//---------------------------------------------------------------------------
void __fastcall TForm1::Image2Click(TObject *Sender)
{
	// Если какая-нибудь таблица уже открыта, то закрываем таблицу
	if(ADOTable1->TableName != "")
	{
		ADOTable1->Close();
	}
	ADOTable1->TableName = ComboBox1->Text;
	ADOTable1->Open();
}
//---------------------------------------------------------------------------
void __fastcall TForm1::Image3Click(TObject *Sender)
{
	Application->CreateForm(__classid(TForm3), &Form3);
	Form3->ShowModal();
	Form3->Free();
}
//---------------------------------------------------------------------------
void __fastcall TForm1::Image4Click(TObject *Sender)
{
	Application->CreateForm(__classid(TForm4), &Form4);
	Form4->ShowModal();
	Form4->Free();
}
//---------------------------------------------------------------------------
void __fastcall TForm1::ComboBox2Change(TObject *Sender)
{
	Application->CreateForm(__classid(TForm5), &Form5);
	Form5->ShowModal();
	Form5->Free();
}
//---------------------------------------------------------------------------
void __fastcall TForm1::ComboBox3Change(TObject *Sender)
{
	Application->CreateForm(__classid(TForm6), &Form6);
	Form6->ShowModal();
	Form6->Free();
}
//---------------------------------------------------------------------------

void __fastcall TForm1::ComboBox4Change(TObject *Sender)
{
	if(ComboBox4->Text == ComboBox1->Text)
	{
		Application->CreateForm(__classid(TForm6), &Form6);
		Form6->ShowModal();
		Form6->Free();
	}
}
//---------------------------------------------------------------------------

