using System.Collections.Generic;
using UnityEngine;

namespace Hearthstone
{
    /// <summary>
    /// ScriptableObject для хранения данных карты
    /// </summary>
    [CreateAssetMenu(fileName = "New Card", menuName = "Card/CardExample", order = 51)]
    public class CardSO_Model : GameSO_Model
    {
        public string _nameCard;
        public string _descriptionCard;        
        public int _manaCostCard;
        public int _atackDamageCard;
        public int _healthCard;

        public Classes _cardClass;        
        public List<BattleCryType> _battleCryTypes = new List<BattleCryType>();
        public List<AbilityCurrentCard> _abilityInTargetBattleCry = new List<AbilityCurrentCard>();
        public Target _targets;
        public MinionType _targetsType;
        public MinionType _minionType;
       

        public bool _isProvocation;
        public bool _isDivineShield;
        public bool _isPermanentEffect;
        public bool _isCharge;
        public bool _isBerserk;
        public bool _isTakeCard;

        public int _abilityChangeHealth;
        public int _abilityChangeAtack;       
    }
}