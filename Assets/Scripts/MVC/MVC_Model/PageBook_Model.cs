using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hearthstone
{
    public class PageBook_Model : MonoBehaviour
    {
        /// <summary>
        /// ������� ���� �� ����� = ID
        /// </summary>
        public Dictionary<int, CardSO_Model> _cardsDictionary = new Dictionary<int, CardSO_Model>();      
        /// <summary>
        /// SO ������� ���� ����
        /// </summary>
        public CardCollectionSO_Model cardCollectionSO_Model;
        /// <summary>
        /// ����� ������ ���� ���� �� ID �� ������������
        /// </summary>
        [HideInInspector]
        public List<CardSO_Model> _resultCollection = new List<CardSO_Model>();
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
                            Union(cardCollectionSO_Model._7manaCostCard).ToList();
            foreach (CardSO_Model card in _resultCollection)
            {
                _cardsDictionary.Add(card._idCard, card);
            }
        }
    }

    /// <summary>
    /// ��������� ������
    /// </summary>
    public enum CreateDeckState
    {
        ChoiseHero,
        ChoiseDeck,
        CreateDeck,
    }
}