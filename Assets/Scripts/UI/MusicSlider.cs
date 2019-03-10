﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dasky14.Gunslinger
{
    public class MusicSlider : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_goOffImage = null;

        public void OnSliderChanged(float val)
        {
            AudioManager.m_fMusicVolume = val;
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

