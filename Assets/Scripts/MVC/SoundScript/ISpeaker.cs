using UnityEngine;

namespace Hearthstone
{
    public interface ISpeaker
    {
        public void PlaySound(AudioSource currentSound)
        {
            currentSound.Play();
        }

    }
}