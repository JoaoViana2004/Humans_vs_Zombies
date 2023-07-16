using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Zona_espada : MonoBehaviour
{
    public string token;
    public GameObject player;
    public bool enemy;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Arma")
        {
            if (collision.gameObject.tag == "Mob")
            {
                collision.gameObject.GetComponent<Life>().vida -= 1;
            }
            else if ((collision.gameObject.tag == "Enemy"  && !enemy)|| collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<Life>().vida -= 1;
                collision.GetComponent<Animator>().SetBool("Hurt", true);
            }
        }
    }
}
