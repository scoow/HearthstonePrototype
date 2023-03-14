using System.ComponentModel;
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
        [Inject(Id = "First")]
        private readonly Board _firstPlayerBoard;
        [Inject(Id = "Second")]
        private readonly Board _secondPlayerBoard;
        private void Start()
        {
            _playerDeck = GameObject.Find("PlayerDeck").transform;
            _enemyDeck = GameObject.Find("EnemyDeck").transform;
           
            CreateDeck(Players.First, _playerDeck);
            //_loadDeckController.ShuffleCardInDeck();
            CreateDeck(Players.Second, _enemyDeck);
        }

        private void CreateDeck(Players side, Transform deck)
        {
            int layerStep = 0;
            foreach (int i in _loadDeckController._activeDeck)
            {
                CreateCard(side, deck, ref layerStep, i, false);
            }
        }

        public int CreateCard(Players side, Transform deck, ref int layerStep, int i, bool isMinion)
        {
            GameObject newCard = Instantiate(_cardPrefab, deck.position, deck.rotation);

            newCard.transform.parent = deck;
            var battlemodeCardView = newCard.GetComponent<BattleModeCard_View>();
            var battleModeCard = newCard.GetComponent<BattleModeCard>();

            var newCardModel = newCard.GetComponent<Card_Model>();
            var newlayerRenderUp = newCard.GetComponent<LayerRenderUp>();            
            newlayerRenderUp.LayerUp(layerStep);
            layerStep--;            
            var newCardView = newCard.GetComponent<Card_View>();
            newCardModel.SetCardSettings(i, isMinion);//вынести в отд метод для создания минионов
            battlemodeCardView.SetSettingsCardInBattle();
            Card card = newCard.GetComponent<Card>();
            if (isMinion)
            {
                battlemodeCardView.ChangeCardViewMode();
                
                card.ChangeState(CardState.Board);
                
                if (side == Players.First)
                {
                    _firstPlayerBoard.AddCard(card);
                    card.SetParent(_firstPlayerBoard);
                }
                else
                {
                    _secondPlayerBoard.AddCard(card);
                    card.SetParent(_secondPlayerBoard);
                }
            }
            card.SetSide(side);
            newCardView.ChangeViewCard();
            battleModeCard.SetSide(side);//упростить

            return layerStep;
        }
    }
}