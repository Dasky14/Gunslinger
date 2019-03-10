using UnityEngine;
using UnityEngine.UI;

namespace Dasky14.Gunslinger
{
    public class EffectSlider : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_goOffImage = null;
        [SerializeField]
        private Slider m_gcSlider = null;

        private void Start()
        {
            m_gcSlider.value = AudioManager.m_fEffectVolume;
        }

        public void OnSliderChanged(float val)
        {
            AudioManager.m_fEffectVolume = val;
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

