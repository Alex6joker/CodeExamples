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
    class ImprovedWorker : Worker
    {        
        public ImprovedWorker(Game game, Int32 nX, Int32 nY, Cells Cell) : base(game, nX, nY, Cell)
        {
            TexturePath = "Улучшенный Рабочий";
            LoadContent();
            MoveRange = 5;
            UnitType = 2;
            MovePoints = MoveRange;
            Sound = Game1.ThisGame.Content.Load<SoundEffect>("GetResource");
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            sbSpriteBatch = new SpriteBatch(GraphicsDevice);
            UnitTexture = Game.Content.Load<Texture2D>(TexturePath);
            // TODO: use this.Content to load your game content here
        }

        public override void ProduceAnAction()
        {
            if (UnitOnThisCell.ResourceOnCell != null && MovePoints != 0)
            {   // Добываем весь ресурс
                while (UnitOnThisCell.GetResourceFromCell()) { };
                Sound.Play();
                MovePoints--;
                MoveRange++;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime); // Производим все действия для стандартного рабочего
        }

        public override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            sbSpriteBatch.Begin();
            TotalFrames = UnitTexture.Width / FrameWidth;
            sbSpriteBatch.Draw(UnitTexture, new Rectangle((int)Position.X, (int)Position.Y, FrameWidth, FrameHeight),
                new Rectangle(CurrentFrame * FrameWidth, 8, FrameWidth, FrameHeight), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0f);
            sbSpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}