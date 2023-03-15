using System.ComponentModel;
using UnityEngine;
using Zenject;

namespace Hearthstone
{
    /// <summary>
    /// ������ �� ����� ������� ������� ���� ��� ��������� ������
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
        private PermanentEffect_Controller _permanentEffectController;

        private void Start()
        {
            _permanentEffectController = FindObjectOfType<PermanentEffect_Controller>();

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
            newCardModel.SetCardSettings(i, isMinion);//������� � ��� ����� ��� �������� ��������
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
            battleModeCard.SetSide(side);//���������

            //������� ���� ��� ������� ������� � ����� 
            if (card.GetState() == CardState.Board)
            {
                _permanentEffectController.GetActivePermanentEffect(card.GetComponent<Card_Controller>());
            }

            return layerStep;
        }
    }
}