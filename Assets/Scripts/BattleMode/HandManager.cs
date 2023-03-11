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
        private readonly LoadDeck_Controller _loadDeckController;        
        private Transform _playerDeck;
        private Transform _enemyDeck;
        [SerializeField]
        private GameObject _cardPrefab;
        [Inject]
        private readonly PageBook_Model _pageBook_Model;
        [Inject]
        private readonly MulliganManager _mulliganManager;
        private void Start()
        {
            _playerDeck = GameObject.Find("PlayerDeck").transform;
            _enemyDeck = GameObject.Find("EnemyDeck").transform;
           
            CreateDeck(Players.First, _playerDeck);
            CreateDeck(Players.Second, _enemyDeck);
        }

        private void CreateDeck(Players side, Transform deck)
        {
            int layerStep = 0;
            foreach (int i in _loadDeckController._activeDeck)
            {
                CreateCard(side, deck, layerStep, i, false);
            }
        }

        public int CreateCard(Players side, Transform deck, int layerStep, int i, bool isMinion)
        {
            var newCard = Instantiate(_cardPrefab, deck.position, deck.rotation);
            newCard.transform.parent = _mulliganManager.transform;
            var battlemodeCardView = newCard.GetComponent<BattleModeCard_View>();
            var battleModeCard = newCard.GetComponent<BattleModeCard>();

            var newCardModel = newCard.GetComponent<Card_Model>();
            var newlayerRenderUp = newCard.GetComponent<LayerRenderUp>();
            newlayerRenderUp.LayerUp(layerStep);
            layerStep--;
            var newCardView = newCard.GetComponent<Card_View>();
            newCardModel.SetCardSettings(i, isMinion);//вынести в отд метод для создания минионов
            battlemodeCardView.SetSettingsCardInBattle();
            newCardView.ChangeViewCard();
            battleModeCard.SetSide(side);
            return layerStep;
        }
    }
}