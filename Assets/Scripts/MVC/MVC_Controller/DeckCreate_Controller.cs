
using UnityEngine;
using UnityEngine.UI;

namespace Hearthstone
{
    public class DeckCreate_Controller : MonoBehaviour
    {
        private Text _nameDeck;


        private void Start()
        {
            _nameDeck = GetComponentInChildren<Text>();
        }
        private void InputTextChange()
        {

        }
    }

}