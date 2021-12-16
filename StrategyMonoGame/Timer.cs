using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Strategy
{
    public class Timer
    {
        public int MM { get; set; }
        public int SS { get; set; }
        public int MS { get; set; }
        public SpriteFont Font { get; set; }
        public string Text;
        public Vector2 Position { get; set; }


        public Timer(SpriteFont NewFont, Vector2 NewPosition, string NewText)
        {
            Font = NewFont;
            Position = NewPosition;
            Text = NewText;
            MM = SS = MS = 0;
        }

        public void Update(GameTime gameTime)
        {
            MS = MS + (int)gameTime.ElapsedGameTime.Milliseconds;
            if (MS >= 1000)
            {
                MS = MS - 1000;
                SS++;
            }
            if (SS >= 60)
            {
                SS = SS - 60;
                MM++;
            }

            if (SS >= 10)
                Text = MM + ":" + SS;
            else
                Text = MM + ":0" + SS;

        }

        public string GetText()
        {
            return Text;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, Text, Position, Color.White);
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.DrawString(Font, Text, Position, color);
        }
    }
}
