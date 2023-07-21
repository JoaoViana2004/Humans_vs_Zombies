using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Genrenciador_Encontrador : MonoBehaviour
{
    public UnityEngine.UI.Image ak;
    public UnityEngine.UI.Image pistola;
    public Text mortes;
    public int mortos=0;

    public UnityEngine.UI.Image Salada_de_fruta(string nome)
    {
        if (nome == "AK"){
            return ak;
        }
        else
        {
            return pistola;
        }
    }

    public void Salada_de_texto()
    {
        mortos++;
        mortes.text = mortos.ToString();
    }
}
