using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour {

    private GameObject[] animals;
    private List<GameObject> activeFish = new List<GameObject>();
    private List<Transform> spawnPoints = new List<Transform>();

    private const float spawnCheckItr = 5f;
    private const float destoryCheckItr = 5f;


    void Start() {

        spawnPoints = new List<Transform>();

        for (int i = 0; i < transform.childCount; i++)       
            spawnPoints.Add(transform.GetChild(i));
        

        animals = Resources.LoadAll<GameObject>( "FishModels/" );
        StartCoroutine( SpawnAnimals( spawnCheckItr ) );
        StartCoroutine( DestoryAnimals( destoryCheckItr ) );
    }

    private IEnumerator SpawnAnimals(float time) {
        int rndAnimal;
        int rndSpawnLoc;

        while(true) {

            rndAnimal = Random.Range( 0 , animals.Length );
            rndSpawnLoc = Random.Range( 0 , spawnPoints.Count );

            if(Stats.Sheet.m_plasticCount <= 2 && Stats.Sheet.m_coralCount >= 5) {
                GameObject newFish = Instantiate( animals[rndAnimal] , spawnPoints[rndSpawnLoc].position , transform.rotation );
                activeFish.Add( newFish );
            }
            yield return new WaitForSeconds( time );
        }
    }

    private IEnumerator DestoryAnimals(float time) {
        while(true) {
            if(Stats.Sheet.m_plasticCount >= 2 || Stats.Sheet.m_coralCount <= 5) {
                Destroy( activeFish[activeFish.Count - 1].gameObject );
                activeFish.RemoveAt( activeFish.Count - 1 );
            }
            yield return new WaitForSeconds( time );
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.DrawSphere(transform.GetChild(i).position, 0.5f);
        }

    }
}
