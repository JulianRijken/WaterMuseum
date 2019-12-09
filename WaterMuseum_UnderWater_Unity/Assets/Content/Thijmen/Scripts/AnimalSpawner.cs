using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour {

    [SerializeField] private GameObject[] animals;

    private float spawnCheck = 5f;


    void Start() {
        animals = Resources.LoadAll<GameObject>( "FishModels/" );
        StartCoroutine( SpawnAnimals( spawnCheck ) );
    }

    private IEnumerator SpawnAnimals(float time) {
        while(true) {

            yield return new WaitForSeconds( time );
        }
    }
}
