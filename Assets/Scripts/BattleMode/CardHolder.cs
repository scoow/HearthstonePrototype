using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Hearthstone
{
    /// <summary>
    /// ������������ ����� ��� ������� Hand � Board
    /// </summary>
    public class CardHolder : MonoBehaviour, IDropHandler
    {
        public Players _side;
        protected List<CardInHand> _cardsList;//������� serializable
        protected Vector3 _lastPosition;

        //todo �������� ������ �� ��������� ����� ��� ���������� ��������� ������ �� ����

        [SerializeField]
        protected float _offset;
        [SerializeField]
        protected int _cardCount = 0;

        //������� ��� ����� ����������� ����� ���������� � �������� ����� Board

        private void Start()
        {
            _cardsList = new();
            _lastPosition= transform.position;
        }
        /// <summary>
        /// �������� ������� ��������� ����� � ����
        /// </summary>
        /// <returns></returns>
        public virtual Vector3 GetLastCardPosition()
        {
            if (_cardCount == 0)
                return transform.position;
            else
            {
                Vector3 newPosition = transform.position;
                newPosition.x += _cardCount * _offset;
                return newPosition;
            }
        }
        /// <summary>
        /// �������� ����� � ����
        /// </summary>
        public virtual void AddCard(CardInHand card)
        {
            Vector3 newPosition = GetLastCardPosition();
            newPosition.x += _offset;
            _cardsList.Add(card);
            _cardCount++;
            card.SetParent(this);
        }
        /// <summary>
        /// ������ ����� �� ����
        /// </summary>
        public void RemoveCard(CardInHand card)
        {
            if (_cardCount > 0)
            {
                _cardCount--;
                foreach (var c in _cardsList)//������ �� ��������?
                {
                    if (c.transform.position.x > card.transform.position.x)
                    {
                        Vector3 newPosition = c.transform.position;
                        newPosition.x -= _offset;
                        c.transform.position = newPosition;
                    }
                        
                }
                _cardsList.Remove(card);
            }
        }
        public virtual void OnDrop(PointerEventData eventData)
        {
            if (!eventData.pointerDrag.TryGetComponent<CardInHand>(out var card)) return;
            var _parent = card.transform.parent.GetComponent<CardHolder>();
            if (_parent != this)
            {
                Debug.Log("drop card");
                _parent.RemoveCard(card);
                card.transform.position = GetLastCardPosition();
                AddCard(card);                
            }
        }
    }
}