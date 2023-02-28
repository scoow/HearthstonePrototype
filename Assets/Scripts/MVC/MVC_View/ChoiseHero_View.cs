using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Hearthstone
{
    public class ChoiseHero_View : MonoBehaviour, IPointerClickHandler
    {
        public SpriteRenderer _currentHeroSprite;
        public Image _buttonChoiseHero;
        private ContentDeck_Model _contentDeck_Model;

        public HeroSO_Model _heroSO;
        private int _cerrentIDHero;

        private void Start()
        {
            _currentHeroSprite.gameObject.SetActive(false);
            _buttonChoiseHero.gameObject.SetActive(false);
            _contentDeck_Model = FindObjectOfType<ContentDeck_Model>();
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            _currentHeroSprite.gameObject.SetActive(true);
            _buttonChoiseHero.gameObject.SetActive(true);
            _currentHeroSprite.sprite = _heroSO._spriteCard;
            _cerrentIDHero = _heroSO._idCard;
            _contentDeck_Model._classHeroInDeck = _heroSO._cardClass;
            AddHeroInDeck();
        }

        public void AddHeroInDeck()
        {
            if (_contentDeck_Model._contentDeck.Count > 0)
            {
                _contentDeck_Model._contentDeck.Clear();
                _contentDeck_Model._contentDeck.Add(_cerrentIDHero);                         
            }
            else
            {
                _contentDeck_Model._contentDeck.Add(_cerrentIDHero);                
            }
        }
    }
}