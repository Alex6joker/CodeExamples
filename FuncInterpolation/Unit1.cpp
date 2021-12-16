//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit1.h"
#include "Unit2.h"
#include "Unit3.h"
#include "Global.h"

#include "DeCastelie.h"
#include "Hermit.h"
#include "Bernstain.h"
#include "Linear.h"
#include "DeCastelieOld.h"
#include "Lagrange.h"
#include "Newton.h"

#include <math.h>
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TForm1 *Form1;
void DeleteDot();
void AddAllDots();
void AddDot(int,int,TColor,int);
void DrawGrid();
void Clear();
int NearDot(int);
void DrawCurrentFunction();

void DoZoom();

int Mode = 0;

	struct {
		int X;
		int Y;
	} MousePos,BgOffset;

	struct  {
		int Width;
		int Height;
	} TrueSize;

bool FlagDrag = false;
bool FirstResize = true;
bool BgActive = false;
int DragIndex = 0;
int LastSelectIndex = 0;

//---------------------------------------------------------------------------
__fastcall TForm1::TForm1(TComponent* Owner)
	: TForm(Owner)
{

	BgOffset.X = BgOffset.Y = 0;
	Form1->StringGrid1->Cells[0][0] = "N";
	Form1->StringGrid1->Cells[1][0] = "X";
	Form1->StringGrid1->Cells[2][0] = "Y";

	Form1->Canvas->Brush->Color = BgColor;
	ImageColor1->Canvas->Rectangle(-1,-1,ImageColor1->Width+1,ImageColor1->Height+1);
	ImageColor2->Canvas->Rectangle(-1,-1,ImageColor1->Width+1,ImageColor1->Height+1);
	ImageColor3->Canvas->Rectangle(-1,-1,ImageColor1->Width+1,ImageColor1->Height+1);
	ImageColor4->Canvas->Rectangle(-1,-1,ImageColor1->Width+1,ImageColor1->Height+1);
	ImageColor5->Canvas->Rectangle(-1,-1,ImageColor1->Width+1,ImageColor1->Height+1);
	ImageColor6->Canvas->Rectangle(-1,-1,ImageColor1->Width+1,ImageColor1->Height+1);

	Form1->ImageColor1->Canvas->Brush->Color = LinearColor;
	ImageColor1->Canvas->Ellipse(2,2,ImageColor1->Width-2,ImageColor1->Height-2);

	Form1->ImageColor2->Canvas->Brush->Color = HermitColor;
	ImageColor2->Canvas->Ellipse(2,2,ImageColor1->Width-2,ImageColor1->Height-2);

	Form1->ImageColor3->Canvas->Brush->Color = LagrangeColor;
	ImageColor3->Canvas->Ellipse(2,2,ImageColor1->Width-2,ImageColor1->Height-2);

	Form1->ImageColor4->Canvas->Brush->Color = NewtonColor;
	ImageColor4->Canvas->Ellipse(2,2,ImageColor1->Width-2,ImageColor1->Height-2);

	Form1->ImageColor5->Canvas->Brush->Color = BernstainColor;
	ImageColor5->Canvas->Ellipse(2,2,ImageColor1->Width-2,ImageColor1->Height-2);

	Form1->ImageColor6->Canvas->Brush->Color = DeCastejoColor;
	ImageColor6->Canvas->Ellipse(2,2,ImageColor1->Width-2,ImageColor1->Height-2);
}
//---------------------------------------------------------------------------

void __fastcall TForm1::Image1Click(TObject *Sender)
{
	if(Mode == 0) {

		MousePos.X = MousePos.X - BgOffset.X;
		MousePos.Y = MousePos.Y - BgOffset.Y;
		
		AddDot(MousePos.X,MousePos.Y,clWhite,3);
		Form1->StringGrid1->RowCount++;
		DrawCurrentFunction();
	}

	if(Mode == 2) {
		if(Zoom >= 4) {}
		else Zoom += 0.25;
		DoZoom();
	}
}
//---------------------------------------------------------------------------

