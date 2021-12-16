//---------------------------------------------------------------------------
struct frequency_of_k_gramms_in_text
{
	AnsiString k_gramm; // Считанная из текста буква
	int frequency; // Частота появления в анализируемом тексте
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
	int polygramm; // Разрядность текущей k-граммы
	int click_counter; // Считает кол-во нажатий на клавишу расшифровки
	int count; // кол-во k-грамм
	int dictionary_count; // кол-во слов в словаре
	char** k_gramms; // двумерный массив k-грамм
	float current_decode_percent; // Процент расшифровки текста
	int change_group[2]; // Пара символов в частоном анализе, короые мы меняем между собой
	int right_words; // Количетво правильно расшифрованных слов (совпавших со словарем)
	int Find_and_write_k_gramm(char*,frequency_of_k_gramms_in_text*,char**,int,int);
	float get_decode_percent(char*); // 2-й параметр для проверки
	bool match_the_language(char*,int);
	AnsiString* dictionary_words; // Хранит все слова из словаря
public:		// User declarations
	__fastcall TForm1(TComponent* Owner);
	float final_percent; // Конечный вещественный процент расшифрованного текста
	char upcase_letters_of_the_current_language[2]; // Содержит в себе заглавные буквы текущего языка
	char lowercase_letters_of_the_current_language[2]; // Содержит в себе строчные буквы текущего языка
};
//---------------------------------------------------------------------------
extern PACKAGE TForm1 *Form1;
//---------------------------------------------------------------------------
#endif
