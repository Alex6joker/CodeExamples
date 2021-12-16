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
    class EntryField : Microsoft.Xna.Framework.DrawableGameComponent
    {
        String Str; // Строка, которая выводится
        Texture2D EntryBox; // Текстура, внутри которой вводится имя игрока
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        Keys[] lastPressedKeys;
        Vector2 Position;

        public EntryField(Game1 game) : base(game)
        {
            Str = "";
            spriteBatch = new SpriteBatch(GraphicsDevice);
            EntryBox = Game.Content.Load<Texture2D>("EntryField");
            spriteFont = Game.Content.Load<SpriteFont>("MyFont");
            lastPressedKeys = new Keys[0];
            Position.X = 400;
            Position.Y = 300;
        }

        public String GetContainigString()
        {
            return Str;
        }

        public void SetNewSymbol(Keys key)
        {
            if (Str.Length <= 15)
            {
                if (key >= Keys.D0 && key <= Keys.D9)
                    Str += key.ToString()[1];
                char Char = Convert.ToChar(key);
                if (ToRussian(Char) != ' ')
                    Str += ToRussian(Char);
            }
        }

        private void BackSymbol()
        {
            if (Str.Length != 0)
                Str = Str.Remove(Str.Length - 1, 1);
        }

        protected override void LoadContent()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            KbUpdate();
            Game1.PlayerName = Str;
        }

        public override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            //  spriteBatch.Draw(UnitTexture, Position, Color.White);
            spriteBatch.Draw(EntryBox, new Rectangle((int)Position.X, (int)Position.Y, EntryBox.Width, EntryBox.Height), Color.White);
            spriteBatch.DrawString(spriteFont, "Введите имя игрока", new Vector2(Position.X, Position.Y - 40), Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(spriteFont, Str, new Vector2(Position.X + 5, Position.Y + EntryBox.Height / 4), Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0f);
            spriteBatch.End();
            base.Draw(gameTime);
        }

    private void KbUpdate()
        {
            KeyboardState kbState = Keyboard.GetState();
            Keys[] pressedKeys = kbState.GetPressedKeys();

            //check if any of the previous update's keys are no longer pressed
            foreach (Keys key in lastPressedKeys)
            {
                if (!pressedKeys.Contains(key))
                    OnKeyUp(key);
            }

            //check if the currently pressed keys were already pressed
            foreach (Keys key in pressedKeys)
            {
                if (!lastPressedKeys.Contains(key))
                    OnKeyDown(key);
            }

            //save the currently pressed keys so we can compare on the next update
            lastPressedKeys = pressedKeys;
        }

        private void OnKeyDown(Keys key)
        {

        }

        private char ToRussian(char Char)
        {
            switch (Char)
            {
                case 'Q': return 'Й';
                case 'W': return 'Ц';
                case 'E': return 'У';
                case 'R': return 'К';
                case 'T': return 'Е';
                case 'Y': return 'Н';
                case 'U': return 'Г';
                case 'I': return 'Ш';
                case 'O': return 'Щ';
                case 'P': return 'З';
                case (char)219: return 'Х';
                case (char)221: return 'Ъ';
                case 'A': return 'Ф';
                case 'S': return 'Ы';
                case 'D': return 'В';
                case 'F': return 'А';
                case 'G': return 'П';
                case 'H': return 'Р';
                case 'J': return 'О';
                case 'K': return 'Л';
                case 'L': return 'Д';
                case (char)186: return 'Ж';
                case (char)222: return 'Э';
                case 'Z': return 'Я';
                case 'X': return 'Ч';
                case 'C': return 'С';
                case 'V': return 'М';
                case 'B': return 'И';
                case 'N': return 'Т';
                case 'M': return 'Ь';
                case (char)188: return 'Б';
                case (char)190: return 'Ю';
                default: return ' ';
            }
        }

        private void OnKeyUp(Keys key)
        {

            if (key == Keys.Back && Str.Length != 0)
            {
                BackSymbol();
            }
            else
            {
                SetNewSymbol(key);
            }
        }
    }
}
