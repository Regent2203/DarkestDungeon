using DarkestDungeon.Battle.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DarkestDungeon.Battle.TurnControllers
{
    /// <summary>
    /// Defines in which order characters in battle perform actions (one after another)
    /// </summary>
    public interface IOrderTool
    {
        Queue<int> GenerateOrder(List<Character> allCharacters);
    }
}