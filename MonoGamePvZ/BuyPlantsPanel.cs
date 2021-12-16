using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PvZ
{
    public class BuyPlantsPanel : Microsoft.Xna.Framework.DrawableGameComponent
    {        
        public struct BuyingPlants
        {
            public Plants Plant;
            public Rectangle RectangleToBuy;
            public bool Enabled;
            public Texture2D BuyPlantTexture;
            public bool ThisPlantSelected;
            public int Cost;
        }
        
        Texture2D BuyPlantsPanelTexture; // Текстура панели
        SpriteBatch SpriteBatch;
        SpriteFont SpriteFont;
        public int SunMoney;
        MouseState LastState;
        MouseState CurrentState;

        public BuyingPlants[] BuyingPlantsVar;
        GameMap ThisMap;


        public BuyPlantsPanel(Game game, GameMap nThisMap) : base(game)
        {
            BuyPlantsPanelTexture = Game.Content.Load<Texture2D>("BuyPlantsPanel");
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            SpriteFont = Game.Content.Load<SpriteFont>("MyFont");
            SunMoney = 550;

            BuyingPlantsVar = new BuyingPlants[3];
            BuyingPlantsVar[0].Plant = new PeaShooter(Game1.ThisGame, null, new Vector2(0, 0));
            BuyingPlantsVar[0].RectangleToBuy = new Rectangle(75 , 5 , 50, 75);
            BuyingPlantsVar[0].BuyPlantTexture = Game.Content.Load<Texture2D>("PeaBuy");
            BuyingPlantsVar[0].Enabled = true;
            BuyingPlantsVar[0].ThisPlantSelected = false;
            BuyingPlantsVar[0].Cost = 100;
            BuyingPlantsVar[1].Plant = new SunFlower(Game1.ThisGame, null, new Vector2(0, 0));
            BuyingPlantsVar[1].RectangleToBuy = new Rectangle(130, 5, 50, 75);
            BuyingPlantsVar[1].BuyPlantTexture = Game.Content.Load<Texture2D>("SunFlowerBuy");
            BuyingPlantsVar[1].Enabled = false;
            BuyingPlantsVar[1].ThisPlantSelected = false;
            BuyingPlantsVar[1].Cost = 50;
            BuyingPlantsVar[2].Plant = new FreezePeaShooter(Game1.ThisGame, null, new Vector2(0, 0));
            BuyingPlantsVar[2].RectangleToBuy = new Rectangle(185, 5, 50, 75);
            BuyingPlantsVar[2].BuyPlantTexture = Game.Content.Load<Texture2D>("FreezePeaShooter");
            BuyingPlantsVar[2].Enabled = true;
            BuyingPlantsVar[2].ThisPlantSelected = false;
            BuyingPlantsVar[2].Cost = 125;
            ThisMap = nThisMap;
        }

        public override void Update(GameTime gameTime)
        {
            LastState = CurrentState;
            CurrentState = Mouse.GetState();
            Rectangle MouseRectangle = new Rectangle(CurrentState.X, CurrentState.Y, 1, 1);
            for (int i = 0; i < BuyingPlantsVar.Length; i++)
            {
                if (MouseRectangle.Intersects(BuyingPlantsVar[i].RectangleToBuy) && CurrentState.LeftButton == ButtonState.Pressed && LastState.LeftButton == ButtonState.Released
                    && BuyingPlantsVar[i].Enabled)
                {
                    PositiveButtonEvent(i + 1);
                    BuyingPlantsVar[i].ThisPlantSelected = true;
                }
                if (!MouseRectangle.Intersects(BuyingPlantsVar[i].RectangleToBuy) && CurrentState.LeftButton == ButtonState.Pressed && LastState.LeftButton == ButtonState.Released
                    && BuyingPlantsVar[i].Enabled && BuyingPlantsVar[i].ThisPlantSelected && SunMoney >= BuyingPlantsVar[i].Cost)
                {
                    ThisMap.SetNewPlant(i);
                }
                if (CurrentState.RightButton == ButtonState.Pressed && LastState.RightButton == ButtonState.Released && BuyingPlantsVar[i].ThisPlantSelected)
                {
                    UnsetDrawableSelectedPlant();
                }
            }                
        }

        void UnsetDrawableSelectedPlant()
        {
            ThisMap.UnsetDrawablePlant();
        }

        protected void PositiveButtonEvent(int Type)
        {
            ThisMap.SetDrawableSelectedPlant(Type);
        }

        protected void NegativeButtonEvent()
        {

        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(BuyPlantsPanelTexture, new Rectangle(0, 0, BuyPlantsPanelTexture.Width, BuyPlantsPanelTexture.Height), Color.White);
            SpriteBatch.DrawString(SpriteFont, SunMoney.ToString(), new Vector2((int)30, (int)BuyPlantsPanelTexture.Height - 25), Color.Black);

            for(int i = 0; i < BuyingPlantsVar.Length; i++)
            {
                if (BuyingPlantsVar[i].Enabled)
                    SpriteBatch.Draw(BuyingPlantsVar[i].BuyPlantTexture, BuyingPlantsVar[i].RectangleToBuy, Color.White);
                else
                    SpriteBatch.Draw(BuyingPlantsVar[i].BuyPlantTexture, BuyingPlantsVar[i].RectangleToBuy, Color.Red);
            }
            SpriteBatch.End();
        }
    }
}
