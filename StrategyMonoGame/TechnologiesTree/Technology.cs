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
    public class Technology : Microsoft.Xna.Framework.DrawableGameComponent
    {
        /*
         * Содержит в себе общее описание тенологий,
         * представленных в игре
        */

        // Разблокирующийся юнит
        // private UnblockingUnit
        // Здание
        // private UnblockingBuilding;
        protected Int32 Cost; // Стоймость технологии в очках науки
        protected bool NewEra; // Технология переводит в новую эпоху (аналог конца уровня)
        protected TechnologyTree TechTree;

        protected SpriteBatch sbSpriteBatch;
        public Texture2D TechnologyTexture;
        protected Vector2 Position;// = new Vector2(150, 30);
        protected String TexturePath; // Путь к тексутре ресурса
        protected Texture2D TechonolgyBG;
        protected SpriteFont ThisTechnology;
        protected String NowResearch;
        // protected Int32 Turnsleft; // Остатоль ходов до изучения
        public bool Investigated;
        public Vector2 PositionOnTree;
        public Rectangle PositionOnTreeRectangle;
        public bool ResearchThisTechnology;

        public Technology(Game game, TechnologyTree TTr, Int32 nCost, bool nNewEra, String nTexturePath, Vector2 nPositionOnTree)
            : base(game)
        {
            Position.X = 0;
            Position.Y = Game1.graphics.PreferredBackBufferHeight / 8;
            //NowResearch = "Сейчас исследуется";
            NowResearch = "Сейчас изучается";
            TechTree = TTr;
            Cost = nCost;
            NewEra = nNewEra;
            TexturePath = nTexturePath;
            Investigated = false;
            PositionOnTree = nPositionOnTree;
            PositionOnTreeRectangle = new Rectangle((int)PositionOnTree.X, (int)PositionOnTree.Y, 40, 40);
            ResearchThisTechnology = false;
            LoadContent();
        }

        public void ChangeThisTechStatus()
        {
            if (ResearchThisTechnology)
                ResearchThisTechnology = false;
            else
                ResearchThisTechnology = true;
        }

        public override void Update(GameTime gameTime)
        {
            if (Cost - Game1.Sciense <= 0 && !Investigated)
            {
                if (NewEra)
                {
                    TechTree.NextEra(Cost);
                    TechTree.CurrentAge++;
                }                    
                Investigated = true;
                Game1.Sciense = 0;
            }
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            sbSpriteBatch = new SpriteBatch(GraphicsDevice);
            TechnologyTexture = Game.Content.Load<Texture2D>(TexturePath);
            TechonolgyBG = Game.Content.Load<Texture2D>("TechBG");
            ThisTechnology = Game.Content.Load<SpriteFont>("MyFont");
            // TODO: use this.Content to load your game content here
        }

        public override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            // Find the center of the string
            String Tech = TexturePath;
            String Turns = "Осталось ходов: ";
            if (UpStatisticsMenu.ScienseScore != 0)
            {
                double temp = (double)(Cost - Game1.Sciense) / (double)UpStatisticsMenu.ScienseScore;
                int TurnsLeft = (int)Math.Ceiling(temp);
                String[] Arr = { Turns, TurnsLeft.ToString() };
                Turns = String.Join(null, Arr);
            }
            sbSpriteBatch.Begin();
            sbSpriteBatch.Draw(TechonolgyBG, new Rectangle((int)0, (int)Game1.graphics.PreferredBackBufferHeight / 9, 220, 100), Color.White);
            if (ResearchThisTechnology)
            {
                sbSpriteBatch.DrawString(ThisTechnology, NowResearch, new Vector2(60, (int)Game1.graphics.PreferredBackBufferHeight / 7 + 5), Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0f);
                sbSpriteBatch.Draw(TechnologyTexture, new Rectangle((int)Position.X, (int)Position.Y + 10, 40, 40), Color.White);
                sbSpriteBatch.DrawString(ThisTechnology, Tech, new Vector2(60, (int)Game1.graphics.PreferredBackBufferHeight / 6 + 5), Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0f);
                if (Investigated)
                    sbSpriteBatch.DrawString(ThisTechnology, "Изучено", new Vector2(60, (int)Game1.graphics.PreferredBackBufferHeight / 5 + 5), Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0f);
                else
                    sbSpriteBatch.DrawString(ThisTechnology, Turns, new Vector2(60, (int)Game1.graphics.PreferredBackBufferHeight / 5 + 5), Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0f);
            }
            else
                sbSpriteBatch.DrawString(ThisTechnology, "Выберите технологию", new Vector2(20, (int)Game1.graphics.PreferredBackBufferHeight / 6), Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0f);
            sbSpriteBatch.End();
            base.Draw(gameTime);
        }

        public bool GetTechEraState()
        {
            return NewEra;
        }
    }
}
