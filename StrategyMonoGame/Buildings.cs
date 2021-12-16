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
    public enum BuildingTypes
    {   // Тип здания
        BUILDING_LIBRIARY = 0,
        BUILDING_PASTURE,
        BUILDING_FACTORY,
        BUILDING_MARKET
    }
    
    public class Buildings : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public Int32 SciencePoints; // Кол-во производимых зданием очков научки
        public Int32 GoldPoints; // Кол-во производимых зданием золота
        Cells BuildingOnThisCell;

        // Графика
        private SpriteBatch sbSpriteBatch;
        private Texture2D BuildingTexture;
        private Rectangle Position;
        private String TexturePath; // Путь к тексутре ресурса

        bool Destroy;

        BuildingTypes BuildingType;

        public Buildings(Game game, BuildingTypes nBuildingType, Rectangle nPosition, Cells nBuildingOnThisCell)
            : base(game)
        {
            // В первую очередь проверяется кол-во ресурсов, хватает ли его на постройку
            
            // При постройке здания открывается определенная ветка технологий
            // Также улучшаются показатели золота и науки
            BuildingType = nBuildingType;
            Byte[] Resourses = Game1.ThisGame.GetMap().FindCity().CityStorage.GetAllResoursesInfo();
            Destroy = false;
            BuildingOnThisCell = nBuildingOnThisCell;

            if(BuildingType == BuildingTypes.BUILDING_LIBRIARY)
            {
                Game1.ThisGame.GetMap().FindCity().CityStorage.SubResourse(10, 0);
                Game1.ThisGame.GetMap().FindCity().CityStorage.SubResourse(5, 1);
                
                SciencePoints = 5;
                GoldPoints = 2;
                Game1.ThisGame.GetTechTree().OpenBranch(1);
                TexturePath = "Библиотека";
            }
            else if (BuildingType == BuildingTypes.BUILDING_PASTURE)
            {
                Game1.ThisGame.GetMap().FindCity().CityStorage.SubResourse(20, 0);
                Game1.ThisGame.GetMap().FindCity().CityStorage.SubResourse(10, 1);
                
                SciencePoints = 1;
                GoldPoints = 5;
                Game1.ThisGame.GetTechTree().OpenBranch(2);
                TexturePath = "Пастбище";
            }
            else if (BuildingType == BuildingTypes.BUILDING_FACTORY)
            {
                Game1.ThisGame.GetMap().FindCity().CityStorage.SubResourse(10, 2);
                Game1.ThisGame.GetMap().FindCity().CityStorage.SubResourse(30, 1);
                
                SciencePoints = 3;
                GoldPoints = 3;
                Game1.ThisGame.GetTechTree().OpenBranch(3);
                TexturePath = "Завод";
            }
            else if (BuildingType == BuildingTypes.BUILDING_MARKET)
            {
                Game1.ThisGame.GetMap().FindCity().CityStorage.SubResourse(20, 0);
                Game1.ThisGame.GetMap().FindCity().CityStorage.SubResourse(10, 2);

                SciencePoints = 0;
                GoldPoints = 10;
                Game1.ThisGame.GetTechTree().OpenBranch(4);
                TexturePath = "Рынок";
            }
            Position = nPosition;
            LoadContent();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            sbSpriteBatch = new SpriteBatch(GraphicsDevice);
            BuildingTexture = Game.Content.Load<Texture2D>(TexturePath);
            // TODO: use this.Content to load your game content here
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            sbSpriteBatch.Begin();
            sbSpriteBatch.Draw(BuildingTexture, Position, Color.White);
            sbSpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
