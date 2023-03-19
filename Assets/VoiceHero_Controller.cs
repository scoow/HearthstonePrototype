using Hearthstone;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceHero_Controller : MonoBehaviour
{
    [SerializeField] private List<HeroVoiceSO_Model> heroVoiceSO_Models = new List<HeroVoiceSO_Model>();
    [SerializeField] private Hero_Controller _heroFirst;
    [SerializeField] private Hero_Controller _heroSecond;
    private HeroVoiceSO_Model _firstHeroVoice;
    private HeroVoiceSO_Model _secondHeroVoice;
    [SerializeField] private AudioSource _audioSourceHero;
    [SerializeField] private AudioClip _audioClipHero;

    private void OnEnable()
    {
        _audioSourceHero = GetComponent<AudioSource>();
        _audioClipHero = _audioSourceHero.clip;
    }


    public void SetAudioVoiceHero()
    {
        foreach (HeroVoiceSO_Model heroVoiceSO in heroVoiceSO_Models)
        {
            if (heroVoiceSO.CardClasses == _heroFirst.HeroClass)
            {
                _firstHeroVoice = heroVoiceSO;
            }
            if (heroVoiceSO.CardClasses == _heroSecond.HeroClass)
            {
                _secondHeroVoice = heroVoiceSO;
            }
        }
    }

    /// <summary>
    /// ������� ������� ����������
    /// </summary>
    /// <param name="talkExample">��� �������</param>
    public void PlayVoiceHero(TalkExample talkExample, Players _side)
    {
        HeroVoiceSO_Model currentAudioModel;
        if (_side == Players.First)
        {
            currentAudioModel = _firstHeroVoice;
        }
        else
        {
            currentAudioModel = _secondHeroVoice;
        }

        switch (talkExample)
        {
            case TalkExample.ErrorNoCard:
                _audioClipHero = currentAudioModel.ErrorNoCard;
                break;
            case TalkExample.AtackTaunt:
                _audioClipHero = currentAudioModel.AtackTaunt;
                break;
            case TalkExample.ErrorUseCard:
                _audioClipHero = currentAudioModel.ErrorUseCard;
                break;
            case TalkExample.AtackNextTurn:
                _audioClipHero = currentAudioModel.AtackNextTurn;
                break;
        }
        _audioSourceHero.clip = _audioClipHero;
        _audioSourceHero.Play();
    }
}