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
        public Action<int, int> ChangeManaValue;
        private EndTurnButton _endTurnButton;//кнопка конца хода
        public Action<Players> OnChangeTurn;

        [HideInInspector] public int _onePlayerCrystalCount;
        [HideInInspector] public int _twoPlayerCrystalCount;
        private int _onePlayerManaCount = 1;
        private int _twoPlayerManaCount = 0;

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
                RestoreMana(_playersTurn);

                ChangeManaValue?.Invoke(_twoPlayerManaCount, _twoPlayerCrystalCount);
                OnChangeTurn?.Invoke(Players.Second);
                return;
            }

            if (_playersTurn == Players.Second)
            {
                
                _playersTurn = Players.First;
                AddCristal(_playersTurn);
                RestoreMana(_playersTurn);

                ChangeManaValue?.Invoke(_onePlayerManaCount, _onePlayerCrystalCount);
                OnChangeTurn?.Invoke(Players.First);
                return;
            }
            
        }

        private void AddCristal(Players playersTurn)
        {
            switch (playersTurn)
            {
                case Players.First: CreateCrystal(_onePlayerMana, ref _onePlayerCrystalCount); break;
                case Players.Second: CreateCrystal(_twoPlayerMana, ref _twoPlayerCrystalCount); break;
            }
        }

        private void CreateCrystal(Transform parent, ref int manaCounter)
        {
            if (manaCounter < _playermanaMaxValue)
            {
                manaCounter++;
                ManaCristal crystal = Instantiate(_manaCristalPrefab, parent);
            }
        }
        /// <summary>
        /// Восстановить ману в начале хода
        /// </summary>
        /// <param name="player"></param>
        private void RestoreMana(Players player)
        {
            switch (player)
            {
                case Players.First:
                    _onePlayerManaCount = _onePlayerCrystalCount;
                    ChangeManaValue?.Invoke(_onePlayerManaCount, _onePlayerCrystalCount);
                    break;
                case Players.Second:
                    _twoPlayerManaCount = _twoPlayerCrystalCount;
                    ChangeManaValue?.Invoke(_twoPlayerManaCount, _twoPlayerCrystalCount);
                    break;
            }
            //Debug.Log("Ходит " + _playersTurn + " У первого игрока " + _onePlayerManaCount + " маны, а у второго - " + _twoPlayerManaCount);
        }

        public void SpendMana(Players player, int manacost)
        {
            switch (player)
            {
                case Players.First:
                    _onePlayerManaCount -= manacost;
                    break;
                case Players.Second:
                    _twoPlayerManaCount -= manacost;
                    break;

            }
        }

        public int GetManaCount(Players player)
        {
            int result = 0;
            switch (player)
            {
                case Players.First:
                    result = _onePlayerManaCount;
                    break;
                case Players.Second:
                    result = _twoPlayerManaCount;
                    break;
            }
            return result;
        }
    }
}