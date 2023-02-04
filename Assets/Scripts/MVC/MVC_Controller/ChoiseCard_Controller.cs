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
        private ICreating _cardController;
        /// <summary>
        /// параметры выбранной карты
        /// </summary>
        private CardSettings_Model _settingsChioseCard;        
        /// <summary>
        /// увеличенный вариант выбранной карты
        /// </summary>        
        private GameObject _zoomingCard;
        /// <summary>
        /// параметры увеличенной карты
        /// </summary>
        private CardSettings_Model _settingsZoomingCard;
        /// <summary>
        /// ссылка на View компонент
        /// </summary>
        private ChoiseCard_View _choiseCardView;
        private PageBook_Model _pageBookModel;



        private void Awake()
        {
            _pageBookModel = FindObjectOfType<PageBook_Model>();
            _choiseCardView = GetComponent<ChoiseCard_View>();
            _settingsChioseCard = GetComponent<CardSettings_Model>();
            _zoomingCard = FindObjectOfType<CardZoomingTemplateMarker>().GetComponentInChildren<CardSettings_Model>().gameObject;
            _settingsZoomingCard = _zoomingCard.GetComponent<CardSettings_Model>();
            if(gameObject.tag == "CardTemplate")            
                _spriteEmission = GetComponentInChildren<EmissionMarker>().gameObject.GetComponent<Renderer>();           
            
            _contentDeckController = FindObjectOfType<ContentDeck_Controller>();
        }

        private void Start()
        {           
            _zoomingCard.gameObject.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {            
            if (gameObject.tag == "CardTemplate")
                _spriteEmission.gameObject.SetActive(true);
                _choiseCardView.ChangeViewCard(_settingsZoomingCard, _settingsChioseCard.Id);            
                _zoomingCard.gameObject.SetActive(true);                                 
        }

        public void OnPointerExit(PointerEventData eventData)
        {       
            if(eventData.pointerEnter != null)
            {
                if (gameObject.tag == "CardTemplate")
                    _spriteEmission.gameObject.SetActive(false);                
                    _zoomingCard.gameObject.SetActive(false);
            }                        
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(_pageBookModel._createDeckState == CreateDeckState.CreateDeck)
            {
                if (gameObject.tag == "CardTemplate")
                    _contentDeckController.AddContent(_settingsChioseCard.Id);
                if (gameObject.tag == "ChoiseCard")
                {
                    _contentDeckController.RemoveContent(_settingsChioseCard.Id);
                    Destroy(this.gameObject);                    
                    _zoomingCard.gameObject.SetActive(false);
                }
            }            
        }
    }
}