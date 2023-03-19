using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Hearthstone
{
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
        private VoiceHero_Controller _voiceHero_Controller;
        private WinnerMesage _winnerMesage;
        [SerializeField] private int _health;
        [SerializeField] private int _defaultHealtValue;
        [SerializeField] private Text _textHealth;
        [SerializeField] private Players _side;
        [SerializeField] private CardClasses _heroClass;
        public int Health { get => _health; set => _health = value; }        
        public int DefaultHealtValue { get => _defaultHealtValue; set => _defaultHealtValue = value; }
        public Text TextHealth { get => _textHealth; set => _textHealth = value; }
        public Players Side { get => _side; }
        public CardClasses HeroClass { get => _heroClass; }
        public Image _spriteHeroWinner;
        private void OnEnable()
        {
            _textHealth = GetComponentInChildren<TextHealthMarker>().GetComponent<Text>();
            _loadDeck_Controller.SetHeroSettings += SetHeroSettings;
            _voiceHero_Controller = FindObjectOfType<VoiceHero_Controller>();
            _winnerMesage = FindAnyObjectByType<WinnerMesage>();
        }

        private void OnDisable()
        {
            _loadDeck_Controller.SetHeroSettings -= SetHeroSettings;            
        }

        public void SetHeroSettings(int idCard)
        {
            HeroSO_Model heroSO_Model = (HeroSO_Model)_pageBook_Model._cardsDictionary[idCard];
            _health = heroSO_Model.HealthCard;
            _heroClass = heroSO_Model.HeroClass;
            _defaultHealtValue = _health;
            _textHealth.text = _health.ToString();
            _voiceHero_Controller.SetAudioVoiceHero();
        }

        public void ChangeHealthValue(int incomingValue, ChangeHealthType changeHealthType)
        {

            if (changeHealthType == ChangeHealthType.DealDamage)
            {
                _health -= incomingValue;

                //_singleEffect_Controller.ApplyEffect(this);
                if (_health <= 0)
                {
                    _winnerMesage.SetWiner(((Players)((int)(_side + 1) % 2)).ToString() + " Player" , _spriteHeroWinner);
                    Debug.Log("ÏÎÁÅÄÀ ÈÃÐÎÊÀ " + (Players)((int)(_side + 1) % 2));
                }                      
                
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
                        ChangeHealthValue(_battleCry_Controller.BattleCryChangeHealth, ChangeHealthType.DealDamage);
                        //_battleCry_Controller._targetBattleCry.gameObject.SetActive(false);
                        _battleCry_Controller.IsActiveCry = false;
                        //DrawHealth();
                        BattleCryOff();
                    }
                    if (battleCryType == BattleCryType.Heal)
                    {
                        ChangeHealthValue(_battleCry_Controller.BattleCryChangeHealth, ChangeHealthType.Healing);
                        //_battleCry_Controller._targetBattleCry.gameObject.SetActive(false);
                        _battleCry_Controller.IsActiveCry = false;
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

                    var attacker = _indicatorTarget.GetWatcher().GetComponent<Card_Model>();//ïðèâåñòè â ïîðÿäîê

                    if (board.HasMinionWithTaunt())
                    {
                        if (!attacker._isProvocation)
                        {
                            Debug.Log("Ìîæíî àòàêîâàòü òîëüêî ïðîâîêàòîðà");
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
            _battleCry_Controller.CursorBattleCry.ChangeCursorState(false);
            _battleCry_Controller.IsActiveCry = false;
        }
    }
}