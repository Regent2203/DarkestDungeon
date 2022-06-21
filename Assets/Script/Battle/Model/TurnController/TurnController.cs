using DarkestDungeon.Battle.BattleLoggers;
using DarkestDungeon.Battle.OrderGenerators;

namespace DarkestDungeon.Battle
{
    /// <summary>
    /// Controls activity of characters by generated turn order
    /// </summary>
    public class TurnController : ITurnController
    {
        private int _currentTurn;
        private IOrderController _orderController;
        private IBattleLogger _battleLogger => _battleView.BattleLogger;
        private BattleView _battleView;

        public TurnController(IOrderGenerator orderGenerator, bool doRefreshOrder, int charactersCount, BattleView battleView)
        {
            _orderController = new OrderController(orderGenerator, doRefreshOrder, charactersCount);
            _battleView = battleView;
        }

        public void StartBattle()
        {
            _battleLogger.AddString($"Start of battle.");
            _currentTurn = 0;
            
            NextTurn();
        }

        /*
        private void EndBattle()
        {
            _battleLogger.AddString($"End of battle.");
        }
        */

        private void NextTurn()
        {
            _currentTurn++;
            _orderController.CreateOrder();
            _battleLogger.AddString($"New turn {_currentTurn}.");

            ActivateNextCharacter();
        }

        private void ActivateNextCharacter()
        {
            if (_orderController.GetNextIndex(out var index))
            {
                _battleView.SetCharacterActive(index, ActivateNextCharacter);
            }                
            else
                CompleteTurn();
        }       

        private void CompleteTurn()
        {
            _battleLogger.AddString($"End of turn {_currentTurn}.");            
            NextTurn();
        }
    }
}