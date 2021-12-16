using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PvZ
{
    public abstract class NPCs : Microsoft.Xna.Framework.DrawableGameComponent
    {
        protected int Health;
        protected SpriteBatch sbSpriteBatch;
        protected Texture2D NPCTexture;
        protected Vector2 Position;// = new Vector2(150, 30);
        protected Byte UnitType;
        protected Int32 FrameWidth; // Ширина одного кадра
        protected Int32 FrameHeight; // Высота одного кадра
        protected Int32 CurrentFrame; // Текущий кадр
        protected Int32 TotalFrames; // Общее число кадров в спрайте
        protected Int32 CurrentTime; // Время с последней смены кадра
        protected Int32 Period; // Период смены

        public NPCs(Game game, Vector2 nPosition) : base(game)
        {
            CurrentFrame = 0;
            CurrentTime = 0;
            sbSpriteBatch = new SpriteBatch(GraphicsDevice);
            Position = nPosition;
        }

        protected void Animate(GameTime gameTime)
        {
            int InternalPeriod = Period;
            CurrentTime += gameTime.ElapsedGameTime.Milliseconds;
            if (CurrentTime > InternalPeriod)
            {
                CurrentTime -= InternalPeriod;
                CurrentFrame++;
                if (CurrentFrame >= TotalFrames) CurrentFrame = 0;
            }
        }

        public void CorrectPositionVector(Vector2 nPosVector)
        {
            Position = nPosVector;
        }

        public override void Update(GameTime gameTime)
        {
            Animate(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            sbSpriteBatch.Begin();
            TotalFrames = NPCTexture.Width / FrameWidth;
            sbSpriteBatch.Draw(NPCTexture, new Rectangle((int)Position.X, (int)Position.Y + 30, FrameWidth, FrameHeight),
                new Rectangle(CurrentFrame * FrameWidth, 0, FrameWidth, FrameHeight), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.99f);
            sbSpriteBatch.End();
            base.Draw(gameTime);
        }

        public void GetDamage(int Damage)
        {
            if (Health < Damage)
                Health = 0;
            else
                Health -= Damage;
        }

        public int GetHealth()
        {
            return Health;
        }

        public Vector2 GetPositionVector()
        {
            return Position;
        }
    }
}
