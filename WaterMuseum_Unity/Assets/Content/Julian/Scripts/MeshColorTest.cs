using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshColorTest : MonoBehaviour
{
    private void Update()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;

        // create new colors array where the colors will be created.
        Color[] colors = new Color[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            Debug.Log(i / vertices.Length);
            colors[i] = Color.Lerp(Color.red, Color.green, vertices[i].y);
            //colors[i] = Color.Lerp(Color.black, Color.white, (float)i / vertices.Length);
            //colors[i] = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            //colors[i] = Color.green;
        }

        // assign the array of colors to the Mesh.
        mesh.colors = colors;

    }
}
