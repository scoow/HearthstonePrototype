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
        
        public int _сhangeHealthValue; //
        public int _changeAtackValue;       

        public bool _isProvocation;
        public bool _isDivineShield;
        public bool _isPermanentEffect;
        public bool _isCharge;
        public bool _isBerserk;
        public bool _isGetCard;
        public Dictionary<AbilityCurrentCard, bool> _activeAbility = new(); //список активных способностей

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
            SetDefaultParameters(cardSOModel._healthCard, cardSOModel._atackDamageCard);

            _changeAtackValue  = cardSOModel._abilityChangeAtack;            
            _сhangeHealthValue  = cardSOModel._abilityChangeHealth;                     

            _atackDamageCard = cardSOModel._atackDamageCard;
            _descriptionCard = cardSOModel._descriptionCard;
            _manaCostCard = cardSOModel._manaCostCard;
            _healthCard = cardSOModel._healthCard;
            _spriteCard = cardSOModel._spriteCard;
            _nameCard = cardSOModel._nameCard;
            _idCard = cardSOModel._idCard;
            
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
            _isDivineShield = cardSOModel._isDivineShield;
            _isProvocation = cardSOModel._isProvocation;
            _isBerserk = cardSOModel._isBerserk;
            _isCharge = cardSOModel._isCharge;
            _isGetCard = cardSOModel._isTakeCard;            

            _activeAbility.Add(AbilityCurrentCard.PermanentEffect, _isPermanentEffect);
            _activeAbility.Add(AbilityCurrentCard.DivineShield,_isDivineShield);
            _activeAbility.Add(AbilityCurrentCard.Provocation,_isProvocation);
            _activeAbility.Add(AbilityCurrentCard.Berserk,_isBerserk);
            _activeAbility.Add(AbilityCurrentCard.Charge,_isCharge);
            _activeAbility.Add(AbilityCurrentCard.GetCard,_isGetCard);
        }
        public int GetManaCostCard()
        { return _manaCostCard; }
    }
}