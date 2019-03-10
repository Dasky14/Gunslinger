using UnityEngine;

namespace Dasky14.Gunslinger
{
    public class GameManager : MonoBehaviour
    {

        public static GameManager instance;

        #region Variables

        [Header("Scene references")]
        public GameObject m_goPlayerObject;
        public GameObject m_goCamera;
        public Camera m_gcCamera;
        public Transform m_tGunContainer;
        public Transform m_tBackgroundContainer;
        public ParticleSystem m_psEnemyDeathParticles;
        public GameObject m_goScoreParticles;

        [Header("Level variables")]
        [SerializeField]
        private bool m_bLoadUI = true;
        public Vector2 m_vScrollDirection = Vector2.zero;
        public float m_fParallaxMultiplier = 1.2f;
        public Vector2 m_vSpawnTimeRange = new Vector2(3f, 6f); // Vector2 used to define min and max of a random timer
        private float m_fSpawnTimer = 0f;
        public Vector2 m_vWeedSpawnTimeRange = new Vector2(5f, 8f);
        private float m_fWeedSpawnTimer = 0f;
        public bool m_bLevelEnded = false;

        [Header("Enemies")]
        [SerializeField]
        private GameObject[] m_goEnemyPrefabs = null;
        [SerializeField]
        private GameObject m_goWeedPrefab = null;

        [Header("Score")]
        public int m_iScore = 0;

        #endregion

        private void Awake()
        {
            // Singleton pattern
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);

            // Load the HUD of the game, if it isn't already loaded
            if (m_bLoadUI)
            {
                if (!UIManager.instance)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(1, UnityEngine.SceneManagement.LoadSceneMode.Additive);
                }
            }
        }

        private void Start()
        {
            m_gcCamera = m_goCamera.GetComponent<Camera>();
            PlayerHealth.ResetHealth();
        }

        private void Update()
        {
            ScrollBackgrounds();
            SpawnEnemies();
            SpawnWeed();
            if (m_bLevelEnded)
                EndTheGame();
        }

        /// <summary>
        /// Slowly lerps timescale to 0 and opends ending menu.
        /// </summary>
        void EndTheGame()
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 0, 0.8f * Time.unscaledDeltaTime);
            UIManager.instance.OpenEndMenu();
        }

        /// <summary>
        /// Scrolls the images in m_tBackgroundContainer
        /// </summary>
        void ScrollBackgrounds()
        {
            foreach (Transform image in m_tBackgroundContainer)
            {
                if ((m_goCamera.transform.position.x - image.position.x) >= 45f)
                    image.Translate(-m_vScrollDirection.normalized * 90f);
                image.Translate(m_vScrollDirection * m_fParallaxMultiplier * Time.deltaTime);
            }
        }

        /// <summary>
        /// Spawns enemies as random intervals.
        /// </summary>
        void SpawnEnemies()
        {
            if (m_goEnemyPrefabs == null || m_goEnemyPrefabs.Length == 0)
                return;
            if ((m_fSpawnTimer -= Time.deltaTime) <= 0f)
            {
                float spawnDistance = m_gcCamera.orthographicSize * (Screen.width / Screen.height);
                Instantiate(m_goEnemyPrefabs[Random.Range(0, m_goEnemyPrefabs.Length)], new Vector2(spawnDistance + 4f, Random.Range(-4f, 4f)), Quaternion.identity);
                m_fSpawnTimer = Random.Range(m_vSpawnTimeRange.x, m_vSpawnTimeRange.y);
            }
        }

        /// <summary>
        /// Spawns a tumbleweed at random intervals.
        /// </summary>
        void SpawnWeed()
        {
            if ((m_fWeedSpawnTimer -= Time.deltaTime) <= 0f)
            {
                float spawnDistance = m_gcCamera.orthographicSize * (Screen.width / Screen.height);
                Instantiate(m_goWeedPrefab, new Vector2(spawnDistance + 4f, -5), Quaternion.identity);
                m_fWeedSpawnTimer = Random.Range(m_vWeedSpawnTimeRange.x, m_vWeedSpawnTimeRange.y);
            }
        }

        /// <summary>
        /// This is called by the enemy when it dies, and it increments score and makes a score particle from death position.
        /// </summary>
        /// <param name="pos">Position of the dead enemy.</param>
        /// <param name="score">How much score should be increased.</param>
        public static void EnemyDeath(Vector2 pos, int score)
        {
            if (!instance)
                return;

            instance.m_psEnemyDeathParticles.transform.position = pos;
            instance.m_psEnemyDeathParticles.Play();

            ScoreParticles newParticles = Instantiate(instance.m_goScoreParticles, pos, Quaternion.identity).GetComponent<ScoreParticles>();
            newParticles.m_iScore = score;

            AudioManager.PlayClip("DeathFart", AudioManager.m_fEffectVolume);
        }
    }
}
