using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileset_config : MonoBehaviour
{
    private List<BoxCollider2D> boxColliders;
    private SpriteRenderer Player;
    private SpriteRenderer Sprite_eu;

    public bool player_acima = false;
    public bool player_abaixo = false;

    public float Posicao_self;
    public float Posicao_Player;
    public float dif;

    public bool baixou = false;

    public float corrigido;
    public bool correcao;
    public GameObject Eu;
    public GameObject pay;
    public bool debugar=false;

    

    private void Start()
    {
        Sprite_eu = GetComponent<SpriteRenderer>();
        // Inicializa a lista de box colliders
        boxColliders = new List<BoxCollider2D>();

        // Obtém todos os BoxColliders2D presentes no objeto
        BoxCollider2D[] colliders = GetComponentsInChildren<BoxCollider2D>();

        // Adiciona os box colliders à lista
        boxColliders.AddRange(colliders);

        Player = GameObject.Find("Player").GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        float distance = transform.position.x - Player.transform.position.x;
        Posicao_Player = (Player.transform.position.y - (Player.bounds.size.y/2));
        Posicao_self = transform.position.y + (Sprite_eu.bounds.size.y)/2 + dif ;

        if (correcao )
        {
            Posicao_self += corrigido;
        }

        if (debugar)
        {
            Eu.transform.position = new Vector3(Eu.transform.position.x, Posicao_self, Eu.transform.position.z);
            pay.transform.position = new Vector3(pay.transform.position.x, Posicao_Player, pay.transform.position.z);
        }
        if ( (Posicao_Player >= Posicao_self) && baixou== false && (Mathf.Abs(distance) <= 1f))
        {
            Ativa();
            player_abaixo=false;
            player_acima=true;
        }
        else if ((Posicao_Player < Posicao_self))
        {
            Desativa();
            player_abaixo = true;
            player_acima = false;
            if (baixou== true) 
            {
                baixou = false;
            }
        }

        if(baixou== true)
        {
            Desativa();
        }
    }
    void Desativa()
    {
        // Desativa todos os box colliders
        foreach (BoxCollider2D collider in boxColliders)
        {
            collider.enabled = false;
        }
    }

    void Ativa()
    {
        // Desativa todos os box colliders
        foreach (BoxCollider2D collider in boxColliders)
        {
            collider.enabled = true;
        }
    }
}
