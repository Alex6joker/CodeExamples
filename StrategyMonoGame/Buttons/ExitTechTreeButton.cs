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
    class ExitTechTreeButton: cButton
    {

        public ExitTechTreeButton(Game1 game) : base(game)
        {
            Size = new Vector2(Game1.graphics.PreferredBackBufferWidth / 8, Game1.graphics.PreferredBackBufferHeight / 30);
            Position = new Vector2(1120, 60);
            LoadContent();
            Button = new Rectangle((int)Position.X, (int)Position.Y, (int)ButtonTexture.Width, (int)ButtonTexture.Height);           
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            sbSpriteBatch = new SpriteBatch(GraphicsDevice);
            ButtonTexture = Game.Content.Load<Texture2D>("Крестик");
            // TODO: use this.Content to load your game content here
        }

        protected override void PositiveButtonEvent()
        {
            // Продолжаем игру
            PressedStatus = true;
            Game1.ThisGame.GetTechTree().CloseTechTree();
        }

        protected override void NegativeButtonEvent()
        {
            PressedStatus = false;
        }

        public override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            sbSpriteBatch.Begin();
            sbSpriteBatch.Draw(ButtonTexture, new Rectangle((int)Position.X, (int)Position.Y, 20, 20), Color.White);
            sbSpriteBatch.End();
            //base.Draw(gameTime);
        }
    }
}
