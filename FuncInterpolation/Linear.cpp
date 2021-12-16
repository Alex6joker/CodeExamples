//---------------------------------------------------------------------------

#pragma hdrstop

#include "Linear.h"
#include "Global.h"
#include "Unit1.h"

void LinearInterpolation() {
	Form1->Image1->Canvas->Pen->Color = LinearColor;
	for(int i = 1; i < Form1->StringGrid1->RowCount-1; i++) {
		int X1 = StrToInt(Form1->StringGrid1->Cells[1][i]);
		int Y1 = StrToInt(Form1->StringGrid1->Cells[2][i]);
		int X2 = StrToInt(Form1->StringGrid1->Cells[1][i+1]);
		int Y2 = StrToInt(Form1->StringGrid1->Cells[2][i+1]);
		Form1->Image1->Canvas->MoveTo(X1*Zoom,Y1*Zoom);
		Form1->Image1->Canvas->LineTo(X2*Zoom,Y2*Zoom);
	}
}

//---------------------------------------------------------------------------
#pragma package(smart_init)
