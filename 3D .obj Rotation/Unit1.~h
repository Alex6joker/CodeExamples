//---------------------------------------------------------------------------
struct vert        // Структура координат вершин
{
        float x;
        float y;
        float z;
}vertices[1000];
struct vert transf_vertices[1000];// Структура преобразованных координат
struct indexs      // Структура индексов
{
        int index_vertices[4];
        int index_vertex_normals[4];
        int index_texture_coords[4];
        int number_of_index; // Кол-во значений индексов(3-triangles,4-polygons);
}index[1000];
struct coord
{
        int x; // Структура для занесения в нее координат Y при определенных Х
        int y;
}coord[60000];
int number_of_coords[1000]; // Кол-во эл-тов в структуре координат полигона X,Y при их изменении
int R,teta,phi,object_size,smech_x,smech_y; // Параметры прокрутки TrackBar
int k_vertices; // Общее кол-во строк координат вершин
int k_index; // Общее кол-во строк индексов
float d;
float mas[1000];  //  Массив расстояний до экрана
int mas_x[1000]; // Массив значений X с данной Y(j)
void read_from_file(char*);
void update_wind();
void viewport_transformation(struct vert[1000],struct vert[1000],int,int,int);
float perspective_transformation(int,int,int,int,struct vert[1000],struct vert[1000]);
void Screen_conversion(struct vert[1000],struct indexs[1000],float,int,int);
void sort_vertices(struct vert[1000],struct indexs[1000]);
void light(struct vert[1000],struct indexs[1000]);
void rastr(float,float,float,float);
int color_rastr(float,float,float,float,int);
void color_image(float,float,struct vert[1000],int,int,int);
#include <limits.h>
#include <stdio.h>
#include <string.h>
#include <math.h>
#include <Classes.hpp>
#include <Controls.hpp>
#include <ExtCtrls.hpp>
#include <StdCtrls.hpp>
#include <ComCtrls.hpp>
#include <DBCtrls.hpp>
#include <Mask.hpp>
#include <Dialogs.hpp>
#ifndef Unit1H
#define Unit1H
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <ExtCtrls.hpp>
//---------------------------------------------------------------------------
class TForm1 : public TForm
{
__published:	// IDE-managed Components
        TImage *Image1;
        TButton *Button1;
        TOpenDialog *OpenFileDialog1;
        TTrackBar *TrackBar1;
        TTrackBar *TrackBar2;
        TTrackBar *TrackBar3;
        TTrackBar *TrackBar4;
        TTrackBar *TrackBar5;
        TTrackBar *TrackBar6;
        TStaticText *StaticText1;
        TStaticText *StaticText7;
        TStaticText *StaticText6;
        TStaticText *StaticText5;
        TStaticText *StaticText4;
        TStaticText *StaticText3;
        TButton *Button2;
        void __fastcall Button1Click(TObject *Sender);
        void __fastcall TrackBar1Change(TObject *Sender);
        void __fastcall TrackBar2Change(TObject *Sender);
        void __fastcall TrackBar3Change(TObject *Sender);
        void __fastcall TrackBar4Change(TObject *Sender);
        void __fastcall TrackBar5Change(TObject *Sender);
        void __fastcall TrackBar6Change(TObject *Sender);
        void __fastcall Button2Click(TObject *Sender);
private:	// User declarations
public:		// User declarations
        __fastcall TForm1(TComponent* Owner);
};
//---------------------------------------------------------------------------
extern PACKAGE TForm1 *Form1;
//---------------------------------------------------------------------------
#endif

