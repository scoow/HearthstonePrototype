using System.Collections.Generic;
using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

namespace Hearthstone
{
    public class PageBook : MonoBehaviour, IReadable
    {        
        public CardSettings[] _cardTemplatePrefabs ;        
        public List<CardSO> _1manaCostCard;
        public List<CardSO> _2manaCostCard;
        public List<CardSO> _3manaCostCard;
        public List<CardSO> _4manaCostCard;
        public List<CardSO> _5manaCostCard;
        public List<CardSO> _6manaCostCard;
        public List<CardSO> _7manaCostCard;
        

        public List<CardSO> _resultCollection = new List<CardSO>();
        /// <summary>
        /// ñëîâàðü êàðò ïî êëþ÷ó = ID
        /// </summary>
        public Dictionary<int, CardSO> _cardsDictionary = new Dictionary<int, CardSO>();
        private int _maxPageNumber;
        private int _minPageNumber = 0;
        private int _pageCounter = 0;       
        

        private void Awake()
        {
            _resultCollection = _1manaCostCard.
                Union(_2manaCostCard).
                Union(_3manaCostCard).
                Union(_4manaCostCard).
                Union(_5manaCostCard).
                Union(_6manaCostCard).
                Union(_7manaCostCard).ToList();
            foreach(CardSO card in _resultCollection)
            {
                _cardsDictionary.Add(card._id, card);
            }
        }

        private void Start()
        {
            ÑhangingÑards();            
        }

        private void ÑhangingÑards()           
        { 
            for(int i = 0; i <= _cardTemplatePrefabs.Length-1; i++)
            {
                _cardTemplatePrefabs[i].SpriteCard.sprite = _resultCollection[i + (_pageCounter * _cardTemplatePrefabs.Length)]._spriteCard;
                _cardTemplatePrefabs[i].Id = _resultCollection[i + (_pageCounter * _cardTemplatePrefabs.Length)]._id;
                _cardTemplatePrefabs[i].ManaCost.text = _resultCollection[i + (_pageCounter * _cardTemplatePrefabs.Length)]._manaCost.ToString();
                _cardTemplatePrefabs[i].AtackDamage.text = _resultCollection[i + (_pageCounter * _cardTemplatePrefabs.Length)]._atackDamage.ToString();
                _cardTemplatePrefabs[i].Healt.text = _resultCollection[i + (_pageCounter * _cardTemplatePrefabs.Length)]._healt.ToString();
                _cardTemplatePrefabs[i].Name.text = _resultCollection[i + (_pageCounter * _cardTemplatePrefabs.Length)]._name.ToString();
                _cardTemplatePrefabs[i].Description.text = _resultCollection[i + (_pageCounter * _cardTemplatePrefabs.Length)]._description.ToString();
            }
        }

        public CardSO GetCard(int cardId)
        {
            CardSO cardSO_example = _cardsDictionary[cardId];
            return cardSO_example;
        }

        public void NextPage()
        {            
            if ((_resultCollection.Count % _cardTemplatePrefabs.Length) == 0)
                _maxPageNumber = _resultCollection.Count / _cardTemplatePrefabs.Length;
            else
                _maxPageNumber = _resultCollection.Count / _cardTemplatePrefabs.Length + 1;

            _pageCounter++;
            if (_pageCounter > _maxPageNumber)
                _pageCounter = _maxPageNumber;
            ÑhangingÑards();
        }

        public void PreviousPage()
        {         
            _pageCounter--;
            if (_pageCounter < _minPageNumber)
                _pageCounter = _minPageNumber;
            ÑhangingÑards();
        }        
    }
}