//---------------------------------------------------------------------------

#pragma hdrstop

#include "DeCastelie.h"
#include "Global.h"
#include "Unit1.h"
void Recurve(double*, double* , double , int);

void BezierDeCastelie() {
	int PointCount = Form1->StringGrid1->RowCount-1;
	double Step = 1/(double)(CountSplinePoint*PointCount);
	Form1->Image1->Canvas->Pen->Color = DeCastejoColor;

	double* P_x = new double[PointCount];
	double* P_y = new double[PointCount];

	for(int i = 0; i < PointCount; i++) {
		P_x[i] = StrToInt(Form1->StringGrid1->Cells[1][i+1]);
		P_y[i] = StrToInt(Form1->StringGrid1->Cells[2][i+1]);
	}
	Form1->Image1->Canvas->MoveTo(P_x[0]*Zoom,P_y[0]*Zoom); // Устанавливаем стартовую точку

	for (double t = 0; t < 1; t += Step) {
		Recurve(P_x, P_y, t, PointCount - 1);
	}

	Form1->Image1->Canvas->LineTo(Form1->StringGrid1->Cells[1][PointCount]*Zoom,Form1->StringGrid1->Cells[2][PointCount]*Zoom);

	delete[] P_x;
	delete[] P_y;
}

void Recurve(double* P_x, double* P_y, double t, int PointCount) {

	double* R_x = new double[PointCount]; //Массивы новых точек
	double* R_y = new double[PointCount];

	for (int i = 0; i < PointCount; i++) {
		R_x[i] = (1-t) * P_x[i] + t * P_x[i+1]; //Вычисляем их относительно имеющихся
		R_y[i] = (1-t) * P_y[i] + t * P_y[i+1];
	}
	if(PointCount == 1)
		Form1->Image1->Canvas->LineTo(R_x[0]*Zoom,R_y[0]*Zoom);
	else
		Recurve(R_x, R_y, t, PointCount-1);

	delete[] R_x;
	delete[] R_y;
}
//---------------------------------------------------------------------------
#pragma package(smart_init)
