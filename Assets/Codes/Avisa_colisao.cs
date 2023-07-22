using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avisa_colisao : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(gameObject.name+"-Colisao: " + collision.name);
    }
}
