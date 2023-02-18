using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using Unity.VisualScripting;
using System.Collections.Generic;

namespace Hearthstone
{
    public class Board : CardHolder, IPointerEnterHandler, IPointerExitHandler
    {
        private Camera _camera;
        private GameObject _tempMinionGO;
        private bool _draggingCard; //несём ли карту
        private bool _rightCard; //добавление карты справа?

        private List<CardInHand> _allCards;

        private void Awake()
        {
            _camera = Camera.main;
            _tempMinionGO = GameObject.Find("TempMinion");

            
        }
        public void InitCardList()
        {
             _allCards = FindObjectsOfType<CardInHand>().ToList();
            foreach (CardInHand card in _allCards)
            {
                card.BeginDrag += ReactionToCardDragging;
            }
        }
        public void ReactionToCardDragging(bool drag)//если несём карту - реагировать, иначе - нет
        {
            _draggingCard = drag;
        }
        public override void AddCard(CardInHand card)
        {
            Debug.Log("1 : " + this);
            base.AddCard(card);

            EndDragCard?.Invoke(transform); ///событие для смены отображения карты
        }
        /// <summary>
        /// получить позицию последней карты в руке
        /// </summary>
        /// <returns></returns>
        public override Vector3 GetLastCardPosition()//переделать
        {
            Vector3 center = transform.position;
            center.x = 0;
            if (_cardCount == 0)
                return center;
            else
            {

                Vector3 newPosition = center;
                newPosition.x = _rightCard ? _cardsList.Max(x => x.transform.position.x) : _cardsList.Min(x => x.transform.position.x);
                newPosition.x += _rightCard ? _offset : -_offset;
                return newPosition;
            }
        }
        public override void OnDrop(PointerEventData eventData)//вынести в класс-родитель
        {
            CardInHand card = eventData.pointerDrag.GetComponent<CardInHand>();
            if (card == null) return;
            var _parent = card.transform.parent.GetComponent<CardHolder>();
            if (_parent != this)
            {
                //Debug.Log("drop card");
                _parent.RemoveCard(card);
                card.transform.position = GetLastCardPosition();
                AddCard(card);
            }
        }

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
                _tempMinionGO.transform.position = GetLastCardPosition();// + new Vector3(-_offset, 0, -1);
            }
            else
            {
                _rightCard = true;
                _tempMinionGO.transform.position = GetLastCardPosition();// + new Vector3(_offset, 0, -1);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _tempMinionGO.transform.position = new Vector3(100, 0, 0);
        }
        private void OnDisable()
        {
            foreach (CardInHand card in _allCards)
            {
                if (card != null)
                    card.BeginDrag -= ReactionToCardDragging;
            }
        }
    }
}