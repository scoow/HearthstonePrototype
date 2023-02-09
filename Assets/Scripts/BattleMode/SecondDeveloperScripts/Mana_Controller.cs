using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hearthstone
{
    public class Mana_Controller : MonoBehaviour , IGrowing
    {
        [SerializeField]
        private ManaCristal _manaCristalPrefab;
        [SerializeField]
        private Transform _onePlayerMana , _twoPlayerMana;
        public Players _playersTurn;


        public int _onePlayermanaCount;
        public int _twoPlayermanaCount;

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
                return;
            }           
            
            if (_playersTurn == Players.Second)
            {
                _playersTurn = Players.First;
                AddCristal(_playersTurn);
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

        void IGrowing.AddCristal(Players playersTurn)
        {
            throw new System.NotImplementedException();
        }
    }
}