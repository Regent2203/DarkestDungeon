using System;
using DarkestDungeon.Battle.Characters;
using DarkestDungeon.Battle.BattleLoggers;

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
        protected BattleView _battleView;
        protected IBattleLogger _battleLogger => _battleView.BattleLogger;

        public event Action Confirmed;
        public event Action Completed;


        public BattleAction(string name, Character owner, BattleView battleView)
        {
            Name = name;
            _owner = owner;
            _battleView = battleView;
        }

        public abstract void OnButtonClick();

        protected void Confirm()
        {
            Confirmed?.Invoke();
        }

        protected void Complete()
        {
            LogAction();
            Completed?.Invoke();
        }

        public void SetListeners(Action onActionConfirmed, Action onActionCompleted)
        {
            Confirmed = null;
            Completed = null;
            
            Confirmed += onActionConfirmed;
            Completed += onActionCompleted;
        }

        protected virtual void LogAction()
        {
            _battleLogger.AddString($"--{_owner.name} performed action {Name}.");
        }
    }
}
