//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit4.h"
#include "Unit1.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TForm4 *Form4;
AnsiString selected_ID;
AnsiString ID_Column;
//---------------------------------------------------------------------------
__fastcall TForm4::TForm4(TComponent* Owner)
	: TForm(Owner)
{
}
// Удаление данных SQL DELETE
void __fastcall TForm4::FormActivate(TObject *Sender)
{
	Form4->Left = (Screen->Width/3);
	Form4->Top = (Screen->Height/8);
	ADOQuery1->Connection = Form1->ADOConnection1;
	ADOQuery1->SQL->Clear();
	// Выполняем запрос, который возратит информацию о столбцах таблицы
	ADOQuery1->SQL->Text = "SELECT ORDINAL_POSITION,COLUMN_NAME,DATA_TYPE,CHARACTER_MAXIMUM_LENGTH,IS_NULLABLE,COLUMN_DEFAULT FROM INFORMATION_SCHEMA.COLUMNS WHERE (TABLE_NAME = '"+Form1->ComboBox1->Text+"') ORDER BY ORDINAL_POSITION ASC;";
	ADOQuery1->Open();
	ADOQuery1->First();
	selected_ID = Form1->DBGrid1->DataSource->DataSet->FieldByName(ADOQuery1->FieldByName("COLUMN_NAME")->AsString)->AsAnsiString;
	ID_Column = ADOQuery1->FieldByName("COLUMN_NAME")->AsString;
}
//---------------------------------------------------------------------------
void __fastcall TForm4::Button2Click(TObject *Sender)
{
	Form4->Close();
}
//---------------------------------------------------------------------------
void __fastcall TForm4::Button1Click(TObject *Sender)
{
	ADOQuery2->Connection = Form1->ADOConnection1;
	ADOQuery2->SQL->Clear();
	ADOQuery2->SQL->Text = "DELETE FROM "+Form1->ComboBox1->Text+" WHERE "+ ID_Column+ " = "+selected_ID;
	int err = ADOQuery2->ExecSQL();
	//Если какая-нибудь таблица уже открыта, то закрываем таблицу
	if(Form1->ADOTable1->TableName != "")
	{
		Form1->ADOTable1->Close();
	}
	Form1->ADOTable1->TableName = Form1->ComboBox1->Text;
	Form1->ADOTable1->Open();
	Form4->Close();
}
//---------------------------------------------------------------------------
