using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Genrenciador_Encontrador : MonoBehaviour
{
    public UnityEngine.UI.Image ak;
    public UnityEngine.UI.Image pistola;

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
}
