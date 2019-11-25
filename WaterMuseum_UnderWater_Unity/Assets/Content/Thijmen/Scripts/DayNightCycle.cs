using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DayNightCycle : MonoBehaviour {

    [SerializeField] [Range(0f,24f)] private float m_currentTime;
    [SerializeField] private float m_timeSpeed;
    [SerializeField] private Light m_sun;
    [SerializeField] private Light m_moon;
    [SerializeField] private AnimationCurve m_dayLightCurve;
    [SerializeField] private AnimationCurve m_nightLightCurve;

    private void Update()
    {
        m_currentTime += Time.deltaTime / 60f * m_timeSpeed;
        UpdateTime();
    }

    private void UpdateTime()
    {
        m_sun.intensity = m_dayLightCurve.Evaluate(m_currentTime / 24f);
        m_moon.intensity = m_nightLightCurve.Evaluate(m_currentTime / 24f);

        transform.eulerAngles = Vector3.left * Mathf.Lerp(0, 360, m_currentTime / 24f);

        if (m_currentTime >= 24f)
            m_currentTime = 0;
        else if (m_currentTime <= 0)
            m_currentTime = 24f;

        NotificationCenter.FireTimeChange(m_currentTime);
    }
}
