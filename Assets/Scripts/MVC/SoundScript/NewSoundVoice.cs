using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hearthstone
{
    public class NewSoundVoice : MonoBehaviour
    {
        [SerializeField] private List<HeroVoiceSO_Model> heroVoiceSO_Models = new List<HeroVoiceSO_Model>();
        private Hero_Controller _heroController;
        private HeroVoiceSO_Model _currentHeroVoice;

        private void OnEnable()
        {
            _heroController = GetComponent<Hero_Controller>();
        }

        private void Start()
        {
            foreach(HeroVoiceSO_Model heroVoiceSO in heroVoiceSO_Models)
            {
                if(heroVoiceSO.CardClasses == _heroController.HeroClass)
                {
                    _currentHeroVoice = heroVoiceSO;
                }
            }
        }
    }
}