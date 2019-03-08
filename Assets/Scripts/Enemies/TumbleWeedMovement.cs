using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dasky14.Gunslinger
{
    public class TumbleWeedMovement : MonoBehaviour
    {
        [SerializeField]
        private float m_fHorizontalSpeed = -5f;
        [SerializeField]
        private float m_fAcceleration = 10f;
        private Rigidbody2D m_rbBallBody = null;

        private void Start()
        {
            m_rbBallBody = GetComponent<Rigidbody2D>();
        }
    
        void FixedUpdate()
        {
            Vector2 vel = m_rbBallBody.velocity;
            vel.x = Mathf.Lerp(vel.x, m_fHorizontalSpeed, m_fAcceleration * Time.fixedDeltaTime);
            m_rbBallBody.velocity = vel;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                Vector2 vel = m_rbBallBody.velocity;
                vel.x += -m_fHorizontalSpeed * 8f;
                m_rbBallBody.velocity = vel;
            }
        }
    }
}
