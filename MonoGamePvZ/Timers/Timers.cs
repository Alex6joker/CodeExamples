using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PvZ.Timers
{
    public class Timers : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public int MM { get; set; }
        public int SS { get; set; }
        public int MS { get; set; }
        public string Text { get; set; }

        protected SpriteBatch spriteBatch;
        protected Vector2 Position;
        protected SpriteFont Font;

        public Timers(Game game, SpriteFont NFont, Vector2 NPosition, string NText) : base(game)
        {
            Font = NFont;
            Position = NPosition;
            Text = NText;
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(Font, Text, Position, Color.White);
            spriteBatch.End();
        }

        public void Draw(GameTime gameTime, Color color)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(Font, Text, Position, color);
            spriteBatch.End();
        }
    }
}
