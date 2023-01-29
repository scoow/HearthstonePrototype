using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Hearthstone
{
    public class ChoiseHero_View : MonoBehaviour, IPointerClickHandler
    {        
        public SpriteRenderer _currentHeroSprite;
        public Image _buttonChoiseHero;


        public HeroSO_Model _heroSO;

        private void Start()
        {
            _currentHeroSprite.gameObject.SetActive(false);
            _buttonChoiseHero.gameObject.SetActive(false);
        }
        public void OnPointerClick(PointerEventData eventData)
        {           
            _currentHeroSprite.gameObject.SetActive(true);
            _buttonChoiseHero.gameObject.SetActive(true);
            _currentHeroSprite.sprite = _heroSO._spriteCard;
        }       
    }
}