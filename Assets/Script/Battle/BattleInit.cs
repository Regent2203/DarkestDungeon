using UnityEngine;
using DarkestDungeon.Configs;

namespace DarkestDungeon.Battle
{
    /// <summary>
    /// Here we assign characters for both teams.
    /// Also works as entry point in tesk task
    /// </summary>
    public class BattleInit : MonoBehaviour
    {
        [SerializeField]
        private BattleView _battleView = default;
        [SerializeField]
        private CharacterConfig _characterConfig = default;

        //note: decided not to support more than two opposing sides (teams) in this game; thus, we strictly define Left and Right teams.
        [SerializeField]
        private Team _leftTeam = new Team();
        [SerializeField]
        private Team _rightTeam = new Team();


        private void Awake()
        {
            _battleView.CreateCharacters(_characterConfig, _leftTeam, _rightTeam);            
        }
    }
}