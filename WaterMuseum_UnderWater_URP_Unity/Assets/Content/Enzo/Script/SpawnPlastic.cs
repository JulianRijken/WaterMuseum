using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlastic : MonoBehaviour
{
    
    public GameObject Plasticprefab;
    public Vector3 center;
    public Vector3 size;
    [SerializeField] float plastic_Time;

    [SerializeField] string m_PlasticName;
    [SerializeField] private Vector3 m_ofset;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawnplastic(plastic_Time));
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    public IEnumerator Spawnplastic(float time)
    {
        while (true)
        {
            Quaternion rndRotation = Random.rotation;
            Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
            ObjectPooler.SpawnObject(m_PlasticName, pos, rndRotation, true);
            Stats.Sheet.m_plasticCount++;
            yield return new WaitForSeconds(time);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }
}
