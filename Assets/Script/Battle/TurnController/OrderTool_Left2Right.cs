using DarkestDungeon.Battle.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DarkestDungeon.Battle.TurnControllers
{
    public class OrderTool_Left2Right : IOrderTool
    {
        public Queue<int> GenerateOrder(List<Character> allCharacters)
        {
            var result = new Queue<int>();
            for (int i = 0; i < allCharacters.Count; i++)            
                result.Enqueue(i);

            return result;
        }
    }
}