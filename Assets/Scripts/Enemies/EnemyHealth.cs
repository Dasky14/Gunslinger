using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dasky14.Gunslinger
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField]
        private int m_iHealth = 3;

        public void TakeDamage()
        {
            m_iHealth--;
            if (m_iHealth <= 0)
            {
                GameManager.EnemyDeath(transform.position, 1);
                Destroy(gameObject);
            }
        }
    }
}
