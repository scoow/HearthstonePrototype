using System;
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
        public int _abilityAddCard;

        public bool _isProvocation;
        public bool _isDivineShield;
        public bool _isPermanentEffect;
        public bool _isCharge;
        public bool _isBerserk;

        public SpriteRenderer _protectionImage;
        public Sprite _spriteCard;
        public string _descriptionCard;
        public string _nameCard;
        public int _idCard;    
        public int _manaCostCard;
        public int _atackDamageCard;
        public int _healthCard;
        public BattleCryType _battleCryType;


        private void OnEnable()
        {            
            _pageBook_Model = FindObjectOfType<PageBook_Model>();
        }


        public void SetCardSettings(int idCard)
        {
            CardSO_Model cardSOModel = _pageBook_Model._cardsDictionary[idCard];
            _abilityChangeAtackDamage = cardSOModel._abilityChangeAtackDamage;
            _abilityChangeSpellDamage = cardSOModel._abilityChangeSpellDamage;
            _abilityChangeHealth = cardSOModel._abilityChangeHealth;
            _abilityAddCard = cardSOModel._abilityAddCard;
            _isPermanentEffect = cardSOModel._isPermanentEffect;
            _isDivineShield = cardSOModel._isDivineShield;
            _isProvocation = cardSOModel._isProvocation;
            _isBerserk = cardSOModel._isBerserk;
            _isCharge = cardSOModel._isCharge;
            _atackDamageCard = cardSOModel._atackDamageCard;
            _descriptionCard = cardSOModel._descriptionCard;
            _manaCostCard = cardSOModel._manaCostCard;
            _healthCard = cardSOModel._healthCard;
            _spriteCard = cardSOModel._spriteCard;
            _nameCard = cardSOModel._nameCard;
            _idCard = cardSOModel._idCard;
            _battleCryType = cardSOModel._battleCryType;
        }
    }
}