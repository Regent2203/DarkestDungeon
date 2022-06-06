using UnityEngine;
using System;
using DarkestDungeon.Battle.Characters;

namespace DarkestDungeon.Battle.BattleActions
{
    /// <summary>
    /// A basic class for actions used by characters in battles
    /// </summary>
    [Serializable]
    public abstract class BattleAction
    {
        public readonly string Name;
        protected readonly Character _owner;

        public event Action OnHideButtons;
        public event Action OnActionCompleted;


        public BattleAction(string name, Character owner)
        {
            Name = name;
            _owner = owner;
        }

        public abstract void OnButtonClick();

        protected void HideButtons()
        {
            OnHideButtons?.Invoke();
        }

        protected void Complete()
        {
            Debug.Log($"{Name} completed ({_owner.name}).", _owner);
            OnActionCompleted?.Invoke();
        }
    }
}
