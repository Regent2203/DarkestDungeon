using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using DarkestDungeon.Configs;
using DarkestDungeon.UI;
using DarkestDungeon.Battle.Characters;
using DarkestDungeon.Battle.BattleLoggers;

namespace DarkestDungeon.Battle
{
    public class BattleView : MonoBehaviour
    {
        [SerializeField]
        private BattleUI _battleUI = default;

        [SerializeField]
        private TeamPlacement _leftTeamPlacement = default;
        [SerializeField]
        private TeamPlacement _rightTeamPlacement = default;

        [Header("BattleLogger_UI")]
        [SerializeField]
        private Text _loggerText = default;
        [SerializeField]
        private Button _loggerBtnUp = default;
        [SerializeField]
        private Button _loggerBtnDown = default;


        private List<Character> _characters;
        private Character _currentCharacter;
        private IBattleModel _battleModel;
        public IBattleLogger BattleLogger { get; protected set; }
        public TargetController<Character> TargetController { get; protected set; }


        private void Start()
        {
            //BattleLogger  = new BattleLogger_UnityDebug();
            BattleLogger = new BattleLogger_UI(_loggerText, _loggerBtnUp, _loggerBtnDown);

            _battleModel = new BattleModel(_characters, this);

            TargetController = new TargetController<Character>();

            //for tesk task, we start battle immediately
            _battleModel.StartBattle();
        }

        public void CreateCharacters(CharacterConfig characterConfig, Team leftTeam, Team rightTeam)
        {
            var charactersLeft = CreateCharactersForTeam(leftTeam, _leftTeamPlacement);
            var charactersRight = CreateCharactersForTeam(rightTeam, _rightTeamPlacement);
            
            _characters = new List<Character>();
            _characters.AddRange(charactersLeft);
            _characters.AddRange(charactersRight);


            List<Character> CreateCharactersForTeam(Team team, TeamPlacement teamPlacement)
            {                
                return team.InstantiateCharacters(characterConfig, teamPlacement, this);
            }
        }

        public void SetCharacterActive(int index, Action callback)
        {
            _currentCharacter = _characters[index];

            _currentCharacter.FrameController.HighlightCurrentTurn(true);
            _battleUI.CreateButtonsForBattleActions(_currentCharacter.BattleActions, OnActionConfirmed, OnActionCompleted);


            void OnActionConfirmed()
            {
                _currentCharacter.FrameController.HighlightCurrentTurn(false);
                _battleUI.ResetButtonsForBattleActions();                
            }
            void OnActionCompleted()
            {
                callback();
            }
        }
    }
}