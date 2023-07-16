using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickupWeapon : MonoBehaviour
{
    public PickupWeapon self; 
    public GameObject Player; 
    public bool get = false;

    public string nome;

    public float scaleSpeed = .8f;
    public float diference = .5f;
    public float normalScale = .9f;
    private bool isScalingUp = true;
    private float maxScale;
    private float minScale;
    public float scale;
    public bool Volta_normal= false;

    public bool Enemy_Weapon;

    public AudioSource sound;

    private void Start()
    {
        self = GetComponent<PickupWeapon>();
        maxScale = transform.localScale.x + diference; 
        minScale = transform.localScale.x - diference;
        Player = GameObject.Find("Player");
        scale = transform.localScale.x;
    }

    private void Update()
    {
        if (Volta_normal)
        {
            transform.localScale = Vector3.one * scale;
            maxScale = transform.localScale.x + diference;
            minScale = transform.localScale.x - diference;
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
