using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PvZ.Labels
{
    class Label : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteFont SpriteFont;
        Vector2 Position;
        SpriteBatch SpriteBatch;
        String Text;

        public Label(Game game, SpriteFont nSpriteFont, Vector2 nPosition, String nText) : base(game)
        {
            SpriteFont = nSpriteFont;
            Text = nText;
            Position = nPosition;
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.DrawString(SpriteFont, Text, Position, Color.White);
        }

        public void Draw(GameTime gameTime, Color Color)
        {
            SpriteBatch.DrawString(SpriteFont, Text, Position, Color);
        }
    }
}
