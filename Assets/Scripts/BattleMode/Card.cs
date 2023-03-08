using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Hearthstone
{
    public class Card : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler, IDropHandler
    {
        private Camera _camera;
        private LayerRenderUp _layersRenderUp;
        private TempCard_Marker _tempCardGO;

        private Transform parent;
        [Inject]
        private Mana_Controller _mana_Controller;

        private Players _side;
        private bool _cancelDrag;
        public Action<bool> BeginDrag;

        private CardState _card_State;

        public void ChangeState(CardState newState)
        {
            _card_State = newState;
        }
        public CardState GetState()
        {
            return _card_State;
        }
        private void Awake()
        {
            _camera = Camera.main;
            _tempCardGO = FindObjectOfType<TempCard_Marker>();
            _layersRenderUp = GetComponent<LayerRenderUp>();
            _side = GetComponent<BattleModeCard>().GetSide();

            ChangeState(CardState.Deck);//состояние по-умолчанию

            _mana_Controller = FindObjectOfType<Mana_Controller>();//Zenject не сработал. Почему?
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            _cancelDrag = _side != _mana_Controller.WhoMovesNow() || _mana_Controller.GetManaCount(_side) < GetComponent<Card_Model>().GetManaCostCard();
            if (_cancelDrag)//Если не наш ход - нельзя схватить карту
            {
                return;
            }

            _tempCardGO.gameObject.transform.position = transform.position;//временная карта
            _layersRenderUp.LayerUp(50);

            BeginDrag?.Invoke(true);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_cancelDrag)//Если не наш ход - нельзя схватить карту
            {
                return;
            }
            Vector3 newPosition = eventData.position;
            newPosition.z = _camera.transform.position.z;

            newPosition = -_camera.ScreenToWorldPoint(newPosition);

            newPosition.z = -1;
            transform.position = newPosition;
            //разобраться почему надо умножать на -1
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_cancelDrag)//Если не наш ход - нельзя схватить карту
            {
                return;
            }
            CardHolder _parent = transform.parent.GetComponent<CardHolder>();
            _layersRenderUp.LayerUp(-50);
            if (_parent is Hand)
            {
                transform.position = _tempCardGO.transform.position;
            }
            _tempCardGO.transform.position = new Vector3(100, 0);//Убираем временную карту за пределы экрана
            BeginDrag?.Invoke(false);
        }
        /// <summary>
        /// Установить родителя
        /// </summary>
        /// <param name="cardHolder">родитель</param>
        public void SetParent(CardHolder cardHolder)
        {
            parent = transform.parent = cardHolder.transform;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_card_State == CardState.Deck) return;
            _layersRenderUp.LayerUp(50);
            transform.localScale *= 1.2f;
            transform.position += new Vector3(0, 0, 5f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_card_State == CardState.Deck) return;
            _layersRenderUp.LayerUp(-50);
            transform.localScale /= 1.2f;
            transform.position -= new Vector3(0, 0, 5f);
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (!eventData.pointerDrag.TryGetComponent<Card>(out var attacker)) return;
            if (attacker.GetSide() == _side) return;
            Debug.Log("Атака");
        }

        public Players GetSide()
        {
            return _side;
        }
    }
}