using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonMovement : MonoBehaviour
{
    public float movementSpeed = 1f; // Velocidade do movimento
    public float movementAmplitude = 1f; // Amplitude do movimento
    public float movementFrequency = 1f; // Frequência do movimento

    private Transform playerTransform;
    private Vector3 initialPosition;
    public float activeDuration;
    public GameObject effc;

    private void Start()
    {
        playerTransform = transform.parent;
        initialPosition = transform.localPosition;
    }

    private void Update()
    {
        float verticalMovement = Mathf.Sin(Time.time * movementFrequency) * movementAmplitude;
        Vector3 newPosition = initialPosition + new Vector3(0f, verticalMovement, 0f);
        transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, movementSpeed * Time.deltaTime);
    }

    private void OnEnable()
    {
        Invoke("DeactivateObject", activeDuration);
    }

    private void DeactivateObject()
    {
        Instantiate(effc, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }
}

