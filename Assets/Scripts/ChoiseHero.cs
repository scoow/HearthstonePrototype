using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Hearthstone
{
    public class ChoiseHero : MonoBehaviour , IPointerClickHandler
    {        
        public Image _choiseHeroSprite;
        //private Sprite _choiseHero;
        public HeroSO _heroSO;


        private void Start()
        {
            //_choiseHero = _choiseHeroSprite.GetComponent<Sprite>();
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("Выбор карты");
            _choiseHeroSprite.sprite = _heroSO._spriteHero;
            //_choiseHero ;
        }
    }

}