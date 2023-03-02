using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hearthstone
{    
    public class Card_Model : MonoBehaviour
    {
        private PageBook_Model _pageBook_Model;
        // ////////////////////////////////////
        
        public int _abilityChangeHealth;
        public int _abilityChangeAtackDamage;
        public int _abilityChangeSpellDamage;
        public int _abilityAddCard; // GO переношу в список булевых переменных

        public bool _isProvocation;
        public bool _isDivineShield;
        public bool _isPermanentEffect;
        public bool _isCharge;
        public bool _isBerserk;
        public bool _isGetCard;
        public Dictionary<string,bool> _activeAbility = new Dictionary<string, bool>(); //список активных способностей

        public SpriteRenderer _protectionImage;
        public Sprite _spriteCard;
        public string _descriptionCard;
        public string _nameCard;
        public int _idCard;    
        public int _manaCostCard;
        public int _atackDamageCard;
        public int _healthCard;

        public int _maxHealtValue;
        public int _maxAtackValue;
        public int _defaultHealtValue;
        public int _defaultAtackValue;

        public List<BattleCryType> _battleCryTypes = new List<BattleCryType>();
        //public BattleCryType _battleCryType;        
        public BattleCryTargets _battleCryTargets;
        public MinionType _battleCryTargetsType;
        public MinionType _minionType;
        /// <summary>
        /// тип классовых карт , которые можно добавлять в колоду
        /// </summary>
        public Classes _cardClassInDeck;


        private void OnEnable()
        {   
            _pageBook_Model = FindObjectOfType<PageBook_Model>();
        }

        private void Start()
        {
            _defaultHealtValue = _maxHealtValue = _healthCard;
            _defaultAtackValue = _maxAtackValue = _atackDamageCard;
        }


        public void SetCardSettings(int idCard)
        {
            CardSO_Model cardSOModel = _pageBook_Model._cardsDictionary[idCard];
            _abilityChangeAtackDamage = cardSOModel._abilityChangeAtackDamage;
            _abilityChangeSpellDamage = cardSOModel._abilityChangeSpellDamage;
            _abilityChangeHealth = cardSOModel._abilityChangeHealth;
            _abilityAddCard = cardSOModel._abilityAddCard;          

            _atackDamageCard = cardSOModel._atackDamageCard;
            _descriptionCard = cardSOModel._descriptionCard;
            _manaCostCard = cardSOModel._manaCostCard;
            _healthCard = cardSOModel._healthCard;
            _spriteCard = cardSOModel._spriteCard;
            _nameCard = cardSOModel._nameCard;
            _idCard = cardSOModel._idCard;

            //_battleCryType = cardSOModel._battleCryType;
            foreach(BattleCryType cryType in cardSOModel._battleCryTypes)
            {
                _battleCryTypes.Add(cryType);
            }
            //_battleCryTypes = cardSOModel._battleCryTypes;
            _battleCryTargets = cardSOModel._battleCryTargets;
            _battleCryTargetsType = cardSOModel._battleCryTargetsType;
            _minionType = cardSOModel._minionType;
            _cardClassInDeck = cardSOModel._cardClass;

            _isPermanentEffect = cardSOModel._isPermanentEffect;
            _isDivineShield = cardSOModel._isDivineShield;
            _isProvocation = cardSOModel._isProvocation;
            _isBerserk = cardSOModel._isBerserk;
            _isCharge = cardSOModel._isCharge;
            _isGetCard = cardSOModel._isTakeCard;

            _activeAbility.Add("PermanentEffect",_isPermanentEffect);
            _activeAbility.Add("DivineShield",_isDivineShield);
            _activeAbility.Add("Provocation",_isProvocation);
            _activeAbility.Add("Berserk",_isBerserk); // вынести отдельно
            _activeAbility.Add("Charge",_isCharge);
            _activeAbility.Add("GetCard",_isGetCard);
        }
        public int GetManaCostCard()
        { return _manaCostCard; }
    }
}