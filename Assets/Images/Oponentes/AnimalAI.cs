using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class AnimalAI : MonoBehaviour
{
    public float decisionInterval = 1f;
    public Animator animator;
    public float speed = 1;
    public float speed_a = 1;

    public GameObject dead_effect;
    private bool isWalking = false;

    public int pos = 1;
    public float scale;

    public bool Vingativo=false;
    public GameObject Obj_Vingar;
    public bool Vingou = true;
    public Vector2 Distancia;
    public float Distancia2;

    public bool Voador;

    private void Start()
    {
        animator = GetComponent<Animator>();
        InvokeRepeating("MakeDecision", decisionInterval, decisionInterval);
        scale = transform.localScale.x;
    }

    private void Update()
    {
        if (Vingou)
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
                // Lógica para mover o animal para a direita ou esquerda.

                transform.Translate(Vector2.right * speed_a * Time.deltaTime);
            }

            // Atualiza o parâmetro 'walk' do animator para definir a animação correta.
            animator.SetBool("Walk", isWalking);
        }
        else
        {
            Atras_da_Vinganca();
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

    public void Vingar(string token)
    {
        if (Vingativo)
        {
            Debug.Log("Vou atrás do Token: " + token);

            Rastreia(token);
            Debug.Log("Vou atrás do: " + Obj_Vingar.name);
            Vingou = false;
        }
    }

    public void Atras_da_Vinganca()
    {
        if (Obj_Vingar == null)
        {
            Vingou = true;
            animator.SetBool("Attack", false);
        }
        else
        {
            Vector3 direction = Obj_Vingar.transform.position - transform.position;
            direction.Normalize();
            Distancia = new Vector2(direction.x, direction.y);
            Distancia2 = Mathf.Abs(Obj_Vingar.transform.position.x - transform.position.x) + Mathf.Abs(Obj_Vingar.transform.position.y - transform.position.y);

            if (Mathf.Abs(Distancia.x) > 0.97 && Voador == false)
            {
                animator.SetBool("Walk", true);
                animator.SetBool("Attack", false);

                if (direction.x > 0)
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
                else if (direction.x < 0)
                {
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
                transform.position += new Vector3(direction.x, 0, 0) * speed * Time.deltaTime;

            }
            else if ((Mathf.Abs(Obj_Vingar.transform.position.x - transform.position.x) + Mathf.Abs(Obj_Vingar.transform.position.y - transform.position.y)) > 0.4 && Voador == true)
            {
                animator.SetBool("Walk", true);
                animator.SetBool("Attack", false);
                if (direction.x > 0)
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
                else if (direction.x < 0)
                {
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
                transform.position += direction * speed * Time.deltaTime;
            }
            else if (Voador)
            {
                //
            }
            else
            {
                animator.SetBool("Attack", true);
                animator.SetBool("Walk", false);
            }
        }
    }

    
    private void Rastreia(string token)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject Player = GameObject.Find("Player");

        if(Player.GetComponent<Identify>().token == token)
        {
            Obj_Vingar =  Player;
        }

        foreach (GameObject obj in objects)
        {
            if (obj.GetComponent<Identify>().token == token)
            {
                Obj_Vingar = obj;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Instantiate(dead_effect, transform.position, transform.rotation);
            animator.SetBool("Hurt", true);
        }
    }
}