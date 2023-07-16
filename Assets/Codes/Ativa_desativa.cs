using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ativa_desativa : MonoBehaviour
{
    public bool ativado;
    public UnityEngine.UI.Image eu;
    void Update()
    {
        eu.enabled = ativado;
    }
}
