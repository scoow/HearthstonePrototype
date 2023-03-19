using Hearthstone;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour, ISound
{
    [SerializeField] private List<AudioSource> _audioColection;
    [SerializeField] private AudioSource _buttonClick;
    [SerializeField] private AudioSource _cardShrink;

    
   
    public AudioSource CardShrink { get => _cardShrink; }
    public AudioSource ButtonClick { get => _buttonClick; }
    public void PlaySound(AudioSource currentSound)
    {
        currentSound.Play();
    }
}
