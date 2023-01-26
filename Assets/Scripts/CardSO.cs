using UnityEngine;

namespace Hearthstone
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card/CardExample", order = 51)]
    public class CardSO : ScriptableObject
    {
        public string _nameCard;
        public string _descriptionCard;        
        public int _idCard;
        public Sprite _spriteCard;
        public int _manaCostCard;
        public int _atackDamageCard;
        public int _healtCard;
    }
}