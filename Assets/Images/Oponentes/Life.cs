using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public int vida = 10;
    private Animator anim;
    public GameObject cabou;

    public bool Anim_Death;

    public bool animal;
    public bool morreu = false;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (vida <= 0 && !morreu)
        {
            Instantiate(cabou, transform.position, transform.rotation);
            if (Anim_Death)
            {
                anim.SetBool("Death", true);
            }
            else
            {
                Destroy(gameObject);
            }

            morreu = true;
        }
    }
}
