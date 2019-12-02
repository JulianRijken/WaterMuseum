using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private Rigidbody m_rigidbody;
    [SerializeField] private float m_stopVelocity;
    [SerializeField] private float m_stopAngularVelocity;
    [SerializeField] private float m_stopLifeTime;
    [SerializeField] private float m_groundDistance;
    [SerializeField] private float m_moveSpeed;
    [SerializeField] private Mesh[] m_rockMeshes;

    private Vector3 m_finalPos;
    private float m_timeAlive = 0;

    private void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            meshFilter.mesh = m_rockMeshes[Random.Range(0, m_rockMeshes.Length)];

            MeshCollider meshCollider = GetComponent<MeshCollider>();
            if (meshCollider != null)
                meshCollider.sharedMesh = meshFilter.mesh;
        }

        transform.localScale = Vector3.one * Random.Range(1f, 5f);

    }

    private void Update()
    {
        m_timeAlive += Time.deltaTime;

        if (m_rigidbody != null)
            if (m_rigidbody.velocity.magnitude < m_stopVelocity && m_timeAlive > m_stopLifeTime && m_rigidbody.angularVelocity.magnitude < m_stopAngularVelocity)
            {
                Destroy(m_rigidbody);
                m_finalPos = transform.position + Vector3.down * m_groundDistance;
                StartCoroutine(MoveToFinalPos());
            }


    }

    private IEnumerator MoveToFinalPos()
    {
        while(transform.position != m_finalPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_finalPos, m_moveSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    
}
