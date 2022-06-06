using UnityEngine;
using System;
using System.Collections.Generic;
using Spine.Unity;
using DarkestDungeon.Battle.BattleActions;
using DarkestDungeon.UI;
using Spine;
using AnimationState = Spine.AnimationState;

namespace DarkestDungeon.Battle.Characters
{
    /// <summary>
    /// A basic class for any character that participates in battle.
    /// </summary>
    public class Character : MonoBehaviour
    {
        //frame is a sprite in child gameobject, drawn above this character, we use boxcollider2d as a rectangle to resize it
        [SerializeField]
        private FrameController _frameController = default;

        private List<BattleAction> _battleActions = new List<BattleAction>();
        private TargetController<Character> _targetController;
        private MeshRenderer _meshRenderer;
        private SkeletonAnimation _skelAnim;
        public Team Team { get; protected set; }


        //todo parameters later
        //public Parameters Parameters { get; protected set; }
        //int _health = 100;
        //int _mana = 0;
        //int _attackPower;
        //int _armor;

        //public bool IsDead => _health <= 0;
        //public bool IsAlive => _health > 0;
        public bool IsAlive => true; //todo, remove later


        private void Awake()
        {
            _skelAnim = GetComponent<SkeletonAnimation>();
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        public void Init(int number, Team team, TargetController<Character> targetController, bool doMirror)
        {
            name += $"_{number:00}"; //for small amounts of concatinations, StringBuilder is not better.
            _meshRenderer.sortingOrder += number;

            if (doMirror)
                transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 1));

            Team = team;
            _targetController = targetController;
            _frameController.PrepareFrames();


            //todo: rework this hardcode later, into smth good and customizable in scene inspector
            //for test task, leaving this way
            _battleActions.Add(new BattleAction_SkipTurn("Skip turn", this));
            _battleActions.Add(new BattleAction_Attack("Attack", this, _targetController, 20));
        }
        
        public void ActivateForBattle(BattleUI battleUI, Action onActionCompleted)
        {
            battleUI.CreateButtonsForBattleActions(_battleActions, onActionCompleted, OnHideButtons);
            _frameController.HighlightCurrentTurn(true);            
            

            void OnHideButtons()
            {
                _frameController.HighlightCurrentTurn(false);
            }
        }

        #region 'Animations-related' //todo: move?
        public void PlayAnimation(string animationName, bool loop = false, float timeScale = 1.0f)
        {
            _skelAnim.ClearState();
            //_skelAnim.AnimationName = animationName;
            //_skelAnim.loop = loop;
            _skelAnim.timeScale = timeScale;            
            _skelAnim.AnimationState.SetAnimation(0, animationName, loop);            
        }

        public void PlayAnimation(AnimationState.TrackEntryDelegate onAnimComplete, string animationName, bool loop = false, float timeScale = 1.0f)
        {
            PlayAnimation(animationName, loop, timeScale);
            _skelAnim.AnimationState.Complete += OnComplete;

            void OnComplete(TrackEntry trackEntry)
            {
                onAnimComplete(trackEntry);
                _skelAnim.AnimationState.Complete -= OnComplete;
            }
        }

        public void PlayAnimationWithTarget(Character target, AnimationState.TrackEntryDelegate onAnimComplete, string animationName, bool loop = false, float timeScale = 1.0f)
        {
            //todo: expand this method for more types of skills and animations (fine for test task)
            const float OFFSET = 4; 
            const int TOFRONTTHIS = 1010; //bring both characters to front
            const int TOFRONTTARG = 1000;
                        
            var startPos = this.transform.position;
            var newPos = target.transform.position + target.transform.localScale.x * OFFSET * new Vector3( 1, 0, 0);

            //teleport character to new pos
            this.AddOrderInLayer(TOFRONTTHIS);
            target.AddOrderInLayer(TOFRONTTARG);
            this.transform.position = newPos;


            PlayAnimation(OnComplete, animationName, loop, timeScale);
            

            void OnComplete(TrackEntry trackEntry)
            {
                onAnimComplete(trackEntry);
                
                //teleport character to old pos
                this.AddOrderInLayer(-TOFRONTTHIS);
                target.AddOrderInLayer(-TOFRONTTARG);
                this.transform.position = startPos;
            }
        }

        public void AddOrderInLayer(int z)
        {
            _meshRenderer.sortingOrder += z;
        }
        #endregion
        #region 'Mouse+Trigger related'
        private void OnMouseDown()
        {
            _targetController.ConfirmTarget(this, onSuccess);

            void onSuccess()
            {
                _frameController.HighlightAllowedTarget(false);
            }
        }

        private void OnMouseEnter()
        {
            _targetController.CheckTarget(this, onSuccess);

            void onSuccess()
            {
                _frameController.HighlightAllowedTarget(true);
            }
        }

        private void OnMouseExit()
        {            
            _frameController.HighlightAllowedTarget(false);
        }
        #endregion
    }
}