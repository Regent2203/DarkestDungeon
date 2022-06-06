using System;
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
        protected int _damage;
        //protected int _damage => _owner._attackPower * 2; //todo rework if we wanna use character's attackpower with dynamic update

        private TargetController _targetController;


        public BattleAction_Attack(string name, Character owner, TargetController targetController, int damage) : base (name, owner)
        {
            _damage = damage;
            _targetController = targetController;
        }
        
        public override void OnButtonClick()
        {
            _targetController.Prepare( (x) => _owner.Team != x.Team, StartAnim);


            void StartAnim(Character target)
            {
                HideButtons();

                _owner.PlayAnimationWithTarget(target, OnAnimCompleteOwner, "Miner_1"); //todo: fix hardcode (animation name)                
                target.PlayAnimation(OnAnimCompleteTarget, "Damage", false, 0.8f); //todo: fix hardcode (animation name)


                void OnAnimCompleteOwner(TrackEntry trackEntry)
                {
                    _owner.PlayAnimation("Idle", true); //todo: fix hardcode (animation name)
                    Complete();
                }

                void OnAnimCompleteTarget(TrackEntry trackEntry)
                {
                    target.PlayAnimation("Idle", true); //todo: fix hardcode (animation name)
                    Complete();
                }
            }
        }


        /*
        private void PlayIdleAnimation()
        {
            _owner.PlayAnimation("Idle", true);
        }
        */
    }
}