using System.Collections.Generic;
using UnityEngine;

namespace Hearthstone
{
    [CreateAssetMenu(fileName = "New CardManaCostArrey", menuName = "Card/CardManaCostArrey", order = 51)]
    public class CardCollectionSO_Model : ScriptableObject
    {
        public List<CardSO_Model> _1manaCostCard;
        public List<CardSO_Model> _2manaCostCard;
        public List<CardSO_Model> _3manaCostCard;
        public List<CardSO_Model> _4manaCostCard;
        public List<CardSO_Model> _5manaCostCard;
        public List<CardSO_Model> _6manaCostCard;
        public List<CardSO_Model> _7manaCostCard;
        public List<CardSO_Model> _heroCollection;

        public Dictionary<string, List<int>> _deckSO_Collection = new Dictionary<string, List<int>>() ;
        
    }
}