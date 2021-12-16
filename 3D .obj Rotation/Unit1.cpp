//---------------------------------------------------------------------------
#include <vcl.h>
#pragma hdrstop
#include "Unit1.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TForm1 *Form1;
AnsiString FileName ="";
char* MyFile;
//---------------------------------------------------------------------------
__fastcall TForm1::TForm1(TComponent* Owner)
        : TForm(Owner)
{
}
//---------------------------------------------------------------------------
void __fastcall TForm1::Button1Click(TObject *Sender)
{
        if(OpenFileDialog1->Execute())// Если в результате вызова окна файл был выбран
        {
                FileName = OpenFileDialog1->FileName; // Получаем имя файла
                MyFile = FileName.c_str(); // Перевод Ansi в char* через функцию c_str()
        }
        else return; // Если файл не был выбран, то уходим из функции
        Image1->Picture->Bitmap = NULL;
        read_from_file(MyFile);
        Form1->Button2->Visible = true;
        Form1->StaticText1->Visible = true;
        Form1->StaticText4->Visible = true;
        Form1->StaticText3->Visible = true;
        Form1->StaticText5->Visible = true;
        Form1->StaticText6->Visible = true;
        Form1->StaticText7->Visible = true;
        Form1->TrackBar1->Visible = true;
        Form1->TrackBar2->Visible = true;
        Form1->TrackBar3->Visible = true;
        Form1->TrackBar4->Visible = true;
        Form1->TrackBar5->Visible = true;
        Form1->TrackBar6->Visible = true;
        Form1->Button1->Enabled = false;
        update_wind();
}
//---------------------------------------------------------------------------

void __fastcall TForm1::TrackBar1Change(TObject *Sender)
{
         smech_x = TrackBar1->Position;
         Image1->Picture->Bitmap = NULL;
         update_wind();
}
//---------------------------------------------------------------------------
void __fastcall TForm1::TrackBar2Change(TObject *Sender)
{
         smech_y = TrackBar2->Position;
         Image1->Picture->Bitmap = NULL;
         update_wind();
}
//---------------------------------------------------------------------------

void __fastcall TForm1::TrackBar3Change(TObject *Sender)
{
        R = TrackBar3->Position;
        Image1->Picture->Bitmap = NULL;
        update_wind();
}
//---------------------------------------------------------------------------
void __fastcall TForm1::TrackBar4Change(TObject *Sender)
{
        teta = TrackBar4->Position;
        Image1->Picture->Bitmap = NULL;
        update_wind();
}
//---------------------------------------------------------------------------
void __fastcall TForm1::TrackBar5Change(TObject *Sender)
{
        phi = TrackBar5->Position;
        Image1->Picture->Bitmap = NULL;
        update_wind();
}
//---------------------------------------------------------------------------

