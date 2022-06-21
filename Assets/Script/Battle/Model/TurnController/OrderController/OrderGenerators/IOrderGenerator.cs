using DarkestDungeon.Battle.Characters;
using System.Collections;
using System.Collections.Generic;

namespace DarkestDungeon.Battle.OrderGenerators
{
    /// <summary>
    /// Defines in which order characters in battle perform actions (one after another)
    /// </summary>
    public interface IOrderGenerator
    {
        Queue<int> GenerateOrder(int objectsCount);
    }
}