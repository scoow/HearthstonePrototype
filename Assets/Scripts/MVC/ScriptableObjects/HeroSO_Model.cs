using UnityEngine;

namespace Hearthstone
{
    [CreateAssetMenu(fileName = "New Hero", menuName = "Card/HeroExample", order = 51)]
    public class HeroSO_Model : CardSO_Model
    {       
        public HeroType _heroType;      
    }
   
    public enum HeroType
    {
        War,
        Shaman,
        Rouge,
        Paladin,
        Hunter,
        Druid,
        Warlock,
        Mage,
        Priest
    }
}