using UnityEngine;

namespace DarkestDungeon.Battle.BattleLoggers
{
    /// <summary>
    /// Simply uses Debug.Log()
    /// </summary>
    public class BattleLogger_UnityDebug : IBattleLogger
    {
        public void AddString(string str)
        {
            Debug.Log(str);
        }
    }
}