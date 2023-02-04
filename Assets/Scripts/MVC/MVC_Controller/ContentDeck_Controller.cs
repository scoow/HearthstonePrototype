using UnityEngine;

namespace Hearthstone
{
    [RequireComponent(typeof(ContentDeck_Model))]
    public class ContentDeck_Controller : MonoBehaviour , ICreating
    {
        private IReadable _readable;      
        private ContentDeck_Model _contentDeck_Model;
        public Transform _contentDeckTransform;
        

        private void Start()
        {
            _contentDeck_Model = GetComponent<ContentDeck_Model>();            
            _readable = FindObjectOfType<PageBook_Controller>();
            
        }

        public void AddContent(int cardId) 
        {
            GameObject obj = Instantiate(_contentDeck_Model._prefabChioseCardSettings, _contentDeckTransform);
            CardSettings_Model addCardSettings = obj.GetComponent<CardSettings_Model>();
            CardSO_Model cardSettings = _readable.GetCard(cardId);
            addCardSettings.Id = cardId;
            addCardSettings.Name.text = cardSettings._nameCard;
            addCardSettings.ManaCost.text = cardSettings._manaCostCard.ToString();
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