void DoZoom() {
	
	Form1->Image1->Width = Form1->ScrollBox1->Width * Zoom;
	Form1->Image1->Height = Form1->ScrollBox1->Height * Zoom;

	Form1->Image1->Picture = NULL;
	DrawCurrentFunction();

	Form1->ScrollBox1->HorzScrollBar->Position = MousePos.X*Zoom - MousePos.X;
	Form1->ScrollBox1->VertScrollBar->Position = MousePos.Y*Zoom - MousePos.Y;
}

void DrawGrid() {
	int Cell = CellSize*Zoom;

	Form1->Image1->Canvas->Pen->Width = 1;
	//Заполняем рабочую область одним цветом или фоном
	if(BgActive) {
		Form1->Image1->Picture = Form1->ImageBgUser->Picture;
	} else {
		Form1->Image1->Canvas->Brush->Color = BgColor;
		Form1->Image1->Canvas->Rectangle(0,0,Form1->Image1->Width+1,Form1->Image1->Height+1);
	}
	
	//Сетка
	Form1->Image1->Canvas->Pen->Color = GridColor;
	for(int i = 0; i < Form1->Image1->Height/Cell+1; i++) {
		Form1->Image1->Canvas->MoveTo(0,i*Cell);
		Form1->Image1->Canvas->LineTo(Form1->Image1->Width,i*Cell);
	}
	for(int i = 0; i < Form1->Image1->Width/Cell+1; i++) {
		Form1->Image1->Canvas->MoveTo(i*Cell,0);
		Form1->Image1->Canvas->LineTo(i*Cell,Form1->Image1->Height);
	}

	//Ось Y
   Form1->Image1->Canvas->Pen->Color = RGB(220,220,220);
   Form1->Image1->Canvas->MoveTo(0,0);
   Form1->Image1->Canvas->LineTo(0,Form1->Image1->Height);
   Form1->Image1->Canvas->MoveTo(0,Form1->Image1->Height);
   Form1->Image1->Canvas->LineTo(0 + Cell/2, Form1->Image1->Height - Cell/2);
	//Ось X
   Form1->Image1->Canvas->Pen->Color = RGB(220,220,220);
   Form1->Image1->Canvas->MoveTo(0,0);
   Form1->Image1->Canvas->LineTo(Form1->Image1->Width,0);
   Form1->Image1->Canvas->MoveTo(Form1->Image1->Width,0);
   Form1->Image1->Canvas->LineTo(Form1->Image1->Width - Cell/2, 0 + Cell/2);
}

void AddDot(int X, int Y, TColor Color, int R) {
	int CountDots = Form1->StringGrid1->RowCount-1;
	Form1->Image1->Canvas->Pen->Color = Color;
	Form1->Image1->Canvas->Brush->Color = Color;
	Form1->Image1->Canvas->Ellipse(X*Zoom-R,Y*Zoom-R,X*Zoom+R,Y*Zoom+R);

	Form1->StringGrid1->Cells[0][CountDots+1] = IntToStr(CountDots);
	Form1->StringGrid1->Cells[1][CountDots+1] = IntToStr(X);
	Form1->StringGrid1->Cells[2][CountDots+1] = IntToStr(Y);
}

void DrawAllDots() {
	for(int i = 1; i < Form1->StringGrid1->RowCount; i++) {
		AddDot(StrToInt(Form1->StringGrid1->Cells[1][i]),StrToInt(Form1->StringGrid1->Cells[2][i]),clWhite, 3);
	}
}

