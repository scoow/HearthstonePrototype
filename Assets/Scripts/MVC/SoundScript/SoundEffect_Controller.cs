using UnityEngine;

namespace Hearthstone
{
    public class SoundEffect_Controller : Sound
    {        
        [SerializeField] private AudioSource _putCardInBoard;
        [SerializeField] private AudioSource _drawCard;
        [SerializeField] private AudioSource _taunt;

        public AudioSource PutCardInBoard { get => _putCardInBoard; }        
        public AudioSource DrawCard { get => _drawCard; }        
        public AudioSource Taunt { get => _taunt; }       
    }
}