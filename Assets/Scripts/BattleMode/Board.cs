using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using System.Collections.Generic;
using System;
using Zenject;

namespace Hearthstone
{
    public class Board : CardHolder, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        private Camera _camera;
        [Inject]
        private TempMinion_Marker _tempMinionGO;
        [Inject]
        private Mana_Controller _mana_Controller;
        [Inject]
        private IndicatorTarget _indicatorTarget;
        private bool _draggingCard; //���� �� �����
        private bool _rightCard; //���������� ����� ������?

        public Action<Card> EndDragCard; ///������� ��� ����� ����������� �����

        private (Vector3, bool)[] _minionSlots;//����� ��� ��������� �������
        private void Awake()
        {
            _camera = Camera.main;
            CreateSlots();
        }

        private void CreateSlots()
        {
            _minionSlots = new (Vector3, bool)[7];
            _minionSlots[0].Item1 = transform.position - new Vector3(_offset * 3, 0, 0);
            _minionSlots[0].Item2 = false;
            //Debug.Log(_minionSlots[0].Item1);
            for (int i = 1; i < 7; i++)
            {
                _minionSlots[i].Item1 = _minionSlots[i - 1].Item1 + new Vector3(_offset, 0, 0);
                //Debug.Log(_minionSlots[i].Item1);
                _minionSlots[i].Item2 = false;
            }
        }

        /// <summary>
        /// �������� ���� ���� �� �������. ���� ����� ����� - ���������� ����� ��� ������ ����� �� ����
        /// </summary>
        public void InitializeCardsList(Players side)//todo ��������� ������
        {
            List<Card> cards = new();
            cards = FindObjectsOfType<Card>().Where(x => x._side == side).ToList();
            foreach (Card card in cards)
            {
                if (card != null)
                {
                    card.BeginDrag += ReactionToCardDragging;
                }
            }
        }
        private void ReactionToCardDragging(bool drag)//���� ���� ����� - �����������, ����� - ���
        {
            _draggingCard = drag;
        }
        public void EnableAttackForAllMinions()
        {
            foreach (Card card in _cardsList)
            {
                card.EnableAttack();
            }
        }
        public override void AddCard(Card card)
        {
/*            if (CardsCount() > 7) return;
*/
            base.AddCard(card);
            card.ChangeState(CardState.Board);

            if (!card.GetComponent<Card_Model>()._isCharge)//���� ��� ����� - ��������� ����������� ��������� �� ���� ����!!!!!!!!!!
                card.DisableAttack();
            ////////////////////////////////////////////////////////////////
            if (card.GetState() == CardState.Board)
                EndDragCard?.Invoke(card); ///������� ��� ����� ����������� �����
        }
        /// <summary>
        /// �������� ������� ��������� ����� � ����
        /// </summary>
        /// <returns></returns>
        public override Vector3 GetLastCardPosition()
        {
            Vector3 newPosition;
            newPosition = new Vector3(0, transform.position.y, transform.position.z);
            if (_cardsList.Count  > 0)
            {
                newPosition = _rightCard ? _cardsList.First(x => x.transform.position.x == _cardsList.Max(x => x.transform.position.x)).transform.position : _cardsList.First(x => x.transform.position.x == _cardsList.Min(x => x.transform.position.x)).transform.position;
                newPosition += _rightCard ? new Vector3(_offset, 0, 0) : new Vector3(-_offset, 0, 0);
            }
            return newPosition;
        }
        /// <summary>
        /// ���������� ����� �� ����
        /// </summary>
        /// <param name="eventData"></param>
        public override void OnDrop(PointerEventData eventData)//������� � �����-��������
        {
            if (!_draggingCard) return;
            if (!eventData.pointerDrag.TryGetComponent<Card>(out var card)) return;//���� ��� �� �����
            if (card.GetState() != CardState.Hand) return; //���� ����� �� �� ����
            if (CardsCount() >= 7)
            {
                Debug.Log("���� ��������");
                return;
            }
            var _parent = card.transform.parent.GetComponent<CardHolder>();
            if (_parent != this)
            {
                _parent.RemoveCard(card);

                ////////////////////////////////////////////////////////////////////////////////////////

                AddCard(card);
                ReorderMinions();

                _mana_Controller.SpendMana(_side, card.GetComponent<Card_Model>().GetManaCostCard());// ������� ����
                OnPointerExit(eventData);

            }
        }

        public void ReorderMinions()
        {
            int range = 3 - CardsCount() / 2;
            for (int i = 0; i < CardsCount(); i++)
            {
                _cardsList[i].transform.position = _minionSlots[range].Item1;
                range++;
            }
        }

        /// <summary>
        /// ��� ��������� ������� �� ���� ������ ����� ��� ������������ �����
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_draggingCard) return;

            Vector3 newPosition = eventData.position;
            newPosition.z = _camera.transform.position.z;
            newPosition = -_camera.ScreenToWorldPoint(newPosition);
            newPosition.z = -1;
            if (newPosition.x < 0)
            {
                _rightCard = false;
                _tempMinionGO.transform.position = GetLastCardPosition();
            }
            else
            {
                _rightCard = true;
                _tempMinionGO.transform.position = GetLastCardPosition();
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _tempMinionGO.transform.position = new Vector3(100, 0, 0);
        }
        private void OnDisable()
        {
            List<Card> _allCards;
            _allCards = new List<Card>();
            _allCards = FindObjectsOfType<Card>().ToList();
            foreach (Card card in _allCards)
            {
                if (card != null)
                    card.BeginDrag -= ReactionToCardDragging;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            DisableCursor();
        }

        private void DisableCursor()
        {
            if (_indicatorTarget.CursorEnabled)
            {
                _indicatorTarget.ChangeCursorState(false);
            }
        }
        /// <summary>
        /// ���� �� �� ����� ����������?
        /// </summary>
        /// <returns></returns>
        public bool HasMinionWithTaunt()
        {
            return _cardsList.Any(x => x.GetComponent<Card_Model>()._isProvocation);
        }
    }
}