using DarkestDungeon.Battle.Characters;
using System;

namespace DarkestDungeon.Battle
{
    /// <summary>
    /// Works with characters which we mouseover/click on, can highlight or set as confirmed target for battle action
    /// </summary>
    public class TargetController<T>
    {
        private Predicate<T> _condition;
        private Action<T> _onConfirmTarget;


        public void Prepare(Predicate<T> predicate, Action<T> onConfirmTarget)
        {
            _condition = predicate;
            _onConfirmTarget = onConfirmTarget;
        }
        private void Clear()
        {
            _condition = null;
            _onConfirmTarget = null;
        }

        public void CheckTarget(T target, Action onSuccess)
        {
            if (_condition == null)
                return;

            if (_condition(target))
                onSuccess();
        }

        public void ConfirmTarget(T target, Action onSuccess)
        {
            if (_condition == null)
                return;

            if (_condition(target))
            {
                onSuccess();
                _onConfirmTarget(target);
                Clear();
            }
        }
    }
}
        