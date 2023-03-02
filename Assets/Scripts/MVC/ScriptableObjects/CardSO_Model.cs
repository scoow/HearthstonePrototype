using System.Collections.Generic;
using UnityEngine;

namespace Hearthstone
{
    /// <summary>
    /// ScriptableObject для хранения данных карты
    /// </summary>
    [CreateAssetMenu(fileName = "New Card", menuName = "Card/CardExample", order = 51)]
    public class CardSO_Model : ScriptableObject
    {
        public string _nameCard;
        public string _descriptionCard;
        public int _idCard;
        public Sprite _spriteCard;
        public int _manaCostCard;
        public int _atackDamageCard;
        public int _healthCard;

        public Classes _cardClass;
        //public BattleCryType _battleCryType;
        public List<BattleCryType> _battleCryTypes = new List<BattleCryType>();
        public BattleCryTargets _battleCryTargets;
        public MinionType _battleCryTargetsType;
        public MinionType _minionType;
       

        public bool _isProvocation;
        public bool _isDivineShield;
        public bool _isPermanentEffect;
        public bool _isCharge;
        public bool _isBerserk;
        public bool _isTakeCard;

        public int _abilityChangeHealth;
        public int _abilityChangeAtackDamage;
        public int _abilityChangeSpellDamage;
        public int _abilityAddCard;
    }
}