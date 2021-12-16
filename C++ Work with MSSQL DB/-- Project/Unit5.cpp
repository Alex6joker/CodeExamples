//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit5.h"
#include "Unit1.h"
#include <string.h>
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TForm5 *Form5;
const char separator[]=" \.,";
//---------------------------------------------------------------------------
__fastcall TForm5::TForm5(TComponent* Owner)
	: TForm(Owner)
{
}
//---------------------------------------------------------------------------
void __fastcall TForm5::FormActivate(TObject *Sender)
{
	Form1->DBGrid2->DataSource->DataSet = NULL;
	Form5->Left = (Screen->Width/3);
	Form5->Top = (Screen->Height/8);
	ComboBox1->Clear(); Label1->Caption = "";  Edit1->Text = "";
	Label2->Caption = "";  Edit2->Text = "";
	Label3->Caption = "";
	Label1->Width = 155; Label2->Width = 155;  Label3->Width = 155; Label4->Width = 155;
	Form5->Width = 320; ComboBox1->Width = 200;
	ADOQuery1->Connection = Form1->ADOConnection1;
	ADOQuery1->SQL->Clear();
	if (!CompareText(AnsiString("Вывод импресарио артиста"),AnsiString(Form1->ComboBox2->Text)))
	{
		ADOQuery1->SQL->Text = "SELECT Name,Middle_Name,Family FROM Artist";
		ADOQuery1->Open();
		ADOQuery1->First();
		ComboBox1->Visible = true;  Label1->Visible = true;
		Label1->Caption = "ФИО артиста";
		while(!ADOQuery1->Eof)
		{
			ComboBox1->Items->Add(AnsiString(ADOQuery1->FieldByName("Name")->AsString)+" "+AnsiString(ADOQuery1->FieldByName("Middle_Name")->AsString)+" "+AnsiString(ADOQuery1->FieldByName("Family")->AsString));
			ADOQuery1->Next();
		}
		Button1->Visible = true;
		Button1->Caption = "Найти импресарио";
		Label4->Caption = "Вывод импресарио артиста"; Label4->Top = 20;
		Label4->Left = 50; Label1->Top = Label4->Top+40; Label1->Left = Label4->Left+20;
		ComboBox1->Left = 50; ComboBox1->Top = Label1->Top+20;
		Button1->Top = ComboBox1->Top + 50; Button1->Left = 60;
		Button1->Width = ComboBox1->Width - 20;
		Form5->Height = Button1->Top+120;
	}
	else if (!CompareText(AnsiString("Выбор артистов работающих в данной филармонии"),AnsiString(Form1->ComboBox2->Text)))
	{
		ADOQuery1->SQL->Text = "SELECT Name FROM Philharmonic";
		ADOQuery1->Open();
		ADOQuery1->First();
		ComboBox1->Visible = true;  Label1->Visible = true;
		Label1->Caption = "Название филармонии";
		while(!ADOQuery1->Eof)
		{
			ComboBox1->Items->Add(ADOQuery1->FieldByName("Name")->AsString);
			ADOQuery1->Next();
		}
		Button1->Visible = true;
		Button1->Caption = "Поиск артистов";
		Label4->Caption = "Выбор артистов работающих в данной филармонии"; Label4->Top = 20;
		Label4->Left = 50; Label1->Top = Label4->Top+40; Label1->Left = Label4->Left+20;
		ComboBox1->Left = 50; ComboBox1->Top = Label1->Top+20;
		Button1->Top = ComboBox1->Top + 50; Button1->Left = 60;
		Button1->Width = ComboBox1->Width - 20;
		Form5->Height = Button1->Top+120;
	}
	else if (!CompareText(AnsiString("Вывод организатора данного события"),AnsiString(Form1->ComboBox2->Text)))
	{
		ADOQuery1->SQL->Text = "SELECT Name FROM Event";
		ADOQuery1->Open();
		ADOQuery1->First();
		ComboBox1->Visible = true;   Label1->Visible = true;
		Label1->Caption = "Мероприятие";
		while(!ADOQuery1->Eof)
		{
			ComboBox1->Items->Add(ADOQuery1->FieldByName("Name")->AsString);
			ADOQuery1->Next();
		}
		Button1->Visible = true;
		Button1->Caption = "Поиск организатора";
		Label4->Caption = "Вывод организатора данного события"; Label4->Top = 20;
		Label4->Left = 50; Label1->Top = Label4->Top+40; Label1->Left = Label4->Left+20;
		ComboBox1->Left = 50; ComboBox1->Top = Label1->Top+20;
		Button1->Top = ComboBox1->Top + 50; Button1->Left = 60;
		Button1->Width = ComboBox1->Width - 20;
		Form5->Height = Button1->Top+120;
	}
	else if (!CompareText(AnsiString("Список судей на мероприятии"),AnsiString(Form1->ComboBox2->Text)))
	{
		ADOQuery1->SQL->Text = "SELECT Name FROM Event";
		ADOQuery1->Open();
		ADOQuery1->First();
		ComboBox1->Visible = true; Label1->Visible = true;
		while(!ADOQuery1->Eof)
		{
			ComboBox1->Items->Add(ADOQuery1->FieldByName("Name")->AsString);
			ADOQuery1->Next();
		}
		Button1->Visible = true;
		Button1->Caption = "Поиск судей";
		Label4->Caption = "Список судей на мероприятии"; Label4->Top = 20;
		Label4->Left = 50; Label1->Top = Label4->Top+40; Label1->Left = Label4->Left+20;
		ComboBox1->Left = 50; ComboBox1->Top = Label1->Top+20;
		Button1->Top = ComboBox1->Top + 50; Button1->Left = 60;
		Button1->Width = ComboBox1->Width - 20;
		Form5->Height = Button1->Top+120;
	}
	else if (!CompareText(AnsiString("Вывод мероприятий данного типа за данный промежуток времени"),AnsiString(Form1->ComboBox2->Text)))
	{
		ADOQuery1->SQL->Text = "SELECT Type_Name FROM Event_Type";
		ADOQuery1->Open();
		ADOQuery1->First();
		ComboBox1->Visible = true;  Label1->Visible = true; Label2->Visible = true;
		Edit1->Visible = true;  	Edit2->Visible = true;  Label3->Visible = true;
		Label1->Caption = "Тип мероприятия";
		Label2->Caption = "Дата начала поиска";
		Label3->Caption = "Конечная дата поиска";
		while(!ADOQuery1->Eof)
		{
			ComboBox1->Items->Add(ADOQuery1->FieldByName("Type_Name")->AsString);
			ADOQuery1->Next();
		}
		Button1->Visible = true;
		Button1->Caption = "Вывести мероприятия";
		Label4->Caption = "Вывод мероприятий данного типа за данный промежуток времени";
		Label4->Top = 20;
		Label4->Left = 50; Label1->Top = Label4->Top+80; Label1->Left = Label4->Left+20;
		ComboBox1->Left = 50; ComboBox1->Top = Label1->Top+20;
		Label2->Top = ComboBox1->Top+30; Label2->Left = Label1->Left;
		Edit1->Left = ComboBox1->Left; Edit1->Top = Label2->Top+20;
		Label3->Top = Edit1->Top+30; Label3->Left = Label2->Left;
		Edit2->Left = Edit1->Left; Edit2->Top = Label3->Top+20;
		Button1->Top = Edit2->Top + 50; Button1->Left = 10;
		Button1->Width = Edit2->Width + 100;
		Form5->Height = Button1->Top+120;
	}
}
//---------------------------------------------------------------------------
void __fastcall TForm5::Button1Click(TObject *Sender)
{
	if (!CompareText(AnsiString("Вывод импресарио артиста"),AnsiString(Form1->ComboBox2->Text)))
	{
		ADOQuery1->SQL->Clear();
		AnsiString full_text = ComboBox1->Text;
		AnsiString art[3];
		art[0] = strtok(full_text.c_str(),separator);
		art[1] = strtok(0,separator);
		art[2] = strtok(0,separator);
		ADOQuery1->SQL->Text = "SELECT c.Name as 'Имя артиста', c.Middle_Name as 'Отчество артиста', c.Family as 'Фамилия артиста', a.Name as 'Имя импресарио', a.Middle_name as 'Отчество импресарио', a.Family as 'Фамилия импресарио' FROM dbo.Impresario AS a, dbo.Artist_with_Impresario AS b, dbo.Artist as c WHERE (c.Name = '"+art[0]+"') AND (c.Middle_Name = '"+art[1]+"') AND (c.Family = '"+art[2]+"') AND (c.ID_Artist = b.ID_Artist) AND (b.ID_Impresario = a.ID_Impresario)";
		ADOQuery1->Open();
		Form1->DBGrid2->DataSource->DataSet = (Form5->ADOQuery1);
	}
	else if (!CompareText(AnsiString("Выбор артистов работающих в данной филармонии"),AnsiString(Form1->ComboBox2->Text)))
	{
		ADOQuery1->SQL->Clear();
		ADOQuery1->SQL->Text = "SELECT a.Name AS [Имя артиста], a.Middle_name AS [Отчество артиста], a.Family AS [Фамилия артиста], b.Name as [Название филармонии], b.Address as [Адрес] FROM dbo.Artist as a, dbo.Philharmonic as b WHERE (b.Name = '"+ComboBox1->Text+"') AND (b.ID_Philharmonic = a.ID_Philharmonic)";
		ADOQuery1->Open();
		Form1->DBGrid2->DataSource->DataSet = (Form5->ADOQuery1);
	}
	else if (!CompareText(AnsiString("Вывод организатора данного события"),AnsiString(Form1->ComboBox2->Text)))
	{
		ADOQuery1->SQL->Clear();
		ADOQuery1->SQL->Text = "SELECT a.Family as [Фамилия организатора], a.Name as [Имя организатора], a.Middle_name as [Отчество организатора], b.Name as [Название мероприятия] FROM dbo.Organizer AS a, dbo.Event AS b WHERE (b.Name = '"+ComboBox1->Text+"') AND (b.ID_Organizer = a.ID_Organizer)";
		ADOQuery1->Open();
		Form1->DBGrid2->DataSource->DataSet = (Form5->ADOQuery1);
	}
	else if (!CompareText(AnsiString("Список судей на мероприятии"),AnsiString(Form1->ComboBox2->Text)))
	{
		ADOQuery1->SQL->Clear();
		ADOQuery1->SQL->Text = "SELECT DISTINCT  b.family as [Фамилия], b.name as [Имя], b.Middle_name as [Отчество], c.name as [Мероприятие] FROM dbo.Judge_on_Event_for_Artist as a inner join dbo.judge as b on (a.ID_judge = b.ID_judge)	inner join dbo.event as c on (c.ID_event = a.ID_event) WHERE (c.name = '"+ComboBox1->Text+"') GROUP BY a.ID_Judge, c.ID_event, c.name, b.family, b.name, b.Middle_name";
		ADOQuery1->Open();
		Form1->DBGrid2->DataSource->DataSet = (Form5->ADOQuery1);
	}
	else if (!CompareText(AnsiString("Вывод мероприятий данного типа за данный промежуток времени"),AnsiString(Form1->ComboBox2->Text)))
	{
		ADOQuery1->SQL->Clear();
		ADOQuery1->SQL->Text = "SELECT b.Type_Name as [Название типа],a.Name as [Название мероприятия], a.[Date-Time] as [Время мероприятия] FROM dbo.event as a, dbo.Event_type as b WHERE (a.[Date-Time] >= '"+Edit1->Text+"' and a.[Date-Time] < '"+Edit2->Text+"') and (b.Type_Name = '"+ComboBox1->Text+"') and (a.ID_Event_Type = b.ID_Event_Type) GROUP BY b.ID_Event_Type, b.Type_Name, a.ID_event, a.Name, a.[Date-Time]";
		ADOQuery1->Open();
		Form1->DBGrid2->DataSource->DataSet = (Form5->ADOQuery1);
    }
}
//---------------------------------------------------------------------------
void __fastcall TForm5::FormClose(TObject *Sender, TCloseAction &Action)
{
		Form1->ComboBox2->Text = "";
		Button1->Visible = false;
		ComboBox1->Visible = false; Label1->Visible = false; Edit1->Visible = false;
		Label2->Visible = false; Edit2->Visible = false;
		Label3->Visible = false;
}
//---------------------------------------------------------------------------
