using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dasky14.Gunslinger
{
    public class ScoreParticles : MonoBehaviour
    {
        public int m_iScore = 0;

        [SerializeField]
        private float m_fMaxSpeed = 5f;
        [SerializeField]
        private float m_fAcceleration = 5f;
        [SerializeField]
        private float m_fDisableDistance = 1f;
        [SerializeField]
        private float m_fStartVelocity = 10f;

        private Rigidbody2D m_rbParticleBody = null;
        private ParticleSystem m_psParticleSystem = null;
        private bool m_bIsDead = false;

        private void Start()
        {
            m_rbParticleBody = GetComponent<Rigidbody2D>();
            m_psParticleSystem = GetComponent<ParticleSystem>();

            m_rbParticleBody.velocity = GetRandomSpawnDir() * m_fStartVelocity;
        }

        // Update is called once per frame
        void Update()
        {
            if (!m_bIsDead)
            {
                Vector2 currPos = transform.position;
                Vector2 goalPos = UIManager.instance.GetScoreWorldPos();

                Vector2 direction = (goalPos - currPos).normalized;

                m_rbParticleBody.velocity = Vector2.Lerp(m_rbParticleBody.velocity, direction * m_fMaxSpeed, m_fAcceleration * Time.deltaTime);

                if (Vector2.Distance(goalPos, transform.position) <= m_fDisableDistance)
                {
                    DisableAndDie();
                }
            }
        }

        private void DisableAndDie()
        {
            m_bIsDead = true;
            m_rbParticleBody.velocity = Vector2.zero;

            GameManager.instance.m_iScore += m_iScore;
            UIManager.instance?.UpdateScoreText();
            Destroy(gameObject, 5);
            m_psParticleSystem.Stop();
        }

        private Vector2 GetRandomSpawnDir()
        {
            Vector2 newDir = Quaternion.Euler(0, 0, Random.Range(0, 360)) * Vector2.up;
            return newDir;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawLine(transform.position, UIManager.instance.GetScoreWorldPos());
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + m_rbParticleBody.velocity);
        }
    }
}
