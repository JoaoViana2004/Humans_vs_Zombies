using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maquina_do_tempo : MonoBehaviour
{
    public bool cmc = false;
    public bool cmc2 = false;
    public GameObject maquina_aberta;
    public GameObject maquina_fechada;

    public GameObject cabou;

    // Update is called once per frame
    void Update()
    {
        if (cmc)
        {
            maquina_aberta.SetActive(true);
            maquina_fechada.SetActive(false);
        }
        if (cmc2)
        {
            maquina_aberta.SetActive(false);
            maquina_fechada.SetActive(true);

            cabou.GetComponent<Acabou>().acabou = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (cmc)
            {
                cmc2 = true;
                collision.gameObject.SetActive(false);
            }
        }
    }
}
