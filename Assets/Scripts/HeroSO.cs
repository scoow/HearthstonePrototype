using UnityEngine;


namespace Hearthstone
{
    [CreateAssetMenu(fileName = "New Hero", menuName = "HeroExample", order = 51)]
    public class HeroSO : ScriptableObject
    {       
        public HeroType _heroType;
        public int _healtHero;
        public int _armorHero;
        public Sprite _spriteHero;
        //public Sprite _spriteHeroAbility;
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