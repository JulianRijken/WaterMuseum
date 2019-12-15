using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{

    public static Stats m_instance;
    private StatsSheet m_sheet;
    public static StatsSheet Sheet { get => m_instance.m_sheet; }

    private void Awake()
    {
        if (m_instance == null)
            m_instance = this;

        m_sheet = new StatsSheet();
    }


#if UNITY_EDITOR
    private void OnGUI()
    {
        StatsSheet sheet = Sheet;
        GUILayout.Label("rockCount Count: " + sheet.m_rockCount);
        GUILayout.Label("plasticCount Count: " + sheet.m_plasticCount);
        GUILayout.Label("fishCount Count: " + sheet.m_fishCount);
        GUILayout.Label("coralCount Count: " + sheet.m_coralCount);
    }
#endif
}

public class StatsSheet
{
    public int m_rockCount;
    public int m_plasticCount;
    public int m_fishCount;
    public int m_coralCount;
}
