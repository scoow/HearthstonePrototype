using Hearthstone;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class Hero_Controller : MonoBehaviour, IChangeHealth, IPointerClickHandler
{
    [Inject]
    private LoadDeck_Controller _loadDeck_Controller;
    [Inject]
    private PageBook_Model _pageBook_Model;
    [Inject]
    private BattleCry_Controller _battleCry_Controller;
    [Inject]
    private IndicatorTarget _indicatorTarget;

    public int _health;
    public int _defaultHealtValue;
    public Text _textHealth;
    [SerializeField] private Players _side;
    public Players Side { get { return _side; } }
    private void OnEnable()
    {
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
            if (_health <= 0)
                Debug.Log("ÏÎÁÅÄÀ ÈÃÐÎÊÀ " + (Players)((int)(_side + 1) % 2));
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

        if (_battleCry_Controller.ÑonditionsTargetBattleCry())
        {
            foreach (BattleCryType battleCryType in _battleCry_Controller._currentBattleCryTypes)
            {
                if (battleCryType == BattleCryType.DealDamage)
                {
                    ChangeHealthValue(_battleCry_Controller._battleCryChangeHealth, ChangeHealthType.DealDamage);
                    //_battleCry_Controller._targetBattleCry.gameObject.SetActive(false);
                    _battleCry_Controller._isActiveCry = false;
                    //DrawHealth();
                    BattleCryOff();
                }
                if (battleCryType == BattleCryType.Heal)
                {
                    ChangeHealthValue(_battleCry_Controller._battleCryChangeHealth, ChangeHealthType.Healing);
                    //_battleCry_Controller._targetBattleCry.gameObject.SetActive(false);
                    _battleCry_Controller._isActiveCry = false;
                    //DrawHealth();
                    BattleCryOff();
                }
            }
        }
        else
        {
            if (_indicatorTarget.CursorEnabled)
            {
                var attacker = _indicatorTarget.GetWatcher().GetComponent<Card_Model>();
                ChangeHealthValue(attacker._atackDamageCard, ChangeHealthType.DealDamage);

                Card attackercard = attacker.GetComponent<Card>();
                if (attackercard.GetSide() == _side) return;

                _indicatorTarget.ChangeCursorState(false);

                attackercard.DisableAttack();
                //Attack(attackercard, this);
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