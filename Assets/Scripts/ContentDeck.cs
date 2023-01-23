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
        /// ������ ������� ������������� ������� ��������� ��������� �����
        /// </summary>
        public GameObject _prefabChioseCardSettings;
        /// <summary>
        /// ������ ��������� ���� � ������
        /// </summary>
        public List<CardSettings> _contentDeck;

        public void AddCardInDeck(CardSettings settingsChioseCard)
        {
            GameObject obj = Instantiate(_prefabChioseCardSettings, transform);
            obj.GetComponent<CardSettings>().Id = settingsChioseCard.Id;
            obj.GetComponent<CardSettings>().Name.text = settingsChioseCard.Name.text;
            obj.GetComponent<CardSettings>().ManaCost.text = settingsChioseCard.ManaCost.text;            
            _contentDeck.Add(settingsChioseCard);    
        }
        public void RemoveCardInDeck(CardSettings settingsChioseCard)
        {

            //����� �������� �������� ����� �� ������
            foreach(CardSettings cardSetting in _contentDeck)
            {
                if(cardSetting.GetComponent<CardSettings>().Id == settingsChioseCard.Id)
                {
                    _contentDeck.Remove(cardSetting);
                    Destroy(settingsChioseCard.gameObject);
                    return;
                }                
            }           
        }       
    }
}