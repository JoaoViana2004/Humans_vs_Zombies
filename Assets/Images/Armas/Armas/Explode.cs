using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public float power = 10f;
    public float explosionRadius = 5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Explosive"))
        {
            ExplodeObjects();
        }

        Debug.Log(collision.gameObject.layer);
        Debug.Log("Bateu");
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
}
