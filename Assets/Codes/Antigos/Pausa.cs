using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausa : MonoBehaviour
{
    private bool isPaused = false;

    public GameObject tela_Pause;
    public GameObject tela_normal;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Define a escala de tempo como 0 para pausar o jogo
            Debug.Log("Jogo pausado");
            tela_Pause.SetActive(true);
            tela_normal.SetActive(false);

        }
        else
        {
            Time.timeScale = 1f; // Define a escala de tempo de volta para 1 para retomar o jogo
            Debug.Log("Jogo retomado");
            tela_Pause.SetActive(false);
            tela_normal.SetActive(true);


        }
    }
}
