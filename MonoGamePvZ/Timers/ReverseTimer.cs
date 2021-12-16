using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PvZ.Timers
{
    class ReverseTimer : Timers
    {
        public ReverseTimer(Game game, SpriteFont NFont, Vector2 NPosition, string NText, Int32 BeginMinutes, Int32 BeginSeconds) : base(game, NFont, NPosition, NText)
        {
            MM = BeginMinutes;
            SS = BeginSeconds;
            MS = 1000;
        }

        public override void Update(GameTime gameTime)
        {
            MS = MS - (int)gameTime.ElapsedGameTime.Milliseconds;
            if (MS <= 0)
            {
                MS = MS + 1000;
                SS--;
            }
            if (SS <= 0)
            {
                SS = SS + 60;
                MM--;
            }
            if (SS >= 10)
                Text = MM + ":" + SS;
            else
                Text = MM + ":0" + SS;
        }
    }
}
