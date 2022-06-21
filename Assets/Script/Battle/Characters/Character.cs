using UnityEngine;
using System.Collections.Generic;
using DarkestDungeon.Battle.BattleActions;
using DarkestDungeon.Battle.BattleLoggers;
using Spine.Unity;
using Spine;
using AnimationState = Spine.AnimationState;

namespace DarkestDungeon.Battle.Characters
{
    /// <summary>
    /// A basic class for any character that participates in battle.
    /// </summary>
    public class Character : MonoBehaviour
    {        
        [SerializeField]
        private FrameController _frameController = default; //frame is a sprite in child gameobject, drawn above this character, we use boxcollider2d as a rectangle to resize it
        private List<BattleAction> _battleActions = new List<BattleAction>();
        private Team _team;
        private BattleView _battleView;

        private MeshRenderer _meshRenderer;
        private SkeletonAnimation _skelAnim;

        //public Parameters Parameters; //todo parameters later
        public FrameController FrameController => _frameController;
        public List<BattleAction> BattleActions => _battleActions;
        public Team Team => _team;




        private void Awake()
        {
            _skelAnim = GetComponent<SkeletonAnimation>();
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        public void Init(int number, Team team, bool doMirror, BattleView battleView)
        {
            name += $"_{number:00}";
            _meshRenderer.sortingOrder += number;

            if (doMirror)
                transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 1));

            _team = team;
            _battleView = battleView;
            _frameController.PrepareFrames();


            //todo: rework hardcode later, into smth customizable in scene inspector
            //fine for test task
            _battleActions.Add(new BattleAction_SkipTurn("Skip turn", this, _battleView));
            _battleActions.Add(new BattleAction_Attack("Attack", this, _battleView, 20));
        }


        #region 'Animations-related'
        public void PlayAnimation(string animationName, bool loop = false, float timeScale = 1.0f)
        {
            _skelAnim.ClearState();
            _skelAnim.timeScale = timeScale;            
            _skelAnim.AnimationState.SetAnimation(0, animationName, loop);            
        }

        public void PlayAnimation(AnimationState.TrackEntryDelegate onAnimComplete, string animationName, bool loop = false, float timeScale = 1.0f)
        {
            PlayAnimation(animationName, loop, timeScale);
            _skelAnim.AnimationState.Complete += OnAnimationComplete;

            void OnAnimationComplete(TrackEntry trackEntry)
            {
                onAnimComplete(trackEntry);
                _skelAnim.AnimationState.Complete -= OnAnimationComplete;
            }
        }

        public void PlayAnimation_TeleportNearTarget(Character target, AnimationState.TrackEntryDelegate onAnimComplete, string animationName, bool loop = false, float timeScale = 1.0f)
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


            PlayAnimation(OnAnimationComplete, animationName, loop, timeScale);
            

            void OnAnimationComplete(TrackEntry trackEntry)
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
            _battleView.TargetController.ConfirmTarget(this, onSuccess);

            void onSuccess()
            {
                _frameController.HighlightAllowedTarget(false);
            }
        }

        private void OnMouseEnter()
        {
            _battleView.TargetController.CheckTarget(this, onSuccess);

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