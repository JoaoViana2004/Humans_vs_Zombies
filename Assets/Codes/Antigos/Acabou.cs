using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acabou : MonoBehaviour
{
    public bool acabou = false;
    public GameObject canva_antigo;
    public GameObject canva_novo;

    private void Update()
    {
        if (acabou)
        {
            canva_antigo.SetActive(false);
            canva_novo.SetActive(true);
        }
    }
}
