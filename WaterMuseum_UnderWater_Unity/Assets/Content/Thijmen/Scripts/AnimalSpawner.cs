using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour {

    [SerializeField] private GameObject[] animals;
    [SerializeField] private Transform[] spawnPoints;

    private float spawnCheck = 5f;


    void Start() {
        animals = Resources.LoadAll<GameObject>( "FishModels/" );
        StartCoroutine( SpawnAnimals( spawnCheck ) );
    }

    private IEnumerator SpawnAnimals(float time) {
        int rndAnimal;
        int rndSpawnLoc;

        while(true) {

            rndAnimal = Random.Range( 0 , animals.Length);
            rndSpawnLoc = Random.Range( 0 , spawnPoints.Length);

            if(Stats.GetSheet().m_plasticCount <= 2 && Stats.GetSheet().m_rockCount >= 5) {
                Instantiate( animals[rndAnimal] , spawnPoints[rndSpawnLoc].position , transform.rotation );
            }

            yield return new WaitForSeconds( time );
        }
    }
}
