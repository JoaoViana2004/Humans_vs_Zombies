using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 direct;

    public float power = 1f;
    public float randomY;
    public float explosionRadius = 1f;
    public string endereco;
    public float speed = -10f;

    public bool Enemy=false;
    public bool pode = false;

    public GameObject Mini_cabou;

    void Update()
    {
        transform.Translate(new Vector3(direct.x*Time.deltaTime, 0,0));
    }

    private void ExplodeObjects()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = rb.transform.position - transform.position;
                float distance = direction.magnitude;
                float force = (1 - distance / explosionRadius) * power;
                rb.AddForce(direction.normalized * force, ForceMode2D.Impulse);
            }
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Arma" && collision.gameObject.tag != "Player" && collision.gameObject.name != "Zona_espada" && collision.gameObject.tag != "BackGround")
        {
                if (collision.gameObject.tag == "Enemy")
                {
                    collision.gameObject.GetComponent<Life>().vida -= 1;
                }

                Debug.Log(collision.name);

                ExplodeObjects();
                Instantiate(Mini_cabou, transform.position, transform.rotation);

                Destroy(gameObject);
        }
    }
}
