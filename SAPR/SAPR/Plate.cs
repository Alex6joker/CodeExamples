using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace SAPR
{
    class Plate
    {
        public int SizePlate; //Длина платы в ячейчках
        public int SizeCell; //Ширина и высота ячейки (в пикселях)
        public int Margin; //Отступ между ячейками

        public Cell[] Cell; //Массив ячеек платы

        public Texture2D TextureBG; //Текстура для фона

        public Plate(int nSizePlate,int nSizeCell, int nMargin)
        {
            SizePlate = nSizePlate;
            SizeCell = nSizeCell;
            Margin = nMargin;
        }
    }
}
