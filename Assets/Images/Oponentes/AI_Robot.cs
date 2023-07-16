using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class AI_Robot : MonoBehaviour
{
    private GameObject Player;
    public float detectionDistance = 2f;
    public int pos = 1;
    public float distance;

    public float decisionInterval = 1f;
    public Animator animator;
    public float speed = 1;
    public float speed_a = 1;
    public float Distancia2;

    public GameObject dead_effect;
    private bool isWalking = false;
    private bool isJumping = false;
    public Vector2 Distancia;

    public float scale;
    public bool Player_Avistado = false;
    private Rigidbody2D rb;

    public bool pode = true;
    public GameObject Jetpack;
    public GameObject Jetpack_i;

    public float timer = 0f;
    public float Interval = 5f;
    public GameObject arma;
    public GameObject arma_local;

    public float duracaoAtirar = 5f; // Duração do atirar
    public float duracaoIntervalo = 4f; // Duração do intervalo entre os tiros


    public bool shoot = false;

    public float tempo_inicial = 0;
    public float TempoAtirar=10f;
    public float intervalo=1f;

    private void Start()
    {
        Player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        InvokeRepeating("MakeDecision", decisionInterval, decisionInterval);
        scale = transform.localScale.x;
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if(GetComponent<Life>().vida <=0)
        {
            arma.GetComponent<PickupWeapon>().Enemy_Weapon = false;
            arma.GetComponent<PickupWeapon>().Volta_normal = true;
            arma.transform.SetParent(null);
            if(Jetpack_i != null)
            {
                Destroy(Jetpack_i);
            }
            Destroy(gameObject);
        }

        if(shoot)
        {
            arma.GetComponent<Shoot>().Atirar(new Vector2());
        }
        arma.transform.position = arma_local.transform.position;
        if (isJumping)
        {
            Jetpack_i.transform.position = transform.position;
        }
        if (!Player_Avistado)
        {
            // Verificar Posição do Personagem (Direita ou Esquerda) 
            if (pos == 1)
            {
                transform.localScale = new Vector3(scale, scale, scale);
            }
            else
            {
                transform.localScale = new Vector3(-scale, scale, scale);
            }

            if (isWalking)
            {
                // Lógica para mover o animal para a direita ou esquerda
                transform.Translate(Vector2.right * speed_a * Time.deltaTime);
            }

            animator.SetBool("Walk", isWalking);
            distance = transform.position.x - Player.transform.position.x;
            if (((Mathf.Abs(distance) <= detectionDistance && distance < 0 && pos == 1) && Mathf.Abs(transform.position.y - Player.transform.position.y) < 0.5) || ((Mathf.Abs(distance) <= detectionDistance && distance > 0 && pos == -1) && Mathf.Abs(transform.position.y - Player.transform.position.y) < 0.5))
            {
                Debug.Log("Player detected!");
                atack_player();
                Player_Avistado = true;
            }
        }
        else
        {
            atack_player();
        }
    }

    private void MakeDecision()
    {
        if (!Player_Avistado)
        {
            // Gera um número aleatório entre 0 e 2.
            int decision =UnityEngine. Random.Range(0, 4);

            if (decision == 0)
            {
                speed_a = speed;
                isWalking = true; // Anda para a direita.
                pos = 1;
            }
            else if (decision == 1)
            {
                isWalking = false; // Fica parado.
            }
            else if (decision == 2)
            {
                isWalking = false; // Fica parado.
                isJumping = true; // Fica parado.
                Jetpack_i = Instantiate(Jetpack, transform.position, transform.rotation);
                rb.AddForce(new Vector2(0f, 7f), ForceMode2D.Impulse);
            }
            else
            {
                isWalking = true; // Anda para a esquerda.
                speed_a = -speed;
                pos = -1;
            }
        }
    }

    public void atack_player()
    {
        Vector3 direction = Player.transform.position - transform.position;
        direction.Normalize();
        Distancia = new Vector2(direction.x, direction.y);
        Distancia2 = Player.transform.position.x - transform.position.x;


        if (Mathf.Abs(Distancia2) > 0.4)
        {
            timer += Time.deltaTime;
            animator.SetBool("Walk", true);
            //animator.SetBool("Attack", false);

            if (pode)
            {
                if (direction.x > 0)
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                    pos = 1;
                }
                else if (direction.x < 0)
                {
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                    pos = -1;
                }
                transform.position += new Vector3(direction.x, 0, 0) * speed * Time.deltaTime;

                if (direction.y > 0.5 && isJumping != true)
                {
                    isJumping = true; // Fica parado.
                    Jetpack_i = Instantiate(Jetpack, transform.position, transform.rotation);
                    rb.AddForce(new Vector2(0f, 7f), ForceMode2D.Impulse);
                }

                if ((Time.time - tempo_inicial) < TempoAtirar)
                {
                    arma.GetComponent<Shoot>().Atirar(new Vector2());

                }
                else if ((Time.time - tempo_inicial) < TempoAtirar + intervalo)
                {
                    //
                }
                else
                {
                    tempo_inicial = Time.time;
                }
            }

        }
        else
        {
            //animator.SetBool("Attack", true);
            animator.SetBool("Walk", false);
        }
    }
    
    public void Desativa_Movimentacao()
    {
        pode = false;
    }

    public void Ativa_Movimentacao()
    {
        pode = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
            Destroy(Jetpack_i);
        }
    }
}