void __fastcall TForm1::Image1MouseMove(TObject *Sender, TShiftState Shift, int X,
		  int Y)
{
	MousePos.X = X/Zoom;
	MousePos.Y = Y/Zoom;

	int Index = NearDot(SearchRadius);
	if(Index != -1) {
		//Выделить текущую точку в таблице
		Form1->StringGrid1->Row = Index;
		Form1->StringGrid1->Col = 0;
		TGridRect myRect;
		myRect.Left = 2;
		myRect.Top = Index;
		myRect.Right = 0;
		myRect.Bottom = Index;
		Form1->StringGrid1->Selection = myRect;
		DrawAllDots();
		Form1->Image1->Canvas->Pen->Color = SelectColor;
		Form1->Image1->Canvas->Brush->Color = SelectColor;
		Form1->Image1->Canvas->Ellipse(
			Form1->StringGrid1->Cells[1][Index]*Zoom-3,
			Form1->StringGrid1->Cells[2][Index]*Zoom-3,
			Form1->StringGrid1->Cells[1][Index]*Zoom+3,
			Form1->StringGrid1->Cells[2][Index]*Zoom+3
		);
	} else {
			TGridRect myRect;
			myRect.Left = 2;
			myRect.Top = 0;
			myRect.Right = 0;
			myRect.Bottom = 0;
			Form1->StringGrid1->Selection = myRect;
			DrawAllDots();
	}

	//Если включен режим перемещения то выполнить его
	if(FlagDrag) {
		Form1->StringGrid1->Cells[1][DragIndex] = MousePos.X;
		Form1->StringGrid1->Cells[2][DragIndex] = MousePos.Y;
		DrawCurrentFunction();
	}

	Form1->Label3->Caption = IntToStr(MousePos.X - BgOffset.X) + "/" + IntToStr(MousePos.Y - BgOffset.Y);
}
//---------------------------------------------------------------------------

void Clear() {
	Form1->EditX->Text = "";
	Form1->EditY->Text = "";
	Form1->StringGrid1->RowCount = 1;
	DrawGrid();
	Form1->Cancel1->Visible = false;
	Form1->Cancel2->Visible = false;
	Form1->Cancel3->Visible = false;
	Form1->Cancel4->Visible = false;
	Form1->Cancel5->Visible = false;
	Form1->Cancel6->Visible = false;

	Form1->Button1->Enabled = true;
	Form1->Button2->Enabled = true;
	Form1->Button3->Enabled = true;
	Form1->Button4->Enabled = true;
	Form1->Button5->Enabled = true;
	Form1->Button6->Enabled = true;

	Form1->ImageColor1->Visible = false;
	Form1->ImageColor2->Visible = false;
	Form1->ImageColor3->Visible = false;
	Form1->ImageColor4->Visible = false;
	Form1->ImageColor5->Visible = false;
	Form1->ImageColor6->Visible = false;

}
//---------------------------------------------------------------------------

void __fastcall TForm1::Button1Click(TObject *Sender)
{
TButton *Button;
Button=(TButton*)Sender;
	if(Form1->StringGrid1->RowCount-1 >=2) {
		if(CompareStr(Button->Name,"Button1") == 0) {
			Form1->Cancel1->Visible = true;
			Form1->Button1->Enabled = false;
			Form1->ImageColor1->Visible = true;
			LinearInterpolation();
		}
		if(CompareStr(Button->Name,"Button2") == 0) {
			Form1->Cancel2->Visible = true;
			Form1->Button2->Enabled = false;
			Form1->ImageColor2->Visible = true;
			HermitInterpolation();
		}
		if(CompareStr(Button->Name,"Button3") == 0) {
			Form1->Cancel3->Visible = true;
			Form1->Button3->Enabled = false;
			Form1->ImageColor3->Visible = true;
			LagrangeInterpolation();
		}
		if(CompareStr(Button->Name,"Button4") == 0) {
			Form1->Cancel4->Visible = true;
			Form1->Button4->Enabled = false;
			Form1->ImageColor4->Visible = true;
			NewtonInterpolation();
		}
		if(CompareStr(Button->Name,"Button5") == 0) {
			Form1->Cancel5->Visible = true;
			Form1->Button5->Enabled = false;
			Form1->ImageColor5->Visible = true;
			BezierBernstain();
		}
		if(CompareStr(Button->Name,"Button6") == 0) {
			Form1->Cancel6->Visible = true;
			Form1->Button6->Enabled = false;
			Form1->ImageColor6->Visible = true;
			BezierDeCastelie();
		}
	} else {
		Form2->Label1->Caption = "Для построения необходимо минимум 2 точки. Добавьте новые точки, нажав ЛКМ на рабочем поле.";
		Form2->ShowModal();
	}
}
//---------------------------------------------------------------------------