void __fastcall TForm1::TrackBar6Change(TObject *Sender)
{
        object_size = TrackBar6->Position;
        Image1->Picture->Bitmap = NULL;
        update_wind();
}
//---------------------------------------------------------------------------
void read_from_file(char* MyFile)
{
        FILE* f0;
        char c; // Для считывания символа
        char next;
        char* buffer;
        char* v = "v  ";
        f0 = fopen(MyFile,"r");
        buffer = (char*)malloc(4);
        // Доходим до места считывания вершин
        do
        {
               fgets(buffer,4,f0);
        }while(strspn(buffer,v)!=strlen(v));
        // На выходе в buffer содержит "v  "
        // Считываем вершины
        int i = 0;
        while(fscanf(f0,"%f%c%f%c%f",&(vertices[i].x),&c,&(vertices[i].y),&c,&(vertices[i].z)) == 5)
        {
                vertices[i].x *= 2;
                vertices[i].y *= 2;
                vertices[i].z *= 2;
                fscanf(f0,"%c",&c);  // Считываем \n
                fgets(buffer,4,f0);   // Читаем  "v  "
                k_vertices++;
                i++;
        }
        // Доходим до места считывания индексов вершин "f "
        do
        {
               fscanf(f0,"%c",&c);
        }while(c!='f');
        fscanf(f0,"%c",&c);
        if(c==' ')
        {
        // Считываем индексы
        i = 0;
        bool indexs = 1;
        while(indexs)
        {
                for(int j = 0;j<3;j++)
                {
                        if(fscanf(f0,"%d%c%d%c%d",&(index[i].index_vertices[j]),&c,&(index[i].index_vertex_normals[j]),&c,&(index[i].index_texture_coords[j])) != 5)
                        {
                                indexs = 0; // Если список индексов закончился
                                break;
                        }
                        fscanf(f0,"%c",&c);  // Считываем пробел
                }
                do
                {
                        fscanf(f0,"%c",&c);
                }while(c!='f' && c!='#');
                if(c=='#')
                {
                        indexs = 0;
                        break;
                }
                fscanf(f0,"%c",&c);
                i++;
                k_index++;
        }
        }
        free(buffer);
        fclose(f0);
        smech_x = Form1->TrackBar1->Position;
        smech_y = Form1->TrackBar2->Position;
        R = Form1->TrackBar3->Min;
        teta = Form1->TrackBar4->Min;
        phi = Form1->TrackBar5->Min;
        object_size = Form1->TrackBar6->Min;
        Form1->TrackBar1->Position = 0;
        Form1->TrackBar2->Position = 0;
        Form1->TrackBar3->Position = Form1->TrackBar3->Min;
        Form1->TrackBar4->Position = Form1->TrackBar4->Min;
        Form1->TrackBar5->Position = Form1->TrackBar5->Min;
        Form1->TrackBar6->Position = Form1->TrackBar6->Min;
}
//---------------------------------------------------------------------------
void update_wind()
{
       viewport_transformation(vertices,transf_vertices,R,teta,phi);  // Видовое преобразование
       //float d = perspective_transformation(R,Form1->Image1->Width/10,Form1->Image1->Height/10,object_size,transf_vertices,vertices); // Перспективное преобразование
       d = R*Form1->Image1->Width/10*Form1->Image1->Height/10/object_size;
       Screen_conversion(transf_vertices,index,d,smech_x,smech_y);   // Экранное преобразование
}
//---------------------------------------------------------------------------
void viewport_transformation(vert v[1000],vert res[1000],int R,int teta,int phi)
{
        const float PI = 3.141592653589793238462643383279;
        float teta1 = teta*PI/180;  // Радианы углов
        float phi1 = phi*PI/180;
        float B[4][4] = {-sin(teta1),-cos(phi1)*cos(teta1),-sin(phi1)*cos(teta1),0,cos(teta1),-cos(phi1)*sin(teta1),-sin(phi1)*sin(teta1),0,0,sin(phi1),-cos(phi1),0,0,0,R,1};
        for(int i = 0;i<k_vertices;i++)
        {
                res[i].x = v[i].y*B[1][0]+v[i].x*B[0][0];
                res[i].y = v[i].z*B[2][1]+v[i].x*B[0][1]+v[i].y*B[1][1];
                res[i].z = B[3][2]+v[i].z*B[2][2]+v[i].x*B[0][2]+v[i].y*B[1][2];
        }
        FILE* f0;
        f0 = fopen("Viewport.txt","w");
        for (int i = 0; i < k_vertices; i++)
        {
           fprintf(f0,"%f%c%f%c%f%s",res[i].x,' ',res[i].y,' ',res[i].z,"\n");
        }
        fclose(f0);
}
//---------------------------------------------------------------------------
float perspective_transformation(int R,int Width,int Height,int object_size,vert res[1000],vert v[1000])
{
        float B[4][4] = {1,0,0,0,0,1,0,0,0,0,0,(float)R*((float)Width*(float)Height/(float)object_size),0,0,0,1};
        for(int i = 0;i<k_vertices;i++)
        {
                float tempx = res[i].x;
                float tempy = res[i].y;
                res[i].x = tempy*B[1][0]+tempx*B[0][0];
                res[i].y = res[i].z*B[2][1]+tempx*B[0][1]+tempy*B[1][1];
                res[i].z = B[2][3]+res[i].z*B[2][2]+tempx*B[0][2]+tempy*B[1][2];
        }
        FILE* f0;
        f0 = fopen("Perspective.txt","w");
        for (int i = 0; i < k_vertices; i++)
        {
           fprintf(f0,"%f%c%f%c%f%s",res[i].x,' ',res[i].y,' ',res[i].z,"\n");
        }
        fclose(f0);
        return B[2][3];
}
//---------------------------------------------------------------------------
void Screen_conversion(vert res[1000],indexs index[1000],float d,int smech_x,int smech_y)
{
     float z1,z2; // Частное для получения преобразованных координат
     // Находим необходимые нам вершины для соединения и соединяем их
     for (int i = 0; i < k_index; i++)
     {
        for (int j = 0; j < index[i].number_of_index-1; j++)
         {
                z1 = d+1;
                z2 = d+1;
                rastr((res[index[i].index_vertices[j]-1].x/z1)+smech_x+300,(res[index[i].index_vertices[j]-1].y/z1)+smech_y+300,(res[index[i].index_vertices[j+1]-1].x/z2)+smech_x+300,(res[index[i].index_vertices[j+1]-1].y/z2)+smech_y+300);
         }
         z1 = d+1;  // Соединяем последнюю вершину с первой
         z2 = d+1;
         rastr((res[index[i].index_vertices[index[i].number_of_index-1]-1].x/z1)+smech_x+300,(res[index[i].index_vertices[index[i].number_of_index-1]-1].y/z1)+smech_y+300,(res[index[i].index_vertices[0]-1].x/z2)+smech_x+300,(res[index[i].index_vertices[0]-1].y/z2)+smech_y+300);
     }
}
//---------------------------------------------------------------------------
void rastr(float fx1,float fy1,float fx2,float fy2)
{
                float deltaX,deltaY,x,y;
                int iterations;
                int LX = abs(floor(fx1)-floor(fx2));
                int LY = abs(floor(fy1)-floor(fy2));
                if(LX>LY) iterations = LX;
                else iterations = LY;
                x = fx1;
                y = fy1;
                if(iterations == 0)
                {
                        Form1->Image1->Canvas->Pixels[x][y] = clBlack;
                        return;
                }
                deltaX = (fx2-fx1)/iterations;
                deltaY = (fy2-fy1)/iterations;
                while(iterations)
                {
                        x += deltaX;
                        y += deltaY;
                        Form1->Image1->Canvas->Pixels[x][y] = clBlack;
                        iterations--;
                }
}
//---------------------------------------------------------------------------
void sort_vertices(vert res[1000],indexs index[1000])   // Сортируем преобразованные координаты
{
        for(int i = 0;i < k_index; i++)// Обнуляем наш вектор расстояний
        {
                for(int j = 0; j < index[i].number_of_index; j++) // Пока есть индексы, определяющие вершины
                {
                        mas[i] = 0;
                }
        }
       for(int i = 0;i < k_index; i++) // Считаем расстояние до экрана для каждого полигона
       {
                for(int j = 0; j < index[i].number_of_index; j++) // Пока есть индексы, определяющие вершины
                {
                        mas[i] += res[index[i].index_vertices[j]-1].z; // Берем координату z нужной нам вершины по индексу
                }
                mas[i] /= index[i].number_of_index; // Находим среднее арифметическое
       }
        float temp; // Временная переменная для хранения данных
        // Производим сортировку списка
        // 1) Должны получить отсортированный список расстояний до экрана по возврастанию
        // 2) Поставить индексы вершин согласно отсортированному списку
        for(int i = 0; i < k_index; i++)
        {
                for(int j = 0; j < k_index; j++)
                {
                     if(mas[i] >= mas[j])  // Если это значение больше другого
                     {
                            for(int k = 0; k < 4; k++)// Производим замену индексов вершин для каждого из значений
                            {
                                temp = index[j].index_vertices[k]; // Заносим k-ый индекс вершины во временную переменную
                                index[j].index_vertices[k] = index[i].index_vertices[k];
                                index[i].index_vertices[k] = temp; // Меняем значения местами
                            }
                            temp = mas[j];    // Записываем меньшее расстояние во временную переменную
                            mas[j] = mas[i];
                            mas[i] = temp;   // Меняем значения местами
                     }
                }
        }
        // Совершаем пересчет кол-ва вершин, указанных индексами
        for(int i = 0; i < k_index; i++)
        {
                int k = 0;  // Кол-во ненулевых вершин
                for(int j = 0; j < 4; j++)
                {
                        if(index[i].index_vertices[j] != 0) k++;
                }
                index[i].number_of_index = k;
        }
     FILE* f0;
     f0 = fopen("Sort Z.txt","w");
     for (int i = 0; i < k_index; i++)
     {
        fprintf(f0,"%f%s",mas[i],"   ");
        for(int j = 0; j < index[i].number_of_index; j++)
        {
                fprintf(f0,"%d%c",index[i].index_vertices[j],'/');
        }
        fprintf(f0,"%s","\n");
     }
     fclose(f0);
}
//---------------------------------------------------------------------------
void __fastcall TForm1::Button2Click(TObject *Sender)
{
        sort_vertices(transf_vertices,index); // Сортировка полигонов по Z
        light(transf_vertices,index); // Расчет освещения каждого полигона
}
//---------------------------------------------------------------------------
void light(vert res[1000],indexs index[1000]) // Расчет освещения каждого полигона
{
        float wiki;
        float a[3] = {0,0,1};
        float norm[3];  // Координаты нормализированного вектора
        float n[3];  // Нормали по x,y,z
        float V[2][3];  // Расчетные векторы сторон
        float cos_teta,cos_phi;
        int i,j;
        // Рассчитываем нормаль для каждого полигона
        for (i = 0; i < k_index; i++) // Находим векторы сторон
        {
                // Из координат нынешней вершины вычитаем кординаты следующей
                V[0][0] = res[index[i].index_vertices[0]-1].x - res[index[i].index_vertices[1]-1].x;
                V[0][1] = res[index[i].index_vertices[0]-1].y - res[index[i].index_vertices[1]-1].y;
                V[0][2] = res[index[i].index_vertices[0]-1].z - res[index[i].index_vertices[1]-1].z;
                V[1][0] = res[index[i].index_vertices[1]-1].x - res[index[i].index_vertices[2]-1].x;
                V[1][1] = res[index[i].index_vertices[1]-1].y - res[index[i].index_vertices[2]-1].y;
                V[1][2] = res[index[i].index_vertices[1]-1].z - res[index[i].index_vertices[2]-1].z;
                n[0] = V[0][1]*V[1][2] - V[0][2]*V[1][1]; // nx
                n[1] = V[0][2]*V[1][0] - V[0][0]*V[1][2]; // ny
                n[2] = V[0][0]*V[1][1] - V[0][1]*V[1][0]; // nz
                wiki = sqrt(pow(n[0],2)+pow(n[1],2)+pow(n[2],2)); // Нормализация
                if(wiki == 0) // Если результат нормализации 0, то вектор - нулевой
                {
                    norm[0] = 0;
                    norm[1] = 0;
                    norm[2] = 0;
                }
                else  // Если результат нормализации не 0, то нормализируем координаты вектора
                {
                    norm[0] = n[0]/wiki;
                    norm[1] = n[1]/wiki;
                    norm[2] = n[2]/wiki;
                }
                // Считаем углы для закраски фигуры
                cos_teta = (norm[0]*a[0]+norm[1]*a[1]+norm[2]*a[2])/(sqrt(pow(norm[0],2)+pow(norm[1],2)+pow(norm[2],2))*(sqrt(pow(a[0],2)+pow(a[1],2)+pow(a[2],2))));
                cos_phi  = (norm[0]*a[0]+norm[1]*a[1]+norm[2]*a[2])/(sqrt(pow(norm[0],2)+pow(norm[1],2)+pow(norm[2],2))*(sqrt(pow(a[0],2)+pow(a[1],2)+pow(a[2],2))));
                int k = 0;
                float z1,z2;
                for (j = 0; j < index[i].number_of_index-1; j++)
                {
                        z1 = d+1;
                        z2 = d+1;
                        k = color_rastr(res[index[i].index_vertices[j]-1].x/z1+smech_x+300,res[index[i].index_vertices[j]-1].y/z1+smech_y+300,res[index[i].index_vertices[j+1]-1].x/z2+smech_x+300,res[index[i].index_vertices[j+1]-1].y/z2+smech_y+300,k);
                }
                z1 = d+1;  // Соединяем последнюю вершину с первой
                z2 = d+1;
                k = color_rastr(res[index[i].index_vertices[index[i].number_of_index-1]-1].x/z1+smech_x+300,res[index[i].index_vertices[index[i].number_of_index-1]-1].y/z1+smech_y+300,res[index[i].index_vertices[0]-1].x/z2+smech_x+300,res[index[i].index_vertices[0]-1].y/z2+smech_y+300,k);
                color_image(cos_teta,cos_phi,res,i,j,k);
        }
}
//---------------------------------------------------------------------------
void color_image(float cos_phi,float cos_teta,struct vert res[1000],int i,int j,int s)
{
    // Нам известно кол-во координат, которые изменили координату Y
    for(int k = 0;k<s;k++)
    {
    for(int l = 0;l<s;l++)
    {
        if(coord[k].y == coord[l].y && k!=l)
        {
        if(cos_phi>0) // Если свет падает на полигон
        {
                Form1->Image1->Canvas->Pen->Color=RGB(0,cos_phi*255,cos_teta*255);
                Form1->Image1->Canvas->MoveTo(coord[k].x,coord[k].y);
                Form1->Image1->Canvas->LineTo(coord[l].x,coord[l].y);
                Form1->Image1->Canvas->Pen->Color=clBlack;
                }
        }
        }
        }
    for(int j = 0; j < s; j++)
    {
        coord[j].x = 0;
        coord[j].y = 0;
    }
    Form1->Image1->Refresh();
    s = 0;
}
int color_rastr(float fx1,float fy1,float fx2,float fy2,int k)
{
                float deltaX,deltaY,x,y;
                int iterations;
                int LX = abs(floor(fx1)-floor(fx2));
                int LY = abs(floor(fy1)-floor(fy2));
                if(LX>LY) iterations = LX;
                else iterations = LY;
                x = fx1;
                y = fy1;
                if(iterations == 0)
                {
                        Form1->Image1->Canvas->Pixels[x][y] = clBlack;
                        return 0;
                }
                deltaX = (fx2-fx1)/iterations;
                deltaY = (fy2-fy1)/iterations;
                while(iterations)
                {
                        x += deltaX;
                        float temp = y; // Записываем старый y
                        y += deltaY;
                        if(floor(temp) != floor(y))  // Если y изменился
                        {
                                coord[k].x = x - deltaX;
                                coord[k].y = y - deltaX;
                                coord[k+1].x = x;
                                coord[k+1].y = y;
                                k += 2;
                        }
                        iterations--;
                }
        return k;
}



