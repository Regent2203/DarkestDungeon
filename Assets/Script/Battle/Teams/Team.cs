using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using DarkestDungeon.Configs;
using DarkestDungeon.Battle.TurnControllers;
using DarkestDungeon.Battle.Characters;
using System.Linq;

namespace DarkestDungeon.Battle
{
    /// <summary>
    /// Holds a squad of characters that can fight against another team
    /// </summary>
    [Serializable]
    public class Team
    {
        /// <summary>
        /// Name of the team, if needed
        /// </summary>
        [SerializeField]
        private string _title = "";

        [SerializeField]
        private List<int> _characterIds = new List<int>();

        private List<Character> _characters;
        public int Count => _characterIds.Count;
        public bool IsDefeated => (from v in _characters select v.IsAlive).Count() == 0;


        public Team()
        {
            _characters = new List<Character>(_characterIds.Count);
        }
        
        public void CreateCharacters(CharacterConfig _characterConfig, IEnumerator<Vector3> positions, TeamPlacement teamPlacement, TurnController turnController, TargetController targetController)
        {
            int i = 0;

            foreach (int id in _characterIds)
            {
                var charLine = _characterConfig.FindCharacterById(id);
                positions.MoveNext();

                var obj = GameObject.Instantiate(charLine.Prefab, positions.Current, Quaternion.identity, teamPlacement.transform);
                var character = obj.GetComponent<Character>();
                character.Init(++i, this, targetController, teamPlacement.DoMirror);

                _characters.Add(character);
            }

            turnController.FillCharacters(_characters);
        }

        
    }
}