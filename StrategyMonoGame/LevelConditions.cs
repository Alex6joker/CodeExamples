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
    class LevelConditions
    {
        //Game1 game; // Игра, для которой проверяются критерии
        bool ConditionState;
        Int32 Level;
        Int32 StartTurn;
        bool IsActive;
        public static String Target;

        public LevelConditions(Game1 ngame, Int32 nLevel, Int32 nStartTurn)
        {
            //game = ngame;
            ConditionState = false;
            Level = nLevel;
            StartTurn = nStartTurn;
            if (Level == 1)
                IsActive = true;
            else
                IsActive = false;
            Target = "";
        }

        public void Update(GameTime gameTime)
        {
            switch(Level)
            {
                case(1) :
                {
                    Target = "Вступить в античность за 15 ходов";
                    if (IsActive && Game1.Turn > 15 && Game1.ThisGame.GetTechTree().CurrentAge < 1)
                    {
                        IsActive = false;
                        ConditionState = true;
                    }                        
                    else if (IsActive && Game1.ThisGame.GetTechTree().CurrentAge >= 1)
                        IsActive = false;
                    break;
                }
                case(2) :
                {
                    int TurnsLeft = 25 - (Game1.Turn - StartTurn);
                    if (TurnsLeft < 0)
                        TurnsLeft = 0;
                    String[] s = {"Изучить Технологию будущего за оставшиеся ", TurnsLeft.ToString(), " ходов и накопить 300 золота"};
                    Target = String.Join(null, s);
                    if (IsActive && Game1.ThisGame.GetTechTree().Branchs[5].TechsList[0].Investigated && Game1.Turn - StartTurn < 25 && Game1.Gold >= 300)
                    {
                        IsActive = false;                        
                    }
                    else if (IsActive && Game1.Turn - StartTurn > 25 && (!Game1.ThisGame.GetTechTree().Branchs[5].TechsList[0].Investigated || Game1.Gold < 300))
                    {
                        IsActive = false;
                        ConditionState = true;
                    }  
                    break;
                }
            }

        }

        public void GoToNewLevel(Int32 nLevel, Int32 nStartTurn)
        {
            IsActive = true;
            Level = nLevel;            
            ConditionState = false;
            StartTurn = nStartTurn;
        }

        public bool GetConditionState()
        {
            return ConditionState;
        }

        public bool GetActiveState()
        {
            return IsActive;
        }
    }
}
