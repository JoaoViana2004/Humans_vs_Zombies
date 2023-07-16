using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troca_arma_player : MonoBehaviour
{
    public void troca()
    {
        GameObject.Find("Player").GetComponent<Player_Code>().Troca_arma();
    }
}
