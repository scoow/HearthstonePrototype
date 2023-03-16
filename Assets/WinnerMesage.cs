using UnityEngine;
using UnityEngine.UI;

namespace Hearthstone
{
    public class WinnerMesage : MonoBehaviour
    {
        public GameObject _winerEvent;
        public Text _textWiner;
        public Image _spriteWiner;

        public void SetWiner(string text, Image sprite)
        {
            _winerEvent.gameObject.SetActive(true);
            _textWiner.text = text;
            _spriteWiner.sprite = sprite.sprite;
        }
    }
}