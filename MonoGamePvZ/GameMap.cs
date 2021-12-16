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
    public class GameMap : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Texture2D BgLevelTexture; // Текстура уровня
        SpriteBatch SpriteBatch;
        Vector2 TextureBgCutPosition; // Позиция, от которой вырезаем нужную часть картинки
        public Cells[,] CellsMatrix; // Клетки, на которые ставятся растения
        public BuyPlantsPanel BuyPlantsPanel; // Панель покупки растений
        public static Pea[] Peas; // Выстрелы от различных горохометов
        public static SunMoney[] SunMoneys;
        public static Zombies[] Zombies;
        Level Levels;
        public Timers.Timer GameTimer;
        SoundEffect ZombieDestroy;

        public struct SelectedPlant
        {
            public bool PlantFromPanelSelected;
            public int PlantType;
            public Texture2D Plant;
        }

        SelectedPlant SelectedPlantVar;

        public GameMap(Game game) : base(game)
        {
            BgLevelTexture = Game.Content.Load<Texture2D>("BgLevel");
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            TextureBgCutPosition = new Vector2(160, 0);
            Levels = new Level(Game1.ThisGame, this);
            GameTimer = new Timers.Timer(Game1.ThisGame, Game.Content.Load<SpriteFont>("MyFont"), new Vector2(250, 2), "00:00");

            // Инициализируем клетки уровня
            int XCellPosition = 90;
            int YCellPosition = 50;
            int Lenght = 0;
            CellsMatrix = new Cells[5, 9];
            for(int i = 0; i < 5; i++)
            {               
                if (i != 0)
                    YCellPosition += 120;
                XCellPosition = 90;
                for (int j = 0; j < 9; j++)
                {
                    if (j % 2 == 1)
                    {
                        Lenght = 120;
                        XCellPosition += 120;
                    }
                    else
                    {
                        Lenght = 90;
                        XCellPosition += 90;
                    }
                    CellsMatrix[i, j] = new Cells(Game1.ThisGame, new Vector2(XCellPosition, YCellPosition), Lenght);
                }
            }
            ZombieDestroy = Game.Content.Load<SoundEffect>("ZombieDestroy");
            
            // Инициализируем панель покупки растений
            BuyPlantsPanel = new BuyPlantsPanel(Game1.ThisGame, this);

            Peas = new Pea[0];
            SunMoneys = new SunMoney[0];

            Zombies = new Zombies[0];
            SelectedPlantVar.PlantFromPanelSelected = false;
            SelectedPlantVar.PlantType = 0;
            SelectedPlantVar.Plant = null;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            BuyPlantsPanel.Update(gameTime);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    CellsMatrix[i, j].Update(gameTime);
                }
            }
            for (int i = 0; i < Peas.Length; i++)
            {
                if(Peas[i] != null)
                {
                    Peas[i].Update(gameTime);
                    if (Peas[i].DeleteThisPea)
                    {
                        Peas[i].Dispose();
                        Peas[i] = null;
                    } 
                }                                   
            }
            for (int i = 0; i < SunMoneys.Length; i++)
            {
                if (SunMoneys[i] != null)
                {
                    SunMoneys[i].Update(gameTime);
                    if (SunMoneys[i].DeleteThisSun)
                    {
                        BuyPlantsPanel.SunMoney += SunMoneys[i].MoneyCash;
                        SunMoneys[i].Dispose();
                        SunMoneys[i] = null;
                    }   
                }                                 
            }
            for (int i = 0; i < Zombies.Length; i++)
            {
                if (Zombies[i] != null)
                {
                    Zombies[i].Update(gameTime);
                    if (Zombies[i].GetHealth() == 0)
                    {
                        Zombies[i].Dispose();
                        Zombies[i] = null;
                        ZombieDestroy.Play();
                    }
                }                    
            }
            if(SelectedPlantVar.PlantFromPanelSelected)
            {
                if (SelectedPlantVar.PlantType == 1)
                {
                    SelectedPlantVar.Plant = Game.Content.Load<Texture2D>("PeaShooter");
                }
                else if (SelectedPlantVar.PlantType == 2)
                {
                    SelectedPlantVar.Plant = Game.Content.Load<Texture2D>("SunFlower");
                }
                else if (SelectedPlantVar.PlantType == 3)
                {
                    SelectedPlantVar.Plant = Game.Content.Load<Texture2D>("PeaShooter");
                }
            }
            Levels.Update(gameTime);
            GameTimer.Update(gameTime);
        }

        public void SetNewPlant(int Type)
        {
            MouseState MState = Mouse.GetState();
            Cells TargetCell = CalculateCellByMousePosition(MState.X, MState.Y);
            if (TargetCell != null && TargetCell.PlantOnCell == null)
            {
                if (SelectedPlantVar.PlantType == 1)
                {
                    TargetCell.PlantOnCell = BuyPlantsPanel.BuyingPlantsVar[0].Plant;
                    BuyPlantsPanel.BuyingPlantsVar[0].Plant = new PeaShooter(Game1.ThisGame, null, new Vector2(0, 0));
                    BuyPlantsPanel.BuyingPlantsVar[0].ThisPlantSelected = false;
                }
                else if (SelectedPlantVar.PlantType == 2)
                {
                    TargetCell.PlantOnCell = BuyPlantsPanel.BuyingPlantsVar[1].Plant;
                    BuyPlantsPanel.BuyingPlantsVar[1].Plant = new SunFlower(Game1.ThisGame, null, new Vector2(0, 0));
                    BuyPlantsPanel.BuyingPlantsVar[1].ThisPlantSelected = false;
                }
                else if (SelectedPlantVar.PlantType == 3)
                {
                    TargetCell.PlantOnCell = BuyPlantsPanel.BuyingPlantsVar[2].Plant;
                    BuyPlantsPanel.BuyingPlantsVar[2].Plant = new FreezePeaShooter(Game1.ThisGame, null, new Vector2(0, 0));
                    BuyPlantsPanel.BuyingPlantsVar[2].ThisPlantSelected = false;
                }
                TargetCell.PlantOnCell.CorrectPositionVector(TargetCell.CellPosition);
                SelectedPlantVar.PlantFromPanelSelected = false;
                SelectedPlantVar.PlantType = 0;
                SelectedPlantVar.Plant = null;
                BuyPlantsPanel.SunMoney -= BuyPlantsPanel.BuyingPlantsVar[Type].Cost;
            }                
        }

        public void UnsetDrawablePlant()
        {
            
            SelectedPlantVar.PlantFromPanelSelected = false;
            SelectedPlantVar.PlantType = 0;
            SelectedPlantVar.Plant = null;
        }

        Cells CalculateCellByMousePosition(int X, int Y)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if(X >= CellsMatrix[i,j].CellPosition.X && X < CellsMatrix[i,j].CellPosition.X + CellsMatrix[i,j].CellLenght
                        && Y >= CellsMatrix[i,j].CellPosition.Y && Y < CellsMatrix[i,j].CellPosition.Y + 120)
                    {
                        return CellsMatrix[i, j];
                    }
                }
            }
            return null;
        }

        public void SetDrawableSelectedPlant(int Type)
        {
            SelectedPlantVar.PlantFromPanelSelected = true;
            SelectedPlantVar.PlantType = Type;
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(BgLevelTexture, new Rectangle(0,0, Game1.graphics.PreferredBackBufferWidth, Game1.graphics.PreferredBackBufferHeight),
                new Rectangle((int)TextureBgCutPosition.X, (int)TextureBgCutPosition.Y, BgLevelTexture.Width - (int)TextureBgCutPosition.X, BgLevelTexture.Height), Color.White);
            if (SelectedPlantVar.PlantFromPanelSelected)
            {
                MouseState MState = Mouse.GetState();
                Cells TargetCell = CalculateCellByMousePosition(MState.X, MState.Y);
                if (TargetCell != null && TargetCell.PlantOnCell == null)
                {
                        SpriteBatch.Draw(SelectedPlantVar.Plant, new Rectangle((int)TargetCell.CellPosition.X, (int)TargetCell.CellPosition.Y + 30, 80, 80),
                    new Rectangle(80, 0, 80, 80), new Color(255, 255, 255, 20), 0, Vector2.Zero, SpriteEffects.None, 0.99f);
                }                
            }
            SpriteBatch.End();
            BuyPlantsPanel.Draw(gameTime);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    CellsMatrix[i, j].Draw(gameTime);
                }
            }
            for (int i = 0; i < Peas.Length; i++)
            {
                if(Peas[i] != null)
                    Peas[i].Draw(gameTime);
            }
                
            for (int i = 0; i < SunMoneys.Length; i++)
            {
                if (SunMoneys[i] != null)
                    SunMoneys[i].Draw(gameTime);
            }
            for (int i = 0; i < Zombies.Length; i++)
            {
                if (Zombies[i] != null)
                    Zombies[i].Draw(gameTime);
            }
            Levels.Draw(gameTime);
        }

        public void GenerateEneny(int Type)
        {
            Random Rnd = new Random();
            int[] YPositions = new int[5];
            int Y = 15;
            for (int i = 0; i < 5; i++ )
            {
                YPositions[i] = Y;
                Y += 120;
            }
            if (Type == 1)
            {
                Array.Resize<Zombies>(ref Zombies, Zombies.Length + 1);
                //Zombies[Zombies.Length - 1] = new Zombie(Game1.ThisGame, new Vector2(Game1.graphics.PreferredBackBufferWidth, Rnd.Next(50, Game1.graphics.PreferredBackBufferHeight - 260)));
                Zombies[Zombies.Length - 1] = new Zombie(Game1.ThisGame, new Vector2(Game1.graphics.PreferredBackBufferWidth, YPositions[Rnd.Next(0, 5)]));
            }
            else
            {
                Array.Resize<Zombies>(ref Zombies, Zombies.Length + 1);
                //Zombies[Zombies.Length - 1] = new ZombieCone(Game1.ThisGame, new Vector2(Game1.graphics.PreferredBackBufferWidth, Rnd.Next(50, Game1.graphics.PreferredBackBufferHeight - 260)));
                Zombies[Zombies.Length - 1] = new ZombieCone(Game1.ThisGame, new Vector2(Game1.graphics.PreferredBackBufferWidth, YPositions[Rnd.Next(0, 5)]));
            }
        }
    }
}
