using System.Collections.Generic;
using UnityEngine;

namespace Hearthstone
{
    public class ContentDeck : MonoBehaviour, ICreating
    {
        /// <summary>
        /// префаб шаблона отображающего краткие настройки выбранной карты
        /// </summary>
        public GameObject _prefabChioseCardSettings;
        /// <summary>
        /// префаб шаблона созданной колоды
        /// </summary>
        public GameObject _prefabTemplateDeck;
        /// <summary>
        /// список ID выбранных карт в колоде
        /// </summary>
        public List<int> _contentDeck;

        private HeroType _heroType;
        public IReadable _readable;
        private ISerialization _serialization;


        private void Awake()
        {
            _readable = FindObjectOfType<PageBook>();
            _serialization = FindObjectOfType<Storage>();
        }

        public void Save()
        {
            _serialization.SaveDeck(_contentDeck);
        }
        public void AddContent(int cardId)
        {
            GameObject obj = Instantiate(_prefabChioseCardSettings, transform);
            CardSettings addCardSettings = obj.GetComponent<CardSettings>();
            CardSO cardSettings = _readable.GetCard(cardId);
            addCardSettings.Id = cardId;
            addCardSettings.Name.text = cardSettings._nameCard;
            addCardSettings.ManaCost.text = cardSettings._manaCostCard.ToString();            
            _contentDeck.Add(cardId);    
        }
        public void RemoveContent(int cardId)
        {            
            foreach(int id in _contentDeck)
            {
                if(id == cardId)
                {
                    _contentDeck.Remove(cardId);                    
                    return;
                }                
            }           
        }       
    }
}