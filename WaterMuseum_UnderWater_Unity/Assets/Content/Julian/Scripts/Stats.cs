using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{

    public static Stats m_instance;
    private StatsSheet m_sheet;

    public static StatsSheet GetSheet()
    {
        return m_instance.m_sheet;
    }
}

public class StatsSheet
{
    public int m_rockCount;
    public int m_plasticCount;
    public int m_fishCount;
    public int m_sharkCount;
    public int m_coralCount;
}
