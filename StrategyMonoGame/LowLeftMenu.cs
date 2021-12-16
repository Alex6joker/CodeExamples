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
    public class LowLeftMenu : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Texture2D UpperMenu; // Текстура нижней левой статистической панели
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        Texture2D DrawableUnitTexture;
        string TextureString;
        public bool DrawBuildingsCreateMenu;
        public Vector2 CreateMenuPosition;
        public Vector2 CreateMenuIconsSize;
        public Rectangle[] CreateMenu;
        public Texture2D[] CreateMenuIcons;
        MouseState LastState;
        MouseState CurrentState;
        Timer CreateMenuBuildingsInfoTimer;
        String[,] BuildingsInfo;
        bool DrawBuildingsCreateMenuInfo;
        int DrawingBuildingInfo;
        bool DrawUnitInfo;
        Units ThisUnit;

        public LowLeftMenu(Game1 game)
            : base(game)
        {            
            DrawableUnitTexture = null;
            DrawBuildingsCreateMenu = false;
            CreateMenuPosition.X = 250;
            CreateMenuPosition.Y = Game1.graphics.PreferredBackBufferHeight - Game1.graphics.PreferredBackBufferHeight / 10;
            TextureString = "";
            CreateMenuIconsSize.X = 50;
            CreateMenuIconsSize.Y = 50;
            CreateMenu = new Rectangle[4];
            CreateMenu[0] = new Rectangle((int)CreateMenuPosition.X + 50, (int)CreateMenuPosition.Y + 10, (int)CreateMenuIconsSize.X, (int)CreateMenuIconsSize.Y);
            CreateMenu[1] = new Rectangle((int)CreateMenuPosition.X + 50 + 80, (int)CreateMenuPosition.Y + 10, (int)CreateMenuIconsSize.X, (int)CreateMenuIconsSize.Y);
            CreateMenu[2] = new Rectangle((int)CreateMenuPosition.X + 50 + 160, (int)CreateMenuPosition.Y + 10, (int)CreateMenuIconsSize.X, (int)CreateMenuIconsSize.Y);
            CreateMenu[3] = new Rectangle((int)CreateMenuPosition.X + 50 + 240, (int)CreateMenuPosition.Y + 10, (int)CreateMenuIconsSize.X, (int)CreateMenuIconsSize.Y);
            CreateMenuIcons = new Texture2D[CreateMenu.Length];
            BuildingsInfo = new String[4,4];
            FillBuildingsInfo();
            CreateMenuBuildingsInfoTimer = new Timer(Game.Content.Load<SpriteFont>("MyFont"), new Vector2(430, 2), "00:00");
            DrawBuildingsCreateMenuInfo = false;
            DrawUnitInfo = false;
            DrawingBuildingInfo = Int32.MaxValue;
            LoadContent();
        }

        void FillBuildingsInfo()
        {
            char[] val = {'0','1'};
            for(int i = 0; i < 4; i++)
            {
                BuildingsInfo[i, 0] = new String(val);
                BuildingsInfo[i, 1] = new String(val);
                BuildingsInfo[i, 2] = new String(val);
                BuildingsInfo[i, 3] = new String(val);
                BuildingsInfo[i, 1] = "Требуется :";
            }
            BuildingsInfo[0, 0] = "Библиотека";
            BuildingsInfo[0, 2] = "Медь - 10";
            BuildingsInfo[0, 3] = "Железо - 5";
            BuildingsInfo[1, 0] = "Пастбище";
            BuildingsInfo[1, 2] = "Медь - 20";
            BuildingsInfo[1, 3] = "Железо - 10";
            BuildingsInfo[2, 0] = "Завод";
            BuildingsInfo[2, 2] = "Нефть - 10";
            BuildingsInfo[2, 3] = "Железо - 30";
            BuildingsInfo[3, 0] = "Рынок";
            BuildingsInfo[3, 2] = "Медь - 20";
            BuildingsInfo[3, 3] = "Нефть - 10";
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            UpperMenu = Game.Content.Load<Texture2D>("UpperMenu");
            spriteFont = Game.Content.Load<SpriteFont>("MyFont");
            CreateMenuIcons[0] = Game.Content.Load<Texture2D>("Библиотека");
            CreateMenuIcons[1] = Game.Content.Load<Texture2D>("Пастбище");
            CreateMenuIcons[2] = Game.Content.Load<Texture2D>("Завод");
            CreateMenuIcons[3] = Game.Content.Load<Texture2D>("Рынок");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (DrawBuildingsCreateMenu)
            {
                LastState = CurrentState;
                CurrentState = Mouse.GetState();
                Rectangle MouseRectangle = new Rectangle(CurrentState.X, CurrentState.Y, 1, 1);
                for (int i = 0; i < CreateMenu.Length; i++)
                {
                    if (MouseRectangle.Intersects(CreateMenu[i]) &&
                        CurrentState.LeftButton == ButtonState.Pressed && LastState.LeftButton == ButtonState.Released)
                    {
                        if (ThisUnit.GetMovePoints() > 0)
                        {
                            if(Game1.ThisGame.GetMap().GetActiveCell().CreateNewBuilding((Strategy.BuildingTypes)i))
                                ThisUnit.MakeOneTurn();
                        }
                    }
                    if (MouseRectangle.Intersects(CreateMenu[i]))
                    {
                        CreateMenuBuildingsInfoTimer.Update(gameTime);
                    }
                    else if (!IntersectsSomeRect(MouseRectangle))
                    {
                        CreateMenuBuildingsInfoTimer.SS = CreateMenuBuildingsInfoTimer.MS = CreateMenuBuildingsInfoTimer.MM = 0;
                    }
                    if (CreateMenuBuildingsInfoTimer.MS >= 250 || CreateMenuBuildingsInfoTimer.SS > 0)
                    {
                        DrawBuildingsCreateMenuInfo = true;
                        DrawingBuildingInfo = GetIntersectionRect(MouseRectangle);
                    }
                    else
                        DrawBuildingsCreateMenuInfo = false;
                }
            }
            else
            {   // Когда строим здание с отображаемой информацией
                // оно остается, нужно убрать
                DrawBuildingsCreateMenuInfo = false;
            }
        }

        public bool IntersectsSomeRect(Rectangle Mouse)
        {
            for (int i = 0; i < CreateMenu.Length; i++)
                if (Mouse.Intersects(CreateMenu[i]))
                    return true;
            return false;
        }

        public int GetIntersectionRect(Rectangle Mouse)
        {
            for (int i = 0; i < CreateMenu.Length; i++)
                if (Mouse.Intersects(CreateMenu[i]))
                    return i;
            return 0;
        }

        public override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            // Рисуем нижнее левое статистическое меню
            spriteBatch.Draw(UpperMenu, new Rectangle(0, Game1.graphics.PreferredBackBufferHeight - Game1.graphics.PreferredBackBufferHeight / 10, 
                Game1.graphics.PreferredBackBufferWidth / 12, Game1.graphics.PreferredBackBufferHeight/ 10), Color.White);
            // Если есть юнит, который нужно отрисовывать, то выводим его иконку
            if (DrawableUnitTexture != null)
            {
                spriteBatch.Draw(DrawableUnitTexture, new Rectangle(0, Game1.graphics.PreferredBackBufferHeight - Game1.graphics.PreferredBackBufferHeight / 10,
                Game1.graphics.PreferredBackBufferWidth / 12, Game1.graphics.PreferredBackBufferHeight / 10), Color.White);
            }
            if (DrawBuildingsCreateMenu)
            {
                spriteBatch.Draw(UpperMenu,
                    new Rectangle((int)CreateMenuPosition.X, (int)CreateMenuPosition.Y,
                    400, 100),
                    Color.White);
                for(int i = 0; i < CreateMenu.Length; i++)
                {
                    spriteBatch.Draw(CreateMenuIcons[i], CreateMenu[i], Color.White);
                }
            }
            if (DrawBuildingsCreateMenuInfo)
            {
                spriteBatch.Draw(UpperMenu, new Rectangle((int)CreateMenuPosition.X + 70 + (80 * DrawingBuildingInfo), (int)CreateMenuPosition.Y - 100,
                100, 100), Color.White);
                spriteBatch.DrawString(spriteFont, BuildingsInfo[DrawingBuildingInfo, 0],
                    new Vector2((int)CreateMenuPosition.X + 75 + (80 * DrawingBuildingInfo), (int)CreateMenuPosition.Y - 90), Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(spriteFont, BuildingsInfo[DrawingBuildingInfo, 1],
                    new Vector2((int)CreateMenuPosition.X + 75 + (80 * DrawingBuildingInfo), (int)CreateMenuPosition.Y - 70), Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(spriteFont, BuildingsInfo[DrawingBuildingInfo, 2],
                    new Vector2((int)CreateMenuPosition.X + 75 + (80 * DrawingBuildingInfo), (int)CreateMenuPosition.Y - 50), Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(spriteFont, BuildingsInfo[DrawingBuildingInfo, 3],
                    new Vector2((int)CreateMenuPosition.X + 75 + (80 * DrawingBuildingInfo), (int)CreateMenuPosition.Y - 30), Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0f);
            }
            if (DrawUnitInfo)
            {
                // Получаем информацию о юните
                spriteBatch.Draw(UpperMenu, new Rectangle(100, (int)CreateMenuPosition.Y,
                120, 100), Color.White);
                String UnitMove = ThisUnit.GetMoveString();
                String[] UnitName = TextureString.Split(new char[] { ' ' });

                spriteBatch.DrawString(spriteFont, UnitName[0],
                    new Vector2(110, (int)CreateMenuPosition.Y + 5), Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0f);

                if (UnitName.Length > 2)
                {
                    spriteBatch.DrawString(spriteFont, UnitName[1],
                    new Vector2(110, (int)CreateMenuPosition.Y + 25), Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0f);
                }

                spriteBatch.DrawString(spriteFont, UnitMove,
                    new Vector2(110, (int)CreateMenuPosition.Y + 50), Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0f);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void SetDrawableUnit(Texture2D UnitTexture, String nTextureString, Units nThisUnit)
        {   // Устанавливаем юнит, который будем отрисовывать
            TextureString = nTextureString;
            if (UnitTexture != null && TextureString == "Улучшенный Рабочий")
                DrawBuildingsCreateMenu = true;
            else
                DrawBuildingsCreateMenu = false;
            if (TextureString != "")
            {
                String[] ttt = { TextureString, "Ico" };
                TextureString = String.Join(" ", ttt);
                DrawableUnitTexture = Game.Content.Load<Texture2D>(TextureString);
            }
            else
                DrawableUnitTexture = null;

            if (DrawableUnitTexture != null)
            {
                DrawUnitInfo = true;
            }
            else
                DrawUnitInfo = false;
            ThisUnit = nThisUnit;
        }

        public Texture2D GetDrawableUnit()
        {
            return DrawableUnitTexture;
        }
    }
}
