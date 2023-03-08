using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Hearthstone
{
    public class Mana_View : MonoBehaviour
    {
        [SerializeField] Text _textCrystalOnePlayer;
        [SerializeField] Text _textCrystalTwoPlayer;
        [Inject]
        private Mana_Controller _controller;

        private void OnEnable()
        {
            _textCrystalOnePlayer.text = "1/1";
            _textCrystalTwoPlayer.text = "0/0";
            _controller.ChangeManaValue += UpdateTextCrystalsValue;
        }

        private void OnDisable()
        {
            _controller.ChangeManaValue -= UpdateTextCrystalsValue;
        }

        private void UpdateTextCrystalsValue(int mana, int crystals)
        {
            if (_controller._playersTurn == Players.First)
            {
                _textCrystalOnePlayer.text = mana + "/" + crystals.ToString();
            }
            if (_controller._playersTurn == Players.Second)
            {
                _textCrystalTwoPlayer.text = mana + "/" + crystals.ToString(); 
            }
        }
    }
}