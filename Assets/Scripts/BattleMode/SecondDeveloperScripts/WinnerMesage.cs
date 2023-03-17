using UnityEngine;
using UnityEngine.UI;

namespace Hearthstone
{
    public class WinnerMesage : MonoBehaviour
    {
        [SerializeField] private GameObject _winerEvent;
        [SerializeField] private Text _textWiner;
        [SerializeField] private Image _spriteWiner;

        public void SetWiner(string text, Image sprite)
        {
            _winerEvent.gameObject.SetActive(true);
            _textWiner.text = text;
            _spriteWiner.sprite = sprite.sprite;
        }
    }
}