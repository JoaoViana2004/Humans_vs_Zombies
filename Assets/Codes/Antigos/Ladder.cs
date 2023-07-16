using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public float climbingSpeed = 5f;
    private bool isClimbing = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isClimbing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isClimbing = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isClimbing && other.CompareTag("Player"))
        {
            float verticalInput = Input.GetAxis("Vertical");
            Vector2 climbVelocity = new Vector2(0f, verticalInput * climbingSpeed);
            other.GetComponent<Rigidbody2D>().velocity = climbVelocity;
        }
    }
}
