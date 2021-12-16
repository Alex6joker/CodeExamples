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
    public class Map : Microsoft.Xna.Framework.DrawableGameComponent
    {
        /*
         * Класс описывает общую структуру карты игры
        */

        // Клетки карты
        private Cells[,] CellsOnMap;
        // Размеры карты
        private Int32 MapSizeWidth;
        private Int32 MapSizeHeight;

        public Map(Game game, Int32 nMapSizeWidth, Int32 nMapSizeHeight)
            : base(game)
        {
            MapSizeWidth = nMapSizeWidth;
            MapSizeHeight = nMapSizeHeight;
            CellsOnMap = new Cells[MapSizeWidth, MapSizeHeight];
        }

        public override void Initialize()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            for (Int32 i = 0; i < MapSizeWidth; i++)
                for (Int32 j = 0; j < MapSizeHeight; j++)
                    GenerateCell(i, j);
            // После инициализации карты создаем 3 юнита: поселенца и 2 рабочих
            // в двух неповторяющихся клетках
            // Выбираем случайную клетку для поселенца (временно оставляем место для рабочего снизу справа от него)
            Random RandNum = new Random();
            int CellX = RandNum.Next(14, MapSizeWidth - 2);
            int CellY = RandNum.Next(4, MapSizeHeight - 5);
            // Создаем поселенца
            CellsOnMap[CellX, CellY].CreateStartUnitSettler();
            // Создаем (временно) рабочего снизу справа от него
            CellsOnMap[CellX + 1, CellY + 1].CreateStartUnitWorker();
            // Создаем (временно) рабочего сверху слева
            CellsOnMap[CellX - 1, CellY - 1].CreateStartUnitWorker();

            // На карте случайно генерируются ресурсы
            // (временно) от 10 до 25 ресурсов меди или железа
            Int32 ResoursesCount = RandNum.Next(11, 26);
            for (Int32 i = 0; i <= ResoursesCount; i++)
            {
                // Берем случайные клетки
                int X = RandNum.Next(0, MapSizeWidth - 1);
                int Y = RandNum.Next(0, MapSizeHeight - 1);
                // Ресурс - железо (0), либо медь (1)
                int Resourse = RandNum.Next(0, 2);
                CellsOnMap[X, Y].CreateResourseOnMap(Resourse);
            }                
            // TODO: use this.Content to load your game content here
        }

        public void GenerateOil()
        {
            Random RandNum = new Random();
            Int32 ResoursesCount = RandNum.Next(11, 26);
            for (Int32 i = 0; i <= ResoursesCount; i++)
            {
                // Берем случайные клетки
                int X = RandNum.Next(0, MapSizeWidth - 1);
                int Y = RandNum.Next(0, MapSizeHeight - 1);
                int Resourse = 2;
                if (CellsOnMap[X, Y].ResourceOnCell == null)
                    CellsOnMap[X, Y].CreateResourseOnMap(Resourse);
            }  
        }

        private void GenerateCell(Int32 CellXPozition, Int32 CellYPozition)
        {
            Random RandNum = new Random();
            int CellType = RandNum.Next(0, 5);
            // По записанным размерам генерируем карту
            switch(CellType)
            {
                case 0:
                    CellsOnMap[CellXPozition, CellYPozition] = new Cells(Game1.ThisGame, CellXPozition, CellYPozition, this, "Луга", true, CellXPozition, CellYPozition);
                    break;
                case 1:
                    CellsOnMap[CellXPozition, CellYPozition] = new Cells(Game1.ThisGame, CellXPozition, CellYPozition, this, "Равнины", true, CellXPozition, CellYPozition);
                    break;
                case 2:
                    CellsOnMap[CellXPozition, CellYPozition] = new Cells(Game1.ThisGame, CellXPozition, CellYPozition, this, "Пустыня", true, CellXPozition, CellYPozition);
                    break;
                case 3:
                    CellsOnMap[CellXPozition, CellYPozition] = new Cells(Game1.ThisGame, CellXPozition, CellYPozition, this, "Снег", true, CellXPozition, CellYPozition);
                    break;
                case 4:
                    CellsOnMap[CellXPozition, CellYPozition] = new Cells(Game1.ThisGame, CellXPozition, CellYPozition, this, "Тундра", true, CellXPozition, CellYPozition);
                    break;
                case 5:
                    CellsOnMap[CellXPozition, CellYPozition] = new Cells(Game1.ThisGame, CellXPozition, CellYPozition, this, "Гора", false, CellXPozition, CellYPozition);
                    break;
            }
        }

        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            for (Int32 i = 0; i < MapSizeWidth; i++)
                for (Int32 j = 0; j < MapSizeHeight; j++)
                    CellsOnMap[i, j].Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            // Отрисовываем все клетки на карте
            for (Int32 i = 0; i < MapSizeWidth; i++)
                for (Int32 j = 0; j < MapSizeHeight; j++)
                    CellsOnMap[i, j].Draw(gameTime);
            base.Draw(gameTime);
        }

        public Cells GetActiveCell()
        {   // Получить клетку, на которой выбран юнит
            for (Int32 i = 0; i < MapSizeWidth; i++)
                for (Int32 j = 0; j < MapSizeHeight; j++)
                    if (CellsOnMap[i, j].CellIsActive == true)
                        return CellsOnMap[i, j];
            return null;
        }

        public int[] GetActiveCellIndex()
        {   // Получить информацию о клетке, на которой выбран юнит
            int[] Info = {0, 0};
            for (Info[0] = 0; Info[0] < MapSizeWidth; Info[0]++)
                for (Info[1] = 0; Info[1] < MapSizeHeight; Info[1]++)
                    if (CellsOnMap[Info[0], Info[1]].CellIsActive == true)
                        return Info;
            return null;
        }

        public int[] GetCellInfo(Vector2 TargetCellPos)
        {   // Получить информацию об интересующей клетке, на которой выбран юнит
            int[] Info = {0, 0};
            for (Info[0] = 0; Info[0] < MapSizeWidth; Info[0]++)
                for (Info[1] = 0; Info[1] < MapSizeHeight; Info[1]++)
                    if (CellsOnMap[Info[0], Info[1]].CellPosition == TargetCellPos)
                        return Info;
            return null;
        }

        public Cells GetCellByCoordinates(int[] Position)
        {
            return CellsOnMap[Position[0], Position[1]];
        }

        public void StartMoveUnits(int[] OldPos, int[] NewPos)
        {   // Вызываем функцию MoveUnits для отрисовки перемещения юнита
            CellsOnMap[NewPos[0], NewPos[1]].MoveUnit(CellsOnMap[OldPos[0], OldPos[1]], CellsOnMap[NewPos[0], NewPos[1]]);            
        }

        public void NullActiveCells()
        {
            for (int i = 0; i < MapSizeWidth; i++)
                for (int j = 0; j < MapSizeHeight; j++)
                    if (CellsOnMap[i, j].CellIsActive == true)
                        CellsOnMap[i, j].CellIsActive = false;
        }

        public City FindCity()
        {
            for (int i = 0; i < MapSizeWidth; i++)
                for (int j = 0; j < MapSizeHeight; j++)
                    if (CellsOnMap[i, j].CityOnCell != null)
                        return CellsOnMap[i, j].CityOnCell;
            return null;
        }

        public Buildings[] FindAllBuildings()
        {
            Buildings[] BuildingsOnMap = new Buildings[0];
            for (int i = 0; i < MapSizeWidth; i++)
                for (int j = 0; j < MapSizeHeight; j++)
                    if (CellsOnMap[i, j].BuildingOnCell != null)
                    {
                        Array.Resize<Buildings>(ref BuildingsOnMap, BuildingsOnMap.Length + 1);
                        BuildingsOnMap[BuildingsOnMap.Length - 1] = CellsOnMap[i, j].BuildingOnCell;
                    }
            if (BuildingsOnMap.Length == 0)
                return null;
            else
                return BuildingsOnMap;
        }

        public bool StorageOpenInSomeCity()
        {
            for (int i = 0; i < MapSizeWidth; i++)
                for (int j = 0; j < MapSizeHeight; j++)
                    if (CellsOnMap[i, j].CityOnCell != null)
                        if (CellsOnMap[i, j].CityOnCell.StorageMenu != null)
                            return true;
            return false;
        }

        public City GetOpenStorageCity()
        {
            for (int i = 0; i < MapSizeWidth; i++)
                for (int j = 0; j < MapSizeHeight; j++)
                    if (CellsOnMap[i, j].CityOnCell != null)
                        if (CellsOnMap[i, j].CityOnCell.StorageMenu != null)
                            return CellsOnMap[i, j].CityOnCell;
            return null;
        }

        public void NewTurn()
        {   // Начинаем пересчет ходов всех юнитов, которые есть на клетках
            for (int i = 0; i < MapSizeWidth; i++)
                for (int j = 0; j < MapSizeHeight; j++)
                    if (CellsOnMap[i, j].UnitOnCell != null)
                        CellsOnMap[i, j].UnitOnCell.RestoreMovePoints();
        }

        public void ModifyAllWorkers()
        {
            for (int i = 0; i < MapSizeWidth; i++)
                for (int j = 0; j < MapSizeHeight; j++)
                    if (CellsOnMap[i, j].UnitOnCell != null && CellsOnMap[i, j].UnitOnCell.UnitType == 2)
                    {
                        CellsOnMap[i, j].UnitOnCell.Dispose();
                        CellsOnMap[i, j].UnitOnCell = null;
                        CellsOnMap[i, j].UnitOnCell = new ImprovedWorker(Game1.ThisGame, (int)CellsOnMap[i, j].CellPosition.X, (int)CellsOnMap[i, j].CellPosition.Y, CellsOnMap[i, j]);
                    }
        }
    }
}
