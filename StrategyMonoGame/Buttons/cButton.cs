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
    abstract class cButton : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public Texture2D ButtonTexture;
        public Vector2 Position;
        protected Color Colour = new Color(255, 255, 255);
        protected Vector2 Size;
        protected SpriteBatch sbSpriteBatch;
        protected Rectangle Button;
        protected bool PressedStatus;
        protected MouseState LastState;
        protected MouseState CurrentState;
        protected SoundEffect Sound;

        public cButton(Game1 game) : base(game)
        {
            PressedStatus = false;
            Sound = Game1.ThisGame.Content.Load<SoundEffect>("ButtonClick");
        }

        public override void Update(GameTime gameTime)
        {
            LastState = CurrentState;
            CurrentState = Mouse.GetState();
            Rectangle MouseRectangle = new Rectangle(CurrentState.X, CurrentState.Y, 1, 1);
            if (MouseRectangle.Intersects(Button) && CurrentState.LeftButton == ButtonState.Pressed && LastState.LeftButton == ButtonState.Released)
            {
                PositiveButtonEvent();
                Sound.Play();
            }
            else
            {
                NegativeButtonEvent();
            }
            base.Update(gameTime);
        }

        protected virtual void PositiveButtonEvent()
        {

        }

        protected virtual void NegativeButtonEvent()
        {

        }

        public bool GetPressedStatus()
        {
            return PressedStatus;
        }
    }
}
