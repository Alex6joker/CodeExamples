//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit1.h"
#include <stdio.h>
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TForm1 *Form1;
// смена букв в биграммах
struct s1
{
	char name[2];
	int frequency;
}s[1024];
//---------------------------------------------------------------------------
__fastcall TForm1::TForm1(TComponent* Owner)
	: TForm(Owner)
{
}
//---------------------------------------------------------------------------
void __fastcall TForm1::Button1Click(TObject *Sender)
{
    if(!OpenDialog1->Execute()) return;
	FILE* f0 = fopen(((AnsiString)OpenDialog1->FileName).c_str(),"r");
	int counter = 0;
	while(!feof(f0))
	{
		char c;
		fscanf(f0,"%s",s[counter].name);
		fscanf(f0,"%c",&c);
		fscanf(f0,"%d",&s[counter].frequency);
		fscanf(f0,"%c",&c);
		counter++;
	}
	fclose(f0);
	for(int i = 0; i < counter; i++)
	{
		char c = s[i].name[1];
		s[i].name[1] = s[i].name[0];
		s[i].name[0] = c;
	}
	f0 = fopen("reverse_file.txt","w");
	for(int i = 0; i < counter; i++)
	{
		fprintf(f0,"%s%c%d%c",s[i].name,'\t',s[i].frequency,'\n');
	}
	fclose(f0);
}
//---------------------------------------------------------------------------

void __fastcall TForm1::Button2Click(TObject *Sender)
{
	if(!OpenDialog1->Execute()) return;
	FILE* f0 = fopen(((AnsiString)OpenDialog1->FileName).c_str(),"r");
	int counter = 0;
	while(!feof(f0))
	{
		char c;
		fscanf(f0,"%s",s[counter].name);
		fscanf(f0,"%c",&c);
		fscanf(f0,"%d",&s[counter].frequency);
		fscanf(f0,"%c",&c);
		counter++;
	}
	fclose(f0);
	for(int i = 0; i < counter; i++)
	{
		for(int j = 0; j < counter; j++)
			if(s[i].frequency > s[j].frequency)
			{
				char* n = "  ";
				strcpy(n,s[j].name);
				int fr = s[j].frequency;
				s[j].frequency = s[i].frequency;
				strcpy(s[j].name,s[i].name);
				s[i].frequency = fr;
				strcpy(s[i].name,n);
			}
	}
	f0 = fopen("sort_file.txt","w");
	for(int i = 0; i < counter; i++)
	{
		fprintf(f0,"%s%c%d%c",s[i].name,'\t',s[i].frequency,'\n');
	}
	fclose(f0);
}
//---------------------------------------------------------------------------

void __fastcall TForm1::Button3Click(TObject *Sender)
{
	if(!OpenDialog1->Execute()) return;
	FILE* f0 = fopen(((AnsiString)OpenDialog1->FileName).c_str(),"r");
	if(!OpenDialog1->Execute()) return;
	FILE* f1 = fopen(((AnsiString)OpenDialog1->FileName).c_str(),"r");
    FILE* f2 = fopen("1.txt","w");
	char str[1024];
	while(!feof(f0))
	{
		char c;
		fscanf(f0,"%s",str);
		do fscanf(f0,"%c",&c); while(c!='\n');
		fseek(f1,0,SEEK_SET);
		bool in_file_1 = false; // Уже есть в файле 1
		while(!feof(f1))
		{
			char str2[1024];
			fscanf(f1,"%s",str2);
			do fscanf(f1,"%c",&c); while(c!='\n');
			if(!strcmp(str,str2))
			{
				in_file_1 = true;
				break;
			}
		}
		if(!in_file_1) fprintf(f2,"%s%c",str,'\n');
	}
	fclose(f0);
	fclose(f2);
	fseek(f1,0,SEEK_SET);
    while(!feof(f1))
	{
			char str2[1024];
			char c;
			fscanf(f1,"%s",str2);
			do fscanf(f1,"%c",&c); while(c!='\n');
			fprintf(f2,"%s%c",str,'\n');
	}
	fclose(f1);
	fclose(f2);
}
//---------------------------------------------------------------------------

