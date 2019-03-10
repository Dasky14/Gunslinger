using UnityEngine;
using UnityEngine.UI;

namespace Dasky14.Gunslinger
{
    /// <summary>
    /// Slider that updates the music volume.
    /// </summary>
    public class MusicSlider : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_goOffImage = null;
        [SerializeField]
        private Slider m_gcSlider = null;

        private void Start()
        {
            m_gcSlider.value = AudioManager.m_fMusicVolume;
        }

        public void OnSliderChanged(float val)
        {
            AudioManager.ChangeMusicVolume(val);
            if (Mathf.Approximately(val, 0f))
            {
                m_goOffImage.SetActive(true);
            }
            else
            {
                m_goOffImage.SetActive(false);
            }
        }
    }
}

