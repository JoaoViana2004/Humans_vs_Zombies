using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troca_arma_player : MonoBehaviour
{
    public UnityEngine.UI.Image ak;
    public UnityEngine.UI.Image pistol;
    public UnityEngine.UI.Image escop;

    private void Start()
    {
        Desativa();
    }

    private void Update()
    {
        if (GameObject.Find("Player").GetComponent<Player_Code>().armaAtual != null)
        {
            if (GameObject.Find("Player").GetComponent<Player_Code>().armaAtual.GetComponent<PickupWeapon>().nome == "AK")
            {
                ak.enabled = true;
                pistol.enabled = false;
                escop.enabled = false;
            }
            else if (GameObject.Find("Player").GetComponent<Player_Code>().armaAtual.GetComponent<PickupWeapon>().nome == "Pistol")
            {
                ak.enabled = false;
                pistol.enabled = true;
                escop.enabled = false;
            }
            else
            {
                Desativa();
            }
        }
    }

    public void Desativa()
    {
        ak.enabled = false;
        pistol.enabled = false;
        escop.enabled = false;
    }
}
