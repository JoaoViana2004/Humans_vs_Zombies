using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Identify : MonoBehaviour
{
    private const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";
    public string token;
    public bool subindo;
    public bool descendo;

    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            int randomIndex = Random.Range(0, alphabet.Length);
            char randomLetter = alphabet[randomIndex];
            token += randomLetter;
        }

        Debug.Log("Generated Token "+gameObject.name+": " + token);
    }
}
