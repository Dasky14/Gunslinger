using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dasky14.Gunslinger
{
    public class MusicPlayer : MonoBehaviour
    {
        private AudioSource m_gcMusicSource;
        private Sound m_sSound = null;

        private void Awake()
        {
            m_gcMusicSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }

        public void Play(Sound sound)
        {
            if (!m_gcMusicSource.isPlaying)
            {
                m_sSound = sound;
                m_gcMusicSource.clip = sound.m_sClip;
                m_gcMusicSource.Play();
                m_gcMusicSource.volume = AudioManager.m_fMusicVolume * sound.m_fVolumeMult;
            }
        }

        public void Stop()
        {
            m_gcMusicSource.Stop();
        }

        public void UpdateVolume()
        {
            m_gcMusicSource.volume = AudioManager.m_fMusicVolume * m_sSound.m_fVolumeMult;
        }
    }
}
