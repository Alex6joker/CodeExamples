using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PvZ.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PvZ
{
    public class Plants : NPCs
    {
        protected Cells PlantOnThisCell; // На какой из клеток находится NPC
        public Timers.Timers PlantTimer;
        protected SpriteFont TimerFont;

        public Plants(Game1 game, Cells nCell, Vector2 nPosition) : base(game, nPosition)
        {
            PlantOnThisCell = nCell;            
            TimerFont = Game.Content.Load<SpriteFont>("MyFont");
            PlantTimer = new Timer(Game1.ThisGame, TimerFont, new Microsoft.Xna.Framework.Vector2(0, 0), "00:00");
            Health = 30;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime); // Должна проигрываться анимация
            PlantTimer.Update(gameTime); // Производим подсчет времени между выстрелами (или получением солнца)
        }        
    }
}
