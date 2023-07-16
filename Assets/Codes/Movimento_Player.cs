using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movimento_Player : MonoBehaviour
{
    public Vector2 myInput; // Vector2 que armazena os inputs do joystick de movimento
    public float velocidade = 5f; // Velocidade de movimento do jogador
    private Animator anim;
    public FixedJoystick js_mov;
    private float scale;
    public GameObject ponta;
    private float scale_ponta;
    private float scale_arma;
    public bool pos;

    public float limit_min_x;
    public float limit_max_x;

    public float limit_min_y;
    public float limit_max_y;

    public GameObject effect;

    public int vida = 3;

    private void Start()
    {
        anim = GetComponent<Animator>();
        scale = transform.localScale.x;
    }

    public void Update()
    {
        myInput = new Vector2(Input.GetAxis("Horizontal") + js_mov.Horizontal, Input.GetAxis("Vertical")+js_mov.Vertical);

        anim.SetBool("Walk_x", Mathf.Abs(myInput.x) > 0.05f);
        anim.SetBool("Walk_y", Mathf.Abs(myInput.y) > 0.3f);

        if (myInput.x > 0)
        {
            transform.localScale = new Vector3(scale, scale, scale);
            pos = true;
        }
        else if (myInput.x < 0) 
        {
            transform.localScale = new Vector3(-scale, scale, scale);

            pos = false;    
        }
        Movimentar_Player();
    }
    public void Movimentar_Player()
    {
        Vector2 direcaoMovimento = myInput;

        // Normaliza o vetor para evitar movimento mais rápido na diagonal
        direcaoMovimento.Normalize();

        // Calcula o vetor de deslocamento multiplicando pela velocidade e deltaTime
        Vector2 deslocamento = direcaoMovimento * velocidade * Time.deltaTime;

        if (transform.position.x > limit_min_x && transform.position.x < limit_max_x && transform.position.y > limit_min_y && transform.position.y < limit_max_y)
        {
            // Move o jogador na direção desejada
            transform.Translate(deslocamento);
        }
        if (transform.position.x > limit_max_x)
        {
            transform.position = new Vector3(limit_max_x- 0.1f, transform.position.y, 0);
        }
        else if (transform.position.x < limit_min_x)
        {
            transform.position = new Vector3(limit_min_x+ 0.1f, transform.position.y, 0);
        }
        if (transform.position.y < limit_min_y)
        {
            transform.position = new Vector3(transform.position.x, limit_min_y+ 0.1f, 0);
        }
        else if (transform.position.y > limit_max_y)
        {
            transform.position = new Vector3(transform.position.x, limit_max_y - 0.1f, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.CompareTag("Zona_Espada"))
        {
            vida--;
            Instantiate(effect, transform.position, transform.rotation);
        }
    }
}
