//---------------------------------------------------------------------------
struct frequency_of_k_gramms_in_text
{
	AnsiString k_gramm; // ��������� �� ������ �����
	int frequency; // ������� ��������� � ������������� ������
}*text_k_gramms;
#ifndef Unit1H
#define Unit1H
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <Dialogs.hpp>
#include <Menus.hpp>
#include <stdio.h>
#include <string.h>
#include <math.h>
#include <time.h>
//---------------------------------------------------------------------------
class TForm1 : public TForm
{
__published:	// IDE-managed Components
	TMainMenu *MainMenu1;
	TOpenDialog *OpenDialog1;
	TSaveDialog *SaveDialog1;
	TMenuItem *A1;
	TMenuItem *N1;
	TMenuItem *N2;
	TMenuItem *N3;
	TMenuItem *N4;
	TMemo *Memo1;
	TMemo *Memo2;
	TButton *Button1;
	TMemo *Memo3;
	TMenuItem *N5;
	TMenuItem *N7;
	TMenuItem *N21;
	void __fastcall N2Click(TObject *Sender);
	void __fastcall N3Click(TObject *Sender);
	void __fastcall N4Click(TObject *Sender);
	void __fastcall Button1Click(TObject *Sender);
	void __fastcall FormCreate(TObject *Sender);
	void __fastcall N7Click(TObject *Sender);
	void __fastcall N21Click(TObject *Sender);
private:	// User declarations
	int polygramm; // ����������� ������� k-������
	int click_counter; // ������� ���-�� ������� �� ������� �����������
	int count; // ���-�� k-�����
	int dictionary_count; // ���-�� ���� � �������
	char** k_gramms; // ��������� ������ k-�����
	float current_decode_percent; // ������� ����������� ������
	int change_group[2]; // ���� �������� � �������� �������, ������ �� ������ ����� �����
	int right_words; // ��������� ��������� �������������� ���� (��������� �� ��������)
	int Find_and_write_k_gramm(char*,frequency_of_k_gramms_in_text*,char**,int,int);
	float get_decode_percent(char*); // 2-� �������� ��� ��������
	bool match_the_language(char*,int);
	AnsiString* dictionary_words; // ������ ��� ����� �� �������
public:		// User declarations
	__fastcall TForm1(TComponent* Owner);
	float final_percent; // �������� ������������ ������� ��������������� ������
	char upcase_letters_of_the_current_language[2]; // �������� � ���� ��������� ����� �������� �����
	char lowercase_letters_of_the_current_language[2]; // �������� � ���� �������� ����� �������� �����
};
//---------------------------------------------------------------------------
extern PACKAGE TForm1 *Form1;
//---------------------------------------------------------------------------
#endif
