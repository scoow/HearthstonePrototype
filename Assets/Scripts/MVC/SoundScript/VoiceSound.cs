using Hearthstone;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceSound : MonoBehaviour, ISound
{
    private ISpeaker _speaker;    

    public VoiceSound(ISpeaker speaker)
    {
        _speaker = speaker;
    }

    public void PlaySound(AudioSource currentSound)
    {
        currentSound.Play();
    }
}