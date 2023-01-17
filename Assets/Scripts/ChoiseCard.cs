using UnityEngine;
using UnityEngine.EventSystems;

namespace Hearthstone
{
    public class ChoiseCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Renderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<EmissionMarker>().gameObject.GetComponent<Renderer>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _spriteRenderer.gameObject.SetActive(true);
            Debug.Log("Произошло наведение");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _spriteRenderer.gameObject.SetActive(false);
            Debug.Log("Произошло отведение");
        }
    }
}