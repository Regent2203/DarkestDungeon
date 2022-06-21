using DarkestDungeon.Battle.Characters;
using DarkestDungeon.Battle.OrderGenerators;
using System.Collections;
using System.Collections.Generic;

namespace DarkestDungeon.Battle
{
    /// <summary>
    /// Works with order generators
    /// </summary>
    public class OrderController : IOrderController
    {
        private Queue<int> _order;
        private int _objectsCount;

        private bool _doRefreshOrder = false; //when false, the order will be the same every turn
        private Queue<int> _orderCopy; //used when doRefreshOrder == false
        private IOrderGenerator _orderGenerator;


        public OrderController(IOrderGenerator orderGenerator, bool doRefreshOrder, int objectsCount)
        {
            _orderGenerator = orderGenerator;
            _doRefreshOrder = doRefreshOrder;
            _objectsCount = objectsCount;
        }

        public bool GetNextIndex(out int next)
        {
            next = -1;

            if (_order.Count > 0)
            {
                next = _order.Dequeue();
                return true;
            }
            else
                return false;
        }

        public void CreateOrder()
        {
            if (_doRefreshOrder)
            {
                _order = _orderGenerator.GenerateOrder(_objectsCount);
            }
            else
            {
                if (_orderCopy == null)
                {
                    _orderCopy = _orderGenerator.GenerateOrder(_objectsCount);
                }
                _order = new Queue<int>(_orderCopy);
            }
        }
    }
}