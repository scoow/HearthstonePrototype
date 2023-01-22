using Hearthstone;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hearthstone
{
    public class ContentDeck : MonoBehaviour, ICreating
    {
        /// <summary>
        /// префаб шаблона отображающего краткие настройки выбранной карты
        /// </summary>
        public GameObject _prefabChioseCardSettings;
        /// <summary>
        /// список выбранных карт в колоде
        /// </summary>
        public List<CardSettings> _contentDeck;

        public void AddCardInDeck(CardSettings settingsChioseCard)
        {
            GameObject obj = Instantiate(_prefabChioseCardSettings, transform);
            obj.GetComponent<CardSettings>().Id = settingsChioseCard.Id;
            obj.GetComponent<CardSettings>().Name.text = settingsChioseCard.Name.text;
            obj.GetComponent<CardSettings>().ManaCost.text = settingsChioseCard.ManaCost.text;
            //obj.GetComponent<CardSettings>().Description.text = settingsChioseCard.Description.text;
            //obj.GetComponent<CardSettings>().AtackDamage = settingsChioseCard.AtackDamage;
            //obj.GetComponent<CardSettings>().Healt.text = settingsChioseCard.Healt.text;
            _contentDeck.Add(settingsChioseCard);    
        }
        public void RemoveCardInDeck(CardSettings settingsChioseCard)
        {
            foreach(CardSettings cardSetting in _contentDeck)
            {

            }

            //contentDeck.Remove(settingsChioseCard);
        }
       
    }
}