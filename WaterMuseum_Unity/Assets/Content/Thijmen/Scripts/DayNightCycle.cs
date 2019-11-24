using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour {

    [SerializeField] private float currentTime;

    // JULIAN VOOR THIJMEN: ZOG DAT DE ZON INTENSITY LAGER WORD ALS HIJ ONDER GAAT EN OP 0 STAAT ALS HIJ HELEMAAL WEG IS

    void Update() {
        currentTime += Time.deltaTime / 60 * 4; //Makes 1 day = 6 minutes.
        transform.eulerAngles = Vector3.left * Mathf.Lerp( 0 , 360 , currentTime / 24f);
        if(currentTime > 24) {
            currentTime = 0; 
        }
    }
}
