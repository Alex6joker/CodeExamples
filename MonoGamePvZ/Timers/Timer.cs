using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PvZ.Timers
{
    public class Timer : Timers
    {
        public Timer(Game game, SpriteFont NFont, Vector2 NPosition, string NText) : base(game, NFont, NPosition, NText)
        {
            MM = SS = MS = 0;
        }

        public override void Update(GameTime gameTime)
        {
            MS = MS + (int)gameTime.ElapsedGameTime.Milliseconds;
            if (MS >= 1000)
            {
                MS = MS - 1000;
                SS++;
            }
            if (SS >= 60)
            {
                SS = SS - 60;
                MM++;
            }
            if (SS >= 10)
                Text = MM + ":" + SS;
            else
                Text = MM + ":0" + SS;
        }
    }
}
