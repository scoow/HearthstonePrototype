using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

/*
 * �������� ������ ���������:
1. 3 ��� 4 ����� �������� �� ������
2. ����� ��������, ����� ����� ������
3. ��������� ����� ������, �� �� ����� �������� ��������� �� ������
4. ���������� ����� �������� � ����
 */

//todo ��������� ������ ��� ����� �� ���� ��������� �� ����, ��� ��������� �����

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
        private List<MulliganCardPosition> _mulliganCardsPositions;//����� ��� ���������� ����
        private List<BattleModeCard> _mulliganCards;//���� �����
        private List<BattleModeCard> _mulliganCardsFirstPlayer;
        private List<BattleModeCard> _mulliganCardsSecondPlayer;
        private MulliganConfirmButton _mulliganConfirmButton;
        private VoiceHero_Controller _voiceHero_Controller;
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

        [Inject]
        private EndTurnButton _endTurnButton;//������ ����� ����

        private int _playerFatigueDamage = 1;
        private int _enemyFatigueDamage = 1;

        private async void Start()
        {
            _voiceHero_Controller = FindObjectOfType<VoiceHero_Controller>();
            //await UniTask.Delay(TimeSpan.FromSeconds(0.1));//��������, ���� ���������������� �����
            _mulliganConfirmButton = FindObjectOfType<MulliganConfirmButton>();
            _mulliganConfirmButton.Initialize();
            _mulliganConfirmButton.HideButton();
            _mulliganConfirmButton.onClick.AddListener(delegate { MulliganStage3(Players.First); });

            _mulliganCardsPositions = new List<MulliganCardPosition>();
            _mulliganCardsPositions = FindObjectsOfType<MulliganCardPosition>().ToList();
            _mulliganCardsPositions.Sort((c1, c2) => string.Compare(c1.gameObject.name, c2.gameObject.name));//todo �������� ����������

            _mulliganCards = new List<BattleModeCard>();
            _mulliganCards = FindObjectsOfType<BattleModeCard>().ToList();
            _mulliganCardsFirstPlayer = _mulliganCards.Where(x => x._side == Players.First).ToList();
            _mulliganCardsSecondPlayer = _mulliganCards.Where(x => x._side == Players.Second).ToList();
            foreach (var card in _mulliganCards)
                card.gameObject.SetActive(true);

            _endTurnButton.GetComponent<Button>().enabled= false;

            MulliganStage1(Players.First);
            await UniTask.Delay(TimeSpan.FromSeconds(_time * 4));
            MulliganStage2();
        }

        private async void MulliganStage1(Players side)
        {
            BattleModeCard _card;
            Card card;
            //int i = 0;
            foreach (MulliganCardPosition position in _mulliganCardsPositions)
            {
                // _card = _currentDeck[i];
                card = TakeOneRandomCard(side);
/*                if (card == null)
                    return;*/
                _card = card.GetComponent<BattleModeCard>();//�����������
                card.ChangeState(CardState.Mulligan);

                var newViewCardInHand = _card.GetComponent<Card_View>(); // ������� ���������� ��� ���������� �������
                newViewCardInHand.CardShirtEnable(false);//�������� ����������� �������

                var coll = position.GetComponent<Collider>();
                coll.enabled = true;
                position.SetCurrentCard(card);//����������� ����� � �������
                _ = _card.MoveCardAsync(_card.transform, position.transform, _time);
                await UniTask.Delay(TimeSpan.FromSeconds(_time));
                //i++;
            }

        }

        private void MulliganStage2()
        {
            _mulliganConfirmButton.ShowButton();
        }

        private async void MulliganStage3(Players side)
        {
            _mulliganConfirmButton.HideButton();

            Transform _currentDeck;

            if (side == Players.First)
            {
                _currentDeck = _playerDeck.transform;
            }
            else
            {
                _currentDeck = _enemyDeck.transform;
            }

            foreach (MulliganCardPosition position in _mulliganCardsPositions)
            {
                //_card = _currentCards[i];
                if (position.Selected)
                {
                    //var card = _card.GetComponent<Card>();
                    var card = position.GetCurrentCard();
                    position.SetCurrentCard(null);
                    card.ChangeState(CardState.Deck);
                    _ = card.GetComponent<BattleModeCard>().MoveCardAsync(position.transform, _currentDeck, _time);
                    await UniTask.Delay(TimeSpan.FromSeconds(_time));
                }
            }
            MulliganStage4(side);
        }
        private async void MulliganStage4(Players side)
        {
            _mulliganConfirmButton.HideButton();

            Card card;
            Transform _currentDeck;
            Hand _currentHand;
            Board _currentBoard;
            if (side == Players.First)
            {
                _currentDeck = _playerDeck.transform;
                _currentHand = _firstPlayerHand;
                _currentBoard = _firstPlayerBoard;
            }
            else
            {
                _currentDeck = _enemyDeck.transform;
                _currentHand = _secondPlayerHand;
                _currentBoard = _secondPlayerBoard;
            }

            foreach (MulliganCardPosition position in _mulliganCardsPositions)
            {
                
                if (position.Selected)
                {
                    card = TakeOneRandomCard(side);//
                    var newViewCardInHand = card.GetComponent<Card_View>(); // ������� ���������� ��� ���������� �������
                    newViewCardInHand.CardShirtEnable(false);//�������� ����������� ��������
                    position.SetCurrentCard(card);
                    card.ChangeState(CardState.Mulligan);
                    _ = card.GetComponent<BattleModeCard>().MoveCardAsync(card.transform, position.transform, _time);
                    await UniTask.Delay(TimeSpan.FromSeconds(_time));
                }

                position.SwitchRenderer(false);
            }
            await UniTask.Delay(TimeSpan.FromSeconds(_time));

            foreach (MulliganCardPosition position in _mulliganCardsPositions)
            {
                card = position.GetCurrentCard();
                _ = card.GetComponent<BattleModeCard>().MoveCardAsync(position.transform.position, _currentHand.GetLastCardPosition(), _time);

                await UniTask.Delay(TimeSpan.FromSeconds(_time));
                if (side == Players.Second)
                    position.gameObject.SetActive(false);

                card.ChangeState(CardState.Hand);
                card.SetSide(side);
                card.SetParent(_currentHand);
                _currentHand.AddCard(card);
            }
            _currentBoard.InitializeCardsList(side);//�������� ������� ���� �������� ������ � �������� ����

            if (side == Players.First)//���� ������ ��������
            {
                MulliganStage1(Players.Second);
                MulliganStage2();
                _mulliganConfirmButton.onClick.RemoveAllListeners();
                _mulliganConfirmButton.onClick.AddListener(delegate { MulliganStage3(Players.Second); });
            }
            else
                _endTurnButton.GetComponent<Button>().enabled = true;
        }
        /// <summary>
        /// ������ ����� ����� �� ������ � ���� �������� ������
        /// </summary>
        /// <param name="side">������� �����</param>
        public void TakeOneCard(Players side)
        {

            Hand _currentHand;
            //List<BattleModeCard> _currentDeck;
            if (side == Players.First)
            {
                _currentHand = _firstPlayerHand;
                // _currentDeck = _mulliganCardsFirstPlayer;
            }
            else
            {
                _currentHand = _secondPlayerHand;
                // _currentDeck = _mulliganCardsSecondPlayer;

            }

            /*if (_nextCardInDeckNumber > _currentDeck.Count - 1)
            {
                DealFatigueDamage(side);
                Debug.Log("� ������ " + side + " �� �������� ����");
                return;
            }*/
            // TakeOneRandomCard(side);
            // var card = _currentDeck.ElementAt(_nextCardInDeckNumber);
            
            var card = TakeOneRandomCard(side);
            if (card == null)
            {
                return;
            }

            if (_currentHand.CardsCount() > 9)
            {
                Debug.Log(card + " ����������");
                card.gameObject.SetActive(false);
                return;
            }

            _ = card.GetComponent<BattleModeCard>().MoveCardAsync(card.transform.position, _currentHand.GetLastCardPosition(), _time);

            //var _cardInHand = card.GetComponent<Card>();

            var newViewCardInHand = card.GetComponent<Card_View>(); // ������� ���������� ��� ���������� �������
            newViewCardInHand.CardShirtEnable(false);//�������� ����������� ��������

            card.ChangeState(CardState.Hand);
            card.SetSide(side);
            card.SetParent(_currentHand);
            _currentHand.AddCard(card);
        }
        /// <summary>
        /// ������ ��������� ����� ������
        /// </summary>
        /// <param name="side"></param>
        /// <returns></returns>
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
                _voiceHero_Controller.PlayVoiceHero(TalkExample.ErrorNoCard, side);
                Debug.Log("� ������ " + side + " �� �������� ����");
                return null;
            }

            //Debug.Log("���� � ������: " + _currentDeck.Count);
            int _random = UnityEngine.Random.Range(0, _currentDeck.Count);
            //Debug.Log("����� ��������� �����: " + _random);
            //Debug.Log("�������� ��������� �����: " + _currentDeck.ElementAt(_random).GetComponent<Card_Model>()._nameCard);
            return _currentDeck.ElementAt(_random);
        }
        /// <summary>
        /// ������� ���� �� ��������� ������
        /// </summary>
        /// <param name="side"></param>
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

        private void Update()//���� ������ 
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