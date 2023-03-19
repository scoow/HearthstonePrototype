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
        private AudioSource _audioSourceHero;
        private AudioClip _audioClipHero;

        private void OnEnable()
        {
            _heroController = GetComponent<Hero_Controller>();
            _audioSourceHero = GetComponent<AudioSource>();
            _audioClipHero = _audioSourceHero.GetComponent<AudioClip>();
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

        /// <summary>
        /// озвучка событий персонажем
        /// </summary>
        /// <param name="talkExample">тип события</param>
        public void PlayVoiceHero(TalkExample talkExample, Players _side)
        {            
            if(_side == Players.First)
            {

            }
            else
            {

            }


            switch (talkExample)
            {
                case TalkExample.ErrorNoCard:
                    _audioClipHero = _currentHeroVoice.ErrorNoCard;
                    break;
                case TalkExample.AtackTaunt:
                    _audioClipHero = _currentHeroVoice.AtackTaunt;
                    break;
                case TalkExample.ErrorUseCard:
                    _audioClipHero = _currentHeroVoice.ErrorUseCard;
                    break;
                case TalkExample.AtackNextTurn:
                    _audioClipHero = _currentHeroVoice.AtackNextTurn;
                    break;                
            }
            _audioSourceHero.Play();
        }
    }
    public enum TalkExample
    {
        ErrorNoCard,
        AtackNextTurn,
        ErrorUseCard,
        AtackTaunt,
    }
}