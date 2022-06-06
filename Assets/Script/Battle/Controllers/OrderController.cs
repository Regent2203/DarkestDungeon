using DarkestDungeon.Battle.Characters;
using DarkestDungeon.Battle.OrderGenerators;
using DarkestDungeon.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DarkestDungeon.Battle
{
    /// <summary>
    /// Works with order generators
    /// </summary>
    public class OrderController
    {
        private bool _doRefreshOrder = false;        
        private Queue<int> _orderCopy;
        private IOrderGenerator _orderGenerator; //todo: may be replaced during same battle in corresponding situations/conditions
        private List<Character> _allCharacters;


        public OrderController(IOrderGenerator orderGenerator, bool doRefreshOrder, List<Character> allCharacters)
        {
            _orderGenerator = orderGenerator;
            _doRefreshOrder = doRefreshOrder;
            _allCharacters = allCharacters;
        }

        public Queue<int> GetOrder()
        {
            if (_doRefreshOrder)
            {
                return _orderGenerator.GenerateOrder(_allCharacters);
            }
            else
            {
                if (_orderCopy == null)
                {
                    _orderCopy = _orderGenerator.GenerateOrder(_allCharacters);
                }
                return new Queue<int>(_orderCopy);
            }
        }
    }
}