using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace DarkestDungeon.Battle.BattleLoggers
{
    /// <summary>
    /// Logs turn's number and characters' actions onto UI, like in Heroes of Might and Magic III
    /// </summary>
    public class BattleLogger_UI : IBattleLogger
    {
        private Text _text;
        private List<string> _log = new List<string>();

        /// <summary>
        /// index of the first string in log that we show in UI
        /// </summary>
        private int _showIndex = 0;
        /// <summary>
        /// quantity of strings from log that we show in UI
        /// </summary>
        private int _showLength;


        public BattleLogger_UI(Text component, Button btnUp, Button btnDown, int showLength = 5)
        {
            _text = component;
            _showLength = showLength;

            btnUp?.onClick.AddListener  ( () => { _showIndex = Mathf.Max(0, _showIndex - 1); UpdateText(); } );
            btnDown?.onClick.AddListener( () => { _showIndex = Mathf.Min(_showIndex + 1, _log.Count - _showLength); UpdateText(); });
        }

        public void AddString(string str)
        {
            _log.Add(str+"\n");
            if (_showIndex == _log.Count - 1 - _showLength) //автопрокрутка если на самой нижней позиции
                _showIndex++;
            UpdateText();
        }

        private void UpdateText()
        {
            var sb = new StringBuilder();

            var iMax = Mathf.Min(_showIndex + _showLength, _log.Count);
            for (int i = _showIndex; i < iMax; i++)
            {
                sb.Append(_log[i]);
            }

            _text.text = sb.ToString();
        }
    }
}