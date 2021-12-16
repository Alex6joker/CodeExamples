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
    public class CityStorageMenu : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Texture2D UpperMenu; // Текстура панели хранилища города
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        Texture2D DrawableStorageTexture;
        // К какому городу относится
        City OwnerCity;

        public CityStorageMenu(Game1 game, City Owner)
            : base(game)
        {
            LoadContent();
            DrawableStorageTexture = null;
            OwnerCity = Owner;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            UpperMenu = Game.Content.Load<Texture2D>("UpperMenu");
            spriteFont = Game.Content.Load<SpriteFont>("MyFont");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        private void LoadResourceTexture(String Resource)
        {
            DrawableStorageTexture = Game.Content.Load<Texture2D>(Resource);
        }

        public override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            // Рисуем нижнее левое статистическое меню
            spriteBatch.Draw(UpperMenu, new Rectangle(Game1.graphics.PreferredBackBufferWidth / 12, Game1.graphics.PreferredBackBufferHeight - Game1.graphics.PreferredBackBufferHeight / 10,
            60 * OwnerCity.CityStorage.GetResourcesCount(), Game1.graphics.PreferredBackBufferHeight / 10), Color.White);
            // Выводим все ресурсы и их количество
            int BeginX = Game1.graphics.PreferredBackBufferWidth / 12;
            int BeginY = Game1.graphics.PreferredBackBufferHeight - Game1.graphics.PreferredBackBufferHeight / 10;
            // (Временно) массив ресурсов: медь и железо
            String[] GameResources = { "Медь", "Железо", "Нефть" };
            for (int i = 0; i < OwnerCity.CityStorage.GetResourcesCount(); i++)
            {
                LoadResourceTexture(GameResources[i]);
                spriteBatch.Draw(DrawableStorageTexture, new Rectangle(BeginX + i * 60, BeginY,
                30, 30), new Rectangle(0,0, 19, 20), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.01f);
                spriteBatch.DrawString(spriteFont, OwnerCity.CityStorage.GetResourceCount(i).ToString(),
                    new Vector2(BeginX + i * 60 + DrawableStorageTexture.Width,
                        BeginY + 40), Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0f);
            }                
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
