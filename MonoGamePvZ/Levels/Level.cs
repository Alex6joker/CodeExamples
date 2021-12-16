using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PvZ
{
    public class Level : Microsoft.Xna.Framework.DrawableGameComponent
    {
        // Читает уровни из файла
        // Загружает уровень, список зомби, время
        // ответственнен за выпуск зомби в положенное время
        public int LevelsCount;        
        public int CurrentLevel;
        public Timers.Timer LevelTimer;
        public int CurrentChapter;
        public int ChaptersCount;
        public SomeLevel[] Levels;
        GameMap ThisMap;
        bool ResultWritten;
        SpriteBatch SpriteBatch;
        SpriteFont spriteFont;
        Texture2D Bg;
        bool DrawEnd;
        Timers.Timer DrawEndTimer;

        public struct SomeLevel
        {
            public int[] ZombiesArray;
            public int[] SS;
            public int[] MM;
            public bool[] Go;
            public int ZombiesOnLevel;
        }

        public Level(Game game, GameMap nThisMap) : base(game)
        {
            ThisMap = nThisMap;
            
            CurrentLevel = 1;
            LevelsCount = 1;
            CurrentChapter = 1;

            Levels = new SomeLevel[1];
            Levels[0] = new SomeLevel();

            SpriteBatch = new SpriteBatch(GraphicsDevice);

            Bg = Game.Content.Load<Texture2D>("NoTexture");
            spriteFont = Game.Content.Load<SpriteFont>("MyFont");
            LevelTimer = new Timers.Timer(Game1.ThisGame, Game1.ThisGame.Content.Load<SpriteFont>("MyFont"), new Vector2(600, 2), "00:00");
            DrawEndTimer = new Timers.Timer(Game1.ThisGame, Game1.ThisGame.Content.Load<SpriteFont>("MyFont"), new Vector2(250, 2), "00:00");

            bool Exit = false;

            while(!Exit)
            {

                String pathSource = @"Level1_";
                String[] s = { pathSource, LevelsCount.ToString(), ".txt" };
                pathSource = string.Join(null, s);
                try
                {
                    using (StreamReader File = new StreamReader(pathSource, Encoding.Default))
                    {   // Read the source file into a byte array.
                        string Line;
                        for (int i = 0; (Line = File.ReadLine()) != null; i++)
                        {
                            string[] Data = Line.Split(new char[] { ' ' });

                            if (i == 0)
                            {
                                CurrentChapter = Convert.ToInt32(Data[0]);
                                ChaptersCount = Convert.ToInt32(Data[0]);
                            }
                            else if (i == 1)
                            {
                                CurrentLevel = Convert.ToInt32(Data[0]);
                                LevelsCount = Convert.ToInt32(Data[0]);
                                if (CurrentLevel != 1)
                                    Array.Resize<SomeLevel>(ref Levels, Levels.Length + 1);
                            }
                            else
                            {
                                int ZombieType = Convert.ToInt32(Data[0]);
                                string[] TimeData = Data[1].Split(new char[] { ':' });
                                if (Levels[CurrentLevel - 1].ZombiesArray != null)
                                {
                                    Array.Resize<int>(ref Levels[CurrentLevel - 1].ZombiesArray, Levels[CurrentLevel - 1].ZombiesArray.Length + 1);
                                    Array.Resize<int>(ref Levels[CurrentLevel - 1].SS, Levels[CurrentLevel - 1].SS.Length + 1);
                                    Array.Resize<int>(ref Levels[CurrentLevel - 1].MM, Levels[CurrentLevel - 1].MM.Length + 1);
                                    Array.Resize<bool>(ref Levels[CurrentLevel - 1].Go, Levels[CurrentLevel - 1].Go.Length + 1);
                                }
                                else
                                {
                                    Levels[CurrentLevel - 1].ZombiesArray = new int[1];
                                    Levels[CurrentLevel - 1].SS = new int[1];
                                    Levels[CurrentLevel - 1].MM = new int[1];
                                    Levels[CurrentLevel - 1].Go = new bool[1];
                                }
                                Levels[CurrentLevel - 1].ZombiesArray[i - 2] = ZombieType;
                                Levels[CurrentLevel - 1].SS[i - 2] = Convert.ToInt32(TimeData[1]);
                                Levels[CurrentLevel - 1].MM[i - 2] = Convert.ToInt32(TimeData[0]);
                                Levels[CurrentLevel - 1].Go[i - 2] = false;
                                Levels[CurrentLevel - 1].ZombiesOnLevel++;
                            }
                        }
                    }
                    LevelsCount++;
                }
                catch (FileNotFoundException ioEx)
                {
                    Exit = true;
                    LevelsCount--;
                }
            }
            CurrentLevel = 1;
            CurrentChapter = 1;
            ResultWritten = false;
            DrawEnd = true;
        }

        public override void Update(GameTime gameTime)
        {
            LevelTimer.Update(gameTime);
            if (DrawEnd)
                DrawEndTimer.Update(gameTime);
            for(int i = 0; i < Levels[CurrentLevel - 1].ZombiesArray.Length; i++)
            {
                if (!Levels[CurrentLevel - 1].Go[i] && LevelTimer.SS == Levels[CurrentLevel - 1].SS[i]
                    && LevelTimer.MM == Levels[CurrentLevel - 1].MM[i])
                {   // Создаем противников по таймеру
                    ThisMap.GenerateEneny(Levels[CurrentLevel - 1].ZombiesArray[i]);
                    Levels[CurrentLevel - 1].Go[i] = true;
                }
            }

            if (CurrentLevel == 2 && LevelTimer.SS == 25)
            { // Поражение
                Game1.WinOrDefeatGameStateVar = Game1.WinOrDefeatGameState.Defeat;
                if (!ResultWritten)
                {
                    String Time = ThisMap.GameTimer.Text;
                    String[] Win = { Game1.PlayerName, "Поражение", Time };
                    Game1.ThisGame.ResultsTableVar.AddNewRecord(Win);
                }

                ResultWritten = true;
            }
            if(EndOfLevel())
            {
                CurrentLevel++;
                GameMap.Zombies = new Zombies[0];
                LevelTimer.MS = LevelTimer.MM = LevelTimer.SS = 0;
                if(CurrentLevel > Levels.Length)
                {   // Победа
                    Game1.WinOrDefeatGameStateVar = Game1.WinOrDefeatGameState.Win;

                    // Записываем результат игры
                    if(!ResultWritten)
                    {
                        String Time = ThisMap.GameTimer.Text;
                        String[] Win = { Game1.PlayerName, "Победа", Time };
                        Game1.ThisGame.ResultsTableVar.AddNewRecord(Win);
                    }

                    ResultWritten = true;
                }
                if(CurrentLevel == 2)
                {   // Открываем подсолнух
                    Game1.ThisGame.GameMap.BuyPlantsPanel.BuyingPlantsVar[1].Enabled = true;
                }
                DrawEnd = true;
            }

            if(SomeZombieWin())
            {   // Поражение
                Game1.WinOrDefeatGameStateVar = Game1.WinOrDefeatGameState.Defeat;

                // Записываем результат игры
                // Записываем результат игры
                if (!ResultWritten)
                {
                    // Выигрыш
                    //MediaPlayer.Stop();
                    //WinEffect.Play();
                    String Time = ThisMap.GameTimer.Text;
                    String[] Win = { Game1.PlayerName, "Поражение", Time };
                    Game1.ThisGame.ResultsTableVar.AddNewRecord(Win);
                }

                ResultWritten = true;
            }

            if(DrawEndTimer.SS == 2)
            {
                DrawEndTimer.SS = DrawEndTimer.MS = DrawEndTimer.MM = 0;
                DrawEnd = false;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (DrawEnd)
            {
                SpriteBatch.Begin();
                SpriteBatch.Draw(Bg, new Rectangle(400, 400, 100, 60), Color.White);
                String[] Str = {"Уровень ", CurrentLevel.ToString()};
                String s = String.Join(null, Str);
                SpriteBatch.DrawString(spriteFont, s, new Vector2(410, 420), Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0f);
                SpriteBatch.End();
            }
            if(CurrentLevel == 2)
            {
                SpriteBatch.Begin();
                LevelTimer.Draw(gameTime);
                SpriteBatch.End();
            }
        }

        public bool EndOfLevel()
        {
            if (GameMap.Zombies.Length != Levels[CurrentLevel - 1].ZombiesOnLevel)
                return false;
            for(int i = 0; i < GameMap.Zombies.Length; i++)
            {
                if (GameMap.Zombies[i] != null)
                    return false;
            }
            return true;
        }

        public bool SomeZombieWin()
        {
            for (int i = 0; i < GameMap.Zombies.Length; i++)
            {
                if (GameMap.Zombies[i] != null && GameMap.Zombies[i].ZombieRectangle.X <= 10)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
