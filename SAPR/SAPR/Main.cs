using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace SAPR
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Main : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Plate Plate;

        int ActiveCell = 0; //Номер ячейки, куда будет вставляться компонент

        int[] TempPlan; //План температурного баланса

        string[] PlatePlan = {"Резистор","Резистор", "Резистор" , "Конденсатор" , "Питание" };

        // Всего 3 компонета(задано изначально), но здесь только обозначаем свойство
        Component[] Component;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.Title = "SAPR";

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            //graphics.IsFullScreen = true;
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            int WinWidth = Window.ClientBounds.Width;
            int WinHeight = Window.ClientBounds.Height;

            //--------------------Плата и ячейки--------------------
            

            Plate = new Plate(PlatePlan.Length, 200, 10); // Сздаем плату с параметрами

            Plate.Cell = new Cell[Plate.SizePlate]; // Создаем массив типа Cell
            for (int i = 0; i < Plate.SizePlate; i++) //Заполняем данный массив
            {
                Plate.Cell[i] = new Cell(
                    new Rectangle(
                        i * (Plate.SizeCell + Plate.Margin) + Plate.Margin,
                        Plate.Margin,
                        Plate.SizeCell,
                        Plate.SizeCell),
                    Content.Load<Texture2D>("img/Cell")
                );
            }

            Plate.TextureBG = Content.Load<Texture2D>("img/fon");
            //------------------------------------------------------

            // Компоненты подгружаются непосредственно из БД MySQL
            // Создаем объект БД, производим подключение к БД посредством строки, поступающей в конструктор
            DataBase DB = new DataBase("Server=localhost;Port=3306;Database=Components;Uid=root;password=;");
            // Производим запрос к БД, чтобы получить информацию о компонентах
            String[] ComponentsInfo = DB.MakeQuery("SELECT * FROM `Components` ORDER BY `ID`");
            // Мы узнали, что у нас 3 компонета из результатов запроса к БД
            Int32 ComponentsCount = DB.GetLastQueryRowsCount();
            // Мы узнали, что у компоеннтов 4 свойтва (считая ID) из результатов запроса к БД
            Int32 ComponentsAttributesCount = DB.GetLastQueryFieldsCount();

            Component = new Component[ComponentsCount];

            // Заполняем в цикле компоненты
            for (Int32 i = 0; i < ComponentsCount; i++)
            {
                /*
                 * Учитывая, что у нас есть приложение и специально созданная
                 * для него БД, полагаем, что данные возвращаются из БД в определнной
                 * последовательности(ID, имя, текстура, температура)
                */

                // Делаем запрос к БД, чтобы получить информацию о несовместимости компонента
                String[] PartialQuery = { "CALL `GetIncompabilityComponents`(", ComponentsInfo[0 + (ComponentsAttributesCount * i)], ");" };
                String Query = String.Join(null, PartialQuery);
                String[] ComponentIncompability = DB.MakeQuery(Query);

                Component[i] = new Component(
                    ComponentsInfo[1 + (ComponentsAttributesCount * i)], //Имя
                    Content.Load<Texture2D>(ComponentsInfo[2 + (ComponentsAttributesCount * i)]), //Текстура
                    new Rectangle(
                        i * (Plate.SizeCell + Plate.Margin) + Plate.Margin,
                        Window.ClientBounds.Height - Plate.SizeCell - Plate.Margin,
                        Plate.SizeCell,
                        Plate.SizeCell),
                    Int32.Parse(ComponentsInfo[3 + (ComponentsAttributesCount * i)]),  //Температура
                    ComponentIncompability.Length != 0 ? ComponentIncompability : new string[] { "None" } // С какими компонентами несовместим
                    );
            }
            //--------------------------------------------------
            SetTempPlan();
        }


        public void SetTempPlan()
        {
            int[] tmpArray = new int[PlatePlan.Length]; //Временный массив температур
            for(int i = 0; i < PlatePlan.Length; i++) //Заполняем его
            {
               for(int j = 0; j < Component.Length; j++)
                {
                    if(PlatePlan[i] == Component[j].Name)
                    {
                        tmpArray[i] = Component[j].Temp;
                    }
                }
            }

            //Сортируем по возрастанию
            Array.Sort(tmpArray);

            TempPlan = new int[PlatePlan.Length]; //Создаем план температуры

            int TopI = 0, BottomI = PlatePlan.Length -1;

            for (int i = 0; i < PlatePlan.Length; i++)
            {
                TempPlan[TopI] = tmpArray[i];
                if(TopI != BottomI) TempPlan[BottomI] = tmpArray[i + 1];
                i++;
                TopI++;
                BottomI--;
            }

        }

        protected override void UnloadContent()
        {

        }

        public void SetComponent()
        {
            if (ActiveCell < Plate.Cell.Length) // Если есть ячейки для установки компонентов
            {
                for (int i = 0; i < Component.Length; i++) //Проверяем кождый компонент
                {
                    Component[i].CheckClick(); //Проверяем не нажат ли компонент
                    if (Component[i].Check) //Если компонент нажат
                    {
                        Plate.Cell[ActiveCell].Texture = Component[i].Texture; //Устанавливаем текстуру компонента в ячейку
                        Plate.Cell[ActiveCell].Component = Component[i].Name; //Устанавливаем имя компонента в ячейку
                        Mouse.SetPosition(
                            Plate.Cell[ActiveCell].Rectangle.X + Plate.Cell[ActiveCell].Rectangle.Width / 2,
                            Plate.Cell[ActiveCell].Rectangle.Y + Plate.Cell[ActiveCell].Rectangle.Height / 2);

                        UpdateActiveStateComponent(); //Оставляем активными только те компоненты, которые могут быть установлены в следующую ячейку

                        ActiveCell++; //Переходим к следующей ячейке
                        break;
                    }
                }
            }
        }

        public void UpdateActiveStateComponent()
        {
            //Возвращаем активность всем компонентам
            for (int i = 0; i < Component.Length; i++)
            {
                Component[i].Active = true;
            }

            //Проверка на несовместимость по электромагнитному воздействию
            for (int i = 0; i < Component.Length; i++)
            {
                for (int j = 0; j < Component[i].IncopatibleComponent.Length; j++)
                {
                    if (Plate.Cell[ActiveCell].Component == Component[i].IncopatibleComponent[j])
                    {
                        Component[i].Active = false;
                    }
                }
            }

            //Проверка баланс температуры
            for (int i = 0; i < Component.Length; i++)
            {
                for (int j = 0; j < Component[i].IncopatibleComponent.Length; j++)
                {
                    if (Plate.Cell[ActiveCell].Component == Component[i].IncopatibleComponent[j])
                    {
                        Component[i].Active = false;
                    }
                }
            }

        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            SetComponent(); //Функция установки компонентов

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            //Отрисовываем фон платы
            spriteBatch.Draw(Plate.TextureBG, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);

            //Отрисосываем ячейчки
            for (int i = 0; i < Plate.SizePlate; i++)
            {
                spriteBatch.Draw(Plate.Cell[i].Texture, Plate.Cell[i].Rectangle, Color.White);
            }

            //Отрисовываем компоненты
            for (int i = 0; i < Component.Length; i++)
            {
                if (Component[i].Active) //Если компонет активен, то отрисовываем его
                {
                    spriteBatch.Draw(Component[i].Texture, Component[i].Rectangle, Color.White);
                }
            }

            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
