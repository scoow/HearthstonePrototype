using UnityEngine;
using UnityEngine.EventSystems;

namespace Hearthstone
{
    public class ChoiseCard_Controller : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        /// <summary>
        /// ��������� ���������� �����
        /// </summary>
        private Renderer _spriteEmission;      
        /// <summary>
        /// ������ �� ����� � ����������� Icreating
        /// </summary>
        private ICreating _contentDeckController;        
        /// <summary>
        /// ��������� ��������� �����
        /// </summary>
        private Card_Model _card_Model;        
        /// <summary>
        /// ��������� ����������� �����
        /// </summary>
        private Card_View _zommingCard_View;           
        private PageBook_Model _pageBook_Model;



        private void Awake()
        {
            _pageBook_Model = FindObjectOfType<PageBook_Model>();            
            _card_Model = GetComponent<Card_Model>();
            _zommingCard_View = FindObjectOfType<CardZoomingTemplateMarker>().GetComponentInChildren<Card_View>();           
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
            _zommingCard_View.gameObject.SetActive(true);
            _zommingCard_View.ChangeViewZomingCard(_card_Model._idCard);
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
                if (gameObject.CompareTag("CardTemplate"))
                    _contentDeckController.AddContent(_card_Model._idCard);
                if (gameObject.CompareTag("ChoiseCard"))
                {
                    _contentDeckController.RemoveContent(_card_Model._idCard);
                    Destroy(this.gameObject);
                    _zommingCard_View.gameObject.SetActive(false);
                }
            }            
        }
    }
}