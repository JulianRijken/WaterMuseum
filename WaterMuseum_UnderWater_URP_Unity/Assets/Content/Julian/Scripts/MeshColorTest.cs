using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshColorTest : MonoBehaviour
{
    [SerializeField] private Transform testObject;
    private Mesh m_mesh;
    private Vector3[] m_vertices;

    private void Start()
    {    
        m_mesh = GetComponent<MeshFilter>().mesh;
        m_vertices = m_mesh.vertices;
    }

    private void Update()
    {

        Color[] colors = new Color[m_vertices.Length];

        colors[GetVectorIndex(testObject.position)] = Color.red;
        colors[GetVectorIndex(testObject.position + Vector3.right)] = Color.red;
        colors[GetVectorIndex(testObject.position + Vector3.left)] = Color.red;
        colors[GetVectorIndex(testObject.position + Vector3.forward)] = Color.red;
        colors[GetVectorIndex(testObject.position + Vector3.back)] = Color.red;
        m_mesh.colors = colors;

    }

    private int GetVectorIndex(Vector3 pos)
    {
        int sameIndex = 0;
        for (int i = 0; i < m_vertices.Length; i++)
        {
            if (Mathf.Round(m_vertices[i].x) == Mathf.Round(pos.x) && Mathf.Round(m_vertices[i].z) == Mathf.Round(pos.z))
                sameIndex = i;
        }

        return sameIndex;
    }
}
