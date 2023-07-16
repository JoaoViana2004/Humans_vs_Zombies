using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticleSystem : MonoBehaviour
{
    private ParticleSystem part;

    private void Start()
    {
        part = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        if (!part.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}
