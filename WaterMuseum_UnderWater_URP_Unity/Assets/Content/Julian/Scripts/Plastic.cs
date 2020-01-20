using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plastic : MonoBehaviour, IRemovable
{

    [SerializeField] private GameObject effect;
    public void OnRemove()
    {
        Stats.Sheet.m_plasticCount--;
        Instantiate(effect, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }
}
