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
