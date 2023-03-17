using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Hearthstone
{
    public class ChoiseHero_View : MonoBehaviour, IPointerClickHandler
    {
        private int _cerrentIDHero;
        private ContentDeck_Model _contentDeck_Model;
        public SpriteRenderer _currentHeroSprite;
        public Image _buttonChoiseHero;
        public HeroSO_Model _heroSO;        

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
            _currentHeroSprite.sprite = _heroSO.SpriteCard;
            _cerrentIDHero = _heroSO.IdCard;
            _contentDeck_Model.ClassHeroInDeck = _heroSO.HeroClass;
            AddHeroInDeck();
        }

        public void AddHeroInDeck()
        {
            if (_contentDeck_Model.ContentDeck.Count > 0)
            {
                _contentDeck_Model.ContentDeck.Clear();
                _contentDeck_Model.ContentDeck.Add(_cerrentIDHero);                         
            }
            else
            {
                _contentDeck_Model.ContentDeck.Add(_cerrentIDHero);                
            }
        }
    }
}