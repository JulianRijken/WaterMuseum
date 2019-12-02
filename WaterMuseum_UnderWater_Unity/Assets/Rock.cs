using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private Rigidbody m_rigidbody;
    [SerializeField] private float m_stopVelocity;
    [SerializeField] private float m_stopLifeTime;

    private float m_timeAlive = 0;

    private void Update()
    {
        m_timeAlive += Time.deltaTime;

        if (m_rigidbody != null)
            if (m_rigidbody.velocity.magnitude < m_stopVelocity && m_timeAlive > m_stopLifeTime)
            {
                Destroy(m_rigidbody);
            }
    }
}
