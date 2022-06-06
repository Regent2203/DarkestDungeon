using DarkestDungeon.Battle.Characters;
using DarkestDungeon.Battle.OrderGenerators;
using DarkestDungeon.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DarkestDungeon.Battle
{
    /// <summary>
    /// Controls activity of characters by generated turn order
    /// </summary>
    public class TurnController
    {
        private int _currentTurn;
        private Queue<int> _order;
        private List<Character> _allCharacters = new List<Character>();
                
        private OrderController _orderController;
        //private IBattleLogger _battleLogger;
        private BattleUI _battleUI;


        public TurnController(BattleUI battleUI, IOrderGenerator orderGenerator, bool doRefreshOrder)
        {
            _battleUI = battleUI;
            _orderController = new OrderController(orderGenerator, doRefreshOrder, _allCharacters);
        }

        public void FillCharacters(List<Character> list)
        {
            _allCharacters.AddRange(list);
        }


        public void StartBattle()
        {
            Debug.Log($"Battle started.");
            _currentTurn = 0;
            
            NextTurn();
        }

        private void NextTurn()
        {
            _currentTurn++;
            _order = _orderController.GetOrder();

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
       

        private void CompleteTurn()
        {
            Debug.Log($"End of turn {_currentTurn}.");            
            NextTurn();

            //Debug.Log($"End of battle.");            
        }
    }
}