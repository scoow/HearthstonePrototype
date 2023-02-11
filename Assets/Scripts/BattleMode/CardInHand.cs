using UnityEngine;
using UnityEngine.EventSystems;

namespace Hearthstone
{
    public class CardInHand : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField]
        private Camera _camera;
        private Transform _defaultTempCardParent;
        private GameObject _tempCardGO;

        public delegate void BeginDrag(CardInHand card);
        public event BeginDrag TellParentBeginDrag;

        private void Awake()
        {
            _camera = Camera.main;
            _tempCardGO = GameObject.Find("TempCard");
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            _tempCardGO.transform.position = transform.position;//временная карта
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            if (TellParentBeginDrag != null)
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
            //
            //_tempCardGO.transform.SetParent(GameObject.Find("PlayerDeck").transform);
            var _parent = transform.parent.GetComponent<CardHolder>();
            if (_parent is Hand)
            {
                transform.position = _tempCardGO.transform.position;
            }
            else
            {
               // _parent.RemoveCard(this);
            }
                

            _tempCardGO.transform.position = new Vector3(100, 0);
            //

        }
        public void SetParent(CardHolder cardHolder)
        {
            transform.parent = cardHolder.transform;
        }
    }
}