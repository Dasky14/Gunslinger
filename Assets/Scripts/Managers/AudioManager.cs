using UnityEngine;

namespace Dasky14.Gunslinger
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;
        public static MusicPlayer c_gcMusicPlayer;

        public static float m_fMusicVolume = 1;
        public static float m_fEffectVolume = 1;

        [SerializeField]
        private GameObject m_goMusicPlayerPrefab = null;

        [SerializeField]
        private Sound[] m_sSounds = null;
        private AudioSource m_gcAudioSource;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);

            if (m_sSounds == null || m_sSounds.Length == 0)
                gameObject.SetActive(false);
        }

        private void Start()
        {
            m_gcAudioSource = GetComponent<AudioSource>();

            if (c_gcMusicPlayer == null)
            {
                c_gcMusicPlayer = Instantiate(m_goMusicPlayerPrefab, null).GetComponent<MusicPlayer>();
            }
            
            c_gcMusicPlayer.Play(GetSound("CountryRoads"));
        }

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

        public static void ChangeMusicVolume(float vol)
        {
            m_fMusicVolume = Mathf.Clamp01(vol);
            c_gcMusicPlayer.UpdateVolume();
        }
    }
}
