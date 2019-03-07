using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBullet : MonoBehaviour
{
    private bool m_bDead = false;
    public float m_fBulletSpeed = 4f;
    private float m_fTimeToDie = 10f;
    private MeshRenderer m_gcRenderer = null;

    private void Start()
    {
        m_gcRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (!m_bDead)
            transform.Translate(Vector2.right * m_fBulletSpeed * Time.deltaTime);

        if ((m_fTimeToDie -= Time.deltaTime) <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        if (!m_bDead)
        {
            Destroy(gameObject, 1f);
            m_gcRenderer.enabled = false;
            m_bDead = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_bDead)
            return;

        switch (collision.gameObject.tag)
        {
            case "Player":
                PlayerHealth.TakeDamage();
                break;
            case "Enemy":
                collision.gameObject.SendMessage("TakeDamage");
                break;
            default:
                break;
        }

        Die();
    }
}
