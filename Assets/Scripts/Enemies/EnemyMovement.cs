using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dasky14.Gunslinger
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField]
        private Vector2 m_vSpeedRange = new Vector2(1f, 3f);
        [SerializeField]
        private Vector2 m_vWaveMagnitudeRange = new Vector2(0.5f, 2f);
        [SerializeField]
        private Vector2 m_vMovementDirection = new Vector2(-1f, 0f);
        private float m_fMovementSpeed = 0f;
        private float m_fWaveMagnitude = 0f;
        private float m_fOrigY = 0f;

        // Start is called before the first frame update
        void Start()
        {
            m_fMovementSpeed = Random.Range(m_vSpeedRange.x, m_vSpeedRange.y);
            m_fWaveMagnitude = Random.Range(m_vWaveMagnitudeRange.x, m_vWaveMagnitudeRange.y);
            m_fOrigY = transform.position.y;
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(m_vMovementDirection.normalized * m_fMovementSpeed * Time.deltaTime);

            // Making wave movement
            Vector2 newPos = transform.position;
            newPos.y = m_fOrigY + Mathf.Sin(Time.time) * m_fWaveMagnitude;
            transform.position = newPos;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, (Vector2) transform.position + m_vMovementDirection.normalized);
        }
    }
}
