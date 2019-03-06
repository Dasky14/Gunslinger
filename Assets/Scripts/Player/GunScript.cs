using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{

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
    }
}
