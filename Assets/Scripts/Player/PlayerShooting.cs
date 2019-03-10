using UnityEngine;

namespace Dasky14.Gunslinger
{
    public class PlayerShooting : MonoBehaviour
    {
        private LineRenderer m_gcLine = null;
        private Camera m_gcCamera;
        private float m_fMagnitude = 0f;
        private Vector2 m_vMouseDirection = Vector2.zero;

        [SerializeField]
        private float m_fLineWidthMultiplier = 1f;
        [SerializeField]
        private float m_fMagnitudeSpeed = 1f;
        [SerializeField]
        private float m_fMaxMagnitude = 3f;
        [SerializeField]
        private float m_fMinShootMagnitude = 0.5f;
        [SerializeField]
        private float m_fThrowForceMultiplier = 300f;
        [SerializeField]
        private Vector2 m_fThrowTorqueRange = new Vector2(200f, 450f);

        [Header("Guns!")]
        [SerializeField]
        private GameObject[] m_goGuns = null;

        private void Start()
        {
            m_gcCamera = GameManager.instance.m_goCamera.GetComponent<Camera>();
            m_gcLine = GetComponent<LineRenderer>();
        }
    
        void Update()
        {
            if (!m_gcLine)
                return;

            if (Input.GetButton("Fire1"))
            {
                m_gcLine.enabled = true;
                m_fMagnitude = Mathf.Clamp(m_fMagnitude + m_fMagnitudeSpeed * Time.deltaTime, 0, m_fMaxMagnitude);

                m_gcLine.endWidth = m_fMagnitude * m_fLineWidthMultiplier;
            
                m_vMouseDirection = ((Vector2)m_gcCamera.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position).normalized;
            
                m_gcLine.SetPosition(1, (Vector3)m_vMouseDirection * m_fMagnitude);
            }
            else
            {
                if (m_fMagnitude > m_fMinShootMagnitude)
                    Shoot();
                m_gcLine.enabled = false;
                m_fMagnitude = 0f;
            }
        }

        void Shoot()
        {
            GameObject newGun = Instantiate(m_goGuns[Random.Range(0, m_goGuns.Length)], transform.position, Quaternion.identity, GameManager.instance.m_tGunContainer);
            Rigidbody2D gunRB = newGun.GetComponent<Rigidbody2D>();
            gunRB.AddForce(m_vMouseDirection * m_fMagnitude * m_fThrowForceMultiplier);
            gunRB.AddTorque(Random.Range(m_fThrowTorqueRange.x, m_fThrowTorqueRange.y));
            AudioManager.PlayClip("Throw", AudioManager.m_fEffectVolume);
        }
    }
}
