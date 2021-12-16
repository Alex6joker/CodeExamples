using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading.Tasks;

namespace Strategy
{
    public class City : Microsoft.Xna.Framework.DrawableGameComponent
    {
        /*
         * Города являются главными производственными единицами
         * их структура единообразна
        */
        static Int32 ID = 0; // Для нумерции городов вводим статическую переменную
        private Int32 ThisCityID;
        private Byte Population; // Население города (Количество обрабатываемых клеток)
        private String Name; // Название города
        // Свойство, обоначающее строящееся в данный момент здание
        // private ????? ProducedNow;
        private Int32 SciencePoints; // Кол-во производимых городом очков научки
        private Int32 GoldPoints; // Кол-во производимых городом золота
        private Int32 ProductionPoints; // Очки производства города
        // Список зданий в городе
        // private ListOfBuildings
        // Список возможных к производству юнитов
        // private CanProduceUnitsList
        public Storage CityStorage; // Хранилище ресурсов города

        // Графика
        private SpriteBatch sbSpriteBatch;
        private Texture2D CityTexture;
        private Vector2 CityPosition;
        private String TexturePath; // Путь к тексутре ресурса
        public CityStorageMenu StorageMenu;

        public City(Game game, Vector2 Position) : base(game)
        {
            // Так как города имеют сложную структуру, то создаем облегченный вариант (временно)
            ID++;
            ThisCityID = ID; // Нумеруем город
            Population = 1; // Изначально население города - 1
            // Формируем название города
            String[] CityNameArray = { "City ", ID.ToString() };
            Name = String.Join(null, CityNameArray);
            // На начальном этапе не предусматривается никаких зданий
            // поэтому города будут производить очков науки, золота и производства
            // 3,2,1 соответственно
            SciencePoints = 3;
            GoldPoints = 2;
            ProductionPoints = 1;
            // Создаем пустое хранилище города
            CityStorage = new Storage();
            TexturePath = "Город";
            CityPosition = Position;
            StorageMenu = null;
            LoadContent();
        }

        public int[] GetCityStatistic()
        {
            int[] CityInfo = {SciencePoints, GoldPoints};
            return CityInfo;
        }
        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            sbSpriteBatch = new SpriteBatch(GraphicsDevice);
            CityTexture = Game.Content.Load<Texture2D>(TexturePath);
            // TODO: use this.Content to load your game content here
        }

        public override void Update(GameTime gameTime)
        {
            if (StorageMenu != null)
                StorageMenu.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            sbSpriteBatch.Begin();
            sbSpriteBatch.Draw(CityTexture, CityPosition, Color.White);
            if (StorageMenu != null)
                StorageMenu.Draw(gameTime);
            sbSpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
