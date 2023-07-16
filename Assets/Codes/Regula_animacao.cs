using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regula_animacao : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();    
    }

    void Regula(string qual)
    {
        anim.SetBool(qual, false);
    }
}
