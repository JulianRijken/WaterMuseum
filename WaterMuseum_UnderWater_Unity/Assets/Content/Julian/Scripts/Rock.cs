using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour, IRemovable
{
    [SerializeField] private Rigidbody m_rigidbody;
    [SerializeField] private float m_stopVelocity;
    [SerializeField] private float m_stopAngularVelocity;
    [SerializeField] private float m_stopLifeTime;
    [SerializeField] private float m_groundDistance;
    [SerializeField] private float m_moveSpeed;
    [SerializeField] private Mesh[] m_rockMeshes;

    private Vector3 m_finalPos;
    private Vector3 m_finalSize;
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
    }

    private void OnEnable()
    {
        m_rigidbody.isKinematic = false;
        m_timeAlive = 0;
        transform.localScale = Vector3.zero;
        m_finalSize = Vector3.one * Random.Range(2f, 5f);
    }

    private void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, m_finalSize, 5f * Time.deltaTime);

        m_timeAlive += Time.deltaTime;

        if (m_rigidbody.isKinematic == false)
            if (m_rigidbody.velocity.magnitude < m_stopVelocity && m_timeAlive > m_stopLifeTime && m_rigidbody.angularVelocity.magnitude < m_stopAngularVelocity)
            {
                m_rigidbody.isKinematic = true;
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

    public void OnRemove()
    {
        gameObject.SetActive(false);
        Stats.GetSheet().m_rockCount--;
    }
}
