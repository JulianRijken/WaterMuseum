using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour {

    [SerializeField] private float currentTime;
    [SerializeField] private Light sun;
    [SerializeField] private Light moon;
    [SerializeField] private AnimationCurve m_dayLightCurve;
    [SerializeField] private AnimationCurve m_nightLightCurve;

    // JULIAN VOOR THIJMEN: ZOG DAT DE ZON INTENSITY LAGER WORD ALS HIJ ONDER GAAT EN OP 0 STAAT ALS HIJ HELEMAAL WEG IS

    void Update() {

        sun.intensity = m_dayLightCurve.Evaluate(currentTime / 24f);
        moon.intensity = m_nightLightCurve.Evaluate(currentTime / 24f);

        currentTime += Time.deltaTime / 60 * 4; //Makes 1 day = 6 minutes.
        transform.eulerAngles = Vector3.left * Mathf.Lerp( 0 , 360 , currentTime / 24f);
        if(currentTime > 24) {
            currentTime = 0; 
        }
    }
}
