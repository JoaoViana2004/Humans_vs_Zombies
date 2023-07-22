using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gerenciador_life : MonoBehaviour
{
    public int vida;
    public int Numero_vidas;

    public UnityEngine.UI.Image[] vidas;
    public Sprite vida_cheia;
    public Sprite vida_vazia;

    public Life vida_player;

    // Update is called once per frame
    void Update()
    {
        vida = vida_player.vida;
        if (vida > Numero_vidas)
        {
            vida = Numero_vidas;
        }

        for (int i =0 ;i<vidas.Length ;i++)
        {
            if (i < vida)
            {
                vidas[i].sprite = vida_cheia;
            } else
            {
                vidas[i].sprite = vida_vazia;
            }
            if (i < Numero_vidas)
            {
                vidas[i].enabled = true;
            }
            else
            {
                vidas[i].enabled = false;
            }
        }
    }
}
