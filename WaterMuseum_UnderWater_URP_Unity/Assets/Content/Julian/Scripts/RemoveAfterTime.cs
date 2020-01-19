using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAfterTime : MonoBehaviour
{
    [SerializeField] private float time;

    void Awake()
    {
        Destroy(gameObject,time);
    }

}
