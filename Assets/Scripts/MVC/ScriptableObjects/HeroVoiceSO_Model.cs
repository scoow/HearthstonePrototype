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
        [SerializeField] private AudioClip _atackNextTurn;
        [SerializeField] private AudioClip _errorUseCard;
        [SerializeField] private AudioClip _atackTaunt;
        [SerializeField] private AudioClip _errorNoCard;

        public AudioClip AtackNextTurn { get => _atackNextTurn; }
        public AudioClip ErrorUseCard { get => _errorUseCard; }
        public AudioClip AtackTaunt { get => _atackTaunt; }
        public AudioClip ErrorNoCard { get => _errorNoCard; }


        public CardClasses CardClasses { get => cardClasses;}


        private void Play()
        {
            // voiceHero.GetComponent<AudioClip>() = _atackTaunt;
        }
    }
}