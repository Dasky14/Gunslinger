using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth
{
    public static int c_iMaxHealth = 3;
    public static int c_iHealth = 3;

    public static void TakeDamage()
    {
        c_iHealth--;
        Debug.Log(c_iHealth);
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
