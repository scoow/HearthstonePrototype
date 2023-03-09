using UnityEngine;

namespace Hearthstone
{
    [CreateAssetMenu(fileName = "New Hero", menuName = "Card/HeroExample", order = 51)]
    public class HeroSO_Model : GameSO_Model
    {       
        public Classes _heroClass;
        public int _healthCard;
    }   
}