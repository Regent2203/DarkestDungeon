using DarkestDungeon.Battle.Characters;
using DarkestDungeon.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DarkestDungeon.Battle.TurnControllers
{
    /// <summary>
    /// Controls activity of characters by generated turn order
    /// </summary>
    public class TurnController
    {
        private int _currentTurn;
        private List<Character> _allCharacters = new List<Character>();

        private bool _doRefreshOrder = false;
        private Queue<int> _order;
        private Queue<int> _orderCopy;
        private IOrderTool _orderTool; //todo: may be replaced during same battle in corresponding situations/conditions

        //private IBattleLogger _battleLogger;
        private BattleUI _battleUI;


        public TurnController(BattleUI battleUI, IOrderTool orderTool, bool doRefreshOrder)
        {
            _orderTool = orderTool;
            _doRefreshOrder = doRefreshOrder;
            _battleUI = battleUI;
        }

        public void FillCharacters(List<Character> list)
        {
            _allCharacters.AddRange(list);
        }

        private Queue<int> GetOrder()
        {            
            if (_doRefreshOrder)
            {
                return _orderTool.GenerateOrder(_allCharacters);
            }                
            else
            {
                if (_orderCopy == null)
                {
                    _orderCopy = _orderTool.GenerateOrder(_allCharacters);
                }
                return new Queue<int>(_orderCopy);
            }                
        }


        public void StartBattle()
        {
            Debug.Log($"Battle started.");
            _currentTurn = 0;
            if (!_doRefreshOrder)
                _order = _orderTool.GenerateOrder(_allCharacters);

            NextTurn();
        }

        public void NextTurn()
        {
            _currentTurn++;
            _order = GetOrder();

            Debug.Log($"New turn {_currentTurn}.");
            ActivateNextCharacter();
        }

        private void ActivateNextCharacter()
        {
            if (_order.Count > 0)
            {
                var nextInd = _order.Dequeue();
                var character = _allCharacters[nextInd];
                if (character.IsAlive)
                {
                    character.ActivateForBattle(_battleUI, ActivateNextCharacter);
                }
                else
                {
                    ActivateNextCharacter();
                }
            }
            else
                CompleteTurn();            
        }
       

        public void CompleteTurn()
        {
            Debug.Log($"End of turn {_currentTurn}.");            
            NextTurn();

            //Debug.Log($"End of battle.");            
        }
    }
}