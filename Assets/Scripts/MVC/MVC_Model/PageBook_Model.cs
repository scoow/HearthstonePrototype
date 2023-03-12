using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hearthstone
{
    public class PageBook_Model : MonoBehaviour
    {
        /// <summary>
        /// только игровые карты
        /// </summary>
        public List<CardSO_Model> _cardSO_Models = new List<CardSO_Model>();
        /// <summary>
        /// словарь карт по ключу = ID
        /// </summary>
        public Dictionary<int, GameSO_Model> _cardsDictionary = new Dictionary<int, GameSO_Model>();
        
        public Dictionary<int,GameSO_Model> _cardAssistDictionary = new Dictionary<int, GameSO_Model>();

        /// <summary>
        /// SO сборник всех карт
        /// </summary>
        public CardCollectionSO_Model cardCollectionSO_Model;
        /// <summary>
        /// общий список всех карт по ID по возрастающей
        /// </summary>
        [HideInInspector]
        public List<GameSO_Model> _resultCollection = new List<GameSO_Model>();
        [HideInInspector]
        public CreateDeckState _createDeckState ;
       
        public void Awake()
        {
            _resultCollection =   cardCollectionSO_Model._1manaCostCard.
                            Union(cardCollectionSO_Model._2manaCostCard).
                            Union(cardCollectionSO_Model._3manaCostCard).
                            Union(cardCollectionSO_Model._4manaCostCard).
                            Union(cardCollectionSO_Model._5manaCostCard).
                            Union(cardCollectionSO_Model._6manaCostCard).
                            Union(cardCollectionSO_Model._7manaCostCard).
                            Union(cardCollectionSO_Model._heroCollection).
                            Union(cardCollectionSO_Model._assistCardCollection).ToList();
            foreach (GameSO_Model card in _resultCollection)
            {
                if (cardCollectionSO_Model._assistCardCollection.Contains(card))
                {
                    _cardAssistDictionary.Add(card._idCard, card);
                    continue;
                }                    
                _cardsDictionary.Add(card._idCard, card);
                if (card is CardSO_Model)
                {
                    _cardSO_Models.Add((CardSO_Model)card);
                }
            }            
        }
        public CardSO_Model GetCardSO_byID(int idCard)
        {
            bool success;
            success = _cardsDictionary.TryGetValue(idCard, out GameSO_Model cardSO_Model);
            if (!success)
                _ = _cardAssistDictionary.TryGetValue(idCard, out cardSO_Model);
            return (CardSO_Model)cardSO_Model;
        }    
    }

    /// <summary>
    /// Состояния выбора
    /// </summary>
    public enum CreateDeckState
    {
        ChoiseHero,
        ChoiseDeck,
        CreateDeck,
    }
}