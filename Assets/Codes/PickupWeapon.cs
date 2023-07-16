using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PickupWeapon : MonoBehaviour
{
    public PickupWeapon self; 
    public GameObject Player; 
    public bool get = false;

    public string nome;

    public float scaleSpeed = .8f;
    public float diference = .5f;
    public float normalScale = .9f;
    public float scale;
    public bool Volta_normal = false;
    public bool Enemy_Weapon;
    public AudioSource sound;

    public UnityEngine.UI.Image fotinha;

    private void Start()
    {
        self = GetComponent<PickupWeapon>();
        Player = GameObject.Find("Player");
        scale = transform.localScale.x;

        GameObject canvasObject = GameObject.FindGameObjectWithTag("Canvas"); // Encontrar o objeto do CanvasS
        fotinha = canvasObject.GetComponentInChildren<UnityEngine.UI.Image>(true);
        string imageTag = "Imagem_" + nome;
        if (canvasObject != null)
        {
            //while (!fotinha.CompareTag(imageTag))
            fotinha = canvasObject.GetComponentInChildren<UnityEngine.UI.Image>(true);
            if (fotinha != null && fotinha.CompareTag(imageTag))
            {
                // A imagem com a tag específica foi encontrada
                Debug.Log("Imagem encontrada com a tag: " + imageTag);
            }
        }
    }

    private void Update()
    {
        if (Volta_normal)
        {
            transform.localScale = Vector3.one * scale;
            Volta_normal = false;
        }
        if (get)
        {
            transform.position = Player.GetComponent<Player_Code>().localArma.position;
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player" && Enemy_Weapon == false)
        {
            if (get == false)
            {
                sound.Play();
                if (!JaTemArma())
                {
                    if (other.name == "Player")
                    {

                        // Torna a arma filha do objeto "Local_Arma"
                        transform.parent = Player.GetComponent<Player_Code>().localArma;

                        // Adiciona a arma à lista "Armas"
                        Player.GetComponent<Player_Code>().armas.Add(gameObject);

                        get = true;

                        transform.localScale = new Vector3(normalScale, normalScale, normalScale);

                        Player.GetComponent<Player_Code>().Troca_arma();
                    }
                }
                else
                {
                    Player.GetComponent<Player_Code>().armaAtual.GetComponent<Shoot>().municao += GetComponent<Shoot>().municao;
                    Destroy(gameObject);
                }
            }
        }
    }

    public bool JaTemArma()
    {
        foreach (GameObject obj in Player.GetComponent<Player_Code>().armas)
        {
            if (obj.GetComponent<PickupWeapon>().name == name)
            {
                return true;
            }
        }
        return false;
    }
}
