using UnityEngine;
using UnityEngine.UI;

namespace Hearthstone
{
    public class DeckCollection_Controller : MonoBehaviour
    {
        private Text _nameDeck;
        public Text Name { get => _nameDeck; set => _nameDeck = value; }

        [SerializeField] private Transform _deckCollection;
        /// <summary>
        /// префаб шаблона созданной колоды
        /// </summary>
        [SerializeField] private GameObject _prefabTemplateDeck;
        private ContentDeck_Model _contentDeck_Model;
        private ContentDeck_Controller _contentDeck_Controller;
        private PageBook_Model _pageBookModel;
        private ISave _memory_Controller;

        private void Start()
        {
            _contentDeck_Model = GetComponent<ContentDeck_Model>();
            _memory_Controller =FindObjectOfType<Memory_Controller>();
            _contentDeck_Controller = GetComponent<ContentDeck_Controller>();
            _pageBookModel = FindObjectOfType<PageBook_Model>();
        }

        public void AddDeckInCollection()
        {            
            GameObject obj = Instantiate(_prefabTemplateDeck, _deckCollection);  
            obj.GetComponentInChildren<TextDeckNameMarker>().GetComponent<Text>().text = _contentDeck_Model.CurrentDeckName;                   
            _contentDeck_Model.InputField.text = "";
            _contentDeck_Controller.ClearContent();
            if(_pageBookModel._createDeckState == CreateDeckState.CreateDeck)
            {
                _memory_Controller.SaveDeck();
            }            
        }
    }
}