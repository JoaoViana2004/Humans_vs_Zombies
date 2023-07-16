using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectChance
{
    public GameObject obj;
    public float chance;
}

public class Gerador : MonoBehaviour
{
    public ObjectChance[] objectsToSpawn; // List of objects with occurrence chance
    public float xMin, xMax, yMin, yMax; // 2D space limits
    public float spawnInterval = 5f; // Intervalo de tempo para o spawn dos objetos
    private float timer = 0f; // Tempo decorrido desde a última invocação dos objetos

    private void Update()
    {
        timer += Time.deltaTime; // Atualiza o timer com o tempo decorrido desde o último quadro

        if (timer >= spawnInterval)
        {
            SpawnObjects();
            timer = 0f; // Reinicia o timer
        }
    }

    private void SpawnObjects()
    {
        foreach (ObjectChance objChance in objectsToSpawn)
        {
            if (UnityEngine.Random.Range(0f, 100f) <= objChance.chance)
            {
                float xPos = UnityEngine.Random.Range(xMin, xMax);
                float yPos = UnityEngine.Random.Range(yMin, yMax);

                Instantiate(objChance.obj, new Vector3(xPos, yPos, 0f), Quaternion.identity);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.position.x > xMax || collision.transform.position.x <xMin || collision.transform.position.y > yMax || collision.transform.position.y < yMin)
        {
            Destroy(collision.gameObject);
        }
    }
}

