using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esteira : MonoBehaviour
{
    public float conveyorSpeed = 5f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D playerRigidbody = other.GetComponent<Rigidbody2D>();
            playerRigidbody.velocity = new Vector2(conveyorSpeed, playerRigidbody.velocity.y);
        }
    }
}
