using UnityEngine;
using UnityEngine.EventSystems;

namespace Hearthstone
{
    public class CardInHand : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField]
        private Camera _camera;
        //private Vector3 _offset;
        private void Awake()
        {
            _camera = Camera.main;
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            //_offset = transform.position- _camera.ScreenToWorldPoint(eventData.position);
            GetComponent<CanvasGroup>().blocksRaycasts= false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector3 newPosition = eventData.position;
            newPosition.z = _camera.transform.position.z;

            newPosition = -_camera.ScreenToWorldPoint(newPosition);

            newPosition.z = -1;
            transform.position = newPosition;

            //����������� ������ ���� �������� �� -1
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            //Debug.Log("End drag");
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }
}