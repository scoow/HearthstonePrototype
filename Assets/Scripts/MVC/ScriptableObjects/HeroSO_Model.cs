using UnityEngine;

namespace Hearthstone
{
    [CreateAssetMenu(fileName = "New Hero", menuName = "Card/HeroExample", order = 51)]
    public class HeroSO_Model : GameSO_Model
    {
        [SerializeField] private CardClasses _heroClass;
        [SerializeField] private int _healthCard;

        public CardClasses HeroClass { get => _heroClass; }
        public int HealthCard { get => _healthCard;}
    }   
}