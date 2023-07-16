using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Barreira : MonoBehaviour
{
    public GameObject Colisor;

    public void Ativa()
    {
        Colisor.SetActive(true);
    }
    public void Desativa()
    {
        Colisor.SetActive(false);
    }
}
