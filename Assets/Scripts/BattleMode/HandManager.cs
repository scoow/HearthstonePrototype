using UnityEngine;

namespace Hearthstone
{
    public class HandManager : MonoBehaviour
    {
        private LoadDeck_Controller _loadDeckController;        
        private Transform _playerDeck;
        [SerializeField]
        private GameObject _cardPrefab;
        private PageBook_Model _pageBook_Model;
        private void Start()
        {
            _pageBook_Model = FindObjectOfType<PageBook_Model>();
            _loadDeckController = FindObjectOfType<LoadDeck_Controller>();
            _playerDeck = GameObject.Find("PlayerDeck").transform;
            foreach (int i in _loadDeckController._activeDeck)
            {
                var newCard = Instantiate(_cardPrefab, _playerDeck.position, _playerDeck.rotation);
                var battlemodeCardView = newCard.GetComponent<BattleModeCard_View>();
                battlemodeCardView.SetSettingsCardInBattle();
                var newCardModel = newCard.GetComponent<Card_Model>();
                var newCardView = newCard.GetComponent<Card_View>();
                newCardModel.SetCardSettings(i);
                newCardView.ChangeViewCard();
            }
        }
    }
}