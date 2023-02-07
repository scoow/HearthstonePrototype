using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Hearthstone
{
    public class CardHolder : MonoBehaviour, IDropHandler
    {
        public Players _side;
        protected List<CardInHand> _cardsList;//������� serializable
        protected Vector3 _lastPosition;

        [SerializeField]
        protected float _offset;
        protected int _cardCount = 0;

        private void Start()
        {
            _cardsList = new();
        }
        /// <summary>
        /// �������� ������� ��������� ����� � ����
        /// </summary>
        /// <returns></returns>
        public Vector3 GetLastCardPosition()
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
        public void AddCard(CardInHand card)
        {
            Vector3 newPosition = GetLastCardPosition();
            newPosition.x += _offset;
            _cardsList.Add(card);
            _cardCount++;
        }
        /// <summary>
        /// ������ ����� �� ����
        /// </summary>
        public void RemoveCard(CardInHand card)
        {
            Debug.Log("card dragged");
            if (_cardCount > 0)
            {
                // _cardsList.Remove(,)//�������� �������� �� ��������
                _cardCount--;
                _cardsList.Remove(card);
            }
        }
        public void OnDrop(PointerEventData eventData)//������� � �����-��������
        {
            CardInHand card = eventData.pointerDrag.GetComponent<CardInHand>();

            if (card != null)
            {
                Debug.Log("drop card");
                card.transform.position = GetLastCardPosition();
                AddCard(card);
            }
        }
    }
}