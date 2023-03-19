using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hearthstone
{
    [CreateAssetMenu(fileName = "New HeroVoice", menuName = "VoiceExample", order = 51)]
    public class HeroVoiceSO_Model : ScriptableObject
    {
        [SerializeField] private CardClasses cardClasses;
        //private AudioSource voiceHero;
        [SerializeField] private AudioClip _atackTaunt;        
        [SerializeField] private AudioClip _errorTakeCard;
        [SerializeField] private AudioClip _errorNoCard;
        [SerializeField] private AudioClip _errorAtackCard;

        public CardClasses CardClasses { get => cardClasses;}


        private void Play()
        {
            // voiceHero.GetComponent<AudioClip>() = _atackTaunt;
        }
    }
}