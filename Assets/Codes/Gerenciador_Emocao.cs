using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gerenciador_Emocao : MonoBehaviour
{
    public bool brabo;
    public bool feliz;

    public GameObject Feliz;
    public GameObject Brabo;



    private void Start()
    {
        Brabo.SetActive(false);
        Feliz.SetActive(false);
    }

    private void Update()
    {
        Emocao_Brabo(brabo);
        Emocao_feliz(feliz);
    }

    public void Emocao_Brabo(bool pode)
    {
        if (pode)
        {
            Brabo.SetActive(true);
            brabo = false;
        }
    }

    public void Emocao_feliz(bool pode)
    {
        if(pode)
        {
            Feliz.SetActive(true);
            feliz = false;
        }
    }
}
