using Hearthstone;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

namespace Hearthstone
{
    public class DeckCollection_Controller : MonoBehaviour
    {
        private Text _nameDeck;
        public Text Name { get => _nameDeck; set => _nameDeck = value; }

        public Transform _deckCollection;
        /// <summary>
        /// префаб шаблона созданной колоды
        /// </summary>
        public GameObject _prefabTemplateDeck;
        private ContentDeck_Model _contentDeck_Model;

        private void Start()
        {
            _contentDeck_Model = GetComponent<ContentDeck_Model>();
        }

        public void AddDeckInCollection()
        {
            GameObject obj = Instantiate(_prefabTemplateDeck, _deckCollection);
            obj.GetComponentInChildren<TextCardNameMarker>().GetComponent<Text>().text = _contentDeck_Model._currentDeckName;
            //_nameDeck.text = _contentDeck_Model._currentDeckName;
            Debug.Log($"Назовём колоду {_nameDeck}");
            /*addCardSettings.Name.text = cardSettings._nameCard;
            addCardSettings.ManaCost.text = cardSettings._manaCostCard.ToString();
            _contentDeck_Model._contentDeck.Add(cardId);
            Debug.Log($"В колоду добавленна карта с ID = {cardId}");*/

        }

        public void RemoveContent(int value)
        {

        }
    }

}