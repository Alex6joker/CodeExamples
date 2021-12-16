using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace SAPR
{
    class Component
    {
        public string Name;
        public Texture2D Texture;
        public Rectangle Rectangle;
        public bool Active; //Можно ли разместить данный компонент на плате
        public bool Check; //Выбран ли данный компонент
        public int Temp; //Температура
        public string[] IncopatibleComponent;


        public Component(string nName, Texture2D nTexture, Rectangle nRectangle, int nTemp, string[] nIncComponent)
        {
            Name = nName;
            Texture = nTexture;
            Rectangle = nRectangle;
            Temp = nTemp;
            IncopatibleComponent = nIncComponent;

            Active = true;
            Check = false;
        }

        public void CheckClick()
        {
            Check = false;
            MouseState mouse = Mouse.GetState();
            if(mouse.X > Rectangle.X && mouse.X < Rectangle.X + Rectangle.Width &&
                mouse.Y > Rectangle.Y && mouse.Y < Rectangle.Y + Rectangle.Height)
            {
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    Check = true;
                }

            }
        }
    }
}
