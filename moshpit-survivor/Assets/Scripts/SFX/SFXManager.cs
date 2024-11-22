using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SFX
{
    public class SFXManager : MonoBehaviour
    {
        
        public static SFXManager instance;

        private void Awake()
        {
            instance = this;
        }

        public AudioSource[] sfxAudio;

        private void PlaySfx(int sfxToPlay)
        {
            sfxAudio[sfxToPlay].Stop();
            sfxAudio[sfxToPlay].Play();
        }

        public void PlaySfxPitched(int sfxToPlay)
        {
            sfxAudio[sfxToPlay].pitch = Random.Range(.9f, 1.1f);
            PlaySfx(sfxToPlay);
        }
    }
}