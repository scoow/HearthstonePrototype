using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

/*
 * ???????? ?????? ?????????:
1. 3 ??? 4 ????? ???????? ?? ??????
2. ????? ????????, ????? ????? ??????
3. ????????? ????? ??????, ?? ?? ????? ???????? ????????? ?? ??????
4. ?????????? ????? ???????? ? ????
 */

//todo ????????? ?????? ??? ????? ?? ???? ????????? ?? ????, ??? ????????? ?????

namespace Hearthstone
{
    public class MulliganManager : MonoBehaviour
    {
        [SerializeField]
        private float _time = 0.1f;

        [SerializeField]
        private GameObject _playerDeck;
        [SerializeField]
        private GameObject _enemyDeck;
        private List<MulliganCardPosition> _mulliganCardsPositions;//????? ??? ?????????? ????
        private List<BattleModeCard> _mulliganCards;//???? ?????
        private List<BattleModeCard> _mulliganCardsFirstPlayer;
        private List<BattleModeCard> _mulliganCardsSecondPlayer;
        private MulliganConfirmButton _mulliganConfirmButton;
        [Inject(Id = "First")]
        private readonly Hand _firstPlayerHand;
        [Inject(Id = "Second")]
        private readonly Hand _secondPlayerHand;
        [Inject(Id = "First")]
        private readonly Board _firstPlayerBoard;
        [Inject(Id = "Second")]
        private readonly Board _secondPlayerBoard;
        [Inject(Id = "First")]
        private Hero_Controller _firstPlayerHero;
        [Inject(Id = "Second")]
        private Hero_Controller _secondPlayerHero;

        private int _nextCardInPlayerDeckNumber = 4;
        private int _nextCardInEnemyDeckNumber = 4;

        private int _playerFatigueDamage = 1;
        private int _enemyFatigueDamage = 1;

