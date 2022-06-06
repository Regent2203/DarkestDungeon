using UnityEngine;
using DarkestDungeon.Configs;
using DarkestDungeon.UI;
using DarkestDungeon.Battle.OrderGenerators;
using DarkestDungeon.Battle.Characters;

namespace DarkestDungeon.Battle
{
    /// <summary>
    /// Here we assign characters for both teams.
    /// In this test example works as an entry point.
    /// </summary>
    public class BattleInit : MonoBehaviour
    {
        [SerializeField]
        private CharacterConfig _characterConfig = default; //access to this object can be implemented in many different ways in larger projects; for simplicity in test task, defined directly via inspector                                                            

        [SerializeField]
        private BattleUI _battleUI = default;

        //note: decided not to support more than two opposing sides (teams) in this game; thus, we strictly define Left and Right teams.
        [SerializeField]
        private Team _leftTeam = new Team();
        [SerializeField]
        private Team _rightTeam = new Team();
        [SerializeField]
        private TeamPlacement _leftTeamPlacement = default;
        [SerializeField]
        private TeamPlacement _rightTeamPlacement = default;

        private TurnController _turnController;
        private TargetController<Character> _targetController;


        void Start()
        {
            /*
            if (_characterConfig == null)
            {
                Debug.LogError($"{nameof(_characterConfig)} is unassigned!", this);
                return;
            }
            */

            CreateControllers();
            CreateCharacters();            
            StartBattle();
        }

        private void CreateControllers()
        {
            //_turnController = new TurnController(_battleUI, new OrderTool_Left2Right(), false);
            _turnController = new TurnController(_battleUI, new OrderGenerator_Random(), false); //in test task - acting order must be random
            _targetController = new TargetController<Character>();
        }

        private void CreateCharacters()
        {
            CreateCharactersForTeam(_leftTeam, _leftTeamPlacement);
            CreateCharactersForTeam(_rightTeam, _rightTeamPlacement);


            void CreateCharactersForTeam(Team team, TeamPlacement teamPlacement)
            {
                var positions = teamPlacement.GetPositions(team.Count);
                team.CreateCharacters(_characterConfig, positions, teamPlacement, _turnController, _targetController);
            }
        }        

        private void StartBattle()
        {
            _turnController.StartBattle();
        }
    }
}