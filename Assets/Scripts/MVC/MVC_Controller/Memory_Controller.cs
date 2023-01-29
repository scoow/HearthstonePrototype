using System.Collections.Generic;
using UnityEngine;


namespace Hearthstone
{
    public class Memory_Controller : MonoBehaviour
    {
        private ContentDeck_Model _contentDeck_Model;
        public CardCollectionSO_Model cardCollectionSO_Model;

        private void Awake()
        {
            _contentDeck_Model = FindObjectOfType<ContentDeck_Model>();
        }

        public void SaveDeckExample(string nameDeck)
        {
            cardCollectionSO_Model._deckSO_Collection.Clear(); //временно зачищаем коллекцию
            cardCollectionSO_Model._deckSO_Collection.Add(nameDeck, _contentDeck_Model._contentDeck);
        }
    }

}