void __fastcall TForm1::ImageOpenClick(TObject *Sender)
{
	//Создаем временное хранилище данных Memo
	TMemo* Memo = new TMemo(Form1);
	Memo->Parent = Form1;
	Memo->Visible = false;
	BOOL Execute = Form1->OpenDialog1->Execute();
	//Если был выбран файл
	if(Execute) {
		//Стираем все уже имеющиеся точки с экрана и из таблицы
        Clear();

		Memo->Lines->LoadFromFile(Form1->OpenDialog1->FileName);

		int DotCount = StrToInt(Memo->Lines->Strings[0]);


		// Загружаем все точки
		for(int i = 1; i < DotCount+1; i++) {
			int Pos = 0; //Номер столбца в который будет записана информация
			AnsiString Line = Memo->Lines->Strings[i];
			AnsiString SubStr = "";
			Form1->StringGrid1->RowCount++;
			for(int j = 1; j < Line.Length()+1; j++) {
				if(Line[j] == '\t' || j == Line.Length()) {
						if(Pos == 2) SubStr = SubStr + Line[j];
						Form1->StringGrid1->Cells[Pos][i] = SubStr;
						SubStr = "";
						Pos++;
				} else {
					SubStr = SubStr + Line[j];
				}
			}
		}
		//Загружаем все активные графики
		for(int i = DotCount+1; i < Memo->Lines->Count; i++) {
			AnsiString Line = Memo->Lines->Strings[i];
			if(CompareStr(Line,"1") == 0) Form1->Button1->Click();
			if(CompareStr(Line,"2") == 0) Form1->Button2->Click();
			if(CompareStr(Line,"3") == 0) Form1->Button3->Click();
			if(CompareStr(Line,"4") == 0) Form1->Button4->Click();
			if(CompareStr(Line,"5") == 0) Form1->Button5->Click();
			if(CompareStr(Line,"6") == 0) Form1->Button6->Click();
		}
		DrawCurrentFunction();
	}
	DrawAllDots();
	Memo->Free();
}
//---------------------------------------------------------------------------

void __fastcall TForm1::ImageSaveClick(TObject *Sender)
{
	TMemo* Memo = new TMemo(Form1);
	Memo->Parent = Form1;
	Memo->Visible = false;
	BOOL Execute = Form1->SaveDialog1->Execute();
	if(Execute) {
	//Сохраняем количество точек
		Memo->Lines->Add(Form1->StringGrid1->RowCount-1);
	// Сохраняем все точки
		for(int i = 1; i< Form1->StringGrid1->RowCount; i++) {
			Memo->Lines->Add(
				Form1->StringGrid1->Cells[0][i] + "\t" +
				Form1->StringGrid1->Cells[1][i] + "\t" +
				Form1->StringGrid1->Cells[2][i]
			);
		}
		// Сохраняем активные графики
		if(!Button1->Enabled) {
			Memo->Lines->Add(1);
		}
		if(!Button2->Enabled) {
			Memo->Lines->Add(2);
		}
		if(!Button3->Enabled) {
			Memo->Lines->Add(3);
		}
		if(!Button4->Enabled) {
			Memo->Lines->Add(4);
		}
		if(!Button5->Enabled) {
			Memo->Lines->Add(5);
		}
		if(!Button6->Enabled) {
			Memo->Lines->Add(6);
		}

		Memo->Lines->SaveToFile(Form1->SaveDialog1->FileName);
	}
}
//---------------------------------------------------------------------------


