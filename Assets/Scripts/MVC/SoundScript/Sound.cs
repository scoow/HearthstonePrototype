using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField] private AudioSource _buttonClick;
    [SerializeField] private AudioSource _cardShrink;
   
    public AudioSource CardShrink { get => _cardShrink; }
    public AudioSource ButtonClick { get => _buttonClick; }    
}
