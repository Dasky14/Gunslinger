using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dasky14.Gunslinger
{
    public class PlayerMovement : MonoBehaviour {

        #region Variables
    
        private Rigidbody2D m_rbPlayerBody;
        private Camera m_gcCamera;
        private float m_fHorizInput = 0;
        private bool m_bJumpPressed = false;
        private int m_iCurrentAirJumps = 0;

        [Header("Gravity")]
        [SerializeField]
        private float m_fGravity = 10f;
        [SerializeField]
        private float m_fJumpingFloatMult = 0.8f;

        [Header("Player movement")]
        [SerializeField]
        private float m_fPlayerSpeed = 9f;
        [SerializeField]
        private float m_fPlayerJumpSpeed = 12f;
        [SerializeField]
        private float m_fGroundDetectionRange = 0.6f;
        [SerializeField]
        private int m_iMaxAirJumps = 1;
        [SerializeField]
        private ParticleSystem m_psJumpParticles = null;

        #endregion

        void Start () {
		    m_rbPlayerBody = GetComponent<Rigidbody2D>();
            m_gcCamera = GameManager.instance.m_goCamera.GetComponent<Camera>();
	    }

        // We check all player input in Update() to avoid dropping them
        private void Update()
        {
		    m_fHorizInput = Input.GetAxis("Horizontal");
            if (Input.GetButtonDown("Jump"))
                m_bJumpPressed = true;
        }

        // All player movement is in FixedUpdate() to not screw up the physics
	    private void FixedUpdate ()
        {
            // Sets the player's X velocity according to input
		    Vector2 playerMovement = new Vector2(m_fHorizInput * m_fPlayerSpeed, m_rbPlayerBody.velocity.y);
		    m_rbPlayerBody.velocity = playerMovement + GameManager.instance.m_vScrollDirection;

            // Make the player floatier when going up
            float fallingMult = m_rbPlayerBody.velocity.y >= 0 ? m_fJumpingFloatMult : 1f; 
		    m_rbPlayerBody.velocity += Vector2.down * m_fGravity * fallingMult * Time.deltaTime;
            
            bool isJumping = false;
            // Check whether the player is on the ground
            if (Physics2D.BoxCast(transform.position, new Vector2(1f, 1f), 0, Vector2.down, m_fGroundDetectionRange, LayerMask.GetMask("Ground")))
            {
                // If grounded, reset air jumps to 0
                m_iCurrentAirJumps = 0;
            }
            else
            {
                isJumping = true;
            }

            // If jump has been pressed on the most recent frame
		    if (m_bJumpPressed) {
                if (!isJumping)
                {
                    // If grounded, jump
                    Jump();
                }
                else if (m_iCurrentAirJumps < m_iMaxAirJumps)
                {
                    // If there are air jumps left, jump
                    Jump();
                    m_psJumpParticles.Play();
                    m_iCurrentAirJumps++;
                }
		    }

            // Reset jump input to false
            m_bJumpPressed = false;
	    }
    
        private void LateUpdate()
        {
            float horizontalCamHalfSize = m_gcCamera.orthographicSize * ((float)Screen.width / Screen.height);
            Vector3 oldPos = transform.position;
            Vector3 newPos = new Vector3(Mathf.Clamp(oldPos.x,
                                                     m_gcCamera.transform.position.x - horizontalCamHalfSize,
                                                     m_gcCamera.transform.position.x + horizontalCamHalfSize),
                                         Mathf.Clamp(oldPos.y,
                                                     m_gcCamera.transform.position.y - m_gcCamera.orthographicSize,
                                                     m_gcCamera.transform.position.y + m_gcCamera.orthographicSize));

            transform.position = newPos;
        }

        /// <summary>
        /// Adds jump velocity to player.
        /// </summary>
        private void Jump()
        {
            m_rbPlayerBody.velocity = new Vector2(m_rbPlayerBody.velocity.x, m_fPlayerJumpSpeed);
        }
    }
}
