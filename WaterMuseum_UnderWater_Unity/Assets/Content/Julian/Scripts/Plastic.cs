using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plastic : MonoBehaviour, IRemovable
{
    public void OnRemove()
    {
        Stats.GetSheet().m_plasticCount--;
        gameObject.SetActive(false);
    }
}
