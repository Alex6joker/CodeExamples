using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PvZ
{
    public class Cells : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public NPCs PlantOnCell;
        public Vector2 CellPosition; // Позиция клетки по X и Y
        public int CellLenght;

        public Cells(Game1 game, Vector2 nCellPosition, int nCellLenght) : base(game)
        {
            PlantOnCell = null;
            CellPosition = nCellPosition;
            CellLenght = nCellLenght;
        }

        public override void Update(GameTime gameTime)
        {
            if(PlantOnCell != null)
            {
                PlantOnCell.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (PlantOnCell != null)
            {
                PlantOnCell.Draw(gameTime);
            }
        }
    }
}
