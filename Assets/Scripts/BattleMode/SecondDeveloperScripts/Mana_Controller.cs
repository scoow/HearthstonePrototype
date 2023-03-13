using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

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
        private EndTurnButton _endTurnButton;//������ ����� ����
        public Action<Players> OnChangeTurn;

        [Inject]
        private IndicatorTarget _indicatorTarget;

        [Inject(Id = "First")]
        private readonly Board _firstPlayerBoard;
        [Inject(Id = "Second")]
        private readonly Board _secondPlayerBoard;
        [Inject]
        private MulliganManager _mulliganManager;


        private int _onePlayerCrystalCount;
        private int _twoPlayerCrystalCount;
        private int _onePlayerManaCount = 1;
        private int _twoPlayerManaCount = 0;

        [SerializeField]
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
                _mulliganManager.TakeOneCard(_playersTurn);
                _secondPlayerBoard.EnableAttackForAllMinions();

                ChangeManaValue?.Invoke(_twoPlayerManaCount, _twoPlayerCrystalCount);
                OnChangeTurn?.Invoke(Players.Second);
            }
            else
            { 
                _playersTurn = Players.First;
                AddCristal(_playersTurn);
                RestoreMana(_playersTurn);
                _mulliganManager.TakeOneCard(_playersTurn);
                _firstPlayerBoard.EnableAttackForAllMinions();

                ChangeManaValue?.Invoke(_onePlayerManaCount, _onePlayerCrystalCount);
                OnChangeTurn?.Invoke(Players.First);
            }
            _indicatorTarget.ChangeCursorState(false);
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
                _ = Instantiate(_manaCristalPrefab, parent);
            }
        }
        /// <summary>
        /// ������������ ���� � ������ ����
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
            //Debug.Log("����� " + _playersTurn + " � ������� ������ " + _onePlayerManaCount + " ����, � � ������� - " + _twoPlayerManaCount);
        }

        public void SpendMana(Players player, int manacost)
        {
            switch (player)
            {
                case Players.First:
                    _onePlayerManaCount -= manacost;
                    ChangeManaValue?.Invoke(_onePlayerManaCount, _onePlayerCrystalCount);
                    break;
                case Players.Second:
                    _twoPlayerManaCount -= manacost;
                    ChangeManaValue?.Invoke(_twoPlayerManaCount, _twoPlayerCrystalCount);
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