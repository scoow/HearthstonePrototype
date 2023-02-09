using Hearthstone;
using UnityEngine;
using UnityEngine.UI;

public class Mana_View : MonoBehaviour
{
    [SerializeField] Text _textManaOnePlayer;
    [SerializeField] Text _textManaTwoPlayer;
    private Mana_Controller _controller;

    private void OnEnable()
    {
        _textManaOnePlayer.text = "1";
        _textManaTwoPlayer.text = "0";
        _controller = GetComponent<Mana_Controller>();
        _controller.ChangeManaValue += UpdateTextManaValue;
    }

    private void OnDisable()
    {
        _controller.ChangeManaValue -= UpdateTextManaValue;
    }

    private void UpdateTextManaValue(int value)
    {
        if(_controller._playersTurn == Players.First) _textManaOnePlayer.text = value.ToString();
        if(_controller._playersTurn == Players.Second) _textManaTwoPlayer.text = value.ToString();
    }
}
