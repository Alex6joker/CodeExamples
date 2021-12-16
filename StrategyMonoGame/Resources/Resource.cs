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
    public class Resource : Microsoft.Xna.Framework.DrawableGameComponent
    {
        /*
         * Это абстрактный класс, описывающий общую структуру
         * ресурсов, которые присутствуют в игре.
         * Сами ресурсы мало отличимы друг от друга на уровне программы.
        */
        protected UInt16 Count; // Кол-во ресурса, находящегося на конкретной клетке
        protected SpriteBatch sbSpriteBatch;
        SpriteFont spriteFont;
        protected Texture2D ResourceTexture;
        protected Vector2 Position;// = new Vector2(150, 30);
        protected String TexturePath; // Путь к текстуре ресурса
        public Byte Type;


        public Resource(Game game, UInt16 nCount, Int32 PositionX, Int32 PositionY, Byte nType, String nTexturePath) : base(game)
        {   // Ресурса может быть разное количество
            Count = nCount;
            Position.X = PositionX;
            Position.Y = PositionY;
            TexturePath = nTexturePath;
            LoadContent();
            Type = nType;
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            sbSpriteBatch = new SpriteBatch(GraphicsDevice);
            ResourceTexture = Game.Content.Load<Texture2D>(TexturePath);
            spriteFont = Game.Content.Load<SpriteFont>("MyFont");
            // TODO: use this.Content to load your game content here
        }

        public UInt16 GetResourceFromCell()
        {   // Собираем 10 единиц ресурса с клетки
            if(Count > 10)
            {
                Count -= 10;
                return 10;
            } 
            else
            {
                UInt16 ret = Count;
                Count = 0;
                return ret;
            }
        }

        public UInt16 GetResourceCount()
        {
            return Count;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            sbSpriteBatch.Begin();
            sbSpriteBatch.Draw(ResourceTexture, Position, Color.White);
            sbSpriteBatch.DrawString(spriteFont, Count.ToString(), new Vector2(Position.X + 25, Position.Y), Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0f);
            sbSpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
