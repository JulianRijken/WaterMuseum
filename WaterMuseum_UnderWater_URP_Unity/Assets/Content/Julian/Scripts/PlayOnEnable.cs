using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class PlayOnEnable : MonoBehaviour
{
    ParticleSystem system;
    private void Awake()
    {
        system = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        system.Play();
    }

    private void OnDisable()
    {
        system.Stop();
    }
}
