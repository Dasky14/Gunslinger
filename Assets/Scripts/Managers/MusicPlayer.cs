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

        /// <summary>
        /// This simply plays a Sound given to it, if none are playing currently.
        /// </summary>
        /// <param name="sound">A Sound object to play.</param>
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

        /// <summary>
        /// Stops any currently playing music.
        /// </summary>
        public void Stop()
        {
            m_gcMusicSource.Stop();
        }

        /// <summary>
        /// Gets new volume of music from AudioManager.
        /// </summary>
        public void UpdateVolume()
        {
            m_gcMusicSource.volume = AudioManager.m_fMusicVolume * m_sSound.m_fVolumeMult;
        }
    }
}
