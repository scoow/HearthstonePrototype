using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Hearthstone
{
    public class CardInHand : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        private Camera _camera;
        private GameObject _tempCardGO;

        public Transform parent;

        /*public delegate void BeginDrag(CardInHand card);
        public event BeginDrag TellParentBeginDrag;*/
        public Action<bool> BeginDrag;

        private void Awake()
        {
            _camera = Camera.main;
            _tempCardGO = GameObject.Find("TempCard");

        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            _tempCardGO.transform.position = transform.position;//временная карта
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            //TellParentBeginDrag?.Invoke(this);
            BeginDrag?.Invoke(true);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector3 newPosition = eventData.position;
            newPosition.z = _camera.transform.position.z;

            newPosition = -_camera.ScreenToWorldPoint(newPosition);

            newPosition.z = -1;
            transform.position = newPosition;            
            //разобраться почему надо умножать на -1
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            CardHolder _parent = transform.parent.GetComponent<CardHolder>();
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
            BattleModeCard_View view = GetComponent<BattleModeCard_View>();
            //view._pa
        }
    }
}