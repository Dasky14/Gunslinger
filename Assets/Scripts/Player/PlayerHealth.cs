using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dasky14.Gunslinger
{
    public class PlayerHealth
    {
        public static int c_iMaxHealth = 3;
        public static int c_iHealth = 3;

        public static void TakeDamage()
        {
            AudioManager.PlayClip("PlayerHurt", AudioManager.m_fEffectVolume);
            c_iHealth--;
            if (c_iHealth <= 0)
            {
                Die();
            }
        }

        public static void ResetHealth()
        {
            c_iHealth = c_iMaxHealth;
        }

        public static void Die()
        {
            Debug.Log("Dead.");
        }
    }
}
