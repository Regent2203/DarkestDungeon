using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

namespace DarkestDungeon.Battle.Characters
{
    [Serializable]
    public class FrameController
    {
        [SerializeField]
        private BoxCollider2D _frameCollider = default;

        [SerializeField]
        private SpriteRenderer _frameCurrentTurn = default;
        [SerializeField]
        private SpriteRenderer _frameAllowedTarget = default;

        public void PrepareFrames()
        {
            PrepareFrameSize(_frameCurrentTurn);
            PrepareFrameSize(_frameAllowedTarget);
        }

        /// <summary>
        /// Resize the sprite frameSprite to match boxcollider2d
        /// </summary>
        private void PrepareFrameSize(SpriteRenderer frame)
        {
            //for non-sliced sprite
            /*
            var sprSize = frame.sprite.rect.size / frame.sprite.pixelsPerUnit;
            var collSize = _frameCollider.size;

            frame.transform.localScale = new Vector3(collSize.x / sprSize.x, collSize.y / sprSize.y, 1);
            frame.transform.localPosition = (Vector3)_frameCollider.offset;
            */

            //for sliced sprite
            frame.size = _frameCollider.size;
            frame.transform.localPosition = (Vector3)_frameCollider.offset;
        }

        public void HighlightCurrentTurn(bool b) //highlight acting character
        {
            if (_frameCurrentTurn.enabled != b)
                _frameCurrentTurn.enabled = b;
        }
        public void HighlightAllowedTarget(bool b) //highlight on mouseover
        {
            if (_frameAllowedTarget.enabled != b)
                _frameAllowedTarget.enabled = b;
        }
    }
}