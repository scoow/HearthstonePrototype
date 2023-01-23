using UnityEngine;
using UnityEngine.EventSystems;

namespace Hearthstone
{
    public class ChoiseCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        /// <summary>
        /// подсветка выделенной карты
        /// </summary>
        private Renderer _spriteEmission;
        /// <summary>
        /// увеличенный вариант выбранной карты
        /// </summary>
        [SerializeField]
        private GameObject _zoomingCard;
        /// <summary>
        /// параметры увеличенной карты
        /// </summary>
        private CardSettings _settingsZoomingCard;
        /// <summary>
        /// параметры выбранной карты
        /// </summary>
        private CardSettings _settingsChioseCard;
        /// <summary>
        /// ссылка на обект с интерфейсом Icreating
        /// </summary>
        private ICreating _creatingDeck;
        private IReadable _readableDictyonaryDeck;


        private void Awake()
        {
            if(gameObject.tag == "CardTemplate")
            {
                _spriteEmission = GetComponentInChildren<EmissionMarker>().gameObject.GetComponent<Renderer>();
            }
            _zoomingCard = FindObjectOfType<CardZoomingTemplateMarker>().gameObject;
            _settingsChioseCard = GetComponent<CardSettings>();
            _settingsZoomingCard = _zoomingCard.GetComponent<CardSettings>();
            _readableDictyonaryDeck = FindObjectOfType<PageBook>();
            _creatingDeck = FindObjectOfType<ContentDeck>();
        }
        private void Start()
        {
            _zoomingCard.gameObject.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            

            if (gameObject.tag == "CardTemplate")
            {
                _spriteEmission.gameObject.SetActive(true);              
            }
            _settingsZoomingCard.ManaCost.text = _readableDictyonaryDeck.GetCardSettingsInCardsDictionary(_settingsChioseCard.Id)._manaCost.ToString();
            _settingsZoomingCard.AtackDamage.text = _readableDictyonaryDeck.GetCardSettingsInCardsDictionary(_settingsChioseCard.Id)._atackDamage.ToString();
            _settingsZoomingCard.Healt.text = _readableDictyonaryDeck.GetCardSettingsInCardsDictionary(_settingsChioseCard.Id)._healt.ToString();
            _settingsZoomingCard.Description.text = _readableDictyonaryDeck.GetCardSettingsInCardsDictionary(_settingsChioseCard.Id)._description;
            _settingsZoomingCard.SpriteCard.sprite = _readableDictyonaryDeck.GetCardSettingsInCardsDictionary(_settingsChioseCard.Id)._spriteCard;  //_settingsChioseCard.SpriteCard.sprite;
            _settingsZoomingCard.Name.text = _readableDictyonaryDeck.GetCardSettingsInCardsDictionary(_settingsChioseCard.Id)._name;
            
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
                _creatingDeck.AddCardInDeck(_settingsChioseCard);
            else
                _creatingDeck.RemoveCardInDeck(_settingsChioseCard);
        }
    }
}