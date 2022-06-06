using System.Collections;
using System.Collections.Generic;

namespace DarkestDungeon.Battle
{
    /// <summary>
    /// Logs turn's number and characters' actions, like in Heroes of Might and Magic III
    /// </summary>
    public class BattleLogger //todo...
    {
        LinkedList<string> _log = new LinkedList<string>();
        
        public void AddString(string str)
        {
            _log.AddLast(str);
        }
    }
}