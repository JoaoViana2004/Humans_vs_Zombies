using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruidor : MonoBehaviour
{
    public float destroyDelay = 5f;

    private void Start()
    {
        Destroy(gameObject, destroyDelay);
    }
}
