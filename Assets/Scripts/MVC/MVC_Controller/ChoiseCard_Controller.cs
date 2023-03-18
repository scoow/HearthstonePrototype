using UnityEngine;
using UnityEngine.EventSystems;

namespace Hearthstone
{
    public class ChoiseCard_Controller : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        /// <summary>
        /// подсветка выделенной карты
        /// </summary>
        private Renderer _spriteEmission;      
        /// <summary>
        /// ссылка на обект с интерфейсом Icreating
        /// </summary>
        private ICreating _contentDeckController;        
        /// <summary>
        /// параметры выбранной карты
        /// </summary>
        private Card_Model _card_Model;        
        /// <summary>
        /// параметры увеличенной карты
        /// </summary>
        private Card_View _zommingCard_View;           
        private PageBook_Model _pageBook_Model;
        private ContentDeck_Model _contentDeck_Model;
        private Sound_Controller _soundController;


        private void Awake()
        {
            _contentDeck_Model = FindObjectOfType<ContentDeck_Model>();
            _pageBook_Model = FindObjectOfType<PageBook_Model>();            
            _card_Model = GetComponent<Card_Model>();
            _zommingCard_View = FindObjectOfType<CardZoomingTemplateMarker>().GetComponentInChildren<Card_View>();
            _soundController = FindAnyObjectByType<Sound_Controller>();
            if (gameObject.CompareTag("CardTemplate"))            
                _spriteEmission = GetComponentInChildren<EmissionMarker>().gameObject.GetComponent<Renderer>();           
            
            _contentDeckController = FindObjectOfType<ContentDeck_Controller>();
        }

        private void Start()
        {
            _zommingCard_View?.gameObject.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {            
            if (gameObject.CompareTag("CardTemplate"))
                _spriteEmission.gameObject.SetActive(true);
            _soundController.PlaySound(_soundController.CardShrink);
            _zommingCard_View.gameObject.SetActive(true);
            _zommingCard_View.ChangeViewZomingCard(_card_Model.IdCard);
        }

        public void OnPointerExit(PointerEventData eventData)
        {       
            if(eventData.pointerEnter != null)
            {
                if (gameObject.CompareTag("CardTemplate"))
                    _spriteEmission.gameObject.SetActive(false);

                _zommingCard_View.gameObject.SetActive(false);
            }                        
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(_pageBook_Model._createDeckState == CreateDeckState.CreateDeck)
            {
                if (gameObject.CompareTag("CardTemplate") && (_card_Model._cardClassInDeck == _contentDeck_Model.ClassHeroInDeck || _card_Model._cardClassInDeck == Classes.Universal))
                    _contentDeckController.AddContent(_card_Model.IdCard);
                    _soundController.PlaySound(_soundController.AddCardToDeck);
                if (gameObject.CompareTag("ChoiseCard"))
                {
                    _contentDeckController.RemoveContent(_card_Model.IdCard);
                    Destroy(this.gameObject);
                    _zommingCard_View.gameObject.SetActive(false);
                }
            }            
        }
    }
}