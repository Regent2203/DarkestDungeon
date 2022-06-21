using DarkestDungeon.Battle.BattleLoggers;
using DarkestDungeon.Battle.Characters;
using DarkestDungeon.Battle.OrderGenerators;
using System.Collections.Generic;

namespace DarkestDungeon.Battle
{
    public class BattleModel : IBattleModel
    {
        private ITurnController _turnController;
        private BattleView _battleView;


        public BattleModel(List<Character> characters, BattleView battleView)
        {
            _battleView = battleView;

            //var orderGenerator = new OrderGenerator_Left2Right();
            var orderGenerator = new OrderGenerator_Random(); //in test task - acting order must be random

            _turnController = new TurnController(orderGenerator, false, characters.Count, _battleView);
        }

        public void StartBattle()
        {
            _turnController.StartBattle();
        }
    }
}