        private async void Start()
        {
            //await UniTask.Delay(TimeSpan.FromSeconds(0.1));//????????, ???? ???????????????? ?????
            _mulliganConfirmButton = FindObjectOfType<MulliganConfirmButton>();
            _mulliganConfirmButton.Initialize();
            _mulliganConfirmButton.HideButton();
            _mulliganConfirmButton.onClick.AddListener(delegate { MulliganStage3(Players.First); });

            _mulliganCardsPositions = new List<MulliganCardPosition>();
            _mulliganCardsPositions = FindObjectsOfType<MulliganCardPosition>().ToList();
            _mulliganCardsPositions.Sort((c1, c2) => string.Compare(c1.gameObject.name, c2.gameObject.name));//todo ???????? ??????????

            _mulliganCards = new List<BattleModeCard>();
            _mulliganCards = FindObjectsOfType<BattleModeCard>().ToList();
            _mulliganCardsFirstPlayer = _mulliganCards.Where(x => x._side == Players.First).ToList();
            _mulliganCardsSecondPlayer = _mulliganCards.Where(x => x._side == Players.Second).ToList();
            foreach (var card in _mulliganCards)
                card.gameObject.SetActive(true);

            MulliganStage1(Players.First);
            await UniTask.Delay(TimeSpan.FromSeconds(_time * 4));
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

                var newViewCardInHand = _card.GetComponent<Card_View>(); // ??????? ?????????? ??? ?????????? ???????
                newViewCardInHand.CardShirtEnable(false);//???????? ??????????? ????????

                var coll = position.GetComponent<Collider>();
                coll.enabled = true;
                position.SetCurrentCard(_card);//??????????? ????? ? ???????
                _ = _card.MoveCardAsync(_card.transform, position.transform, _time);
                await UniTask.Delay(TimeSpan.FromSeconds(_time));
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
                    await UniTask.Delay(TimeSpan.FromSeconds(_time));
                }
                i++;
            }
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
                    await UniTask.Delay(TimeSpan.FromSeconds(_time));
                }

                position.SwitchRenderer(false);
                i++;
            }
            await UniTask.Delay(TimeSpan.FromSeconds(_time));
            i = 0;
            foreach (MulliganCardPosition position in _mulliganCardsPositions)
            {
                _card = _currentCards[i];
                _ = _card.MoveCardAsync(position.transform.position, _currentHand.GetLastCardPosition(), _time);

                await UniTask.Delay(TimeSpan.FromSeconds(_time));
                if (side == Players.Second)
                    position.gameObject.SetActive(false);

                var _cardInHand = _card.GetComponent<Card>();
                _cardInHand.ChangeState(CardState.Hand);
                _cardInHand.SetSide(side);
                _cardInHand.SetParent(_currentHand);
                _currentHand.AddCard(_cardInHand);
                i++;
            }
            _currentBoard.InitializeCardsList(_currentCards);//???????? ??????? ???? ???????? ?????? ? ???????? ????

            if (side == Players.First)//???? ?????? ????????
            {
                MulliganStage1(Players.Second);
                MulliganStage2();
                _mulliganConfirmButton.onClick.RemoveAllListeners();
                _mulliganConfirmButton.onClick.AddListener(delegate { MulliganStage3(Players.Second); });
            }
        }
        /// <summary>
        /// ?????? ????? ????? ?? ?????? ? ???? ???????? ??????
        /// </summary>
        /// <param name="side">??????? ?????</param>
        public void TakeOneCard(Players side)
        {

            Hand _currentHand;
            int _nextCardInDeckNumber;
            //List<BattleModeCard> _currentDeck;
            if (side == Players.First)
            {
                _currentHand = _firstPlayerHand;
                // _currentDeck = _mulliganCardsFirstPlayer;
                _nextCardInDeckNumber = _nextCardInPlayerDeckNumber;
                _nextCardInPlayerDeckNumber++;

            }
            else
            {
                _currentHand = _secondPlayerHand;
                // _currentDeck = _mulliganCardsSecondPlayer;
                _nextCardInDeckNumber = _nextCardInEnemyDeckNumber;
                _nextCardInEnemyDeckNumber++;
            }

            /*if (_nextCardInDeckNumber > _currentDeck.Count - 1)
            {
                DealFatigueDamage(side);
                Debug.Log("? ?????? " + side + " ?? ???????? ????");
                return;
            }*/
            // TakeOneRandomCard(side);
            // var card = _currentDeck.ElementAt(_nextCardInDeckNumber);
            
            var card = TakeOneRandomCard(side);
            if (card == null)
            {
                return;
            }

            if (_currentHand.CountCards() > 9)
            {
                Debug.Log(card + " ??????????");
                card.gameObject.SetActive(false);
                return;
            }

            _ = card.GetComponent<BattleModeCard>().MoveCardAsync(card.transform.position, _currentHand.GetLastCardPosition(), _time);

            //var _cardInHand = card.GetComponent<Card>();

            var newViewCardInHand = card.GetComponent<Card_View>(); // ??????? ?????????? ??? ?????????? ???????
            newViewCardInHand.CardShirtEnable(false);//???????? ??????????? ????????

            card.ChangeState(CardState.Hand);
            card.SetSide(side);
            card.SetParent(_currentHand);
            _currentHand.AddCard(card);
        }
        public Card TakeOneRandomCard(Players side)
        {
            List<Card> _currentDeck = new();
            if (side == Players.First)
            {
                _currentDeck = FindObjectsOfType<Card>().Where(x => x.GetState() == CardState.Deck && x._side == Players.First).ToList();
            }
            else
            {
                _currentDeck = FindObjectsOfType<Card>().Where(x => x.GetState() == CardState.Deck && x._side == Players.Second).ToList();
            }

            if (_currentDeck.Count <= 0)
            {
                DealFatigueDamage(side);
                Debug.Log("? ?????? " + side + " ?? ???????? ????");
                return null;
            }

            Debug.Log("???? ? ??????: " + _currentDeck.Count);
            int _random = UnityEngine.Random.Range(0, _currentDeck.Count);
            Debug.Log("????? ????????? ?????: " + _random);
            Debug.Log("???????? ????????? ?????: " + _currentDeck.ElementAt(_random).GetComponent<Card_Model>()._nameCard);
            return _currentDeck.ElementAt(_random);
        }

        private void DealFatigueDamage(Players side)
        {
            Hero_Controller hero = null;
            int damage = 0;
            switch (side)
            {
                case Players.First:
                    hero = _firstPlayerHero;
                    damage = _playerFatigueDamage;
                    _playerFatigueDamage++;
                    break;
                case Players.Second:
                    hero = _secondPlayerHero;
                    damage = _enemyFatigueDamage;
                    _enemyFatigueDamage++;
                    break;
                default:
                    break;
            }
            hero.ChangeHealthValue(damage, ChangeHealthType.DealDamage);
        }

        private void Update()//???? ?????? 
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