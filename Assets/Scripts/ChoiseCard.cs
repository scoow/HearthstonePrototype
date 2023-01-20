using UnityEngine;
using UnityEngine.EventSystems;

namespace Hearthstone
{
    public class ChoiseCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Renderer _spriteEmission;
        [SerializeField]
        private GameObject _zoomingCard;
        private CardSettings _settingsZoomingCard;
        private CardSettings _settingsChioseCard;
        //[SerializeField]
        //private Vector3 _zoomingCardPosition;
        

        private void Awake()
        {
            _spriteEmission = GetComponentInChildren<EmissionMarker>().gameObject.GetComponent<Renderer>();
            _settingsChioseCard = GetComponent<CardSettings>();
            _settingsZoomingCard = _zoomingCard.GetComponent<CardSettings>();
            _zoomingCard.gameObject.SetActive(false);
            //_zoomingCardPosition = FindObjectOfType<CardZoomingTemplateMarker>().gameObject.transform.position;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _spriteEmission.gameObject.SetActive(true);
            //_zoomingCardPosition.x  = gameObject.transform.position.x - 8;
            _settingsZoomingCard.SpriteCard.sprite = _settingsChioseCard.SpriteCard.sprite;
            _settingsZoomingCard.Name.text = _settingsChioseCard.Name.text;
            _settingsZoomingCard.ManaCost.text = _settingsChioseCard.ManaCost.text;
            _settingsZoomingCard.AtackDamage.text = _settingsChioseCard.AtackDamage.text;
            _settingsZoomingCard.Healt.text = _settingsChioseCard.Healt.text;
            _settingsZoomingCard.Description.text = _settingsChioseCard.Description.text;
            _zoomingCard.gameObject.SetActive(true);            
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _spriteEmission.gameObject.SetActive(false);
            _zoomingCard.gameObject.SetActive(false);            
        }
    }
}