//---------------------------------------------------------------------------

#pragma hdrstop

#include "Newton.h"
#include "Global.h"
#include "Unit1.h"

double GetNewtonPos(int, double* , double, double*);
double* GetACoefficient(double*, int, int, double*);

void NewtonInterpolation()
{
	int LineCount = Form1->StringGrid1->RowCount;
	double Step = 1/(double)(CountSplinePoint*LineCount);

	// Значения t для все точек
	double* tt = new double[LineCount-1];
	for(int i = 1; i < LineCount; i++)
		tt[i-1] = (double)i;

	double* Ax = new double[LineCount-1];  // Вычисляем коэффициенты A
	double* Ay = new double[LineCount-1];  // Вычисляем коэффициенты A
	Ax = GetACoefficient(Ax,1,LineCount,tt);
	Ay = GetACoefficient(Ay,2,LineCount,tt);

	Form1->Image1->Canvas->Pen->Color = NewtonColor;
	Form1->Image1->Canvas->MoveTo(StrToInt(Form1->StringGrid1->Cells[1][1])*Zoom,StrToInt(Form1->StringGrid1->Cells[2][1])*Zoom);
    for(int k = 1; k < LineCount-1; k++) // В каждом из промежутков
	{
		for(double t_cur = tt[k-1]; t_cur <= tt[k]; t_cur += Step)
		{
			double X = GetNewtonPos(LineCount,tt,t_cur,Ax);
			double Y = GetNewtonPos(LineCount,tt,t_cur,Ay);
			Form1->Image1->Canvas->LineTo(X*Zoom,Y*Zoom);
			Form1->Image1->Canvas->MoveTo(X*Zoom,Y*Zoom);
			//Form1->Refresh();
		}
	}
	Form1->Image1->Canvas->LineTo(StrToInt(Form1->StringGrid1->Cells[1][LineCount])*Zoom,StrToInt(Form1->StringGrid1->Cells[2][LineCount])*Zoom);
	delete[] Ax;
	delete[] Ay;
	delete[] tt;
}

double* GetACoefficient(double* A, int Coordinate, int LineCount, double* tt)
{
	for(int i = 1; i < LineCount; i++)
	{
		double numerator = 1;
		double denominator = 1;
		if(i == 1)
			A[i-1] = StrToFloat(Form1->StringGrid1->Cells[Coordinate][1]);
		else
		{
			numerator = StrToFloat(Form1->StringGrid1->Cells[Coordinate][i]);
			for(int j = 1; j < i; j++)
			{
				if(j == 1)
					numerator -= A[j-1];
				else
				{
					double SubRes = A[j-1];
					for(int k = 0; k < j-1; k++)
					{
						SubRes *= tt[i-1] - tt[k];
					}
					numerator -= SubRes;
				}
			}
			// Вычисляем знаменатель
			for(int k = 0; k < i-1; k++)
			{
				denominator *= tt[i-1] - tt[k];
			}
			A[i-1] = numerator/denominator;
		}
	}
	return A;
}
double GetNewtonPos(int LineCount, double* t, double t_cur, double* A)
{
	double Sum = 0;
	for(int i = 1; i < LineCount; i++)
	{
		double SubSum = A[i-1];
		for(int j = 1; j < i; j++)
		{
			SubSum *= (t_cur - t[j-1]);
		}
		Sum += SubSum;
	}
	return Sum;
}
//---------------------------------------------------------------------------
#pragma package(smart_init)
