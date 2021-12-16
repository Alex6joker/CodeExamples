using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PvZ
{
    public class Zombie : Zombies
    {
        
        public Zombie(Game1 game, Vector2 nPosition) : base(game, nPosition)
        {
            Speed = -1;
            Period = 150;
            FrameWidth = 82;
            FrameHeight = 120;
            NPCTexture = Game.Content.Load<Texture2D>("Zombie");
            FillZombieRectangle(new Vector2(NPCTexture.Width, NPCTexture.Height));
            Health = 30;
            Speed_Old = Speed;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            Position.X += Speed;
            ZombieRectangle.X = (int)Position.X;
        }
    }
}
