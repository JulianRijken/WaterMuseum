using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour, IRemovable
{
    [SerializeField] private float m_stopVelocity;
    [SerializeField] private float m_stopAngularVelocity;
    [SerializeField] private float m_stopLifeTime;
    [SerializeField] private float m_groundDistance;
    [SerializeField] private float m_moveSpeed;
    [SerializeField] private Mesh[] m_rockMeshes;
    [SerializeField] private Rigidbody m_rigidbody;
    private StatsSheet m_stats;
    private Vector3 m_finalPos;
    private Vector3 m_finalSize;
    private float m_timeAlive = 0;


    [Header("Coral")]
    [SerializeField] private Vector2 m_randomCoralSize;
    [SerializeField] private GameObject m_coralPrefabs;
    [SerializeField] private float m_coralPostionOffset;
    private GameObject m_childCoral;
    private float m_coralSize;


    private void Start()
    {
        m_stats = Stats.Sheet;

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

        if (!m_rigidbody.isKinematic)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, m_finalSize, 20f * Time.deltaTime);
            m_timeAlive += Time.deltaTime;

            if (m_rigidbody.velocity.magnitude < m_stopVelocity && m_timeAlive > m_stopLifeTime && m_rigidbody.angularVelocity.magnitude < m_stopAngularVelocity)
            {
                m_rigidbody.isKinematic = true;
                m_finalPos = transform.position + Vector3.down * m_groundDistance;
                StartCoroutine(MoveToFinalPos());
            }
        }
        else
        {
            if (m_childCoral == null)
            {
                m_coralSize = Random.Range(m_randomCoralSize.x, m_randomCoralSize.y);

                Vector3 spawnPos = transform.position + Vector3.right * Random.Range(-m_coralPostionOffset, m_coralPostionOffset) + Vector3.forward * Random.Range(-m_coralPostionOffset, m_coralPostionOffset);
                // Random Rotation Offset
                m_childCoral = Instantiate(m_coralPrefabs, spawnPos, Quaternion.identity);
                Stats.Sheet.m_coralCount++;
            }
            else
            {
                m_childCoral.transform.localScale = Vector3.MoveTowards(m_childCoral.transform.localScale, Vector3.one * m_coralSize, Time.deltaTime);
            }

            
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
        Destroy(m_childCoral);
        gameObject.SetActive(false);
        Stats.Sheet.m_rockCount--;
    }
}
