using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dasky14.Gunslinger
{
    public class GunScript : MonoBehaviour
    {
        [SerializeField]
        private Transform m_tShootPoint = null;
        [SerializeField]
        private GameObject m_goBulletPrefab = null;
        [SerializeField]
        private int m_iAmmo = 6;
        [SerializeField]
        private float m_fFireInterval = 0.75f;
        private float m_fFireTimer = 0f;
        private float m_fShootSensitivity = 5f;

        private float m_fTimeToDie = 30f;
        private float m_fCollisionTimeToDie = 5f;
        private float m_fDieTimer = 0f;
        private bool m_bIsDead = false;
        private bool m_bHasCollided = false;

        private void Start()
        {
            m_fDieTimer = m_fTimeToDie;
        }
    
        void Update()
        {
            m_fFireTimer -= Time.deltaTime;

            m_fDieTimer -= Time.deltaTime;
            if (m_fDieTimer <= 0f)
                Die();
        }

        void Die()
        {
            if (!m_bIsDead)
            {
                m_bIsDead = true;
                Destroy(gameObject, 5);
            }

            Vector3 oldScale = transform.localScale;
            transform.localScale = Vector3.Lerp(oldScale, Vector3.zero, 0.5f * Time.deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!m_bHasCollided)
            {
                m_bHasCollided = true;
                m_fDieTimer = m_fCollisionTimeToDie;

                // Could use a delegate or something for this,
                // but SendMessage() is probably fine for rarer events and to objects with few components
                if (collision.gameObject.tag == "Enemy")
                {
                    collision.gameObject.SendMessage("TakeDamage");
                }
            }

            if (collision.relativeVelocity.magnitude < m_fShootSensitivity)
                return;

            if ((m_fFireTimer) <= 0f)
            {
                Shoot();
                m_fFireTimer = m_fFireInterval;
            }
        }

        void Shoot()
        {
            if (m_iAmmo <= 0)
                return;
            AudioManager.PlayClip("GunShot", AudioManager.m_fEffectVolume);
            m_iAmmo--;
            Instantiate(m_goBulletPrefab, m_tShootPoint.position, transform.rotation);
        }
    }
}
