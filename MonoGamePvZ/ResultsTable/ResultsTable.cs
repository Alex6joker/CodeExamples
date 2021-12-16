using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PvZ.ResultsTable
{
    public class ResultsTable : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Texture2D TableImage; // Текстура таблицы
        Int32 MaxSizeX, MaxSizeY; // Максимальные размеры объектов относительно таблицы
        String FileСontent; // Содержит в себе записи таблицы
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        Vector2 TablePosition;
        String[,] TableContent;
        int Lenght;
        int WatchingLine;
        KeyboardState LastKbState;
        KeyboardState CurrentKbState;

        public ResultsTable(Game1 game) : base(game)
        {
            TableImage = game.Content.Load<Texture2D>("Таблица"); // Загружаем текстуру
            spriteFont = Game.Content.Load<SpriteFont>("MyFont");
            TableContent = new String[100,10];
            TableContent[0,0] = "";
            Lenght = 0;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // Помещаем ограничения
            MaxSizeX = TableImage.Width;
            MaxSizeY = TableImage.Height;
            TablePosition.X = 200;
            TablePosition.Y = 50;
            // Пытаемся открыть файл с рекордами
            string pathSource = @"TheBestOf.txt";
            try
            {
                using (StreamReader File = new StreamReader(pathSource, Encoding.Default))
                {   // Read the source file into a byte array.
                    string Line;
                    for (int i = 0; (Line = File.ReadLine()) != null; i++)
                    {
                        string[] Data = Line.Split(new char[] { ' ' });

                        if (Data.Length == 3)
                            for (int j = 0; j < 3; j++)
                            {
                                TableContent[i, j] = Data[j];
                            }
                        Lenght++;
                    }
                }
                Sort();
            }
            catch (FileNotFoundException ioEx)
            {   // Создаем файл и работаем с пустой таблицей
                FileStream fsSource = new FileStream(pathSource, FileMode.CreateNew, FileAccess.Write);
            }
            WatchingLine = 0;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            ChangeWatchingLine();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(TableImage, new Rectangle((int)TablePosition.X, (int)TablePosition.Y, MaxSizeX, MaxSizeY),
                new Rectangle(0, 0, MaxSizeX, MaxSizeY), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0f);
            for (Int32 i = WatchingLine; i < Lenght; i++)
            {
                if (i - WatchingLine >= 15)
                    break;
                for (Int32 j = 0; j < 3; j++)
                {
                    if (j == 0)
                        spriteBatch.DrawString(spriteFont, TableContent[i, j], new Vector2(TablePosition.X + 30, TablePosition.Y + 40 + ((i - WatchingLine) * 30)), Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0f);
                    if (j == 1)
                        spriteBatch.DrawString(spriteFont, TableContent[i, j], new Vector2(TablePosition.X + 200, TablePosition.Y + 40 + ((i - WatchingLine) * 30)), Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0f);
                    if (j == 2)
                        spriteBatch.DrawString(spriteFont, TableContent[i, j], new Vector2(TablePosition.X + 500, TablePosition.Y + 40 + ((i - WatchingLine) * 30)), Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0f);
                }    
            }                                
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void AddNewRecord(String[] NewRecord)
        {
            if (NewRecord[0] == "")
                TableContent[Lenght, 0] = "Unknown";
            else
                TableContent[Lenght, 0] = NewRecord[0];
            TableContent[Lenght, 1] = NewRecord[1];
            TableContent[Lenght, 2] = NewRecord[2];
            TableContent[Lenght, 3] = "\n";
            SaveResult();
            Lenght++;
            Sort();
        }

        public void Sort()
        {
            String[] TableTemp = new String[3];
            for (Int32 i = 0; i < Lenght; i++)
                for (Int32 j = 0; j < Lenght; j++)
                {
                    if (TableContent[i, 1] == "Победа" && TableContent[j, 1] == "Поражение")
                    {
                        TableTemp[0] = TableContent[i, 0];
                        TableTemp[1] = TableContent[i, 1];
                        TableTemp[2] = TableContent[i, 2];
                        TableContent[i, 0] = TableContent[j, 0];
                        TableContent[i, 1] = TableContent[j, 1];
                        TableContent[i, 2] = TableContent[j, 2];
                        TableContent[j, 0] = TableTemp[0];
                        TableContent[j, 1] = TableTemp[1];
                        TableContent[j, 2] = TableTemp[2];
                    }
                    if (StrToTime(TableContent[i, 2]) < StrToTime(TableContent[j, 2]) && TableContent[i, 1] == TableContent[j, 1])
                    {
                        TableTemp[0] = TableContent[i, 0];
                        TableTemp[1] = TableContent[i, 1];
                        TableTemp[2] = TableContent[i, 2];
                        TableContent[i, 0] = TableContent[j, 0];
                        TableContent[i, 1] = TableContent[j, 1];
                        TableContent[i, 2] = TableContent[j, 2];
                        TableContent[j, 0] = TableTemp[0];
                        TableContent[j, 1] = TableTemp[1];
                        TableContent[j, 2] = TableTemp[2];
                    }
                }
        }

        public Int32 StrToTime(String Time)
        {
            Int32 ResTime;

            String[] Times = Time.Split(new char[] { ':' });
            Int32[] IntTimes = new Int32[2];
            IntTimes[0] = Convert.ToInt32(Times[0]);
            IntTimes[1] = Convert.ToInt32(Times[1]);

            IntTimes[0] *= 60;
            ResTime = IntTimes[0] + IntTimes[1];

            return ResTime;
        }

        public void SaveResult()
        {
            using (FileStream fstream = new FileStream(@"TheBestOf.txt", FileMode.Append))
            {
                // преобразуем строку в байты
                byte[] array = Encoding.Default.GetBytes(TableContent[Lenght, 0] + " " + TableContent[Lenght, 1] + " " + TableContent[Lenght, 2] + Environment.NewLine);
                // запись массива байтов в файл
                fstream.Write(array, 0, array.Length);
            }
        }

        public void ChangeWatchingLine()
        {
            CurrentKbState = Keyboard.GetState();
            if (CurrentKbState.IsKeyDown(Keys.Up) && LastKbState.IsKeyUp(Keys.Up) && WatchingLine > 0)
            {
                WatchingLine--;
            }
            CurrentKbState = Keyboard.GetState();
            if (CurrentKbState.IsKeyDown(Keys.Down) && LastKbState.IsKeyUp(Keys.Down) && WatchingLine < Lenght - 1 && Lenght >= 15)
            {
                WatchingLine++;
            }
            LastKbState = CurrentKbState;
        }
    }
}