void __fastcall TForm1::ImageClearClick(TObject *Sender)
{
	Clear();
}
//---------------------------------------------------------------------------

int NearDot(int SearchRadius) {
	int Index;
	int MinDistanse = Form1->Height * Form1->Width;
	//Находим расстояние от курсора до каждой точки
	for(int i = 1 ;i <Form1->StringGrid1->RowCount; i++) {
		int X1 = StrToInt(Form1->StringGrid1->Cells[1][i]);
		int Y1 = StrToInt(Form1->StringGrid1->Cells[2][i]);
		int X2 = MousePos.X;
		int Y2 = MousePos.Y;
		int Distanse = abs(sqrt(pow(X2-X1,2) + pow(Y2-Y1,2)));

		if(Distanse < MinDistanse) {
			MinDistanse = Distanse;
			Index = i;
		}
	}
	if(MinDistanse > SearchRadius) Index = -1;
	return Index;
}

void DeleteDot() {
	int Index = NearDot(SearchRadius);
	if(Index != -1) {
		for(int i = Index; i < Form1->StringGrid1->RowCount-1; i++) {
			Form1->StringGrid1->Cells[1][i] = Form1->StringGrid1->Cells[1][i+1];
			Form1->StringGrid1->Cells[2][i] = Form1->StringGrid1->Cells[2][i+1];
		}
		if (Form1->StringGrid1->RowCount > 1) {
			Form1->StringGrid1->Rows[Form1->StringGrid1->RowCount]->Clear();
			Form1->StringGrid1->RowCount--;
		}
		DrawCurrentFunction();
	}
}

void __fastcall TForm1::Image1ContextPopup(TObject *Sender, TPoint &MousePos, bool &Handled)
{
if(Mode == 0 || Mode == 1)
	if( Form1->Cancel1->Visible == false
		&& Form1->Cancel2->Visible == false
		&& Form1->Cancel3->Visible == false
		&& Form1->Cancel4->Visible == false
		&& Form1->Cancel5->Visible == false
		&& Form1->Cancel6->Visible == false)
		{
			DeleteDot();
		}
		else {
			if(Form1->StringGrid1->RowCount-1 > 2) {
				DeleteDot();
			} else {
				Form2->Label1->Caption = "Нельзя оставлять на рабочем поле менее 2-х точек при хотя бы одной активной функции. Снимите выполение со всех активных функций или полностью очистите поле";
				Form2->ShowModal();
			}
		}

	if(Mode == 2) {
		if(Zoom <= 1) {}
		else Zoom -= 0.25;
		DoZoom();
	}
}
//---------------------------------------------------------------------------

void DrawCurrentFunction() {
	DrawGrid();
	DrawAllDots();
	Form1->Image1->Canvas->Pen->Width = 2;
	if(Form1->Cancel1->Visible == true) {
		LinearInterpolation();
	}
	if(Form1->Cancel2->Visible == true) {
		HermitInterpolation();
	}
	if(Form1->Cancel3->Visible == true) {
		LagrangeInterpolation();
	}
	if(Form1->Cancel4->Visible == true) {
		NewtonInterpolation();
	}
	if(Form1->Cancel5->Visible == true) {
		BezierBernstain();
	}
	if(Form1->Cancel6->Visible == true) {
		BezierDeCastelie();
	}
}

void __fastcall TForm1::BitBtn7Click(TObject *Sender)
{
	if(Form1->EditX->Text != "") {
		if(Form1->EditY->Text != "") {
			if(StrToInt(Form1->EditX->Text) > Form1->Image1->Width ||
				StrToInt(Form1->EditY->Text) > Form1->Image1->Height) {
					Form2->Label1->Caption = "Введенные значения больше чем размер рабочей области. Введите другие данные для установки точки";
					Form2->ShowModal();
				} else {
					AddDot(StrToInt(Form1->EditX->Text),StrToInt(Form1->EditY->Text),clWhite,3);
					Form1->StringGrid1->RowCount++;
					DrawCurrentFunction();
				}
		} else {
			Form2->Label1->Caption = "Введите значение координаты Y.";
			Form2->ShowModal();
		}
	} else {
		Form2->Label1->Caption = "Введите значение координаты X.";
		Form2->ShowModal();
	}
}
//---------------------------------------------------------------------------

