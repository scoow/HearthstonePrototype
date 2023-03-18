using Hearthstone;
using UnityEngine;

public class Sound_Controller : Sound, ISound
{    
    [SerializeField] private AudioSource _pageFlip;
    [SerializeField] private AudioSource _addCardToDeck;

    public AudioSource PageFlip { get => _pageFlip; }    
    public AudioSource AddCardToDeck { get => _addCardToDeck; }

    public void PlaySound(AudioSource currentSound)
    {
        currentSound.Play();
    }    
}