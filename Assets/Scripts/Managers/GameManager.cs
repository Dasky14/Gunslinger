using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    #region Variables

    [Header("Scene references")]
    public GameObject m_goPlayerObject;
    public GameObject m_goCamera;
    public Transform m_tGunContainer;
    public Transform m_tBackgroundContainer;
    public ParticleSystem m_psEnemyDeathParticles;

    [Header("Level variables")]
    public Vector2 m_vScrollDirection = Vector2.zero;
    public float m_fParallaxMultiplier = 1.2f;
    public Vector2 m_vSpawnTimeRange = new Vector2(3f, 6f); // Vector2 used to define min and max of a random timer
    private float m_fSpawnTimer = 0f;

    [Header("Enemies")]
    [SerializeField]
    private GameObject[] m_goEnemyPrefabs = null;

    [Header("Score")]
    public int m_iScore = 0;

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
        ScrollBackgrounds();
        SpawnEnemies();
    }

    void ScrollBackgrounds()
    {
        foreach (Transform image in m_tBackgroundContainer)
        {
            if ((m_goCamera.transform.position.x - image.position.x) >= 30f)
                image.Translate(-m_vScrollDirection.normalized * 60f);
            image.Translate(m_vScrollDirection * m_fParallaxMultiplier * Time.deltaTime);
        }
    }

    void SpawnEnemies()
    {
        if (m_goEnemyPrefabs == null || m_goEnemyPrefabs.Length == 0)
            return;
        if ((m_fSpawnTimer -= Time.deltaTime) <= 0f)
        {
            Instantiate(m_goEnemyPrefabs[Random.Range(0, m_goEnemyPrefabs.Length)], new Vector2(14f, Random.Range(-3f, 3f)), Quaternion.identity);
            m_fSpawnTimer = Random.Range(m_vSpawnTimeRange.x, m_vSpawnTimeRange.y);
        }
    }

    public static void EnemyDeath(Vector2 pos, int score)
    {
        if (!instance)
            return;
        instance.m_iScore += score;
        instance.m_psEnemyDeathParticles.transform.position = pos;
        instance.m_psEnemyDeathParticles.Play();

        AudioManager.PlayClip("DeathFart");
    }
}
