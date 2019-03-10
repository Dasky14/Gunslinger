using UnityEngine;

namespace Dasky14.Gunslinger
{
    /// <summary>
    /// A simple class that contains all information needed to play a sound.
    /// Contains the volume multiplier, name, and the AudioClip itself.
    /// </summary>
    [System.Serializable]
    public class Sound
    {

        public string m_sName = "nada";
        public AudioClip m_sClip;
        [Range(0f, 2f)]
        public float m_fVolumeMult = 1f;

        public Sound(AudioClip clip)
        {
            m_sClip = clip;
            m_fVolumeMult = 1f;
        }

        public Sound(AudioClip clip, float volumeMult)
        {
            m_sClip = clip;
            m_fVolumeMult = volumeMult;
        }
    }
}
