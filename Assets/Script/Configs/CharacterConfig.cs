using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

namespace DarkestDungeon.Configs
{
    /// <summary>
    /// Contains configuration for every possible character (unit) in this game.
    /// </summary>    
    public class CharacterConfig : ScriptableObject
    {
        [SerializeField]
        private List<CharacterLine> _characters = new List<CharacterLine>();
        

        public CharacterLine FindCharacterById(int id)
        {
            var results = from c in _characters where c.Id == id select c;

            if (results.Count() > 1)
                Debug.LogError($"Duplicate id {id} found in CharactersConfig. Character was not created.");
            else if (results.Count() == 0)
                Debug.LogError($"Id {id} not found in CharactersConfig. Character was not created.");
            
            return results.Single(); //raises exception if config is incorrect
        }


        [Serializable]
        public struct CharacterLine
        {
            public int Id;
            public GameObject Prefab;
            public string Name;
            public string Description;
        }
    }
}