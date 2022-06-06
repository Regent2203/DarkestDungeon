using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DarkestDungeon.Battle
{
    /// <summary>
    /// Use two Transforms (gameobjects) on scene to define a line which will be used to place team's characters on.
    /// </summary>
    public class TeamPlacement : MonoBehaviour
    {
        //for rightTeam, we mirrorX the characters (so that both teams look at each other/at center)
        [SerializeField]
        private bool _doMirror = default;
        public bool DoMirror => _doMirror;

        //Two borders, between which characters are placed at equal distances
        [SerializeField]
        private Transform _leftBorder = default;
        [SerializeField]
        private Transform _rightBorder = default;


        private Vector3 _leftPos => _leftBorder.position;
        private Vector3 _rightPos => _rightBorder.position;


        public IEnumerator<Vector3> GetPositions(int count)
        {
            if (count == 0)
                yield break;
            else
            {
                int i = 0;

                var step = new Vector3((_rightPos.x - _leftPos.x) / (count + 1), (_rightPos.y - _leftPos.y) / (count + 1), 0);
                var position = _leftPos + step;

                while (i < count)
                {
                    yield return position;
                    position += step;
                    i++;
                }
            }
        }

        void OnDrawGizmos()
        {
            if (_leftBorder == null || _rightBorder == null)
                return;

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(_leftPos, _rightPos);            
        }
    }
}