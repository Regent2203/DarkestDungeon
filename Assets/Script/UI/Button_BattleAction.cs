using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DarkestDungeon.UI
{
    public class Button_BattleAction : MonoBehaviour
    {
        [SerializeField]
        private Button _button = default;
        [SerializeField]
        private Text _text = default;


        public void Init(string caption, UnityAction call)
        {
            _text.text = caption;            
            _button.onClick.AddListener(call);
            gameObject.SetActive(true);
        }

        public void Reset()
        {
            _text.text = "";
            _button.onClick.RemoveAllListeners();
            gameObject.SetActive(false);
        }
    }
}