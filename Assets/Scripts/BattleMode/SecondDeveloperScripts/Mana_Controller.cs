using System;
using UnityEngine;

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


        [HideInInspector] public int _onePlayermanaCount;
        [HideInInspector] public int _twoPlayermanaCount;

        private int _playermanaMaxValue = 10;

        private void Start()
        {
            _playersTurn = Players.First;
            AddCristal(_playersTurn);
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
            switch(playersTurn)
            {
                case Players.First: CreateCristal(_onePlayerMana, ref _onePlayermanaCount); break;
                case Players.Second: CreateCristal(_twoPlayerMana, ref _twoPlayermanaCount); break;
            }
        }

        private void CreateCristal(Transform parent,ref int manaCounter)
        {
            if(manaCounter < _playermanaMaxValue)
            {
                manaCounter++;
                ManaCristal cristal = Instantiate(_manaCristalPrefab, parent);
            }            
        }        
    }
}