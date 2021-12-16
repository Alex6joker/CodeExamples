using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace PvZ
{
    public class FreezePeaShooter : PeaShooter
    {        
        public FreezePeaShooter(Game1 game, Cells nCell, Vector2 nPosition) : base(game, nCell, nPosition)
        {
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);             
        }

        protected override void CreatePea()
        {
            Array.Resize<Pea>(ref GameMap.Peas, GameMap.Peas.Length + 1);
            GameMap.Peas[GameMap.Peas.Length - 1] = new Pea(Game1.ThisGame, new Vector2(Position.X + FrameWidth - 30, Position.Y + 30), true);
        }
    }
}
