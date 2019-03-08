using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Dasky14.Gunslinger
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;

        public TextMeshProUGUI m_gcScoreText = null;
        public Image m_gcHealthBar = null;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);
        }
    
        private void Update()
        {
            UpdateHealthBar();
        }

        void UpdateHealthBar()
        {
            float currentFill = m_gcHealthBar.fillAmount;
            float healthPercentage = (float) PlayerHealth.c_iHealth / PlayerHealth.c_iMaxHealth;
            currentFill = Mathf.Lerp(currentFill, healthPercentage, 5f * Time.deltaTime);
            m_gcHealthBar.fillAmount = currentFill;
        }

        public void UpdateScoreText()
        {
            m_gcScoreText.text = GameManager.instance.m_iScore.ToString();
        }
    }
}
