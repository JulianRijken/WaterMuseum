using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plastic : MonoBehaviour, IRemovable
{
    public void OnRemove()
    {
        Stats.Sheet.m_plasticCount--;
        gameObject.SetActive(false);
    }
}
