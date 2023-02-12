using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

namespace Hearthstone
{
    public class Board : CardHolder, IPointerEnterHandler, IPointerExitHandler
    {
        private Camera _camera;
        private GameObject _tempMinionGO;
        private void Awake()
        {
            _camera = Camera.main;
            _tempMinionGO = GameObject.Find("TempMinion");
        }
        public override void AddCard(CardInHand card)
        {
            base.AddCard(card);
            EndDragCard?.Invoke(); ///событие для смены отображения карты
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
                newPosition.x += _cardCount * _offset;
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
                Debug.Log("drop card");
                _parent.RemoveCard(card);
                card.transform.position = GetLastCardPosition();
                AddCard(card);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Vector3 newPosition = eventData.position;
            newPosition.z = _camera.transform.position.z;
            newPosition = -_camera.ScreenToWorldPoint(newPosition);
            newPosition.z = -1;
            if (newPosition.x < 0)
            {
                _tempMinionGO.transform.position = GetLastCardPosition() + new Vector3(-_offset, 0, -1) * _cardCount;
            }
            else
            {
                _tempMinionGO.transform.position = GetLastCardPosition() + new Vector3(_offset, 0, -1) * _cardCount;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _tempMinionGO.transform.position = new Vector3(100, 0, 0);
        }
    }
}