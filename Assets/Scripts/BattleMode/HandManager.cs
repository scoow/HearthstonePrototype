using UnityEngine;
using Zenject;

namespace Hearthstone
{
    /// <summary>
    /// Создаёт на сцене игровые объекты карт для выбранной колоды
    /// </summary>
    public class HandManager : MonoBehaviour
    {
        [Inject]
        private LoadDeck_Controller _loadDeckController;        
        private Transform _playerDeck;
        [SerializeField]
        private GameObject _cardPrefab;
        [Inject]
        private PageBook_Model _pageBook_Model;
        [Inject]
        private MulliganManager _mulliganManager;
        private void Start()
        {
            _playerDeck = GameObject.Find("PlayerDeck").transform;
           // _mulliganManager = FindObjectOfType<MulliganManager>();zenject
            foreach (int i in _loadDeckController._activeDeck)
            {
                var newCard = Instantiate(_cardPrefab, _playerDeck.position, _playerDeck.rotation);
                newCard.transform.parent = _mulliganManager.transform;
                var battlemodeCardView = newCard.GetComponent<BattleModeCard_View>();
                
                var newCardModel = newCard.GetComponent<Card_Model>();
                var newCardView = newCard.GetComponent<Card_View>();
                newCardModel.SetCardSettings(i);//вынести в отд метод для создания минионов
                battlemodeCardView.SetSettingsCardInBattle();
                newCardView.ChangeViewCard();
            }
        }
    }
}