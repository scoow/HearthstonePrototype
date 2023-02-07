using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Hearthstone
{
    public class CardInHand : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField]
        private Camera _camera;

        public delegate void BeginDrag(CardInHand card);
        public event BeginDrag TellParentBeginDrag;

        private void Awake()
        {
            _camera = Camera.main;
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            GetComponent<CanvasGroup>().blocksRaycasts= false;
            if (TellParentBeginDrag != null )
            {
                TellParentBeginDrag(this);
            }
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
            //Debug.Log("End drag");
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        public void SetParent(CardHolder cardHolder)
        {
            transform.parent = cardHolder.transform;
        }
    }
}