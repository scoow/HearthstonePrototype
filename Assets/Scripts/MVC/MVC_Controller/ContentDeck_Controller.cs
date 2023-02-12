using UnityEngine;

namespace Hearthstone
{
    [RequireComponent(typeof(ContentDeck_Model))]
    public class ContentDeck_Controller : MonoBehaviour , ICreating
    {        
        private PageBook_Model _pageBook_Model;
        private ContentDeck_Model _contentDeck_Model;
        public Transform _contentDeckTransform;
        

        private void Start()
        {
            _contentDeck_Model = GetComponent<ContentDeck_Model>();            
            _pageBook_Model = FindObjectOfType<PageBook_Model>();
        }

        public void AddContent(int cardId) 
        {
            GameObject obj = Instantiate(_contentDeck_Model._prefabChioseCardSettings, _contentDeckTransform);
            Card_Model addCardSettings = obj.GetComponent<Card_Model>();
            CardSO_Model cardSO_Model = _pageBook_Model._cardsDictionary[cardId];
            addCardSettings._idCard = cardSO_Model._idCard;
            addCardSettings._nameCard = cardSO_Model._nameCard;
            addCardSettings._manaCostCard = cardSO_Model._manaCostCard;
            addCardSettings.gameObject.GetComponent<Card_View>().ChangeViewCard();
            _contentDeck_Model._contentDeck.Add(cardId);
            
        }

        public void RemoveContent(int cardId)
        {
            foreach (int id in _contentDeck_Model._contentDeck)
            {
                if (id == cardId)
                {
                    _contentDeck_Model._contentDeck.Remove(cardId);
                    return;
                }
            }
        }

        /// <summary>
        /// очищение списка префабов выбранных карт
        /// </summary>
        public void ClearContent()
        {
            foreach (Transform child in _contentDeckTransform) Destroy(child.gameObject);
        }

        public string GetNameDeck()
        {
            return _contentDeck_Model._currentDeckName;
        }
    }
}