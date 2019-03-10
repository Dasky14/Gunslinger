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
        public GameObject m_goGameEndUI = null;
        public RectTransform m_tScoreEndPos = null;
        private bool m_bEndMenuOpened = false;
        private float m_fScoreMoveTimer = 1.5f;

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
            if (m_bEndMenuOpened)
                MoveScoreText();
        }

        public void OpenEndMenu()
        {
            if (m_bEndMenuOpened)
                return;
            m_bEndMenuOpened = true;

            m_goGameEndUI.SetActive(true);
        }

        private void MoveScoreText()
        {
            if ((m_fScoreMoveTimer -= Time.unscaledDeltaTime) <= 0f)
                m_gcScoreText.rectTransform.position = Vector2.Lerp(m_gcScoreText.rectTransform.position, m_tScoreEndPos.position, 2f * Time.unscaledDeltaTime);
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

        public Vector2 GetScoreWorldPos()
        {
            Vector2 position = GameManager.instance.m_gcCamera.ScreenToWorldPoint(m_gcScoreText.transform.position);
            return position;
        }

        public void GoToMainMenu()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }
}
