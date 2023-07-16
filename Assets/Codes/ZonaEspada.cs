using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaEspada : MonoBehaviour
{
    public GameObject zona_espada;
    void Start()
    {
        zona_espada.SetActive(false);
    }

    public void comeca_ataque()
    {
        zona_espada.SetActive(true);
    }

    public void termina_ataque()
    {
        zona_espada.SetActive(false);
    }
}