void __fastcall TForm1::FormResize(TObject *Sender)
{
	if(FirstResize) {
		FirstResize = false;
		Form1->Image1->Width = Form1->ScrollBox1->Width;
		Form1->Image1->Height = Form1->ScrollBox1->Height;

		DrawGrid();
		DrawAllDots();
	}
}
//---------------------------------------------------------------------------



void __fastcall TForm1::Cancel1Click(TObject *Sender)
{
	TButton *Button;
	Button=(TButton*)Sender;
	if(CompareStr(Button->Name,"Cancel1") == 0) {
		Form1->Cancel1->Visible = false;
		Form1->Button1->Enabled = true;
		Form1->ImageColor1->Visible = false;
	}
	if(CompareStr(Button->Name,"Cancel2") == 0) {
		Form1->Cancel2->Visible = false;
		Form1->Button2->Enabled = true;
		Form1->ImageColor2->Visible = false;
	}
	if(CompareStr(Button->Name,"Cancel3") == 0) {
		Form1->Cancel3->Visible = false;
		Form1->Button3->Enabled = true;
		Form1->ImageColor3->Visible = false;
	}
	if(CompareStr(Button->Name,"Cancel4") == 0) {
		Form1->Cancel4->Visible = false;
		Form1->Button4->Enabled = true;
		Form1->ImageColor4->Visible = false;
	}
	if(CompareStr(Button->Name,"Cancel5") == 0) {
		Form1->Cancel5->Visible = false;
		Form1->Button5->Enabled = true;
		Form1->ImageColor5->Visible = false;
	}
	if(CompareStr(Button->Name,"Cancel6") == 0) {
		Form1->Cancel6->Visible = false;
		Form1->Button6->Enabled = true;
		Form1->ImageColor6->Visible = false;
	}
	DrawCurrentFunction();
}
//---------------------------------------------------------------------------

void __fastcall TForm1::ImageMoveClick(TObject *Sender)
{
	ImageMove->AlignWithMargins = true;
	ImageDrag->AlignWithMargins = false;
	ImageZoom->AlignWithMargins = false;
	Mode = 0;
	Form1->PanelMove->BevelOuter = TBevelCut::bvLowered;
	Form1->PanelDrag->BevelOuter = TBevelCut::bvRaised;
	Form1->PanelZoom->BevelOuter = TBevelCut::bvRaised;
	Form1->Image1->Cursor = crCross;
}
//---------------------------------------------------------------------------

void __fastcall TForm1::ImageDragClick(TObject *Sender)
{
	ImageMove->AlignWithMargins = false;
	ImageDrag->AlignWithMargins = true;
	ImageZoom->AlignWithMargins = false;
	Mode = 1;
	Form1->PanelMove->BevelOuter = TBevelCut::bvRaised;
	Form1->PanelDrag->BevelOuter = TBevelCut::bvLowered;
	Form1->PanelZoom->BevelOuter = TBevelCut::bvRaised;
	Form1->Image1->Cursor = crSize;
}
//---------------------------------------------------------------------------

void __fastcall TForm1::ImageZoomClick(TObject *Sender)
{
	ImageMove->AlignWithMargins = false;
	ImageDrag->AlignWithMargins = false;
	ImageZoom->AlignWithMargins = true;
	Mode = 2;
	Form1->PanelMove->BevelOuter = TBevelCut::bvRaised;
	Form1->PanelDrag->BevelOuter = TBevelCut::bvRaised;
	Form1->PanelZoom->BevelOuter = TBevelCut::bvLowered;
	Form1->Image1->Cursor = crSizeNWSE;
}
//---------------------------------------------------------------------------

