using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace PvZ
{
    public class Pea : Microsoft.Xna.Framework.DrawableGameComponent
    {
        int Speed;
        public bool DeleteThisPea;
        int XPosition;
        int YPosition;
        Texture2D PeaTexture;
        SpriteBatch SpriteBatch;
        SoundEffect ShotEffect;
        bool Freeze;
        
        public Pea(Game1 game, Vector2 nPeaPosition) : base(game)
        {
            Speed = 3;
            DeleteThisPea = false;
            XPosition = (int)nPeaPosition.X;
            YPosition = (int)nPeaPosition.Y;
            PeaTexture = Game.Content.Load<Texture2D>("Pea");
            ShotEffect = Game.Content.Load<SoundEffect>("PeaRelease");
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Freeze = false;
        }

        public Pea(Game1 game, Vector2 nPeaPosition, bool nFreeze) : this(game, nPeaPosition)
        {
            Freeze = nFreeze;
        }

        public override void Update(GameTime gameTime)
        {
            if (XPosition > Game1.graphics.PreferredBackBufferWidth)
                DeleteThisPea = true;
            XPosition += Speed;
            for(int i = 0; i < GameMap.Zombies.Length; i++)
            {   // Попали в зомби
                if(GameMap.Zombies[i] != null && XPosition >= GameMap.Zombies[i].ZombieRectangle.X && YPosition >= GameMap.Zombies[i].ZombieRectangle.Y
                    && YPosition < GameMap.Zombies[i].ZombieRectangle.Y + GameMap.Zombies[i].ZombieRectangle.Height)
                {
                    DeleteThisPea = true;
                    GameMap.Zombies[i].GetDamage(5);
                    // Заморозка зомби при попадании
                    if(Freeze)
                    {
                        GameMap.Zombies[i].FreezeZombie();
                    }
                }
            }
            if(DeleteThisPea)
            {
                ShotEffect.Play();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SpriteBatch.Begin();
            if(!Freeze)
                SpriteBatch.Draw(PeaTexture, new Rectangle(XPosition, YPosition, PeaTexture.Width, PeaTexture.Height), Color.White);
            else
                SpriteBatch.Draw(PeaTexture, new Rectangle(XPosition, YPosition, PeaTexture.Width, PeaTexture.Height), Color.Blue);
            SpriteBatch.End();
        }
    }
}
