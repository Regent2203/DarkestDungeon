using System;
using DarkestDungeon.Battle.Characters;

namespace DarkestDungeon.Battle.BattleActions
{
    /// <summary>
    /// Battle action: skip turn
    /// </summary>
    [Serializable]
    public class BattleAction_SkipTurn : BattleAction
    {
        //no parameters, nothing to do here


        public BattleAction_SkipTurn(string name, Character owner, BattleView battleView) : base (name, owner, battleView)
        {
        }

        public override void OnButtonClick()
        {
            Confirm();
            Complete();
        }        
    }
}