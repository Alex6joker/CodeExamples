using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Strategy
{
    class GridMapButton : cButton
    {
        public GridMapButton(Game1 game) : base(game)
        {
            Size = new Vector2(Game1.graphics.PreferredBackBufferWidth / 8, Game1.graphics.PreferredBackBufferHeight / 30);
            Position = new Vector2(Game1.graphics.PreferredBackBufferWidth - 300, Game1.graphics.PreferredBackBufferHeight - 200);
            LoadContent();
            Button = new Rectangle((int)Position.X, (int)Position.Y, (int)ButtonTexture.Width, (int)ButtonTexture.Height);           
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            sbSpriteBatch = new SpriteBatch(GraphicsDevice);
            ButtonTexture = Game.Content.Load<Texture2D>("Сетка карты");
            // TODO: use this.Content to load your game content here
        }

        protected override void PositiveButtonEvent()
        {
            // Статус данной кнопки говорит о том, нужно ли показывать сетку карты или нет
            // является переключателем
            if (!PressedStatus)
                PressedStatus = true;
            else
                PressedStatus = false;
        }

        public override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            sbSpriteBatch.Begin();
            sbSpriteBatch.Draw(ButtonTexture, Position, Color.White);
            sbSpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
