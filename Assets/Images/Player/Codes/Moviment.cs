using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moviment : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetBool("Walk_y", true);
            Debug.Log("Anda Y Pressionado");
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetBool("Walk_y", false);
            Debug.Log("Anda Y Des-Pressionado");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetBool("Walk_x", true);
            Debug.Log("Anda X Pressionado");
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetBool("Walk_x", false);
            Debug.Log("Anda X Des-Pressionado");
        }
    }
}