void __fastcall TForm1::Image1MouseDown(TObject *Sender, TMouseButton Button, TShiftState Shift,
          int X, int Y)
{
	if(Mode == 1) {
		int Index = NearDot(SearchRadius);
		if(Index != -1) {
			FlagDrag = true;
			DragIndex = Index;
		}
	}
}
//---------------------------------------------------------------------------

void __fastcall TForm1::Image1MouseUp(TObject *Sender, TMouseButton Button, TShiftState Shift,
          int X, int Y)
{
if(Mode == 1)
	FlagDrag = false;
}
//---------------------------------------------------------------------------

void __fastcall TForm1::Button8Click(TObject *Sender)
{
	BOOL Execute = Form1->OpenDialog2->Execute();
	//Если был выбран файл
	if(Execute) {

		ImageBgUser->Visible = true;
		Button9->Visible = true;
		Button7->Visible = true;
		Button8->Visible = false;
		Form1->ImageBgUser->Picture->LoadFromFile(Form1->OpenDialog2->FileName);
		if(Form1->ImageBgUser->Picture->Width > Form1->Image1->Width ||
		Form1->ImageBgUser->Picture->Height > Form1->Image1->Height)
		{
			Form1->Button7->Click();
			Form2->Label1->Caption = "Изображение слишком большое. Загрузите другое.";
			Form2->ShowModal();
		}
		else {
			Form1->Image1->Picture = Form1->ImageBgUser->Picture;

			Form1->ImageMoveClick(NULL);
			Form1->ImageZoom->Enabled = false;

			BgActive = true;

			BgOffset.X = (Form1->Image1->Width - Form1->Image1->Picture->Width)/2;
			BgOffset.Y = (Form1->Image1->Height - Form1->Image1->Picture->Height)/2;

			DrawGrid();
			DrawAllDots();
			DrawCurrentFunction();
		}
	}
}
//---------------------------------------------------------------------------

void __fastcall TForm1::Button7Click(TObject *Sender)
{
	Button8->Visible = true;
	Button7->Visible = false;
	Button9->Visible = false;
	ImageBgUser->Visible = false;

	BgActive = false;

	Form1->ImageZoom->Enabled = true;

	BgOffset.X = BgOffset.Y = 0;

	Form1->Image1->Picture = NULL;

	Form1->Image1->Width = Form1->ScrollBox1->Width;
	Form1->Image1->Height = Form1->ScrollBox1->Height;

	DrawGrid();
	DrawAllDots();
	DrawCurrentFunction();
}
//---------------------------------------------------------------------------





void __fastcall TForm1::StringGrid1SelectCell(TObject *Sender, int ACol, int ARow,
          bool &CanSelect)
{
	if(ARow != 0) {
	DrawAllDots();
	Form1->Image1->Canvas->Pen->Color = SelectColor;
		Form1->Image1->Canvas->Brush->Color = SelectColor;
		Form1->Image1->Canvas->Ellipse(
			Form1->StringGrid1->Cells[1][ARow]*Zoom-3,
			Form1->StringGrid1->Cells[2][ARow]*Zoom-3,
			Form1->StringGrid1->Cells[1][ARow]*Zoom+3,
			Form1->StringGrid1->Cells[2][ARow]*Zoom+3
		);
    }
}
//---------------------------------------------------------------------------


void __fastcall TForm1::StringGrid1SetEditText(TObject *Sender, int ACol, int ARow,
          const UnicodeString Value)
{
//	if(Value != "") {
//		DrawGrid();
//		DrawAllDots();
//		DrawCurrentFunction();
//	} else {
//		Form1->StringGrid1->Cells[ACol][ARow] = "";
//	}
}
//---------------------------------------------------------------------------


void __fastcall TForm1::Button10Click(TObject *Sender)
{
    Form3->ShowModal();
}
//---------------------------------------------------------------------------

