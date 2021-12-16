//---------------------------------------------------------------------------

#pragma hdrstop

#include "Hermit.h"
#include "Global.h"
#include "Unit1.h"

void GetQ(int, float*, float*);

void HermitInterpolation()
{
	struct Point
	{
		int x;
		int y;
	}FirstPoint,SecondPoint;

	int HermitMatrix[4][4] = {1,0,0,0,0,0,1,0,-3,3,-2,-1,2,-2,1,1};
	int A_Matrix [2][4];

	int LineCount = Form1->StringGrid1->RowCount-1;
	float* Q_x = new float[LineCount];
	float* Q_y = new float[LineCount];
	GetQ(LineCount, Q_x, Q_y);

	for(int i = 1; i < LineCount; i++)
	{
		FirstPoint.x = StrToInt(Form1->StringGrid1->Cells[1][i]);
		FirstPoint.y = StrToInt(Form1->StringGrid1->Cells[2][i]);
		SecondPoint.x = StrToInt(Form1->StringGrid1->Cells[1][i+1]);
		SecondPoint.y = StrToInt(Form1->StringGrid1->Cells[2][i+1]);

		A_Matrix[0][0] = HermitMatrix[0][0]*FirstPoint.x + HermitMatrix[0][1]*SecondPoint.x + HermitMatrix[0][2]*Q_x[i-1] + HermitMatrix[0][3]*Q_x[i];
		A_Matrix[0][1] = HermitMatrix[1][0]*FirstPoint.x + HermitMatrix[1][1]*SecondPoint.x + HermitMatrix[1][2]*Q_x[i-1] + HermitMatrix[1][3]*Q_x[i];
		A_Matrix[0][2] = HermitMatrix[2][0]*FirstPoint.x + HermitMatrix[2][1]*SecondPoint.x + HermitMatrix[2][2]*Q_x[i-1] + HermitMatrix[2][3]*Q_x[i];
		A_Matrix[0][3] = HermitMatrix[3][0]*FirstPoint.x + HermitMatrix[3][1]*SecondPoint.x + HermitMatrix[3][2]*Q_x[i-1] + HermitMatrix[3][3]*Q_x[i];

		A_Matrix[1][0] = HermitMatrix[0][0]*FirstPoint.y + HermitMatrix[0][1]*SecondPoint.y + HermitMatrix[0][2]*Q_y[i-1] + HermitMatrix[0][3]*Q_y[i];
		A_Matrix[1][1] = HermitMatrix[1][0]*FirstPoint.y + HermitMatrix[1][1]*SecondPoint.y + HermitMatrix[1][2]*Q_y[i-1] + HermitMatrix[1][3]*Q_y[i];
		A_Matrix[1][2] = HermitMatrix[2][0]*FirstPoint.y + HermitMatrix[2][1]*SecondPoint.y + HermitMatrix[2][2]*Q_y[i-1] + HermitMatrix[2][3]*Q_y[i];
		A_Matrix[1][3] = HermitMatrix[3][0]*FirstPoint.y + HermitMatrix[3][1]*SecondPoint.y + HermitMatrix[3][2]*Q_y[i-1] + HermitMatrix[3][3]*Q_y[i];

		float Step = 1/(float)CountSplinePoint;
		for(float j = 0; j <= 1+ Step; j = j + Step)
		{
			SecondPoint.x = ceil (A_Matrix[0][0] + A_Matrix[0][1]*j + A_Matrix[0][2]*pow(j,2) + A_Matrix[0][3]*pow(j,3));
			SecondPoint.y = ceil (A_Matrix[1][0] + A_Matrix[1][1]*j + A_Matrix[1][2]*pow(j,2) + A_Matrix[1][3]*pow(j,3));

			Form1->Image1->Canvas->Pen->Color = HermitColor;
			Form1->Image1->Canvas->MoveTo(FirstPoint.x*Zoom,FirstPoint.y*Zoom);
			Form1->Image1->Canvas->LineTo(SecondPoint.x*Zoom,SecondPoint.y*Zoom);

			FirstPoint.x = SecondPoint.x;
			FirstPoint.y = SecondPoint.y;
		}
	}
	delete[] Q_x;
	delete[] Q_y;
}

void GetQ(int LineCount, float* Q_x, float* Q_y)
{

	for(int i = 2; i < LineCount; i++)
	{
		Q_x[i-1] = 0.5*(StrToInt(Form1->StringGrid1->Cells[1][i+1])-StrToInt(Form1->StringGrid1->Cells[1][i-1]));
		Q_y[i-1] = 0.5*(StrToInt(Form1->StringGrid1->Cells[2][i+1])-StrToInt(Form1->StringGrid1->Cells[2][i-1]));
	}
	Q_x[0] = 2*(StrToInt(Form1->StringGrid1->Cells[1][2])-StrToInt(Form1->StringGrid1->Cells[1][1]))-Q_x[1];
	Q_y[0] = 2*(StrToInt(Form1->StringGrid1->Cells[2][2])-StrToInt(Form1->StringGrid1->Cells[2][1]))-Q_y[1];

	Q_x[LineCount-1] = 2*(StrToInt(Form1->StringGrid1->Cells[1][LineCount])-
		StrToInt(Form1->StringGrid1->Cells[1][LineCount-1]))-Q_x[LineCount-2];
	Q_y[LineCount-1] = 2*(StrToInt(Form1->StringGrid1->Cells[2][LineCount])-
		StrToInt(Form1->StringGrid1->Cells[2][LineCount-1]))-Q_y[LineCount-2];
}
//---------------------------------------------------------------------------
#pragma package(smart_init)
