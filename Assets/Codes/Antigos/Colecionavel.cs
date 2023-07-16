using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colecionavel : MonoBehaviour
{
    public float scaleSpeed = 1f;
    public float maxScale = 1.2f;
    public float minScale = 0.8f;
    public float rotationSpeed = 50f;
    public float rotationAmplitude = 15f;
    public GameObject efeito;

    public int num_object;

    private Vector3 baseScale;
    private float rotationOffset;

    private void Start()
    {
        baseScale = transform.localScale;
        rotationOffset = Random.Range(0f, 360f);
    }

    private void Update()
    {
        // Alterna a escala do objeto entre o valor mínimo e máximo
        float scale = Mathf.PingPong(Time.time * scaleSpeed, maxScale - minScale) + minScale;
        transform.localScale = baseScale * scale;

        // Rotaciona o objeto em torno do eixo Y
        float rotationAngle = Mathf.Sin(Time.time * rotationSpeed) * rotationAmplitude;
        transform.rotation = Quaternion.Euler(0f, rotationAngle + rotationOffset, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(num_object == 1) {
                collision.gameObject.GetComponent<PlayerCode>().object1 = true;
            }
            if(num_object == 2) { 
                collision.gameObject.GetComponent<PlayerCode>().object2 = true;
            }
            if (num_object == 3) { 
                collision.gameObject.GetComponent<PlayerCode>().object3 = true;
            }

            Instantiate(efeito, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
