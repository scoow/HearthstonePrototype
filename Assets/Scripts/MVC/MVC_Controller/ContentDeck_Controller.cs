using UnityEngine;

namespace Hearthstone
{
    [RequireComponent(typeof(ContentDeck_Model))]
    public class ContentDeck_Controller : MonoBehaviour , ICreating
    {        
        private PageBook_Model _pageBook_Model;
        private ContentDeck_Model _contentDeck_Model;
        [SerializeField]private Transform _contentDeckTransform;        

        private void Start()
        {
            _contentDeck_Model = GetComponent<ContentDeck_Model>();            
            _pageBook_Model = FindObjectOfType<PageBook_Model>();
        }

        public void AddContent(int cardId) 
        {
            GameObject obj = Instantiate(_contentDeck_Model.PrefabChioseCardSettings, _contentDeckTransform);
            Card_Model addCardSettings = obj.GetComponent<Card_Model>();
            CardSO_Model cardSO_Model = (CardSO_Model)_pageBook_Model._cardsDictionary[cardId];
            addCardSettings.IdCard = cardSO_Model.IdCard;
            addCardSettings.NameCard = cardSO_Model.NameCard;
            addCardSettings.ManaCostCard = cardSO_Model.ManaCostCard;
            addCardSettings.gameObject.GetComponent<Card_View>().ChangeViewCard();
            _contentDeck_Model.ContentDeck.Add(cardId);
            
        }

        public void RemoveContent(int cardId)
        {
            foreach (int id in _contentDeck_Model.ContentDeck)
            {
                if (id == cardId)
                {
                    _contentDeck_Model.ContentDeck.Remove(cardId);
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
            return _contentDeck_Model.CurrentDeckName;
        }
    }
}