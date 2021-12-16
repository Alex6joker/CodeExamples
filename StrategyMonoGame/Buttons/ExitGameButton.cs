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
    class ExitGameButton : cButton
    {
               
        private bool ToMenuButton; // Выход в главное меню, а не полный выход из игры
        
        public ExitGameButton(Game1 game) : base(game)
        {
            Size = new Vector2(Game1.graphics.PreferredBackBufferWidth / 8, Game1.graphics.PreferredBackBufferHeight / 30);
            Position = new Vector2(Game1.graphics.PreferredBackBufferWidth / 3, Game1.graphics.PreferredBackBufferHeight - 150);
            ToMenuButton = false;
            LoadContent();
            Button = new Rectangle((int)Position.X, (int)Position.Y, (int)ButtonTexture.Width, (int)ButtonTexture.Height);           
        }

        public ExitGameButton(Game1 game, bool nToMenuButton) : base(game)
        {
            Size = new Vector2(Game1.graphics.PreferredBackBufferWidth / 8, Game1.graphics.PreferredBackBufferHeight / 30);
            Position = new Vector2(Game1.graphics.PreferredBackBufferWidth / 3, Game1.graphics.PreferredBackBufferHeight / 1.5f);
            ToMenuButton = nToMenuButton;
            LoadContent();
            Button = new Rectangle((int)Position.X, (int)Position.Y, (int)ButtonTexture.Width, (int)ButtonTexture.Height);           
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            sbSpriteBatch = new SpriteBatch(GraphicsDevice);
            if (ToMenuButton)
                ButtonTexture = Game.Content.Load<Texture2D>("Выход в меню");
            else
                ButtonTexture = Game.Content.Load<Texture2D>("Выход");
            // TODO: use this.Content to load your game content here
        }

         protected override void NegativeButtonEvent()
         {
             if (ToMenuButton)
             {
                 PressedStatus = false;
             }
         }        

        protected override void PositiveButtonEvent()
        {
            if (ToMenuButton)
            {
                // Данная кнопка произведет выход в главное меню
                Game1.ThisGame.SetCurrentGameState(Game1.GameState.MainMenu);
                PressedStatus = true;
            }
            else
            {
                switch (Game1.ThisGame.GetCurrentGameState())
                {
                    case Game1.GameState.Pause:
                        {
                            // Данная кнопка произведет выход из игры
                            Game1.ThisGame.Exit();
                            break;
                        }
                    case Game1.GameState.MainMenu:
                        {
                            // Данная кнопка произведет выход из игры
                            Game1.ThisGame.Exit();
                            break;
                        }
                    case Game1.GameState.ResultsTable:
                        {
                            // Данная кнопка произведет выход в главное меню
                            Game1.ThisGame.SetCurrentGameState(Game1.GameState.MainMenu);
                            break;
                        }
                } 
            }                           
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
