using UnityEngine;

namespace Hearthstone
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card/CardExample", order = 51)]
    public class CardSO : ScriptableObject
    {
        public string _name;
        public string _description;
        //public GameObject _cardPrefab; не нужное поле (скорее всего шаблон уже будет на сцене и мы будем подгруждать в него вариант SO по ID
        public int _id;
        public Sprite _spriteCard;
        public int _manaCost;
        public int _atackDamage;
        public int _healt;
    }
}