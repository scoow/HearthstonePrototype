using System.Collections.Generic;
using UnityEngine;

namespace Hearthstone
{
    public class ContentDeck_Model : MonoBehaviour, ICreating
    {
        /// <summary>
        /// префаб шаблона отображающего краткие настройки выбранной карты
        /// </summary>
        public GameObject _prefabChioseCardSettings;        
        /// <summary>
        /// список ID выбранных карт в колоде
        /// </summary>
        public List<int>  _contentDeck;
        
        public IReadable _readable;
        private ISerialization _serialization;


        private void Awake()
        {
            _readable = FindObjectOfType<PageBook_Controller>();
            _serialization = FindObjectOfType<Saving_Controller>();
        }

        public void Save()
        {
            _serialization.SaveDeck(_contentDeck);
        }
        public void AddContent(int cardId)
        {
            GameObject obj = Instantiate(_prefabChioseCardSettings, transform);
            CardSettings_Model addCardSettings = obj.GetComponent<CardSettings_Model>();
            CardSO_Model cardSettings = _readable.GetCard(cardId);
            addCardSettings.Id = cardId;
            addCardSettings.Name.text = cardSettings._nameCard;
            addCardSettings.ManaCost.text = cardSettings._manaCostCard.ToString();            
            _contentDeck.Add(cardId);
            Debug.Log($"¬ колоду добавленна карта с ID = {cardId}");
        }
        public void RemoveContent(int cardId)
        {            
            foreach(int id in _contentDeck)
            {
                if(id == cardId)
                {
                    _contentDeck.Remove(cardId);
                    Debug.Log($"»з колоды убранна карта с ID = {cardId}");
                    return;
                }                
            }           
        }       
    }
}