using System;
using System.Drawing;

namespace BigDataAnalyzer.FileOpeners
{
    public struct RectangleOnPhotos
    {
        public Brush selectionBrush;
        public String name;
        public int type;
        public Rectangle rectangle;
        public Point rectStartPoint;

        public RectangleOnPhotos(string name, int type, Rectangle rectangle, Point rectStartPoint)
        {
            this.name = name;
            this.type = type;
            this.rectangle = rectangle;
            this.rectStartPoint = rectStartPoint;
            if (type == 0)
            {
                selectionBrush = new SolidBrush(Color.FromArgb(150, 72, 145, 220)); // blue
            }
            else if(type == 1)
            {
                selectionBrush = new SolidBrush(Color.FromArgb(150, 172, 45, 20)); // red
            }
            else
            {
                selectionBrush = new SolidBrush(Color.FromArgb(255, 255, 255, 255)); // NONE
            }
        }
    }
}