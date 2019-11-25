using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycleUI : MonoBehaviour {

    [SerializeField] private DayNightCycle dayNightCycle;

    void Start() {

    }

    void Update() {
        transform.eulerAngles = new Vector3( 0 , 0 , Mathf.Lerp( 0 , 360 , dayNightCycle.currentTime / 24f));
    }
}
