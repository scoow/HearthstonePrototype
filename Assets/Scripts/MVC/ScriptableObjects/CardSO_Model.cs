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
        [SerializeField] private string _nameCard;
        [SerializeField] private string _descriptionCard;
        [SerializeField] private int _manaCostCard;
        [SerializeField] private int _atackDamageCard;
        [SerializeField] private int _healthCard;
        [SerializeField] private int _abilityChangeHealth;
        [SerializeField] private int _abilityChangeAtack;

        public string NameCard { get => _nameCard; }        
        public string DescriptionCard { get => _descriptionCard; }        
        public int ManaCostCard { get => _manaCostCard; }        
        public int AtackDamageCard { get => _atackDamageCard; }        
        public int HealthCard { get => _healthCard; }
        public int AbilityChangeHealth { get => _abilityChangeHealth; }
        public int AbilityChangeAtack { get => _abilityChangeAtack; }

        public Classes _cardClass;        
        public List<BattleCryType> _battleCryTypes = new List<BattleCryType>();
        public List<AbilityCurrentCard> _abilityInTargetBattleCry = new List<AbilityCurrentCard>();
        public Target _targets;
        public MinionType _targetsType;
        public MinionType _minionType;
       
        public bool _isProvocation;
        public bool _isPermanentEffect;
        public bool _isCharge;
        public bool _isBerserk;
        public bool _isTakeCard;               
    }
}