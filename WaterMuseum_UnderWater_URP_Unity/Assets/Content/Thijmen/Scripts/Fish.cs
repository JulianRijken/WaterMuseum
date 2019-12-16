using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour {

    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float rotSpeed = 100;
    [SerializeField] private float reach;

    private bool isWandering = false;
    private bool isWalking = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;

    private bool spottedObstacleL = false;
    private bool spottedObstacleR = false;

    private int rotWait;
    private int rotTime;
    private int rotLR;
    private int walkWait;
    private int walkTime;

    private void Update() {
        if(!isWandering) {
            StartCoroutine( Wander() );
        }

        if(isRotatingRight) {
            transform.Rotate( transform.up * Time.deltaTime * rotSpeed );
        }

        if(isRotatingLeft) {
            transform.Rotate( transform.up * Time.deltaTime * -rotSpeed );
        }

        if(isWalking) {
            transform.position = transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }

        Vector3 right = transform.TransformDirection( Vector3.right + Vector3.forward );

        if(Physics.Raycast( transform.position , right , reach )) {
            Debug.DrawRay( transform.position , transform.TransformDirection( Vector3.right + Vector3.forward ) * reach , Color.red );
            rotSpeed = 300;
            spottedObstacleR = true;
            transform.Rotate( transform.up * Time.deltaTime * -rotSpeed );

        } else {
            Debug.DrawRay( transform.position , transform.TransformDirection( Vector3.right + Vector3.forward ) * reach , Color.white );
            spottedObstacleR = false;
            rotSpeed = 100;
        }

        Vector3 left = transform.TransformDirection( Vector3.left + Vector3.forward );

        if(Physics.Raycast( transform.position , left , reach )) {
            Debug.DrawRay( transform.position , transform.TransformDirection( Vector3.left + Vector3.forward ) * reach , Color.red );
            rotSpeed = 300;
            spottedObstacleL = true;
            transform.Rotate( transform.up * Time.deltaTime * -rotSpeed );

        } else {
            Debug.DrawRay( transform.position , transform.TransformDirection( Vector3.left + Vector3.forward ) * reach , Color.white );
            spottedObstacleL = false;
            rotSpeed = 100;
        }

        if(spottedObstacleL && spottedObstacleR) {
            int rndDir = Random.Range( 1 , 3 );

            if(rndDir == 1) {
                transform.Rotate( transform.up * Time.deltaTime * rotSpeed );
            }

            if(rndDir == 2) {
                transform.Rotate( transform.up * Time.deltaTime * -rotSpeed );
            }
        }
    }

    private IEnumerator Wander() {

        rotTime = Random.Range( 1 , 3 );
        rotWait = Random.Range( 1 , 3 );
        rotLR = Random.Range( 1 , 2 );
        walkWait = Random.Range( 1 , 3 );
        walkTime = Random.Range( 1 , 3 );

        isWandering = true;

        yield return new WaitForSeconds( walkWait );
        isWalking = true;
        yield return new WaitForSeconds( walkWait );
        isWalking = true;

        yield return new WaitForSeconds( rotWait );
        if(rotLR == 1) {
            isRotatingLeft = true;
            yield return new WaitForSeconds( rotTime );
            isRotatingLeft = false;
        }
        if(rotLR == 2) {
            isRotatingRight = true;
            yield return new WaitForSeconds( rotTime );
            isRotatingRight = false;
        }
        isWandering = false;
    }
}
