using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public int vida = 10;
    private Animator anim;
    public GameObject cabou;

    public bool Anim_Death;

    public bool animal;
    public bool morreu = false;
    public bool player_life=false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (vida <= 0 && morreu == false)
        {
            GameObject.Find("Gerenciador").GetComponent<Genrenciador_Encontrador>().Salada_de_texto();

            Instantiate(cabou, transform.position, transform.rotation);
            morreu = true;


            if (Anim_Death)
            {
                anim.SetBool("Death", true);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player_life)
        {
            if (collision.CompareTag("Zona_Espada"))
            {
                vida--;
                Instantiate(GetComponent<Movimento_Player>().effect, transform.position, transform.rotation);
                Instantiate(GetComponent<Movimento_Player>().effect, transform.position, transform.rotation);
                Instantiate(GetComponent<Movimento_Player>().effect, transform.position, transform.rotation);
            }
        }
    }
}
