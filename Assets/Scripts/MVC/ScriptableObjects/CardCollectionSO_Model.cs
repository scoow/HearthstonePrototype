using System.Collections.Generic;
using UnityEngine;

namespace Hearthstone
{
    [CreateAssetMenu(fileName = "New CardManaCostArrey", menuName = "Card/CardManaCostArrey", order = 51)]
    public class CardCollectionSO_Model : ScriptableObject
    {
        public List<GameSO_Model> _1manaCostCard;
        public List<GameSO_Model> _2manaCostCard;
        public List<GameSO_Model> _3manaCostCard;
        public List<GameSO_Model> _4manaCostCard;
        public List<GameSO_Model> _5manaCostCard;
        public List<GameSO_Model> _6manaCostCard;
        public List<GameSO_Model> _7manaCostCard;
        public List<HeroSO_Model> _heroCollection;

        public Dictionary<string, List<int>> _deckSO_Collection = new Dictionary<string, List<int>>() ;
        
    }
}