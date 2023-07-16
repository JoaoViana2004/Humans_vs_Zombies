using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coordenate : MonoBehaviour
{

    void Start()
    {
        if (GameObject.Find("Player").GetComponent<PlayerCode>().pos == -1)
        {
            float scale = transform.localScale.x;
            transform.localScale = new Vector3(-scale, -scale, -scale);
        }
    }

}
