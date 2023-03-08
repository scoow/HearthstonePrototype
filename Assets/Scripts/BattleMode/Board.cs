using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using System.Collections.Generic;
using System;
using Zenject;

namespace Hearthstone
{
    public class Board : CardHolder, IPointerEnterHandler, IPointerExitHandler
    {
        private Camera _camera;
        [Inject]
        private TempMinion_Marker _tempMinionGO;
        [Inject]
        private Mana_Controller _mana_Controller;
        private bool _draggingCard; //���� �� �����
        private bool _rightCard; //���������� ����� ������?

        public Action<Transform> EndDragCard; ///������� ��� ����� ����������� �����

        private void Awake()
        {
            _camera = Camera.main;
            //_tempMinionGO = GameObject.Find("TempMinion");
        }
        /// <summary>
        /// �������� ���� ���� �� �������. ���� ����� ����� - ���������� ����� ��� ������ ����� �� ����
        /// </summary>
        public void InitCardList(List<BattleModeCard> BattleModeCards)
        {
            foreach (BattleModeCard BattleModeCard in BattleModeCards)
            {
                CardInHand card = BattleModeCard.GetComponent<CardInHand>();
                if (card != null)
                {
                    card.BeginDrag += ReactionToCardDragging;
                }
            }
        }
        public void ReactionToCardDragging(bool drag)//���� ���� ����� - �����������, ����� - ���
        {
            _draggingCard = drag;
        }
        public override void AddCard(CardInHand card)
        {
            if (_cardsList.Count > 7) return;

            base.AddCard(card);
            var _cardInHand = card.gameObject.AddComponent<Minion>();
            EndDragCard?.Invoke(transform); ///������� ��� ����� ����������� �����
        }
        /// <summary>
        /// �������� ������� ��������� ����� � ����
        /// </summary>
        /// <returns></returns>
        public override Vector3 GetLastCardPosition()
        {
            Vector3 newPosition;
            newPosition = new Vector3(0, transform.position.y, transform.position.z);
            if (_cardsList.Count ==0)
            {
                
            }
            else
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
            if (!eventData.pointerDrag.TryGetComponent<CardInHand>(out var card)) return;
            var _parent = card.transform.parent.GetComponent<CardHolder>();
            if (_parent != this)
            {
                _parent.RemoveCard(card);
                card.transform.position = GetLastCardPosition();
                
                if (_rightCard)
                {
                    foreach (var c in _cardsList)
                    {
                        c.transform.position -= new Vector3(_offset/2 , 0, 0);
                    }
                }
                else
                {
                    foreach (var c in _cardsList)
                    {
                        c.transform.position += new Vector3(_offset/2 , 0, 0);
                    }
                }
                _mana_Controller.SpendMana(_side, card.GetComponent<Card_Model>().GetManaCostCard());// ������� ����
                AddCard(card);
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
            List<CardInHand> _allCards;
            _allCards = new List<CardInHand>();
            _allCards = FindObjectsOfType<CardInHand>().ToList();
            foreach (CardInHand card in _allCards)
            {
                if (card != null)
                    card.BeginDrag -= ReactionToCardDragging;
            }
        }
    }
}