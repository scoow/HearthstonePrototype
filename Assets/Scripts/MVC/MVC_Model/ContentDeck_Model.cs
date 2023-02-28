using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hearthstone
{
    public class ContentDeck_Model : MonoBehaviour
    {
        public InputField _inputField;
        /// <summary>
        /// текущее название колоды
        /// </summary>
        public string _currentDeckName;
        /// <summary>
        /// тип классовых карт , которые можно добавлять в колоду
        /// </summary>
        public Classes _classHeroInDeck;
        /// <summary>
        /// префаб шаблона отображающего краткие настройки выбранной карты
        /// </summary>
        public GameObject _prefabChioseCardSettings;
        /// <summary>
        /// текущий список ID выбранных карт в колоде
        /// </summary>
        public List<int> _contentDeck;

        private void Awake()
        {
            _classHeroInDeck = Classes.Universal;
            _inputField = FindObjectOfType<InputTextMarker>().GetComponent<InputField>();
            _inputField.onEndEdit.AddListener(ToText);
        }
        public void ToText(string inputText)
        {
            _currentDeckName = inputText;            
        }
    }
}