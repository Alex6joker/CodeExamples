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
    public class PeaShooter : Plants
    {
        protected SoundEffect SoundShot;
        
        public PeaShooter(Game1 game, Cells nCell, Vector2 nPosition) : base(game, nCell, nPosition)
        {
            Period = 150;
            FrameWidth = 80;
            FrameHeight = 80;
            NPCTexture = Game.Content.Load<Texture2D>("PeaShooter");
            SoundShot = Game.Content.Load<SoundEffect>("Shot");
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            if (PlantTimer.SS == 1)
            {   // Каждую секунду производим выстрел
                PlantTimer.MM = PlantTimer.MS = PlantTimer.SS = 0;
                
                for(int i = 0; i < GameMap.Zombies.Length; i++)
                {
                    if (GameMap.Zombies[i] != null && 
                        (Position.Y + 30) > GameMap.Zombies[i].ZombieRectangle.Y && (Position.Y + 30) <= GameMap.Zombies[i].ZombieRectangle.Y + GameMap.Zombies[i].ZombieRectangle.Height)
                    {
                        CreatePea();
                        SoundShot.Play();
                    }
                }                
            }                
        }

        protected virtual void CreatePea()
        {
            Array.Resize<Pea>(ref GameMap.Peas, GameMap.Peas.Length + 1);
            GameMap.Peas[GameMap.Peas.Length - 1] = new Pea(Game1.ThisGame, new Vector2(Position.X + FrameWidth - 30, Position.Y + 30));
        }
    }
}
