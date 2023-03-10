using Hearthstone;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Hero_Controller : MonoBehaviour, IChangeHealth , IPointerClickHandler
{
    private LoadDeck_Controller _loadDeck_Controller;
    private PageBook_Model _pageBook_Model;
    private BattleCry_Controller _battleCry_Controller;

    public int _health;
    public int _defaultHealtValue;
    public Text _textHealth;



    private void OnEnable()
    {
        _battleCry_Controller = FindObjectOfType<BattleCry_Controller>();
        _pageBook_Model = FindObjectOfType<PageBook_Model>();
        _loadDeck_Controller = FindObjectOfType<LoadDeck_Controller>();
        _textHealth = GetComponentInChildren<TextHealthMarker>().GetComponent<Text>();
        _loadDeck_Controller.SetHeroSettings += SetHeroSettings;

    }

    private void OnDisable()
    {
        _loadDeck_Controller.SetHeroSettings -= SetHeroSettings;
    }


    public void SetHeroSettings(int idCard)
    {
        HeroSO_Model heroSO_Model = (HeroSO_Model)_pageBook_Model._cardsDictionary[idCard];
        _health = heroSO_Model._healthCard;
        _defaultHealtValue = _health;
        _textHealth.text = _health.ToString();

    }

    public void ChangeHealthValue(int incomingValue, ChangeHealthType changeHealthType)
    {

        if (changeHealthType == ChangeHealthType.DealDamage)
        {
            _health -= incomingValue;
           
            //_singleEffect_Controller.ApplyEffect(this);
            //if (_health <= 0)
                //DiedCreature(); //ñîáûòèå ñìåðòè*/
        }

        if (changeHealthType == ChangeHealthType.Healing)
        {
            _health += incomingValue;
            if (_health > _defaultHealtValue)
                _health = _defaultHealtValue;
            /*_singleEffect_Controller.ApplyEffect(this);
            StartCoroutine(_battleModeCardView.EffectParticle(_battleModeCardView._healtEffect));
            _battleModeCardView.UpdateViewCard();*/
        }
    }

    /*public void ChangeHealtValue(int incomingValue)
    {
        _health += incomingValue;
    }*/

    public void OnPointerClick(PointerEventData eventData)
    {
        if(_battleCry_Controller.ÑonditionsTargetBattleCry())
        {
            foreach(BattleCryType battleCryType in _battleCry_Controller._currentBattleCryTypes)
            {
                if(battleCryType == BattleCryType.DealDamage)
                {
                    ChangeHealthValue(_battleCry_Controller._battleCryChangeHealth, ChangeHealthType.DealDamage);
                    _textHealth.text = _health.ToString();
                    _textHealth.color = Color.red;
                    _battleCry_Controller._targetBattleCry.gameObject.SetActive(false);
                    _battleCry_Controller._isActiveCry = false;
                }
                if(battleCryType == BattleCryType.Heal)
                {
                    ChangeHealthValue(_battleCry_Controller._battleCryChangeHealth, ChangeHealthType.Healing);
                    _textHealth.text = _health.ToString();
                    if (_health == _defaultHealtValue)
                        _textHealth.color = Color.white;
                    if (_health < _defaultHealtValue)
                        _textHealth.color = Color.red;
                    _battleCry_Controller._targetBattleCry.gameObject.SetActive(false);
                    _battleCry_Controller._isActiveCry=false;
                }
            }
            
        }        
    }
}