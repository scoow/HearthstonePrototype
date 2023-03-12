using Hearthstone;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class Hero_Controller : MonoBehaviour, IChangeHealth , IPointerClickHandler
{
    [Inject]
    private LoadDeck_Controller _loadDeck_Controller;
    [Inject]
    private PageBook_Model _pageBook_Model;
    [Inject]
    private BattleCry_Controller _battleCry_Controller;

    public int _health;
    public int _defaultHealtValue;
    public Text _textHealth;
    [SerializeField] private Players _side;
    public Players Side { get { return _side; } }
    private void OnEnable()
    {
        //_battleCry_Controller = FindObjectOfType<BattleCry_Controller>();//zenject
        //_pageBook_Model = FindObjectOfType<PageBook_Model>();//zenject
        //_loadDeck_Controller = FindObjectOfType<LoadDeck_Controller>();//zenject
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
        }
        DrawHealth();
    }
    


    public void OnPointerClick(PointerEventData eventData)
    {
        if(_battleCry_Controller.ÑonditionsTargetBattleCry())
        {
            foreach(BattleCryType battleCryType in _battleCry_Controller._currentBattleCryTypes)
            {
                if(battleCryType == BattleCryType.DealDamage)
                {
                    ChangeHealthValue(_battleCry_Controller._battleCryChangeHealth, ChangeHealthType.DealDamage);
                    //_battleCry_Controller._targetBattleCry.gameObject.SetActive(false);
                    _battleCry_Controller._isActiveCry = false;
                    _textHealth.text = _health.ToString();
                    _textHealth.color = Color.red;
                    BattleCryOff();
                }
                if(battleCryType == BattleCryType.Heal)
                {
                    ChangeHealthValue(_battleCry_Controller._battleCryChangeHealth, ChangeHealthType.Healing);
                    //_battleCry_Controller._targetBattleCry.gameObject.SetActive(false);
                    _battleCry_Controller._isActiveCry = false;
                    _textHealth.text = _health.ToString();
                    if (_health == _defaultHealtValue)
                        _textHealth.color = Color.white;
                    if (_health < _defaultHealtValue)
                        _textHealth.color = Color.red;
                    BattleCryOff();
                }
            }
            
        }        
    }

        private void DrawHealth()
        {
        _textHealth.text = _health.ToString();
        if (_health < _defaultHealtValue)
            _textHealth.color = Color.red;
        else
            _textHealth.color = Color.white;
        }

        private void BattleCryOff()
        {
            _battleCry_Controller._cursorBattleCry.ChangeCursorState(false);
            _battleCry_Controller._isActiveCry = false;
        }
}