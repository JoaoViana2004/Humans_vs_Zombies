using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class AI_Enemy : MonoBehaviour
{
    private GameObject Player;
    public float detectionDistance = 5f;
    public int pos = 1;
    public float distance;

    public float decisionInterval = 1f;
    public Animator animator;
    public float speed = 1;
    public float speed_a = 1;
    public float Distancia2;

    public GameObject dead_effect;
    private bool isWalking = false;
    public Vector2 Distancia;

    public float scale;
    public bool Player_Avistado = false;
    public Rigidbody2D rb;
    public bool isJumping=false;

    public bool pode=true;
    public float linha_distancia = 0.4f;

    private void Start()
    {
        Player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        InvokeRepeating("MakeDecision", decisionInterval, decisionInterval);
        scale = transform.localScale.x;
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (!Player_Avistado)
        {
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
                rb.velocity = (Vector2.right * speed_a);
            }
            animator.SetBool("Walk", isWalking);
            distance = transform.position.x - Player.transform.position.x;
            if (((Mathf.Abs(distance) <= detectionDistance && distance < 0 && pos == 1) && Mathf.Abs(transform.position.y - Player.transform.position.y)<0.5) || ((Mathf.Abs(distance) <= detectionDistance && distance > 0 && pos == -1) && Mathf.Abs(transform.position.y - Player.transform.position.y) < 0.5))
            {
                Debug.Log("Player detected!");
                atack_player();
                Player_Avistado=true;
            }
        }
        else
        {
            atack_player();
        }
    }

    private void MakeDecision()
    {
        // Gera um número aleatório entre 0 e 2.
        int decision = Random.Range(0, 3);

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
        else
        {
            isWalking = true; // Anda para a esquerda.
            speed_a = -speed;
            pos = -1;
        }
    }

    public void atack_player()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);

        Vector3 direction = Player.transform.position - transform.position;
        direction.Normalize();
        Distancia = new Vector2(direction.x, direction.y);
        Distancia2 = Player.transform.position.x - transform.position.x;
        float Distancia3 = Player.transform.position.y - transform.position.y;


        if (distanceToPlayer > linha_distancia)
        {
            animator.SetBool("Walk", true);
            animator.SetBool("Attack", false);

            if (pode)
            {

                if (direction.x > 0)
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
                else if (direction.x < 0)
                {
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
                transform.Translate(direction*Time.deltaTime);
            }

        }
        else
        {
            animator.SetBool("Attack", true);
            animator.SetBool("Walk", false);
            pode = false;
        }
    }

    public void Agora_Pode()
    {
        pode = true;
    }

    public void Desativa_Movimentacao()
    {
        pode = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            animator.SetBool("Hurt",true);
            pode = false;
        }
    }
}