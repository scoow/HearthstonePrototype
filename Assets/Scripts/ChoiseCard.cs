using UnityEngine;
using UnityEngine.EventSystems;

namespace Hearthstone
{
    public class ChoiseCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        /// <summary>
        /// ��������� ���������� �����
        /// </summary>
        private Renderer _spriteEmission;
        /// <summary>
        /// ����������� ������� ��������� �����
        /// </summary>        
        private GameObject _zoomingCard;
        /// <summary>
        /// ��������� ����������� �����
        /// </summary>
        private CardSettings _settingsZoomingCard;
        /// <summary>
        /// ��������� ��������� �����
        /// </summary>
        private CardSettings _settingsChioseCard;
        /// <summary>
        /// ������ �� ����� � ����������� Icreating
        /// </summary>
        private ICreating _creatingDeck;
        /// <summary>
        /// ������ �� ������ � ����������� IReadable
        /// </summary>
        private IReadable _readable;


        private void Awake()
        {
            if(gameObject.tag == "CardTemplate")
            {
                _spriteEmission = GetComponentInChildren<EmissionMarker>().gameObject.GetComponent<Renderer>();
            }
            _zoomingCard = FindObjectOfType<CardZoomingTemplateMarker>().GetComponentInChildren<CardSettings>().gameObject;
            _settingsChioseCard = GetComponent<CardSettings>();
            _settingsZoomingCard = _zoomingCard.GetComponent<CardSettings>();
            _readable = FindObjectOfType<PageBook>();
            _creatingDeck = FindObjectOfType<ContentDeck>();
        }
        private void Start()
        {
            _zoomingCard.gameObject.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            CardSO thisCardSettings = _readable.GetCard(_settingsChioseCard.Id);

            if (gameObject.tag == "CardTemplate")
            {
                _spriteEmission.gameObject.SetActive(true);              
            }
            _settingsZoomingCard.ManaCost.text = thisCardSettings._manaCostCard.ToString();
            _settingsZoomingCard.AtackDamage.text = thisCardSettings._atackDamageCard.ToString();
            _settingsZoomingCard.Healt.text = thisCardSettings._healtCard.ToString();
            _settingsZoomingCard.Description.text = thisCardSettings._descriptionCard;
            _settingsZoomingCard.SpriteCard.sprite = thisCardSettings._spriteCard;
            _settingsZoomingCard.Name.text = thisCardSettings._nameCard;
            
            _zoomingCard.gameObject.SetActive(true);            
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (gameObject.tag == "CardTemplate")
                _spriteEmission.gameObject.SetActive(false);
            _zoomingCard.gameObject.SetActive(false);            
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(gameObject.tag == "CardTemplate")
                _creatingDeck.AddContent(_settingsChioseCard.Id);
            if(gameObject.tag == "ChoiseCard")
            {
                _creatingDeck.RemoveContent(_settingsChioseCard.Id);
                Destroy(this.gameObject);
                //_zoomingCard.gameObject.SetActive(false);
            }
        }
    }
}