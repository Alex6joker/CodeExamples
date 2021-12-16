//---------------------------------------------------------------------------

#pragma hdrstop

#include "Bernstain.h"
#include "Global.h"
#include "Unit1.h"

long double GetBezierBasis(int,int,float);
long double GetFactorial(int);

void BezierBernstain() {
	int Index = 0;
	int PointCount = Form1->StringGrid1->RowCount-1;
	float Step = 1/(float)(CountSplinePoint*PointCount);
	Form1->Image1->Canvas->Pen->Color = BernstainColor;

	long double* P_x = new long double[CountSplinePoint*PointCount];
	long double* P_y = new long double[CountSplinePoint*PointCount];

	for(int i = 0; i < CountSplinePoint*PointCount; i++) {
		P_x[i] = 0;
		P_y[i] = 0;
	}

	Form1->Image1->Canvas->MoveTo(Form1->StringGrid1->Cells[1][1]*Zoom,Form1->StringGrid1->Cells[2][1]*Zoom);

	for (float t = Step; t < 1; t += Step) {
		if (t > 1) t = 1;
		for (int i = 0; i < PointCount; i++) {
			long double B = GetBezierBasis(i, PointCount - 1, t);
			P_x[Index] += StrToInt(Form1->StringGrid1->Cells[1][i+1]) * B;
			P_y[Index] += StrToInt(Form1->StringGrid1->Cells[2][i+1]) * B;
		}
		Form1->Image1->Canvas->LineTo(P_x[Index]*Zoom,P_y[Index]*Zoom);
		Index++;
	}

	Form1->Image1->Canvas->LineTo(Form1->StringGrid1->Cells[1][PointCount+1]*Zoom,Form1->StringGrid1->Cells[2][PointCount+1]*Zoom);

	delete[] P_x;
	delete[] P_y;
}

long double GetFactorial(int n) {
	if (n == 0 || n == 1) return 1;
	return n * GetFactorial(n - 1);
}

long double GetBezierBasis(int i,int n,float t ) {
	// i - номер вершины, n - количество вершин, t - положение кривой (от 0 до 1)
	// считаем i-й элемент полинома Берштейна
	return (long double)(GetFactorial(n)/(GetFactorial(i)*GetFactorial(n - i)))* powf(t, i)*powf(1 - t, n - i);
}
//---------------------------------------------------------------------------
#pragma package(smart_init)
