using UnityEngine;
using UnityEngine.EventSystems;

namespace Hearthstone
{
    public class ChoiseCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Renderer _spriteRenderer;
        [SerializeField]
        private GameObject _zoomingCardTemplate;
        [SerializeField]
        private Vector3 _zoomingCardPosition;
        

        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<EmissionMarker>().gameObject.GetComponent<Renderer>();
            _zoomingCardPosition = FindObjectOfType<CardZoomingTemplateMarker>().gameObject.transform.position;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _spriteRenderer.gameObject.SetActive(true);
            _zoomingCardPosition.x  = gameObject.transform.position.x - 8;
            _zoomingCardTemplate.gameObject.SetActive(true);            
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _spriteRenderer.gameObject.SetActive(false);
            _zoomingCardTemplate.gameObject.SetActive(false);            
        }
    }
}