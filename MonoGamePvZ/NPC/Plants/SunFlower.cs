using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PvZ
{
    public class SunFlower : Plants
    {
        Random RandSeconds;
        int NextMoneySeconds;
        
        public SunFlower(Game1 game, Cells nCell, Vector2 nPosition) : base(game, nCell, nPosition)
        {
            Period = 150;
            FrameWidth = 80;
            FrameHeight = 80;
            NPCTexture = Game.Content.Load<Texture2D>("SunFlower");
            RandSeconds = new Random();
            NextMoneySeconds = RandSeconds.Next(10, 30);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            if (PlantTimer.SS == NextMoneySeconds)
            {
                PlantTimer.MM = PlantTimer.MS = PlantTimer.SS = 0;
                NextMoneySeconds = RandSeconds.Next(10, 30);
                Array.Resize<SunMoney>(ref GameMap.SunMoneys, GameMap.SunMoneys.Length + 1);
                GameMap.SunMoneys[GameMap.SunMoneys.Length - 1] = new SunMoney(Game1.ThisGame, new Vector2(Position.X - 20, Position.Y + 80));
            }                
        }
    }
}
