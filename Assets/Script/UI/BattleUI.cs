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

        private List<Button_BattleAction> _battleActionButtons = new List<Button_BattleAction>();



        public void CreateButtonsForBattleActions(List<BattleAction> battleActions, Action onActionCompleted, Action onHideButtons)
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

                var ba = battleActions[i];
                btnBA.Init(ba.Name, () => { ba.OnButtonClick(); });

                ba.OnHideButtons += HideButtons;
                ba.OnActionCompleted += ActionCompleted;
                

                void HideButtons()
                {
                    onHideButtons();
                    ResetButtonsForBattleActions(battleActions, HideButtons);
                }

                void ActionCompleted()
                {
                    ResetBattleActions(battleActions, ActionCompleted);
                    onActionCompleted();
                }
            }
        }

        private void ResetButtonsForBattleActions(List<BattleAction> battleActions, Action onHideButtons)
        {
            foreach (var ba in battleActions)
                ba.OnHideButtons -= onHideButtons;
            
            foreach (var btnBA in _battleActionButtons)
                btnBA.Reset();
        }
        private void ResetBattleActions(List<BattleAction> battleActions, Action onActionCompleted)
        {
            foreach (var ba in battleActions)
                ba.OnActionCompleted -= onActionCompleted;
        }


        public void CreateStatsPanel()
        {
            //todo
        }
    }
}