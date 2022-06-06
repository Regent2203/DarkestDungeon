using DarkestDungeon.Battle.Characters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DarkestDungeon.Battle
{
    /// <summary>
    /// Works with characters which we mouseover/click on, can highlight or set as confirmed target for battle action
    /// </summary>
    public class TargetController
    {
        private Predicate<Character> _condition;
        private Action<Character> _onConfirmTarget;


        public void Prepare(Predicate<Character> predicate, Action<Character> onConfirmTarget)
        {
            _condition = predicate;
            _onConfirmTarget = onConfirmTarget;
        }
        private void Clear()
        {
            _condition = null;
            _onConfirmTarget = null;
        }

        public void CheckTarget(Character target, Action onSuccess)
        {
            if (_condition == null)
                return;

            if (_condition(target))
                onSuccess();
        }

        public void ConfirmTarget(Character target, Action onSuccess)
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
        