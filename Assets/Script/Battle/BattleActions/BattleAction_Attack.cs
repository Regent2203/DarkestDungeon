using System;
using DarkestDungeon.Battle.BattleLoggers;
using DarkestDungeon.Battle.Characters;
using Spine;

namespace DarkestDungeon.Battle.BattleActions
{
    /// <summary>
    /// Battle action: attack an enemy
    /// </summary>
    [Serializable]
    public class BattleAction_Attack : BattleAction
    {
        protected int _damage; //todo
        //protected int _damage => _owner._attackPower * 2; //todo rework if we wanna use character's attackpower (dynamically updated)


        public BattleAction_Attack(string name, Character owner, BattleView battleView, int damage) : base (name, owner, battleView)
        {
            _damage = damage;
            _battleView = battleView;
        }
        
        public override void OnButtonClick()
        {
            _battleView.TargetController.Prepare( (x) => _owner.Team != x.Team, StartAnimation);

            
            void StartAnimation(Character target) //todo in future: fix hardcoded animation name
            {
                Confirm();

                _owner.PlayAnimation_TeleportNearTarget(target, OnAnimCompleteOwner, "Miner_1");
                target.PlayAnimation(OnAnimCompleteTarget, "Damage", false, 0.8f);


                void OnAnimCompleteOwner(TrackEntry trackEntry)
                {
                    _owner.PlayAnimation("Idle", true);
                    Complete();
                }

                void OnAnimCompleteTarget(TrackEntry trackEntry)
                {
                    target.PlayAnimation("Idle", true);
                }
            }
        }
    }
}