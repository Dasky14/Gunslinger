using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private Vector2 m_vSpeedRange = new Vector2(1f, 3f);
    [SerializeField]
    private Vector2 m_vMovementDirection = new Vector2(-1f, 0f);
    private float m_fMovementSpeed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        m_fMovementSpeed = Random.Range(m_vSpeedRange.x, m_vSpeedRange.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(m_vMovementDirection.normalized * m_fMovementSpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, (Vector2) transform.position + m_vMovementDirection.normalized);
    }
}
