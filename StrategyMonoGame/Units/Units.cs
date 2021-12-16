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
    public abstract class Units : Microsoft.Xna.Framework.DrawableGameComponent
    {
        /*
         * Это абстрактный класс, описывающий общие характеристики
         * юнитов игры
        */
        protected Byte MoveRange; // Юнитов с шагом за ход больше 255 не предусмотрено
        protected SpriteBatch sbSpriteBatch;
        protected Texture2D UnitTexture;
        protected Vector2 Position;// = new Vector2(150, 30);
        protected Byte UnitState; // Описывает состояние юнита (активен, спит, завершил ход, может ходить)
        protected Cells UnitOnThisCell; // Клетка, на которой стоит юнит
        public Byte UnitType;
        protected Byte MovePoints;
        protected MouseState LastState;
        protected MouseState CurrentState;
        protected Int32 FrameWidth; // Ширина одного кадра
        protected Int32 FrameHeight; // Высота одного кадра
        protected Int32 CurrentFrame; // Текущий кадр
        protected Int32 TotalFrames; // Общее число кадров в спрайте
        protected Int32 CurrentTime; // Время с последней смены кадра
        protected Int32 Period; // Период смены
        protected bool InPosition; // Юнит стоит на одном месте
        protected Int32 TargetX, TargetY;
        protected SoundEffect Sound;
        protected SoundEffect UnitReplaceSound;
        protected bool UnitReplaceSoundIsPlaying;
        public String TexturePath; // Путь к тексутре ресурса

        // Информация о ОП юнита
        public String GetMoveString()
        {
            String[] s = { MovePoints.ToString(), MoveRange.ToString()};
            String ret = String.Join("/", s);
            return ret;
        }

        protected enum UnitStates
        {   // Опимывает различные состояния юнитов
            UNIT_STATE_ACTIVE = 0x0,
            UNIT_CAN_MOVE,
            UNIT_STATE_SLEEPING,
            UNIT_STATE_END_OF_TURN
        }

        public Units(Game game, Int32 nX, Int32 nY, Cells Cell) : base(game)
        {   // При создании юнита он всегда активен
            UnitState = (Byte)UnitStates.UNIT_CAN_MOVE;
            Position.X = nX;
            Position.Y = nY;
            TargetX = nX;
            TargetY = nY;
            UnitOnThisCell = Cell;
            CurrentFrame = 0;
            CurrentTime = 0;
            Period = 200;
            FrameWidth = 52;
            FrameHeight = 52;
            InPosition = true;
            UnitReplaceSound = Game.Content.Load<SoundEffect>("UnitReplace");
            UnitReplaceSoundIsPlaying = false;
        }

        public void SetPosition(int X, int Y, Cells TargetCell, bool nInPosition)
        {
            UnitOnThisCell = TargetCell;
            TargetX = X;
            TargetY = Y;
            InPosition = nInPosition;
        }

        public void StopWalking(bool nInPosition)
        {
            InPosition = nInPosition;
        }

        protected void Animate(GameTime gameTime)
        {
            int InternalPeriod = Period;
            CurrentTime += gameTime.ElapsedGameTime.Milliseconds;
            if (CurrentTime > InternalPeriod)
            {
                CurrentTime -= InternalPeriod;
                CurrentFrame++;
                if (CurrentFrame >= TotalFrames) CurrentFrame = 0;
            }
        }

        public override void Update(GameTime gameTime)
        {
            LastState = CurrentState;
            CurrentState = Mouse.GetState();
            if (CurrentState.MiddleButton == ButtonState.Pressed && LastState.MiddleButton == ButtonState.Released && UnitOnThisCell.CityOnCell == null && Game1.LLMenu.GetDrawableUnit() != null)
            {
                // Узнаем исходную клетку
                Cells ActiveCell = UnitOnThisCell.ThisMap.GetActiveCell();
                int[] ActiveCellInfo = UnitOnThisCell.ThisMap.GetActiveCellIndex();
                // Узнаем целевую
                int[] TargetCellInfo = UnitOnThisCell.ThisMap.GetCellInfo(UnitOnThisCell.CellPosition);
                // Если клетка одна и та же, то в случае с поселенцем: строим город
                // в случае с рабочим - собираем ресурс
                if (ActiveCellInfo[0] == TargetCellInfo[0] && ActiveCellInfo[1] == TargetCellInfo[1] && UnitState != (Byte)UnitStates.UNIT_STATE_END_OF_TURN)
                {
                    ProduceAnAction();
                }
            }
            // Если юнит еще не дошел до нужной позиции, то передвигаем его и анимируем
            if (!InPosition)
            {
                if (!UnitReplaceSoundIsPlaying)
                {
                    //UnitReplaceSound.Play();
                    //UnitReplaceSoundIsPlaying = true;
                }
                Animate(gameTime);
                if (Position.Y < TargetY && Position.X > TargetX)
                {
                    if (Position.X < TargetX)
                        Position.X++;
                    else
                        Position.X--;
                    if (Position.Y < TargetY && Position.X == TargetX)
                        Position.Y++;
                    else if (Position.X == TargetX)
                        Position.Y--; 
                }
                else
                {
                    if (Position.Y < TargetY)
                        Position.Y++;
                    else
                        Position.Y--;
                    if (Position.X < TargetX && Position.Y == TargetY)
                        Position.X++;
                    else if (Position.Y == TargetY)
                        Position.X--;  
                }                              
            }
            // Дошли до нужной позиции, закрепляем
            if (!InPosition && Position.X == TargetX && Position.Y == TargetY)
            {
                InPosition = true;
                //UnitReplaceSoundIsPlaying = false;
            }
            base.Update(gameTime);
        }

        public void MakeOneTurn()
        {
            MovePoints--;
        }

        public bool GetInPositionStatus()
        {
            return InPosition;
        }

        public override void Draw(GameTime gameTime)
        {

        }

        public virtual void ProduceAnAction()
        {
            throw new Exception();
        }

        public Texture2D GetUnit2DTexture()
        {
            return UnitTexture;
        }

        public Byte GetMoveRange()
        {
            return MoveRange;
        }

        public Byte GetMovePoints()
        {
            return MovePoints;
        }

        public void SetRemainingMovePoints(Byte Distance)
        {
            if (Distance > MovePoints)
                MovePoints = 0;
            else
                MovePoints -= Distance;
            if(MovePoints == 0)
                UnitState = (Byte)UnitStates.UNIT_STATE_END_OF_TURN;
        }

        public void RestoreMovePoints()
        {   // Восстанавливаем очки перемещения во время нового хода
            MovePoints = MoveRange;
            UnitState = (Byte)UnitStates.UNIT_CAN_MOVE;
        }
    }
}
