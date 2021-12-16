using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Strategy
{
    public class TechnologyTree : Microsoft.Xna.Framework.DrawableGameComponent
    {
        /*
         * Класс представляет из себя дерево технологий
        */

        public Int32 CurrentAge;
        // (Временно) технология будет выбираться из списка по номеру
        private Int32 CurrentTechID;
        bool NewEra;
        SpriteBatch spriteBatch;
        Texture2D UpperMenu;
        SpriteFont spriteFont;
        Timer NewEraMsgTimer;
        SoundEffect SoundNewEra;
        bool SoundIsPlaying;
        Int32 CurrentBranch;
        bool TreeIsOpened;
        ExitTechTreeButton ExitButton;
        Texture2D TechTreePanel;
        Vector2 TechPanelPosition;
        Texture2D LineTexture; 
        MouseState LastState;
        MouseState CurrentState;

        public struct Branch // Ветки дерева технологий
        {
            public int number; // Номер ветки
            public Technology[] TechsList;
            public bool Opened;
            public int[] NextBranch;
            public int ParentBranch;
        };

        public Branch[] Branchs;

        public TechnologyTree(Game game) : base(game)
        {
            // При создании дерева технологий
            // заполняется список технологий и
            // (временно) текущий ID ставится 0
            TechPanelPosition = new Vector2(10, 50);

            Branchs = new Branch[6];
            Branchs[0].number = 0;
            Branchs[0].Opened = true;
            Branchs[0].TechsList = new Technology[2];
            Branchs[0].TechsList[0] = new Technology(game, this, 7, false, "Земледелие", new Vector2(TechPanelPosition.X + 70, TechPanelPosition.Y + 325));
            Branchs[0].TechsList[1] = new Technology(game, this, 12, true, "Обработка железа", new Vector2(TechPanelPosition.X + 140, TechPanelPosition.Y + 325));
            Branchs[0].NextBranch = new int[2];
            Branchs[0].NextBranch[0] = 1;
            Branchs[0].NextBranch[1] = 2;
            Branchs[0].ParentBranch = 0;

            Branchs[1].number = 1;
            Branchs[1].Opened = false;
            Branchs[1].TechsList = new Technology[3];
            Branchs[1].TechsList[0] = new Technology(game, this, 20, false, "Письменность", new Vector2(TechPanelPosition.X + 250, TechPanelPosition.Y + 150));
            Branchs[1].TechsList[1] = new Technology(game, this, 35, false, "Духовенство", new Vector2(TechPanelPosition.X + 380, TechPanelPosition.Y + 150));
            Branchs[1].TechsList[2] = new Technology(game, this, 40, false, "Эстетика", new Vector2(TechPanelPosition.X + 450, TechPanelPosition.Y + 150));
            Branchs[1].NextBranch = new int[1];
            Branchs[1].NextBranch[0] = 5;
            Branchs[1].ParentBranch = 0;

            Branchs[2].number = 2;
            Branchs[2].Opened = false;
            Branchs[2].TechsList = new Technology[2];
            Branchs[2].TechsList[0] = new Technology(game, this, 30, false, "Животноводство", new Vector2(TechPanelPosition.X + 250, TechPanelPosition.Y + 400));
            Branchs[2].TechsList[1] = new Technology(game, this, 12, false, "Компас", new Vector2(TechPanelPosition.X + 400, TechPanelPosition.Y + 400));
            Branchs[2].NextBranch = new int[2];
            Branchs[2].NextBranch[0] = 3;
            Branchs[2].NextBranch[1] = 4;
            Branchs[2].ParentBranch = 0;

            Branchs[3].number = 3;
            Branchs[3].Opened = false;
            Branchs[3].TechsList = new Technology[2];
            Branchs[3].TechsList[0] = new Technology(game, this, 40, false, "Алфавит", new Vector2(TechPanelPosition.X + 500, TechPanelPosition.Y + 325));
            Branchs[3].TechsList[1] = new Technology(game, this, 50, false, "Деньги", new Vector2(TechPanelPosition.X + 650, TechPanelPosition.Y + 325));
            Branchs[3].NextBranch = new int[1];
            Branchs[3].NextBranch[0] = 5;
            Branchs[3].ParentBranch = 2;

            Branchs[4].number = 4;
            Branchs[4].Opened = false;
            Branchs[4].TechsList = new Technology[1];
            Branchs[4].TechsList[0] = new Technology(game, this, 45, false, "Печатный пресс", new Vector2(TechPanelPosition.X + 525, TechPanelPosition.Y + 500));
            Branchs[4].NextBranch = new int[1];
            Branchs[4].NextBranch[0] = 5;
            Branchs[4].ParentBranch = 2;

            Branchs[5].number = 5;
            Branchs[5].Opened = false;
            Branchs[5].TechsList = new Technology[1];
            Branchs[5].TechsList[0] = new Technology(game, this, 80, true, "Технология будущего", new Vector2(TechPanelPosition.X + 750, TechPanelPosition.Y + 350));
            Branchs[5].NextBranch = new int[1];
            Branchs[5].NextBranch[0] = Branchs[5].number;
            Branchs[5].ParentBranch = 5;
            
            CurrentAge = 0;
            CurrentBranch = 0;
            CurrentTechID = 0;
            Branchs[0].TechsList[0].ChangeThisTechStatus();
            
            NewEra = false;
            TreeIsOpened = false;
            LoadContent();
            NewEraMsgTimer = new Timer(Game.Content.Load<SpriteFont>("MyFont"), new Vector2(430, 2), "00:00");
            SoundIsPlaying = false;
            ExitButton = new ExitTechTreeButton(Game1.ThisGame);
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            UpperMenu = Game.Content.Load<Texture2D>("UpperMenu");
            spriteFont = Game.Content.Load<SpriteFont>("MyFont");
            SoundNewEra = Game.Content.Load<SoundEffect>("NewEra");
            TechTreePanel = Game.Content.Load<Texture2D>("Подложка");
            LineTexture = Game.Content.Load<Texture2D>("Линия");
        }

        public override void Update(GameTime gameTime)
        {
            Branchs[CurrentBranch].TechsList[CurrentTechID].Update(gameTime);

            if (NewEra)
            {
                if (!SoundIsPlaying)
                {
                    SoundNewEra.Play();
                    SoundIsPlaying = true;
                }
                NewEraMsgTimer.Update(gameTime);
            }

            LastState = CurrentState;
            CurrentState = Mouse.GetState();
            Rectangle MouseRectangle = new Rectangle(CurrentState.X, CurrentState.Y, 1, 1);
            bool Break = false;
            for (int i = 0; i < Branchs.Length; i++)
            {
                for (int j = 0; j < Branchs[i].TechsList.Length; j++)
                {
                    if (MouseRectangle.Intersects(Branchs[i].TechsList[j].PositionOnTreeRectangle) && CurrentState.LeftButton == ButtonState.Pressed && LastState.LeftButton == ButtonState.Released
                        && Branchs[i].Opened && !Branchs[i].TechsList[j].Investigated)
                    {   // Обработка нажатия на технологию
                        // Ищем технологию, которая уже изучается
                        
                        for (int ii = 0; ii < Branchs.Length; ii++)
                        {
                            if (Break)
                                break;
                            for (int jj = 0; jj < Branchs[ii].TechsList.Length; jj++)
                            {
                                if (Branchs[ii].TechsList[jj].ResearchThisTechnology)
                                {
                                    Branchs[ii].TechsList[jj].ChangeThisTechStatus();
                                    Break = true;
                                    break;
                                }                                    
                            }
                        }
                        // Ставим новую технологию на изучение
                        // Ставиться на изучение технология должна только если изучены все предыдущие
                        if(AllPrevTechsAreInvestigated(i, j))
                        {
                            Branchs[i].TechsList[j].ChangeThisTechStatus();
                            CurrentBranch = i;
                            CurrentTechID = j;
                        }                        
                    }
                }                
            }

            // Обрабатываем текущую ветку на полную изученность
            bool IsOver = true;
            for (int i = 0; i < Branchs[CurrentBranch].TechsList.Length; i++)
            {
                if (!Branchs[CurrentBranch].TechsList[i].Investigated)
                {
                    IsOver = false;
                    break;
                }                    
            }
            if (CurrentBranch != 0 && CurrentBranch != 2 && IsOver)
            {
                for (int i = 0; i < Branchs[CurrentBranch].NextBranch.Length; i++)
                {
                    Branchs[Branchs[CurrentBranch].NextBranch[i]].Opened = true;
                }
            }

            ExitButton.Update(gameTime);
            base.Update(gameTime);
        }

        public bool ResearchNewTech()
        {
            if (Branchs[CurrentBranch].TechsList[CurrentTechID].Investigated)
                return false;
            else
                return true;
        }

        public bool AllPrevTechsAreInvestigated(int ThisBranch, int PositionInTechList)
        {
            // Сначала проверяем текущую ветку
            // Затем переходим к родительской
            if (PositionInTechList > 0) // Проверку производим, если это не первая технология в списке
            {
                for (int j = PositionInTechList - 1; j >= 0; j--)
                {
                    if (!Branchs[ThisBranch].TechsList[j].Investigated)
                        return false;
                }
            }
            // Проверяем рекурсивно родительские ветки
            if (ThisBranch != Branchs[ThisBranch].ParentBranch && !(ThisBranch == 0 || ThisBranch == 5))
                if (!AllPrevTechsAreInvestigated(Branchs[ThisBranch].ParentBranch, Branchs[Branchs[ThisBranch].ParentBranch].TechsList.Length - 1))
                    return false;            

            return true;
        }

        public override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here

            // Рисуем стпрайт, на котором будет отображаться иконка и количество ходов
            spriteBatch.Begin();
            Branchs[CurrentBranch].TechsList[CurrentTechID].Draw(gameTime);
            if(NewEra)
            {
                spriteBatch.Draw(UpperMenu, new Rectangle(Game1.graphics.PreferredBackBufferWidth / 2,
                    (int)Game1.graphics.PreferredBackBufferHeight / 2, 280, 100), Color.White);
                if (CurrentAge == 1) spriteBatch.DrawString(spriteFont, "Вы вступили в эпоху античности", new Vector2(Game1.graphics.PreferredBackBufferWidth / 2 + 2, Game1.graphics.PreferredBackBufferHeight / 2 + 30), 
                    Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.5f);
                if (CurrentAge == 2) spriteBatch.DrawString(spriteFont, "Вы вступили в эпоху средневековья", new Vector2(Game1.graphics.PreferredBackBufferWidth / 2 + 2, Game1.graphics.PreferredBackBufferHeight / 2 + 30),
                    Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.5f);
            }
            if (NewEraMsgTimer.SS == 5)
            {
                NewEra = false;
                NewEraMsgTimer.MM = NewEraMsgTimer.MS = NewEraMsgTimer.SS = 0;
                SoundIsPlaying = false;
            }
            if(TreeIsOpened)
            {
                // Вывод панели
                spriteBatch.Draw(TechTreePanel, new Rectangle((int)TechPanelPosition.X, (int)TechPanelPosition.Y, 1150, 650), Color.White);
                //ExitButton.Draw(gameTime);
                spriteBatch.Draw(ExitButton.ButtonTexture, new Rectangle((int)ExitButton.Position.X, (int)ExitButton.Position.Y, 20, 20), Color.White);
                spriteBatch.DrawString(spriteFont, "Требуется: Библиотека", new Vector2(250, 180), Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(spriteFont, "Требуется: Пастбище", new Vector2(270, 420), Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(spriteFont, "Требуется: Завод", new Vector2(500, 350), Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(spriteFont, "Требуется: Рынок", new Vector2(500, 500), Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0f);
 
                // Вывод дерева
                for (int i = 0; i < Branchs.Length; i++)
                {
                    for (int j = 0; j < Branchs[i].TechsList.Length; j++)
                    {   // Если не последний элемент в списке технологий, соединяем их   
                        if (j < (Branchs[i].TechsList.Length - 1))
                            spriteBatch.Draw
                                (LineTexture,
                                new Rectangle((int)Branchs[i].TechsList[j].PositionOnTree.X + 40,
                                (int)Branchs[i].TechsList[j].PositionOnTree.Y + 15,
                                Math.Abs((int)Branchs[i].TechsList[j].PositionOnTree.X + 40 - (int)Branchs[i].TechsList[j + 1].PositionOnTree.X), 15), Color.White
                                );
                    }
                }
                for (int i = 0; i < Branchs.Length; i++)
                {
                    for (int j = 0; j < Branchs[i].NextBranch.Length; j++)
                    {
                        if (Branchs[i].NextBranch[j] != Branchs[i].number)
                        {
                            int Last = Branchs[i].TechsList.Length - 1;

                            float Rotation;
                            int XX = Math.Abs((int)Branchs[i].TechsList[Last].PositionOnTree.X + 40 - (int)Branchs[Branchs[i].NextBranch[j]].TechsList[0].PositionOnTree.X);
                            int YY;
                            double S;

                            if ((int)Branchs[i].TechsList[Last].PositionOnTree.Y > (int)Branchs[Branchs[i].NextBranch[j]].TechsList[0].PositionOnTree.Y)
                            {
                                YY = Math.Abs((int)Branchs[i].TechsList[Last].PositionOnTree.Y - (int)Branchs[Branchs[i].NextBranch[j]].TechsList[0].PositionOnTree.Y + 40);
                            }
                            else
                            {
                                YY = Math.Abs((int)Branchs[i].TechsList[Last].PositionOnTree.Y + 40 - (int)Branchs[Branchs[i].NextBranch[j]].TechsList[0].PositionOnTree.Y);
                            }
                            Rotation = (float)YY / (float)XX;
                            if ((int)Branchs[i].TechsList[Last].PositionOnTree.Y > (int)Branchs[Branchs[i].NextBranch[j]].TechsList[0].PositionOnTree.Y)
                            {
                                float old = Rotation;
                                Rotation *= -0.35f;
                                if(i == 4)
                                    Rotation = old * -0.6f;
                                S = Math.Sqrt(Math.Pow(YY, 2) + Math.Pow(XX, 2)) - 30;
                            }                                
                            else
                            {
                                float old = Rotation;
                                Rotation *= 1.1f;
                                if(i == 1)
                                    Rotation = old * 0.9f;
                                S = Math.Sqrt(Math.Pow(YY, 2) + Math.Pow(XX, 2)) + 50;
                            }                                
                            
                            Vector2 Origin = new Vector2((int)0, 0);

                            spriteBatch.Draw(LineTexture, new Rectangle((int)Branchs[i].TechsList[Last].PositionOnTree.X + 30,
                                    (int)Branchs[i].TechsList[Last].PositionOnTree.Y + 15,
                                    (int)S, 15),
                                    new Rectangle(0, 0, 30, 15), Color.White, Rotation,
                                    Origin
                                    , SpriteEffects.None, 0f);
                        }
                    }
                }
                
                for(int i = 0; i < Branchs.Length; i++)
                {
                    Color color = Color.White;
                    if (Branchs[i].Opened == false)
                        color = Color.Red;
                    for(int j = 0; j < Branchs[i].TechsList.Length; j++)
                    {
                        if (Branchs[i].TechsList[j].Investigated)
                            color = Color.Green;
                        else if (!Branchs[i].TechsList[j].Investigated && Branchs[i].Opened)
                            color = Color.White;
                        spriteBatch.Draw(Branchs[i].TechsList[j].TechnologyTexture, new Rectangle((int)Branchs[i].TechsList[j].PositionOnTree.X, (int)Branchs[i].TechsList[j].PositionOnTree.Y, 40, 40), color);                      
                        if(i == CurrentBranch && j == CurrentTechID)
                            spriteBatch.Draw(Branchs[i].TechsList[j].TechnologyTexture, new Rectangle((int)Branchs[i].TechsList[j].PositionOnTree.X, (int)Branchs[i].TechsList[j].PositionOnTree.Y, 40, 40), Color.BlueViolet); 
                    }
                }                
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void NextEra(int Cost)
        {
            NewEra = true;
        }

        public void OpenTechTree()
        {
            TreeIsOpened = true;
        }

        public void CloseTechTree()
        {
            TreeIsOpened = false;
        }

        public void OpenBranch(Int32 BranchNum)
        {
            Branchs[BranchNum].Opened = true;
        }
    }
}
