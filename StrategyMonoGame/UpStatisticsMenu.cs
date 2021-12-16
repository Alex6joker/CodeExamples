using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Strategy
{
    public class UpStatisticsMenu : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Texture2D ScienseTexture; // Текстура значка науки
        Texture2D GoldTexture; // Текстура значка золота
        Texture2D Treasury; // Казна

        public Texture2D UpperMenu; // Текстура верней статистической панели
        String Score;   // Строка с обозначением счета игрока
        SpriteBatch spriteBatch;
        String Turn;
        SpriteFont spriteFont;
        static public Int32 ScienseScore;
        static public Int32 GoldScore;
        static public Int32 PlayerScore;
        Game1 ThisGame;
        public Timer GameTimer;
        MouseState LastState;
        MouseState CurrentState;
        Rectangle TechTreeRect;       

        public UpStatisticsMenu(Game1 game) : base(game)
        {
            Score = "Счет: ";
            Turn = "Ход: ";
            ScienseScore = 0;
            GoldScore = 0;
            PlayerScore = 0;
            ThisGame = game;
            LoadContent();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ScienseTexture = Game.Content.Load<Texture2D>("Science");
            GoldTexture = Game.Content.Load<Texture2D>("Gold");
            Treasury = Game.Content.Load<Texture2D>("Казна");
            UpperMenu = Game.Content.Load<Texture2D>("UpperMenu");
            spriteFont = Game.Content.Load<SpriteFont>("MyFont");
            GameTimer = new Timer(Game.Content.Load<SpriteFont>("MyFont"), new Vector2(250, 2), "00:00");
            TechTreeRect = new Rectangle(10, 1, ScienseTexture.Width, ScienseTexture.Height);
        }

        public override void Update(GameTime gameTime)
        {
            // Постоянно собираем информацию со всех городов в игре
            Map GameMap = ThisGame.GetMap();
            
            // (Временно) только 1 город
            City CityOnMap = GameMap.FindCity();
            if(CityOnMap != null)
            {
                // Наука, золото
                int[] CityInfo = CityOnMap.GetCityStatistic();
                ScienseScore = CityInfo[0];
                GoldScore = CityInfo[1];
            }

            // Работаем со зданиями
            Buildings[] Buildings = GameMap.FindAllBuildings();
            if (Buildings != null)
            {
                for(int i = 0; i < Buildings.Length; i++)
                {
                    ScienseScore += Buildings[i].SciencePoints;
                    GoldScore += Buildings[i].GoldPoints;
                }
            }

            GameTimer.Update(gameTime);
            
            LastState = CurrentState;
            CurrentState = Mouse.GetState();
            Rectangle MouseRectangle = new Rectangle(CurrentState.X, CurrentState.Y, 1, 1);
            if (MouseRectangle.Intersects(TechTreeRect) && CurrentState.LeftButton == ButtonState.Pressed && LastState.LeftButton == ButtonState.Released)
            {
                Game1.ThisGame.GetTechTree().OpenTechTree();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            String[] masScore = {Score, PlayerScore.ToString()};
            String FinalScore = String.Join(null, masScore);

            Int32 ThisTurn = Game1.Turn + 1;
            String[] masTurn = {Turn, ThisTurn.ToString()};
            String FinalTurn = String.Join(null, masTurn);

            spriteBatch.Begin();
            // Рисуем вехнее статистическое меню
            spriteBatch.Draw(UpperMenu, new Rectangle(0, 0, Game1.graphics.PreferredBackBufferWidth, 20), Color.White);
            // Наука
            spriteBatch.Draw(ScienseTexture, new Rectangle(10, 1, ScienseTexture.Width, ScienseTexture.Height), Color.White);
            spriteBatch.DrawString(spriteFont, ScienseScore.ToString(), new Vector2(35, 2), Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0f);
            // Золото
            spriteBatch.Draw(GoldTexture, new Rectangle(60 + ScienseTexture.Width, 1, ScienseTexture.Width, ScienseTexture.Height), Color.White);
            spriteBatch.DrawString(spriteFont, GoldScore.ToString(), new Vector2(85 + ScienseTexture.Width, 2), Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0f);
            // Счет
            spriteBatch.DrawString(spriteFont, FinalScore, new Vector2(Game1.graphics.PreferredBackBufferWidth - Score.Length - 100, 10), Color.White, 0, new Vector2(48, 9), 1.0f, SpriteEffects.None, 0f);
            // Ход
            spriteBatch.DrawString(spriteFont, FinalTurn, new Vector2(Game1.graphics.PreferredBackBufferWidth - Turn.Length - 250, 10), Color.White, 0, new Vector2(48, 9), 1.0f, SpriteEffects.None, 0f);
            // Условия победы
            spriteBatch.DrawString(spriteFont, LevelConditions.Target, new Vector2(300, 2), Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0f);
            // Казна (Накопленное золото)
            spriteBatch.Draw(Treasury, new Rectangle(150, 0, 20, 20), Color.White);
            spriteBatch.DrawString(spriteFont, Game1.Gold.ToString(), new Vector2(175, 2), Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0f);

            GameTimer.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
