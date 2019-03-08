using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dasky14.Gunslinger
{
    public class ContactPlayerDamage : MonoBehaviour
    {
        private float m_fDamageTimer = 0f;
        public float m_fDamageInterval = 0.5f;
        public bool m_bCanDamage = true;

        private void Update()
        {
            if ((m_fDamageTimer -= Time.deltaTime) <= 0f)
            {
                m_bCanDamage = true;
            }
            else
            {
                m_bCanDamage = false;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
                TheDamage();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
                TheDamage();
        }

        void TheDamage()
        {
            if (m_bCanDamage)
            {
                PlayerHealth.TakeDamage();
                m_fDamageTimer = m_fDamageInterval;
            }
        }
    }
}
