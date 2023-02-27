using System;
using UnityEngine;
using UnityEngine.UI;

namespace Hearthstone
{
    public class Mana_Controller : MonoBehaviour, IGrowing
    {
        [SerializeField]
        private ManaCristal _manaCristalPrefab;
        [SerializeField]
        private Transform _onePlayerMana, _twoPlayerMana;
        public Players _playersTurn;
        public Action<int> ChangeManaValue;
        private EndTurnButton _endTurnButton;//кнопка конца хода


        [HideInInspector] public int _onePlayermanaCount;
        [HideInInspector] public int _twoPlayermanaCount;

        private int _playermanaMaxValue = 10;

        private void Start()
        {
            _endTurnButton = FindObjectOfType<EndTurnButton>();
            _endTurnButton.GetComponent<Button>().onClick.AddListener(ChangeTurn);
            _playersTurn = Players.First;
            AddCristal(_playersTurn);
        }
        public Players WhoMovesNow()
        {
            return _playersTurn;
        }

        public void ChangeTurn()
        {
            if (_playersTurn == Players.First)
            {
                _playersTurn = Players.Second;
                AddCristal(_playersTurn);
                ChangeManaValue?.Invoke(_twoPlayermanaCount);
                return;
            }

            if (_playersTurn == Players.Second)
            {
                _playersTurn = Players.First;
                AddCristal(_playersTurn);
                ChangeManaValue?.Invoke(_onePlayermanaCount);
                return;
            }
        }

        private void AddCristal(Players playersTurn)
        {
            switch (playersTurn)
            {
                case Players.First: CreateCristal(_onePlayerMana, ref _onePlayermanaCount); break;
                case Players.Second: CreateCristal(_twoPlayerMana, ref _twoPlayermanaCount); break;
            }
        }

        private void CreateCristal(Transform parent, ref int manaCounter)
        {
            if (manaCounter < _playermanaMaxValue)
            {
                manaCounter++;
                ManaCristal cristal = Instantiate(_manaCristalPrefab, parent);
            }
        }
        public int GetManaCount(Players player)
        {
            int result = 0;
            switch (player)
            {
                case Players.First:
                    result = _onePlayermanaCount;
                    break;
                case Players.Second:
                    result = _twoPlayermanaCount;
                    break;
            }
            return result;
        }
    }
}