using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace SAPR
{
    class Cell
    {
        public Rectangle Rectangle;
        public Texture2D Texture;
        public string Component;
        public int Temp;

        public Cell(Rectangle nRectangle, Texture2D nTexture)
        {
            Rectangle = nRectangle;
            Texture = nTexture;
            Component = "None";
            Temp = 0;
        }
    }
}
