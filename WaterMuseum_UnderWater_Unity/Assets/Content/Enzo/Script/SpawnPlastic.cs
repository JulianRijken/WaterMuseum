using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlastic : MonoBehaviour {

    public GameObject Plasticprefab;
    public Vector3 center;
    public Vector3 size;

    [SerializeField] string m_PlasticName;
    [SerializeField] private Vector3 m_ofset;

    void Start() {
        StartCoroutine( Spawnplastic( 5 ) );
    }

    public IEnumerator Spawnplastic(float time) {
        while(true) {
            Vector3 pos = center + new Vector3( Random.Range( -size.x / 2 , size.x / 2 ) , Random.Range( -size.y / 2 , size.y / 2 ) , Random.Range( -size.z / 2 , size.z / 2 ) );
            ObjectPooler.SpawnObject( m_PlasticName , pos , Quaternion.identity , true );
            Stats.Sheet.m_plasticCount++;
            yield return new WaitForSeconds( time );
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color( 1 , 0 , 0 , 0.5f );
        Gizmos.DrawCube( center , size );
    }
}
