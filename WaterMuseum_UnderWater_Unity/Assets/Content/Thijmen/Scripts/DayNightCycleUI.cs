using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycleUI : MonoBehaviour {

    private void Start() {
        NotificationCenter.OnTimeChange += HandleTime;
    }

    private void OnDestroy() {
        NotificationCenter.OnTimeChange -= HandleTime;
    }


    private void HandleTime(float time) {
        transform.eulerAngles = new Vector3( 0 , 0 , Mathf.Lerp( 0 , 360 , time / 24f));

    }
}
