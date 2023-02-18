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

        private MulliganManager _mulliganManager;
        private void Start()
        {
            _pageBook_Model = FindObjectOfType<PageBook_Model>();
            _loadDeckController = FindObjectOfType<LoadDeck_Controller>();
            _playerDeck = GameObject.Find("PlayerDeck").transform;
            _mulliganManager = FindObjectOfType<MulliganManager>();
            foreach (int i in _loadDeckController._activeDeck)
            {
                var newCard = Instantiate(_cardPrefab, _playerDeck.position, _playerDeck.rotation);
                newCard.transform.parent = _mulliganManager.transform;
                var battlemodeCardView = newCard.GetComponent<BattleModeCard_View>();
               // battlemodeCardView
                
                var newCardModel = newCard.GetComponent<Card_Model>();
                var newCardView = newCard.GetComponent<Card_View>();
                newCardModel.SetCardSettings(i);
                battlemodeCardView.SetSettingsCardInBattle();
                newCardView.ChangeViewCard();
            }
        }
    }
}