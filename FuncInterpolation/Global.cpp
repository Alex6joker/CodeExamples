//---------------------------------------------------------------------------

#pragma hdrstop

#include "Global.h"

	float Zoom = 1;

	const TColor HermitColor = RGB(255,80,80);
	const TColor LagrangeColor = RGB(208,255,0);
	const TColor NewtonColor = RGB(0,200,225);
	const TColor BernstainColor = RGB(90,255,0);
	const TColor DeCastejoColor = RGB(255,200,0);
	const TColor LinearColor = RGB(64,64,64);
	const TColor BgColor = RGB(25,29,34);
	const TColor GridColor = RGB(50,50,50);
	const TColor SelectColor = RGB(255,0,0);

	const int CountSplinePoint = 15;
	int CellSize = 20;
	int SearchRadius = 20;
//---------------------------------------------------------------------------
#pragma package(smart_init)
