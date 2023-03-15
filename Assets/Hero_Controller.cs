using Hearthstone;
using System.Linq;
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
    private WinnerMesage _winnerMesage;

    public int _health;
    public int _defaultHealtValue;
    public Text _textHealth;
    [SerializeField] private Players _side;
    public Players Side { get { return _side; } }
    private void OnEnable()
    {
        _textHealth = GetComponentInChildren<TextHealthMarker>().GetComponent<Text>();
        _loadDeck_Controller.SetHeroSettings += SetHeroSettings;
        _winnerMesage = FindAnyObjectByType<WinnerMesage>();
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
                _winnerMesage.SetWiner(((Players)((int)(_side + 1) % 2)).ToString());
                Debug.Log("ПОБЕДА ИГРОКА " + (Players)((int)(_side + 1) % 2));
            //DiedCreature(); //событие смерти*/
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

        if (_battleCry_Controller.СonditionsTargetBattleCry())
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
                Board board = FindObjectsOfType<Board>().Where(board => board._side == _side).FirstOrDefault();

                var attacker = _indicatorTarget.GetWatcher().GetComponent<Card_Model>();//привести в порядок

                if (board.HasMinionWithTaunt())
                {
                    if (!attacker._isProvocation)
                    {
                        Debug.Log("Можно атаковать только провокатора");
                        return;
                    }
                }

                
                
                Card attackercard = attacker.GetComponent<Card>();
                if (attackercard.GetSide() == _side) return;

                _indicatorTarget.ChangeCursorState(false);

                attackercard.DisableAttack();
                attackercard.Attack(attackercard, this);
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