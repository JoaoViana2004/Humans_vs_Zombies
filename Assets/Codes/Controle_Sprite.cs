using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controle_Sprite : MonoBehaviour
{
    public void Desativa()
    {
        gameObject.SetActive(false);
    }
    public void Ativa()
    {
        gameObject.SetActive(true);
    }
}
