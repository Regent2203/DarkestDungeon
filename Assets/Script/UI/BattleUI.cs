using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using DarkestDungeon.Battle.BattleActions;

namespace DarkestDungeon.UI
{
    public class BattleUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject _battleActionBtnPrefab = default;
        [SerializeField]
        private Transform _battleActionButtonsHolder = default;

        private List<Button_BattleAction> _battleActionButtons = new List<Button_BattleAction>(); //object pool for UI buttons


        public void CreateButtonsForBattleActions(List<BattleAction> battleActions, Action onActionConfirmed, Action onActionCompleted)
        {
            for (int i = 0; i < battleActions.Count; i++)
            {
                Button_BattleAction btnBA;
                if (i <= _battleActionButtons.Count - 1)
                {
                    btnBA = _battleActionButtons[i];
                }
                else
                {
                    btnBA = Instantiate(_battleActionBtnPrefab, _battleActionButtonsHolder).GetComponent<Button_BattleAction>();
                    _battleActionButtons.Add(btnBA);
                }


                var battleAction = battleActions[i];
                btnBA.Init(battleAction.Name, () => { battleAction.OnButtonClick(); });
                battleAction.SetListeners(onActionConfirmed, onActionCompleted);
            }
        }

        public void ResetButtonsForBattleActions()
        {            
            foreach (var btnBA in _battleActionButtons)
                btnBA.Reset();
        }
    }
}