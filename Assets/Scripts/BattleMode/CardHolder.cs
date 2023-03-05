using System.Collections.Generic;
using Unity.VisualScripting;
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
        //protected Vector3 _lastPosition;

        //todo �������� ������ �� ��������� ����� ��� ���������� ��������� ������ �� ����
        private GameObject _tempCardGO;

        [SerializeField]
        protected float _offset;

        //������� ��� ����� ����������� ����� ���������� � �������� ����� Board
        private void Start()
        {
            _cardsList = new();
            //_lastPosition= transform.position;
            _tempCardGO = GameObject.Find("TempCard");
        }
        /// <summary>
        /// �������� ������� ��������� ����� � ����
        /// </summary>
        /// <returns></returns>
        public virtual Transform GetLastCardPosition()
        {
            Transform newPosition = new GameObject().transform;
            newPosition.position = transform.position + new Vector3(_cardsList.Count * _offset, 0, 0);
            newPosition.rotation = Quaternion.identity;
            newPosition.localScale = Vector3.one;
            return newPosition;

        }
        /// <summary>
        /// �������� ����� � ����
        /// </summary>
        public virtual void AddCard(CardInHand card)
        {
            /*Transform newPosition = GetLastCardPosition();
            newPosition.position += new Vector3(_offset, 0);*/
            _cardsList.Add(card);
            //_cardCount++;
            card.SetParent(this);
        }
        /// <summary>
        /// ������ ����� �� ����
        /// </summary>
        public void RemoveCard(CardInHand card)
        {
            if (_cardsList.Count > 0)
            {
                foreach (var c in _cardsList)//������ �� ��������?
                {
                    if (c.transform.position.x > _tempCardGO.transform.position.x)
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
                card.transform.position = GetLastCardPosition().position;
                AddCard(card);                
            }
        }
    }
}