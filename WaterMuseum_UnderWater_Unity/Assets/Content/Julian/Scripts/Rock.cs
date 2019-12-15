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
    private MeshFilter m_meshFilter;
    private StatsSheet m_stats;
    private float m_finalSize;
    private float m_timeAlive = 0;
    private bool m_finished;


    [Header("Coral")]
    [SerializeField] private Vector2 m_randomCoralSize;
    [SerializeField] private GameObject m_coralPrefabs;
    [SerializeField] private float m_coralPostionOffset;
    private GameObject m_childCoral;
    private float m_coralSize;


    private void Start()
    {

        m_stats = Stats.Sheet;
        m_finished = false;

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            m_meshFilter = meshFilter;
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
        m_finalSize = Random.Range(2f, 5f);
    }

    private void Update()
    {

        if (!m_rigidbody.isKinematic)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, m_finalSize * Vector3.one, 20f * Time.deltaTime);
            m_timeAlive += Time.deltaTime;

            if (m_rigidbody.velocity.magnitude < m_stopVelocity && m_timeAlive > m_stopLifeTime && m_rigidbody.angularVelocity.magnitude < m_stopAngularVelocity)
            {
                m_rigidbody.isKinematic = true;
                Vector3 finalPos = transform.position + Vector3.down * m_groundDistance;
                StartCoroutine(MoveToFinalPos(finalPos));
            }
        }
        else if(m_finished)
        {
            if (m_childCoral == null)
            {

                m_coralSize = Random.Range(m_randomCoralSize.x, m_randomCoralSize.y);

                Vector3[] vertices = m_meshFilter.mesh.vertices;


                Vector3 spawnPos = (vertices[Random.Range(0, vertices.Length)] * m_finalSize) + transform.position;
                Debug.DrawLine(spawnPos, transform.position, Color.red, 10f);
                m_childCoral = Instantiate(m_coralPrefabs, spawnPos, Quaternion.identity);
                Stats.Sheet.m_coralCount++;

            }
            else
            {
                m_childCoral.transform.localScale = Vector3.MoveTowards(m_childCoral.transform.localScale, Vector3.one * m_coralSize, Time.deltaTime);
            }

            
        }
    }

    private IEnumerator MoveToFinalPos(Vector3 finalPos)
    {
        while(transform.position != finalPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, finalPos, m_moveSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        m_finished = true;  
    }

    public void OnRemove()
    {
        Destroy(m_childCoral);
        gameObject.SetActive(false);
        Stats.Sheet.m_rockCount--;
        Stats.Sheet.m_coralCount--;
    }
}
