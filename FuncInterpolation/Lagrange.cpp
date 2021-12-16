//---------------------------------------------------------------------------

#pragma hdrstop

#include "Lagrange.h"
#include "Global.h"
#include "Unit1.h"

double GetLagrangePos(int, int, double*, double);

void LagrangeInterpolation()
{
	int LineCount = Form1->StringGrid1->RowCount;
	double Step = 1/(double)(CountSplinePoint*LineCount);
	// Значения t для все точек
	double* tt = new double[LineCount-1];
	for(int i = 1; i < LineCount; i++)
		tt[i-1] = (double)i;

	Form1->Image1->Canvas->Pen->Color = LagrangeColor;
	Form1->Image1->Canvas->MoveTo(StrToInt(Form1->StringGrid1->Cells[1][1])*Zoom,StrToInt(Form1->StringGrid1->Cells[2][1])*Zoom);
	for(int k = 1; k < LineCount-1; k++) // В каждом из промежутков
	{
		for(double t_cur = tt[k-1]; t_cur <= tt[k]; t_cur += Step)
		{
			double X = GetLagrangePos(LineCount,1,tt,t_cur);
			double Y = GetLagrangePos(LineCount,2,tt,t_cur);
			Form1->Image1->Canvas->LineTo(X*Zoom,Y*Zoom);
			Form1->Image1->Canvas->MoveTo(X*Zoom,Y*Zoom);
			//Form1->Refresh();
		}
	}
	Form1->Image1->Canvas->LineTo(StrToInt(Form1->StringGrid1->Cells[1][LineCount])*Zoom,StrToInt(Form1->StringGrid1->Cells[2][LineCount])*Zoom);
	delete[] tt;
}

double GetLagrangePos(int LineCount, int Coordinate, double* t, double t_cur)
{
	double Sum = 0;
	for(int i = 1; i < LineCount; i++) // В каждом из промежутков
	{
		double numerator = 1;
		for(int j = 1; j < LineCount; j++)
				if(i != j)
					numerator *= (t_cur - t[j-1]);
		double denominator = 1;
		for(int j = 1; j < LineCount; j++)
				if(i != j)
					denominator *= (t[i-1] - t[j-1]);
		Sum += (numerator/denominator)*StrToFloat(Form1->StringGrid1->Cells[Coordinate][i]);
	}
	return Sum;
}

//---------------------------------------------------------------------------
#pragma package(smart_init)
