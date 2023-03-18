using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hearthstone
{    
    public class Card_Model : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _protectionImage;
        private Sprite _spriteCard;
        private string _descriptionCard;
        private string _nameCard;
        private int _manaCostCard;
        private int _idCard;
        private int _atackDamageCard;
        private int _healthCard;
        private int _сhangeHealthValue;
        private int _changeAtackValue;
        private int _maxHealtValue;
        private int _maxAtackValue;
        private int _defaultHealtValue;
        private int _defaultAtackValue;

        public SpriteRenderer ProtectionImage { get => _protectionImage; }
        public Sprite SpriteCard { get => _spriteCard; set => _spriteCard = value; }
        public string DescriptionCard { get => _descriptionCard; set => _descriptionCard = value; }
        public string NameCard { get => _nameCard; set => _nameCard = value; }
        public int ManaCostCard { get => _manaCostCard; set => _manaCostCard = value; }
        public int IdCard { get => _idCard; set => _idCard = value; }
        public int DefaultHealtValue { get => _defaultHealtValue; set => _defaultHealtValue = value; }
        public int DefaultAtackValue { get => _defaultAtackValue; set => _defaultAtackValue = value; }
        public int AtackDamageCard { get => _atackDamageCard; set => _atackDamageCard = value; }
        public int HealthCard { get => _healthCard; set => _healthCard = value; }
        public int ChangeHealthValue { get => _сhangeHealthValue; set => _сhangeHealthValue = value; }
        public int MaxHealtValue { get => _maxHealtValue; set => _maxHealtValue = value; }
        public int ChangeAtackValue { get => _changeAtackValue; set => _changeAtackValue = value; }
        public int MaxAtackValue { get => _maxAtackValue; set => _maxAtackValue = value; }



        

        public bool _isProvocation;
        public bool _isPermanentEffect;
        public bool _isCharge;
        public bool _isBerserk;
        public bool _isGetCard;

        //////////////////////////////////////
        public Dictionary<AbilityCurrentCard, bool> _activeAbility = new(); //список активных способностей
        private PageBook_Model _pageBook_Model;        
        public List<BattleCryType> _battleCryTypes = new();
        public List<AbilityCurrentCard> _abilityInTargetBattleCry = new();             
        public Target _battleCryTargets;
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
            SetDefaultParameters(_healthCard, _atackDamageCard);
        }

        private void SetDefaultParameters(int healtValue, int atakValue)
        {
            _defaultHealtValue = _maxHealtValue = healtValue;
            _defaultAtackValue = _maxAtackValue = atakValue;
        }

        public void SetCardSettings(int idCard, bool isMinion)
        {
            CardSO_Model cardSOModel;
            if (isMinion)
            {
                cardSOModel = (CardSO_Model)_pageBook_Model._cardAssistDictionary[idCard];
            }
            else
            {
                cardSOModel = (CardSO_Model)_pageBook_Model._cardsDictionary[idCard];
            }
            SetDefaultParameters(cardSOModel.HealthCard, cardSOModel.AtackDamageCard);

            _changeAtackValue  = cardSOModel.AbilityChangeAtack;            
            _сhangeHealthValue  = cardSOModel.AbilityChangeHealth;
            _atackDamageCard = cardSOModel.AtackDamageCard;
            _descriptionCard = cardSOModel.DescriptionCard;
            _manaCostCard = cardSOModel.ManaCostCard;
            _healthCard = cardSOModel.HealthCard;
            _spriteCard = cardSOModel.SpriteCard;
            _nameCard = cardSOModel.NameCard;
            _idCard = cardSOModel.IdCard;
            
            foreach(BattleCryType cryType in cardSOModel._battleCryTypes)
            {
                _battleCryTypes.Add(cryType);
            }

            foreach(AbilityCurrentCard abilityInTarget in cardSOModel._abilityInTargetBattleCry)
            {
                _abilityInTargetBattleCry.Add(abilityInTarget);
            }
           
            _battleCryTargets = cardSOModel._targets;
            _battleCryTargetsType = cardSOModel._targetsType;
            _minionType = cardSOModel._minionType;
            _cardClassInDeck = cardSOModel._cardClass;

            _isPermanentEffect = cardSOModel._isPermanentEffect;            
            _isProvocation = cardSOModel._isProvocation;
            _isBerserk = cardSOModel._isBerserk;
            _isCharge = cardSOModel._isCharge;
            _isGetCard = cardSOModel._isTakeCard;            

            _activeAbility.Add(AbilityCurrentCard.PermanentEffect, _isPermanentEffect);
            _activeAbility.Add(AbilityCurrentCard.Provocation,_isProvocation);
            _activeAbility.Add(AbilityCurrentCard.Berserk,_isBerserk);
            _activeAbility.Add(AbilityCurrentCard.Charge,_isCharge);
            _activeAbility.Add(AbilityCurrentCard.GetCard,_isGetCard);
        }
        public int GetManaCostCard()
        { return _manaCostCard; }
    }
}