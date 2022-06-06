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


        public BattleAction_SkipTurn(string name, Character owner) : base (name, owner)
        {
        }

        public override void OnButtonClick()
        {
            HideButtons();
            Complete();
        }        
    }
}