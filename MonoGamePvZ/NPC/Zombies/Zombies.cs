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
    public class Zombies : NPCs
    {
        protected int Speed;
        protected int Speed_Old;
        public Rectangle ZombieRectangle;
        protected SoundEffect EatingPlantEffect;
        protected SoundEffect ZombieWalk;
        protected Timers.Timer WalkTimer;
        protected bool Freeze;
        protected Timers.Timer FreezeTimer;

        public Zombies(Game1 game, Vector2 nPosition) : base(game, nPosition)
        {
            EatingPlantEffect = Game.Content.Load<SoundEffect>("PlantDestroy");
            ZombieWalk = Game.Content.Load<SoundEffect>("Walk");
            WalkTimer = new Timers.Timer(Game1.ThisGame, Game1.ThisGame.Content.Load<SpriteFont>("MyFont"), new Vector2(250, 2), "00:00");
            FreezeTimer = new Timers.Timer(Game1.ThisGame, Game1.ThisGame.Content.Load<SpriteFont>("MyFont"), new Vector2(250, 2), "00:00");
            Freeze = false;
        }

        protected void FillZombieRectangle(Vector2 TextureCharacteristics)
        {
            ZombieRectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)TextureCharacteristics.X, (int)TextureCharacteristics.Y);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            EatSomePlant();
            WalkTimer.Update(gameTime);
            if(WalkTimer.SS == 1)
            {
                WalkTimer.SS = WalkTimer.MS = WalkTimer.MM = 0;
                ZombieWalk.Play();
            }
            if(Freeze)
            {
                FreezeTimer.Update(gameTime);
                if(FreezeTimer.MS >= 600)
                {
                    Speed = Speed_Old;
                    FreezeTimer.SS = FreezeTimer.MS = FreezeTimer.MM = 0;
                    Freeze = false;
                }
            }
        }

        protected void EatSomePlant()
        {
            for(int i = 0; i < 5; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    if(Game1.ThisGame.GameMap.CellsMatrix[i,j].PlantOnCell == null)
                        continue;
                    Rectangle PlantRectangle = new Rectangle((int)Game1.ThisGame.GameMap.CellsMatrix[i,j].PlantOnCell.GetPositionVector().X,
                        (int)Game1.ThisGame.GameMap.CellsMatrix[i,j].PlantOnCell.GetPositionVector().Y + 30, 40, 40);
                    if(ZombieRectangle.Intersects(PlantRectangle))
                    {
                        Game1.ThisGame.GameMap.CellsMatrix[i, j].PlantOnCell.Dispose();
                        Game1.ThisGame.GameMap.CellsMatrix[i, j].PlantOnCell = null;
                        EatingPlantEffect.Play();
                    }
                }
            }            
        }

        public void FreezeZombie()
        {
            Freeze = true;
            Speed = 0;
        }
    }
}
