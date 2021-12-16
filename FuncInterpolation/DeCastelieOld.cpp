//---------------------------------------------------------------------------

#pragma hdrstop

#include "DeCastelieOld.h"
#include "Global.h"
#include "Unit1.h"

void GetLinesPart(int,float*, float*, float);

void BezierDeCastelieOld() {
	float Step = 1/(float)CountSplinePoint;
	int PointCount = Form1->StringGrid1->RowCount-1;
	int LinesCount = PointCount - 1;
	Form1->Image1->Canvas->Pen->Color = DeCastejoColor;

	float* Point_x = new float[PointCount];
	float* Point_y = new float[PointCount];

	Form1->Image1->Canvas->MoveTo(StrToInt(Form1->StringGrid1->Cells[1][1])*Zoom,StrToInt(Form1->StringGrid1->Cells[2][1]*Zoom));

	for( float t = 0; t <= 1; t+=Step) {
		for(int i = 0; i < PointCount; i++) {
			Point_x[i] = (float)StrToInt(Form1->StringGrid1->Cells[1][i+1]);
			Point_y[i] = (float)StrToInt(Form1->StringGrid1->Cells[2][i+1]);
		}
		GetLinesPart(LinesCount,Point_x,Point_y,t);
	}
	Form1->Image1->Canvas->LineTo(Point_x[PointCount-1]*Zoom,Point_y[PointCount-1]*Zoom);
}

void GetLinesPart(int LineCount,float* Point_x, float* Point_y, float t) {
	if(LineCount != 1){
		for(int i = 0; i < LineCount; i++){
			float X1 = Point_x[i];
			float Y1 = Point_y[i];
			float X2 = Point_x[i+1];
			float Y2 = Point_y[i+1];

			float DeltaX = (X2-X1);
			float DeltaY = (Y2-Y1);

			Point_x[i] = X1 + DeltaX*t;
			Point_y[i] = Y1 + DeltaY*t;
		}

//		//Опционально
//		Form1->Image1->Canvas->Pen->Color = DeCastejoColor;
//		for(int i = 0; i < LineCount-1; i++) {
//			Form1->Image1->Canvas->MoveTo(Point_x[i],Point_y[i]);
//			Form1->Image1->Canvas->LineTo(Point_x[i+1],Point_y[i+1]);
//		}

		GetLinesPart(LineCount-1,Point_x,Point_y,t);
	} else {
		float X1 = Point_x[0];
		float Y1 = Point_y[0];
		float X2 = Point_x[1];
		float Y2 = Point_y[1];
		float DeltaX = (X2-X1);
		float DeltaY = (Y2-Y1);
		Form1->Image1->Canvas->LineTo((X1 + DeltaX*t)*Zoom,(Y1 + DeltaY*t)*Zoom);
	}
}
//---------------------------------------------------------------------------
#pragma package(smart_init)
