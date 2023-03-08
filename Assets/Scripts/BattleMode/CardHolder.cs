using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Hearthstone
{
    /// <summary>
    /// Родительский класс для классов Hand и Board
    /// </summary>
    public class CardHolder : MonoBehaviour, IDropHandler
    {
        public Players _side;
        protected List<Card> _cardsList;//сделать serializable
        //protected Vector3 _lastPosition;

        //todo добавить ссылку на временную карту для корректной обработки взятия из руки
        private GameObject _tempCardGO;

        [SerializeField]
        protected float _offset;

        //событие для смены отображения карты перенесено в дочерний класс Board
        private void Start()
        {
            _cardsList = new();
            //_lastPosition= transform.position;
            _tempCardGO = GameObject.Find("TempCard");
        }
        /// <summary>
        /// получить позицию последней карты в руке
        /// </summary>
        /// <returns></returns>
        public virtual Vector3 GetLastCardPosition()
        {
            Vector3 newPosition = transform.position;
            newPosition += new Vector3(_cardsList.Count * _offset, 0, 0);
            //Destroy(tempGO);
            return newPosition;
        }
        /// <summary>
        /// добавить карту в руку
        /// </summary>
        public virtual void AddCard(Card card)
        {
            /*Transform newPosition = GetLastCardPosition();
            newPosition.position += new Vector3(_offset, 0);*/
            _cardsList.Add(card);
            //_cardCount++;
            card.SetParent(this);
        }
        /// <summary>
        /// убрать карту из руки
        /// </summary>
        public void RemoveCard(Card card)
        {
            if (_cardsList.Count > 0)
            {
                foreach (var c in _cardsList)//почему не работает?
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
            if (!eventData.pointerDrag.TryGetComponent<Card>(out var card)) return;
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