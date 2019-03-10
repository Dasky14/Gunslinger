using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Dasky14.Gunslinger
{
    /// <summary>
    /// A manager for all things UI. Handles all interaction in the HUD scene.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;

        #region Variables

        public TextMeshProUGUI m_gcScoreText = null;
        public Image m_gcHealthBar = null;
        public GameObject m_goGameEndUI = null;
        public GameObject m_goPauseMenu = null;
        public RectTransform m_tScoreEndPos = null;
        private bool m_bEndMenuOpened = false;
        private float m_fScoreMoveTimer = 1.5f;
        private bool m_bPaused = false;

        #endregion

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);
        }
    
        private void Update()
        {
            // Pause only available if the player hasn't lost
            if (!GameManager.instance.m_bLevelEnded)
            {
                if (Input.GetButtonDown("Pause"))
                {
                    m_bPaused = !m_bPaused;
                }

                if (m_bPaused)
                {
                    m_goPauseMenu.SetActive(true);
                    Time.timeScale = 0f;
                }
                else
                {
                    m_goPauseMenu.SetActive(false);
                    Time.timeScale = 1f;
                }
            }

            // Update healthbar every frame
            UpdateHealthBar();

            // If the game has ended, move the score text towards it's new position
            if (m_bEndMenuOpened)
                MoveScoreText();
        }

        /// <summary>
        /// Opens the EndMenu only once.
        /// </summary>
        public void OpenEndMenu()
        {
            if (m_bEndMenuOpened)
                return;
            m_bEndMenuOpened = true;

            m_goGameEndUI.SetActive(true);
        }

        /// <summary>
        /// Slowly moves the score text towards it's EndMenu position.
        /// </summary>
        private void MoveScoreText()
        {
            if ((m_fScoreMoveTimer -= Time.unscaledDeltaTime) <= 0f)
                m_gcScoreText.rectTransform.position = Vector2.Lerp(m_gcScoreText.rectTransform.position, m_tScoreEndPos.position, 2f * Time.unscaledDeltaTime);
        }

        /// <summary>
        /// Refreshes the healthbar to a new value.
        /// </summary>
        void UpdateHealthBar()
        {
            float currentFill = m_gcHealthBar.fillAmount;
            float healthPercentage = (float) PlayerHealth.c_iHealth / PlayerHealth.c_iMaxHealth;
            currentFill = Mathf.Lerp(currentFill, healthPercentage, 5f * Time.deltaTime);
            m_gcHealthBar.fillAmount = currentFill;
        }

        /// <summary>
        /// Updates the score text to match GameManager value.
        /// </summary>
        public void UpdateScoreText()
        {
            m_gcScoreText.text = GameManager.instance.m_iScore.ToString();
        }

        /// <summary>
        /// Gets the world position of the score text.
        /// </summary>
        /// <returns>World position Vector2.</returns>
        public Vector2 GetScoreWorldPos()
        {
            Vector2 position = GameManager.instance.m_gcCamera.ScreenToWorldPoint(m_gcScoreText.transform.position);
            return position;
        }

        /// <summary>
        /// Switches the scene to MainMenu.
        /// </summary>
        public void GoToMainMenu()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}
