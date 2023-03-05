using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

/*
 * Описание стадий муллигана:
1. 3 или 4 карты вылетают из колоды
2. игрок выбирает, какие хочет убрать
3. выбранные карты уходят, на их место приходят случайные из колоды
4. оставшиеся карты попадают в руку
 */

namespace Hearthstone
{
    public class MulliganManager : MonoBehaviour
    {
        [SerializeField]
        private float _time = 0.5f;

        [SerializeField]
        private GameObject _playerDeck;
        [SerializeField]
        private GameObject _enemyDeck;

        [Inject]
        private Mana_Controller _manaController;
        private List<MulliganCardPosition> _mulliganCardsPositions;//Якори для вылетающих карт
        private List<BattleModeCard> _mulliganCards;//Сами карты
        private List<BattleModeCard> _mulliganCardsFirstPlayer;
        private List<BattleModeCard> _mulliganCardsSecondPlayer;
        private MulliganConfirmButton _mulliganConfirmButton;

        [Inject]
        private List<Hand> _hands;
        [Inject(Id = "First")]
        private Hand _firstPlayerHand;
        [Inject(Id = "Second")]
        private Hand _secondPlayerHand;

        [Inject]
        private List<Board> _boards;
        [Inject(Id = "First")]
        private Board _firstPlayerBoard;
        [Inject(Id = "Second")]
        private Board _secondPlayerBoard;

        private int _nextCardInPlayerDeckNumber = 5;
        private int _nextCardInEnemyDeckNumber = 5;

        private async void Start()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.1));//ожидание, пока инициализируется сцена
            _mulliganConfirmButton = FindObjectOfType<MulliganConfirmButton>();
            _mulliganConfirmButton.Init();
            _mulliganConfirmButton.HideButton();
            _mulliganConfirmButton.onClick.AddListener(delegate { MulliganStage3(Players.First); });

            _mulliganCardsPositions = new List<MulliganCardPosition>();
            _mulliganCardsPositions = FindObjectsOfType<MulliganCardPosition>().ToList();
            _mulliganCardsPositions.Sort((c1, c2) => string.Compare(c1.gameObject.name, c2.gameObject.name));//todo улучшить сортировку

            _mulliganCards = new List<BattleModeCard>();
            _mulliganCards = FindObjectsOfType<BattleModeCard>().ToList();
            _mulliganCardsFirstPlayer = _mulliganCards.Where(x => x._side == Players.First).ToList();
            _mulliganCardsSecondPlayer = _mulliganCards.Where(x => x._side == Players.Second).ToList();
            foreach (var card in _mulliganCards)
                card.gameObject.SetActive(true);

            MulliganStage1(Players.First);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5));
            MulliganStage2();
        }

        private async void MulliganStage1(Players side)
        {
            BattleModeCard _card;
            List<BattleModeCard> _currentDeck;
            if (side == Players.First)
                _currentDeck = _mulliganCardsFirstPlayer;
            else
                _currentDeck = _mulliganCardsSecondPlayer;

            int i = 0;
            foreach (MulliganCardPosition position in _mulliganCardsPositions)
            {
                _card = _currentDeck[i];
                position.SetCurrentCard(_card);//привязываем карту к позиции
                _ = _card.MoveCardAsync(_card.transform, position.transform, _time);
                await UniTask.Delay(TimeSpan.FromSeconds(0.5));
                i++;
            }
        }

        private void MulliganStage2()
        {
            _mulliganConfirmButton.ShowButton();
        }

        private async void MulliganStage3(Players side)
        {
            _mulliganConfirmButton.HideButton();

            BattleModeCard _card;
            List<BattleModeCard> _currentCards;
            Transform _currentDeck;

            if (side == Players.First)
            {
                _currentCards = _mulliganCardsFirstPlayer;
                _currentDeck = _playerDeck.transform;
            }
            else
            {
                _currentCards = _mulliganCardsSecondPlayer;
                _currentDeck = _enemyDeck.transform;
            }

            int i = 0;
            foreach (MulliganCardPosition position in _mulliganCardsPositions)
            {
                _card = _currentCards[i];
                if (_card.Selected)
                {
                    _ = _card.MoveCardAsync(position.transform, _currentDeck, _time);
                    await UniTask.Delay(TimeSpan.FromSeconds(0.5));
                }
                i++;
            }
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            MulliganStage4(side);
        }
        private async void MulliganStage4(Players side)
        {
            _mulliganConfirmButton.HideButton();
            BattleModeCard _card;

            List<BattleModeCard> _currentCards;
            Transform _currentDeck;
            Hand _currentHand;
            Board _currentBoard;
            if (side == Players.First)
            {
                _currentCards = _mulliganCardsFirstPlayer;
                _currentDeck = _playerDeck.transform;
                _currentHand = _firstPlayerHand;
                _currentBoard = _firstPlayerBoard;
            }
            else
            {
                _currentCards = _mulliganCardsSecondPlayer;
                _currentDeck = _enemyDeck.transform;
                _currentHand = _secondPlayerHand;
                _currentBoard = _secondPlayerBoard;
            }

            int i = 0;
            foreach (MulliganCardPosition position in _mulliganCardsPositions)
            {
                _card = _currentCards[i];
                if (_card.Selected)
                {
                    _ = _card.MoveCardAsync(_card.transform, position.transform, _time);
                    await UniTask.Delay(TimeSpan.FromSeconds(0.5));
                }

                position.SwitchRenderer(false);
                i++;
            }
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            i = 0;
            foreach (MulliganCardPosition position in _mulliganCardsPositions)
            {
                _card = _currentCards[i];
                //Debug.Log(_currentHand.GetLastCardPosition().position.ToString());
                _ = _card.MoveCardAsync(position.transform, _currentHand.GetLastCardPosition(), _time);

                await UniTask.Delay(TimeSpan.FromSeconds(0.5));
                if (side == Players.Second)
                    position.gameObject.SetActive(false);
                var _cardInHand = _card.AddComponent<CardInHand>();

                _cardInHand.SetParent(_currentHand);
                _currentHand.AddCard(_cardInHand);
                i++;
            }
             _currentBoard.InitCardList(_currentCards);//привязка событий карт текущего игрока к текущему полю

            if (side == Players.First)//если первый муллиган
            {
                MulliganStage1(Players.Second);
                MulliganStage2();
                _mulliganConfirmButton.onClick.RemoveAllListeners();
                _mulliganConfirmButton.onClick.AddListener(delegate { MulliganStage3(Players.Second); });
            }
        }
        /// <summary>
        /// Взятие одной карты из колоды в руку текущего игрока
        /// </summary>
        /// <param name="side">текущий игрок</param>
        public void TakeOneCard(Players side)
        {
            Hand _currentHand;
            int _nextCardInDeckNumber;
            if (side == Players.First)
            {
                _currentHand = _firstPlayerHand;
                _nextCardInDeckNumber = _nextCardInPlayerDeckNumber;
                _nextCardInPlayerDeckNumber++;
            }
            else
            {
                _currentHand = _secondPlayerHand;
                _nextCardInDeckNumber = _nextCardInEnemyDeckNumber;
                _nextCardInEnemyDeckNumber++;
            }
            var card = _mulliganCards.ElementAt(_nextCardInDeckNumber);
            _ = card.MoveCardAsync(card.transform, _currentHand.GetLastCardPosition(), _time);

        }
        private void Update()//тест взятия 
        {
            if (Input.GetMouseButtonDown(2))
            {
                TakeOneCard(Players.First);
            }
            if (Input.GetMouseButtonDown(1))
            {
                TakeOneCard(Players.Second);
            }
        }
    }
}