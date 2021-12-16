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
    public class Cells : Microsoft.Xna.Framework.DrawableGameComponent
    {
        /*
         * Класс описывает общую структуру клеток,
         * из которых состоит карта игры
        */

        public Units UnitOnCell; // Юнит, находящийся на клетке
        public Resource ResourceOnCell; // Ресурс, находящийся на клетке
        public City CityOnCell; // Город, находящийся на клетке
        public Buildings BuildingOnCell;

        // Графика
        protected SpriteBatch sbSpriteBatch;
        public Vector2 CellPosition;
        protected Texture2D CellTexture;
        protected String TexturePath; // Путь к тексутре ресурса
        public bool CellIsActive;    // Данная клетка активна
        public Map ThisMap;
        protected bool IsPassable;
        protected Int32 IndexX, IndexY; // Индексы ячеек карты
        protected MouseState LastState;
        protected MouseState CurrentState;
        bool UpdOld;
        Cells Old;

        public Cells(Game game, Int32 XMatrixPozition, Int32 YMatrixPozition, Map map, String nTexturePath, bool nIsPassable, Int32 nX, Int32 nY)
            : base(game)
        {
            // Изначально на клетке ничего нет
            UnitOnCell = null;
            ResourceOnCell = null;
            CityOnCell = null;
            CellIsActive = false;
            ThisMap = map;
            TexturePath = nTexturePath;
            LoadContent();
            CellPosition.X = CellTexture.Width * XMatrixPozition;
            CellPosition.Y = CellTexture.Height * YMatrixPozition;
            IsPassable = nIsPassable; // Клетка проходима юнитом
            IndexX = nX;
            IndexY = nY;
            UpdOld = false;
            Old = null;
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            sbSpriteBatch = new SpriteBatch(GraphicsDevice);
            CellTexture = Game.Content.Load<Texture2D>(TexturePath);
            // TODO: use this.Content to load your game content here
        }

        // Метод по обработке нажатия на клетки ЛКМ
        void WorkWithCells()
        {            
            if (UnitOnCell != null)
            {
                Game1.LLMenu.SetDrawableUnit(UnitOnCell.GetUnit2DTexture(), UnitOnCell.TexturePath, UnitOnCell);
                ThisMap.NullActiveCells();
                Cells ActiveCell = ThisMap.GetActiveCell();
                if (ActiveCell != null)
                    ActiveCell.CellIsActive = false;
                CellIsActive = true;
            }
            else
            {
                CellIsActive = false;
                Game1.LLMenu.SetDrawableUnit(null, "", null);
            }
            // Если на клетке город, то выводим его склад
            if (CityOnCell != null)
            {
                CityOnCell.StorageMenu = new CityStorageMenu(Game1.ThisGame, CityOnCell);
                ThisMap.NullActiveCells();
                CellIsActive = true;
            }
            else if (ThisMap.StorageOpenInSomeCity())
            {
                City CityWithOpenStorage = ThisMap.GetOpenStorageCity();
                if (CityWithOpenStorage != null)
                {
                    CityWithOpenStorage.StorageMenu.Dispose();
                    CityWithOpenStorage.StorageMenu = null;
                    ThisMap.NullActiveCells();
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (UpdOld)
            {
                Old.UnitOnCell.Update(gameTime);
                UnitOnCell = Old.UnitOnCell;
                if (Old.UnitOnCell.GetInPositionStatus())
                {
                    
                    Old.UnitOnCell = null;
                    UpdOld = false;
                    Old = null;
                }
            }
                
            // Получаем состояние мыши
            LastState = CurrentState;
            CurrentState = Mouse.GetState();
            // Если нажатие ЛКМ мыши входит в рамки клетки и на клетке стоит юнит, то отрисовываем его иконку
            // в нижней панели слева
            Rectangle MouseRectangle = new Rectangle(CurrentState.X, CurrentState.Y, 1, 1);
            Rectangle LLCreateMenuRectangle = new Rectangle((int)Game1.LLMenu.CreateMenuPosition.X, (int)Game1.LLMenu.CreateMenuPosition.Y, 400, 100);
            if (CurrentState.LeftButton == ButtonState.Pressed && LastState.LeftButton == ButtonState.Released && (CurrentState.X >= CellPosition.X && CurrentState.Y >= CellPosition.Y &&
                CurrentState.X <= CellPosition.X + CellTexture.Width && CurrentState.Y <= CellPosition.Y + CellTexture.Height))
            {   
                if(!(MouseRectangle.Intersects(LLCreateMenuRectangle) && Game1.LLMenu.DrawBuildingsCreateMenu))
                {
                    WorkWithCells();
                }                
            }
            // Если нажата ПКМ и при этом есть юнит на активной клетке
            // перемещаем его, если позволяет его радиус передвижения
            if (CurrentState.RightButton == ButtonState.Pressed && LastState.RightButton == ButtonState.Released && (CurrentState.X >= CellPosition.X && CurrentState.Y >= CellPosition.Y &&
                CurrentState.X <= CellPosition.X + CellTexture.Width && CurrentState.Y <= CellPosition.Y + CellTexture.Height))
            {   // Если на клетке не пусто, то (временно) ничего не делаем
                if (UnitOnCell == null && CityOnCell == null)
                {   // Перемещаем юнит
                    // Узнаем исходную клетку
                    Cells ActiveCell = ThisMap.GetActiveCell();
                    int[] ActiveCellInfo = ThisMap.GetActiveCellIndex();
                    // Узнаем целевую
                    int[] TargetCellInfo = ThisMap.GetCellInfo(CellPosition);
                    // Узнаем проходимость клетки
                    // Узнаем, укладывается ли перемещение юнита в его дальность перемещения, если нет - отказываемся перемещаться
                    Cells TargetCell = ThisMap.GetCellByCoordinates(TargetCellInfo);
                    Int32[] MoveDistanceArray = CalculateDistanseBetweenCells(ActiveCell, TargetCell);
                    if (GetCellPassability(TargetCell) && (MoveDistanceArray[0] <= ActiveCell.UnitOnCell.GetMovePoints() && MoveDistanceArray[1] <= ActiveCell.UnitOnCell.GetMovePoints()))
                    {   // Запускаем механизм перемещения юнитов (не перемещается иконка)
                        // Записываем юниту новое количество оставшихся очков перемещения для данного хода
                        ActiveCell.UnitOnCell.SetRemainingMovePoints((Byte)(MoveDistanceArray[0] > MoveDistanceArray[1] ? MoveDistanceArray[0] : MoveDistanceArray[1]));
                        // Снимаем активность со старой клетки
                        ThisMap.NullActiveCells();
                        ThisMap.StartMoveUnits(ActiveCellInfo, TargetCellInfo);
                    }                                  
                }
            }
            if (UnitOnCell != null)
                UnitOnCell.Update(gameTime);
            if (ResourceOnCell != null)
                ResourceOnCell.Update(gameTime);
            if (CityOnCell != null)
                CityOnCell.Update(gameTime);
            if (BuildingOnCell != null)
                BuildingOnCell.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            sbSpriteBatch.Begin();
            sbSpriteBatch.Draw(CellTexture, CellPosition, new Rectangle(0, 0, 60, 60), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
            sbSpriteBatch.End();
            if (ResourceOnCell != null)
                ResourceOnCell.Draw(gameTime);
            if (CityOnCell != null)
                CityOnCell.Draw(gameTime);
            if (UnitOnCell != null)
                UnitOnCell.Draw(gameTime);
            if (BuildingOnCell != null)
                BuildingOnCell.Draw(gameTime);
            base.Draw(gameTime);          
        }

        public Vector2 GetPositionVector()
        {
            return CellPosition;
        }

        public void MoveUnit(Cells OldPos, Cells NewPos)
        {   // Вполне можно просто поменять местами юниты на клетках, так как известны их индексы
            // анимация будет проигрываться от старого положения юнита до нового
            Vector2 OldPosVector = OldPos.GetPositionVector();
            Vector2 NewPosVector = NewPos.GetPositionVector();
            if (!(OldPosVector.X > NewPosVector.X || OldPosVector.Y > NewPosVector.Y))
            {
                NewPos.UnitOnCell = OldPos.UnitOnCell;
                UnitOnCell.SetPosition((int)CellPosition.X, (int)CellPosition.Y, NewPos, false);
                CellIsActive = true;

                OldPos.UnitOnCell = null;
            }
            else
            {
                // В таком случае нужно корректировать отрисовку
                // Менять местами переменные не нужно
                OldPos.UnitOnCell.SetPosition((int)NewPosVector.X, (int)NewPosVector.Y, NewPos, false);
                Old = OldPos;
                UpdOld = true;

                OldPos.CellIsActive = false;
                CellIsActive = true;
            }            
        }

        public void CreateStartUnitSettler()
        {   // Принудительное создание первых двух юнитов: рабочего и поселенца
            // Позиции юнитов известны при инициализации карты
            // Создаем поселенца
            UnitOnCell = new Settler(Game1.ThisGame, (Int32)CellPosition.X, (Int32)CellPosition.Y, this);
        }

        public void CreateStartUnitWorker()
        {   // Принудительное создание первых двух юнитов: рабочего и поселенца
            // Позиции юнитов известны при инициализации карты
            // Создаем рабочего
            UnitOnCell = new Worker(Game1.ThisGame, (Int32)CellPosition.X, (Int32)CellPosition.Y, this);
        }

        public void CreateResourseOnMap(int ResourseType)
        {
            Random Rnd = new Random();
            // Ресурс - железо (0), либо медь (1)
            switch(ResourseType)
            {   // (Временно) создаем ресурсы строго по 20 единиц
                case 0:
                    ResourceOnCell = new Resource(Game1.ThisGame, (ushort)Rnd.Next(5, 20), (Int32)CellPosition.X + 10, (Int32)CellPosition.Y + 10, 2, "Железо");
                    break;
                case 1:
                    ResourceOnCell = new Resource(Game1.ThisGame, (ushort)Rnd.Next(5, 50), (Int32)CellPosition.X + 10, (Int32)CellPosition.Y + 10, 1, "Медь");
                    break;
                case 2:
                    ResourceOnCell = new Resource(Game1.ThisGame, (ushort)Rnd.Next(5, 15), (Int32)CellPosition.X + 10, (Int32)CellPosition.Y + 10, 3, "Нефть");
                    break;
            }
        }

        public void CreateNewCity()
        {   // Удаляем юнит
            UnitOnCell.Dispose();
            // (возможно не нужно) указываем null
            UnitOnCell = null;
            // Убираем иконку снизу
            Game1.LLMenu.SetDrawableUnit(null, "", null);
            // Создаем город
            CityOnCell = new City(Game1.ThisGame, CellPosition);
            UpStatisticsMenu.PlayerScore += 10;
        }

        public bool CreateNewBuilding(Strategy.BuildingTypes Type)
        {
            Byte[] Resourses = Game1.ThisGame.GetMap().FindCity().CityStorage.GetAllResoursesInfo();
            if (Type == BuildingTypes.BUILDING_LIBRIARY)
            {   // Проверка на ресурсы
                if (Resourses[0] < 10 || Resourses[1] < 5)
                    return false;
            }
            else if (Type == BuildingTypes.BUILDING_PASTURE)
            {   // Проверка на ресурсы
                if (Resourses[0] < 20 || Resourses[1] < 10)
                    return false;
            }
            else if (Type == BuildingTypes.BUILDING_FACTORY)
            {   // Проверка на ресурсы
                if (Resourses[2] < 10 || Resourses[1] < 30)
                    return false;
            }
            else if (Type == BuildingTypes.BUILDING_MARKET)
            {   // Проверка на ресурсы
                if (Resourses[0] < 20 || Resourses[2] < 10)
                    return false;
            }
            
            BuildingOnCell = new Buildings(Game1.ThisGame, Type, new Rectangle((int)CellPosition.X + 5, (int)CellPosition.Y + 5, 50, 50), this);
            // Убираем иконку снизу

            // Если на клетке не пусто, то выводим юнит, иначе - очищаем в пустоту
            // Получаем состояние мыши
            LastState = CurrentState;
            CurrentState = Mouse.GetState();

            Rectangle MouseRectangle = new Rectangle(CurrentState.X, CurrentState.Y, 1, 1);
            Rectangle ActivePanel = new Rectangle((int)Game1.LLMenu.CreateMenuPosition.X, (int)Game1.LLMenu.CreateMenuPosition.Y,
                    400, 100);

            if (!(Game1.LLMenu.DrawBuildingsCreateMenu && MouseRectangle.Intersects(ActivePanel)))
                Game1.LLMenu.SetDrawableUnit(null, "", null);
            UpStatisticsMenu.PlayerScore += 15;
            return true;
        }

        public bool GetResourceFromCell()
        {   // Если на клетке есть ресурс и есть город, то собираем 10 единиц ресурса
            if(ResourceOnCell != null && ThisMap.FindCity() != null)
            {
                UInt16 Res = ResourceOnCell.GetResourceFromCell();
                // (Временно) на карте один город
                City TargetCity = ThisMap.FindCity();
                TargetCity.CityStorage.AddResourceToStorage((Byte)Res, ResourceOnCell.Type - 1);
                // Если ресурса не осталось, то удаляем его
                if (ResourceOnCell.GetResourceCount() == 0)
                {
                    ResourceOnCell.Dispose();
                    ResourceOnCell = null;
                }
                UpStatisticsMenu.PlayerScore += 5;
                return true;
            }
            return false;
        }

        public bool GetCellPassability(Cells Cell)
        {
            return Cell.IsPassable;
        }

        private Int32[] CalculateDistanseBetweenCells(Cells OldCell, Cells NewCell)
        {
            Int32[] DistanseArray = new Int32[2];
            DistanseArray[0] = Math.Abs(OldCell.IndexX - NewCell.IndexX);
            DistanseArray[1] = Math.Abs(OldCell.IndexY - NewCell.IndexY);
            return DistanseArray;
        }
    }
}
