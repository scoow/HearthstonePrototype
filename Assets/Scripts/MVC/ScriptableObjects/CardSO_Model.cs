using UnityEngine;

namespace Hearthstone
{
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
        
        public BattleCryType _battleCryType;
        public BattleCryTargets _battleCryTargets;
        public MinionType _battleCryTargetsType;
        public MinionType _minionType;

        
        public bool _isProvocation;
        public bool _isDivineShield;
        public bool _isPermanentEffect;
        public bool _isCharge;
        public bool _isBerserk;


        public int _abilityChangeHealth;
        public int _abilityChangeAtackDamage;
        public int _abilityChangeSpellDamage;
        public int _abilityAddCard;

    }
}