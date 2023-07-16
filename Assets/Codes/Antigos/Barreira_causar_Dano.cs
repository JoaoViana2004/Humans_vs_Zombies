using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barreira_causar_Dano : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Arma")
        {
            if (collision.gameObject.tag == "Mob")
            {
                collision.gameObject.GetComponent<Life>().vida -= 1;
                collision.GetComponent<Animator>().SetBool("Hurt", true);

            }
            else if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<Life>().vida -= 1;
                collision.GetComponent<Animator>().SetBool("Hurt", true);
            }
        }
    }
}
