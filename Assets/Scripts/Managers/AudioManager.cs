using UnityEngine;

namespace Dasky14.Gunslinger
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;
        public static MusicPlayer c_gcMusicPlayer;

        #region Variables

        public static float m_fMusicVolume = 1;
        public static float m_fEffectVolume = 1;

        [SerializeField]
        private GameObject m_goMusicPlayerPrefab = null;

        [SerializeField]
        private Sound[] m_sSounds = null;
        private AudioSource m_gcAudioSource;

        #endregion

        private void Awake()
        {
            // Singleton pattern
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);

            // If there are no sounds in this manager, disable it
            if (m_sSounds == null || m_sSounds.Length == 0)
                gameObject.SetActive(false);
        }

        private void Start()
        {
            m_gcAudioSource = GetComponent<AudioSource>();

            // If a music player doesn't exist, create one
            if (c_gcMusicPlayer == null)
            {
                c_gcMusicPlayer = Instantiate(m_goMusicPlayerPrefab, null).GetComponent<MusicPlayer>();
            }
            
            // Tell music to play
            c_gcMusicPlayer.Play(GetSound("CountryRoads"));
        }


        /// <summary>
        /// Plays a sound from AudioManager.
        /// </summary>
        /// <param name="name">Name of the sound.</param>
        /// <param name="volume">Volume of the sound, usually either m_fEffectVolume or m_fMusicVolume.</param>
        public static void PlayClip(string name, float volume)
        {
            foreach (Sound sound in instance.m_sSounds)
            {
                if (sound.m_sName == name)
                {
                    instance.m_gcAudioSource.PlayOneShot(sound.m_sClip, Mathf.Clamp01(volume) * sound.m_fVolumeMult);
                    return;
                }
            }
            Debug.LogWarning("Sound \"" + name + "\" not found!");
        }

        /// <summary>
        /// Plays a sound from AudioManager with volume of 1.
        /// </summary>
        /// <param name="name">Name of the sound.</param>
        public static void PlayClip(string name)
        {
            PlayClip(name, 1f);
        }

        public static Sound GetSound(string name)
        {
            foreach (Sound sound in instance.m_sSounds)
            {
                if (sound.m_sName == name)
                {
                    return sound;
                }
            }
            Debug.LogWarning("Sound \"" + name + "\" not found!");
            return null;
        }

        /// <summary>
        /// Change the music volume live
        /// </summary>
        /// <param name="vol">New volume, between 0 and 1.</param>
        public static void ChangeMusicVolume(float vol)
        {
            m_fMusicVolume = Mathf.Clamp01(vol);
            c_gcMusicPlayer.UpdateVolume();
        }
    }
}
