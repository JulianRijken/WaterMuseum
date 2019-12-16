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
    [SerializeField] private GameObject[] m_coralPrefabs;
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
            m_meshFilter.mesh = m_rockMeshes[Random.Range(0, m_rockMeshes.Length)];

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

                Vector3[] verteces = m_meshFilter.mesh.vertices;
                List<Vector3> pickVertex = new List<Vector3>();
                for (int i = 0; i < verteces.Length; i++)
                {
                    float height = (transform.rotation * verteces[i]).y;
                    if (height > 0.3f)
                    {
                        pickVertex.Add(verteces[i]);
                    }
                }

                if (pickVertex.Count > 0)
                {
                    Vector3 vertex = pickVertex[Random.Range(0, pickVertex.Count)];
                    Vector3 spawnPos = transform.rotation * (vertex * m_finalSize) + transform.position + Vector3.down * 0.15f;
                    Vector3 direction = spawnPos - transform.position;
                    Quaternion toRot = Quaternion.LookRotation(direction, Vector3.up);
                    m_childCoral = Instantiate(m_coralPrefabs[Random.Range(0, m_coralPrefabs.Length)], spawnPos, Quaternion.Slerp((toRot * Quaternion.Euler(90,0,0)),Quaternion.identity, 0.9f));
                    m_childCoral.transform.localScale = Vector3.zero;
                    Stats.Sheet.m_coralCount++;
                }

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

        if(m_childCoral != null)
        Stats.Sheet.m_coralCount--;
    }

}


