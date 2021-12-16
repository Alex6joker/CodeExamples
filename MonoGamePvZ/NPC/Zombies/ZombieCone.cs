using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PvZ
{
    public class ZombieCone : Zombie
    {
        Texture2D NPCTextureConus;
        
        public ZombieCone(Game1 game, Vector2 nPosition) : base(game, nPosition)
        {
            Speed = -1;
            Period = 150;
            FrameWidth = 82;
            FrameHeight = 120;
            NPCTexture = Game.Content.Load<Texture2D>("Zombie");
            NPCTextureConus = Game.Content.Load<Texture2D>("ZombieCone1");
            FillZombieRectangle(new Vector2(NPCTexture.Width, NPCTexture.Height));
            Health = 100;
            Speed_Old = Speed;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            Position.X += Speed / 2;
            ZombieRectangle.X = (int)Position.X;
            if(Health < 75)
            {
                //NPCTextureConus = Game.Content.Unload();
                NPCTextureConus = null;
                NPCTextureConus = Game.Content.Load<Texture2D>("ZombieCone2");
            }
            if (Health < 30)
            {
                //NPCTextureConus = Game.Content.Unload();
                NPCTextureConus = null;
                NPCTextureConus = Game.Content.Load<Texture2D>("ZombieCone3");
            }
        }

        public override void Draw(GameTime gameTime)
        {
            sbSpriteBatch.Begin();
            TotalFrames = NPCTexture.Width / FrameWidth;
            sbSpriteBatch.Draw(NPCTexture, new Rectangle((int)Position.X, (int)Position.Y + 30, FrameWidth, FrameHeight),
                new Rectangle(CurrentFrame * FrameWidth, 0, FrameWidth, FrameHeight), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.0f);
            sbSpriteBatch.End();
            sbSpriteBatch.Begin();
            sbSpriteBatch.Draw(NPCTextureConus, new Rectangle((int)Position.X, (int)Position.Y, NPCTextureConus.Width, NPCTextureConus.Height), Color.White);
            sbSpriteBatch.End();
        }
    }
}
