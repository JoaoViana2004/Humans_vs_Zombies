using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCode : MonoBehaviour
{
    private float moveSpeed_n = 1f; // Velocidade de movimento do personagem
    private float moveSpeed_s = 0.5f; // Velocidade de movimento do personagem
    private float moveSpeed = 2f; // Velocidade de movimento do personagem
    private float jumpForce = 3f; // Força do salto do personagem
    private float runSpeedMultiplier = 2f; // Multiplicador de velocidade ao correr
    private float scale;
    public LayerMask groundLayer; // Layer para detectar o chão
    private Animator anim;

    public GameObject dash;
    public GameObject jump;

    public GameObject armaAtual;
    public GameObject Alcance_Dano;
    public Transform localArma;
    public List<GameObject> armas;
    private int Arma_Selecionada=0;
    private SpriteRenderer spriteRenderer;

    private float moveX;
    public int pos= 1;

    public AudioSource passos;
    public AudioSource pulo;
    public LayerMask layerMask;
    public GameObject escudo;
    public bool escudo_ativado;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isJumping=false;
    private bool isRunning;
    public bool ladeira = false;

    private float soundRate = 5f; 
    private float nextSoundTime;
    private string token;
    private bool attack;
    private bool hurt;

    public bool object1;
    public bool object2;
    public bool object3;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        scale = transform.localScale.x;
        spriteRenderer = GetComponent<SpriteRenderer>();
        token = GetComponent<Identify>().token;
        Alcance_Dano.SetActive(false);
    }

    private void Update()
    {
        token = GetComponent<Identify>().token;

        if(GetComponent<Life>().vida <= 0)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }

        if (!hurt && !attack)
        {
            moveX = Input.GetAxisRaw("Horizontal");

            // Verificar Posição do Personagem (Direita ou Esquerda) 
            if (pos == 1)
            {
                transform.localScale = new Vector3(scale, scale, scale);
            }
            else
            {
                transform.localScale = new Vector3(-scale, scale, scale);
            }


            if (moveX > 0)
            {
                anim.SetBool("Walk", true);
                pos = 1;

                if (Pode_tocar())
                {
                    nextSoundTime = Time.time + 1f / soundRate;
                    passos.Play();
                }

            }
            else if (moveX < 0)
            {
                pos = -1;
                anim.SetBool("Walk", true);
                if (Pode_tocar())
                {
                    nextSoundTime = Time.time + 1f / soundRate;
                    passos.Play();
                }
            }
            else
            {
                anim.SetBool("Walk", false);
            }
            if (!ladeira)
            {
                transform.Translate(moveX * moveSpeed * (isRunning ? runSpeedMultiplier : 1f) * Time.deltaTime, 0, 0);
            }
            else
            {
                rb.velocity = new Vector2(moveX * moveSpeed * (isRunning ? runSpeedMultiplier : 1f), rb.velocity.y);
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            GetComponent<Identify>().descendo = true;
            // Cria um array para armazenar os resultados do Raycast
            RaycastHit2D[] hits = new RaycastHit2D[1];

            // Lança um raio para baixo a partir da posição atual do objeto, ignorando as camadas 1 e 2
            int numHits = Physics2D.RaycastNonAlloc(transform.position, Vector2.down, hits, Mathf.Infinity, layerMask);

            // Verifica se o raio atingiu um objeto
            if (numHits > 0)
            {

                // Obtém o GameObject do objeto atingido
                GameObject objectBelow = hits[0].collider.gameObject;

                // Faça algo com o objeto abaixo
                Debug.Log("Objeto abaixo: " + objectBelow.name);
                if (objectBelow.layer == 1)
                {
                    objectBelow.gameObject.GetComponent<tileset_config>().baixou = true;
                }


            }
        }
        else
        {
            GetComponent<Identify>().descendo = false;
        }

        // Pulo
        if (Input.GetKeyDown(KeyCode.Space) && isJumping == false && !attack)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
            Instantiate(jump, transform.position, transform.rotation);
            pulo.Play();

            GetComponent<Identify>().subindo = true;
        }

        // Correr
        if (isRunning== false && Input.GetKey(KeyCode.LeftShift) && isJumping == false && !attack)
        {
            Instantiate(dash, new Vector3(transform.position.x, transform.position.y - (spriteRenderer.bounds.size.y/2), transform.position.z), transform.rotation);
        }
        isRunning = Input.GetKey(KeyCode.LeftShift);
        
        // Atirar
        if (Input.GetKey(KeyCode.J) && armas.Count > 0 && !escudo_ativado && !hurt && !attack)
        {
            armaAtual.GetComponent<Shoot>().Atirar(new Vector2());
        }

        if (Input.GetKeyDown(KeyCode.U)&& armas.Count >0 && !attack)
        {
            Troca_arma();
        }

        // Escudo
        if (Input.GetKey(KeyCode.K) && !hurt && !attack)
        {
            escudo.SetActive(true);
            escudo_ativado = true;
            if (armaAtual!= null)
            {
                armaAtual.SetActive(false);
            }
            moveSpeed = moveSpeed_s;
        }
        else
        {
            escudo.SetActive(false);
            if (armaAtual != null)
            {
                armaAtual.SetActive(true);
            }
            moveSpeed = moveSpeed_n;
            escudo_ativado = false;
        }

        // Espada
        if (Input.GetKeyDown(KeyCode.H) && !hurt)
        {
            //escudo.SetActive(true);
            attack = true;
            anim.SetBool("Attack", true);
            if (armaAtual != null)
            {
                armaAtual.SetActive(false);
            }
            moveSpeed = moveSpeed_s;
        }
        else if (Input.GetKeyUp(KeyCode.H) && !hurt)
        {
            //escudo.SetActive(false);
            anim.SetBool("Attack", false);
            attack = false;
            if (armaAtual != null)
            {
                armaAtual.SetActive(true);
            }
            moveSpeed = moveSpeed_n;
            escudo_ativado = false;
        }


        // Interagir
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Implementar interação, podemos usar uma variavel booleana ESTA_INTERAGINDO que pode ativar ao pressionar E
        }

        // Pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {

        }
    }

    public void Troca_arma() 
    {
        // Desativa a arma atual
        armas[Arma_Selecionada].SetActive(false);

        // Avança para a próxima arma na lista
        Arma_Selecionada++;
        if (Arma_Selecionada >= armas.Count)
        {
            Arma_Selecionada = 0;
        }

        // Ativa a nova arma selecionada
        armas[Arma_Selecionada].SetActive(true);
        armaAtual = armas[Arma_Selecionada];
    }

    public bool Pode_tocar()
    {
        return Time.time >= nextSoundTime;
    }

    public void Leva_Dano()
    {
        hurt = true;
    }

    public void Volta_Dano()
    {
        hurt = false;
    }

    public void Comeca_Dano()
    {
        Alcance_Dano.SetActive(true);
    }

    public void Termina_Dano()
    {
        Alcance_Dano.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
            GetComponent<Identify>().subindo = false;
        }
    }

}
