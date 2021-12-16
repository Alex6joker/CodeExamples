using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PvZ
{
    public class SunMoney : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public bool DeleteThisSun;
        int XPosition;
        int YPosition;
        Texture2D SunTexture;
        SpriteBatch SpriteBatch;
        Rectangle SunMoneyRectangle;
        public int MoneyCash;

        MouseState LastState;
        MouseState CurrentState;

        public SunMoney(Game1 game, Vector2 nParentSunFlowerPosition) : base(game)
        {
            DeleteThisSun = false;
            XPosition = (int)nParentSunFlowerPosition.X - 20;
            YPosition = (int)nParentSunFlowerPosition.Y - 40;
            SunTexture = Game.Content.Load<Texture2D>("SunMoney");
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            CurrentState = Mouse.GetState();
            LastState = CurrentState;
            MoneyCash = 25;
            SunMoneyRectangle = new Rectangle(XPosition, YPosition, SunTexture.Width, SunTexture.Height);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            LastState = CurrentState;
            CurrentState = Mouse.GetState();
            Rectangle MouseRectangle = new Rectangle(CurrentState.X, CurrentState.Y, 1, 1);
            if (MouseRectangle.Intersects(SunMoneyRectangle) && CurrentState.LeftButton == ButtonState.Pressed && LastState.LeftButton == ButtonState.Released)
            {
                DeleteThisSun = true;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SpriteBatch.Begin();
            SpriteBatch.Draw(SunTexture, new Rectangle(XPosition, YPosition, SunTexture.Width, SunTexture.Height), Color.White);
            SpriteBatch.End();
        }
    }
}
