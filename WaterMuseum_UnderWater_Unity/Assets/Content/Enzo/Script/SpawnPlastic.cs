using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlastic : MonoBehaviour
{
    
    public GameObject Plasticprefab;
    public Vector3 center;
    public Vector3 size;

    [SerializeField] string m_PlasticName;
    [SerializeField] private Vector3 m_ofset;

    //private float nextActionTime = 0.0f;
    //public float period = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawnplastic(5));
    }

    // Update is called once per frame
    void Update()
    {
        //if (Time.time > nextActionTime)
        //{
        //    nextActionTime += period;
        //    // execute block of code here
        //}
        //ObjectPooler.SpawnObject(m_PlasticName, spawnPoint + m_ofset, Quaternion.identity, true);

    }
    public IEnumerator Spawnplastic(float time)
    {
        while (true)
        {
            Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
            //Instantiate(Plasticprefab, pos, Quaternion.identity);
            ObjectPooler.SpawnObject(m_PlasticName, pos, Quaternion.identity, true);
            Stats.GetSheet().m_plasticCount++;
            yield return new WaitForSeconds(time);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }
}
