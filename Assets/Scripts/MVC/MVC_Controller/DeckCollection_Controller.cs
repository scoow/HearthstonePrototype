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
        /// ������ ������� ��������� ������
        /// </summary>
        public GameObject _prefabTemplateDeck;
        private ContentDeck_Model _contentDeck_Model;
        private ContentDeck_Controller _contentDeck_Controller;
        private PageBook_Model _pageBookModel;
        public ISave _memory_Controller;

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
            obj.GetComponentInChildren<TextDeckNameMarker>().GetComponent<Text>().text = _contentDeck_Model._currentDeckName;                   
            _contentDeck_Model._inputField.text = "";
            _contentDeck_Controller.ClearContent();
            if(_pageBookModel._createDeckState == CreateDeckState.CreateDeck)
            {
                _memory_Controller.SaveDeck();
            }            
        }
    }
}