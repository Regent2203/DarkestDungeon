using DarkestDungeon.Battle.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DarkestDungeon.Battle.OrderGenerators
{
    /// <summary>
    /// Order will be completely random.
    /// </summary>
    public class OrderGenerator_Random : IOrderGenerator
    {
        public Queue<int> GenerateOrder(List<Character> allCharacters)
        {            
            var ints = new List<int>();
            for (int i = 0; i < allCharacters.Count; i++)
                ints.Add(i);

            var result = new Queue<int>();
            int j;
            while (ints.Count > 0)
            {
                j = Random.Range(0, ints.Count);
                result.Enqueue(ints[j]);
                //Debug.Log($"order: {j},{ints[j]},{ints.Count}");
                ints.RemoveAt(j);
            }
            
            return result;
        }
    }
}