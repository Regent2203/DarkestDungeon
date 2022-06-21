﻿using DarkestDungeon.Battle.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DarkestDungeon.Battle.OrderGenerators
{
    /// <summary>
    /// Order is from left to right, one by one.
    /// </summary>
    public class OrderGenerator_Left2Right : IOrderGenerator
    {
        public Queue<int> GenerateOrder(int objectsCount)
        {
            var result = new Queue<int>();
            for (int i = 0; i < objectsCount; i++)
                result.Enqueue(i);

            return result;
        }
    }
}