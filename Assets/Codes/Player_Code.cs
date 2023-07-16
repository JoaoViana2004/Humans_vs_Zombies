using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player_Code : MonoBehaviour
{
    public GameObject armaAtual;
    public Transform localArma;
    public List<GameObject> armas;
    private int Arma_Selecionada = 0;
    public FixedJoystick js_mov;

    public Vector2 myInput; // Vector2 que armazena os inputs do joystick de movimento



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        myInput = new Vector2(js_mov.Horizontal, js_mov.Vertical);

        if (myInput != Vector2.zero  && armas.Count > 0)
        {
            armaAtual.GetComponent<Shoot>().Atirar(myInput);
        }

        if (Input.GetKeyDown(KeyCode.U) && armas.Count > 0)
        {
            Troca_arma();
        }

        if (armaAtual != null)
        {
            Vector2 direcaoJoystick = new Vector2(js_mov.Horizontal, js_mov.Vertical);

            // Rotaciona a arma de acordo com a direção do joystick
            if (direcaoJoystick != Vector2.zero)
            {
                //if (GetComponent<Movimento_Player>().pos)
                //{
                    float angle = Mathf.Atan2(direcaoJoystick.y, direcaoJoystick.x) * Mathf.Rad2Deg;
                    localArma.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                //}
                //else
                //{
                 //   float angle = Mathf.Atan2(direcaoJoystick.x, -direcaoJoystick.y) * Mathf.Rad2Deg;
                  //  localArma.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
               // }

            }
        }
    }

    public void Troca_arma()
    {
        // Desativa a arma atual
        armas[Arma_Selecionada].SetActive(false);
        armas[Arma_Selecionada].GetComponent<PickupWeapon>().fotinha.GetComponent<Ativa_desativa>().ativado = false;

        // Avança para a próxima arma na lista
        Arma_Selecionada++;
        if (Arma_Selecionada >= armas.Count)
        {
            Arma_Selecionada = 0;
        }

        // Ativa a nova arma selecionada
        armas[Arma_Selecionada].SetActive(true);
        armas[Arma_Selecionada].GetComponent<PickupWeapon>().fotinha.GetComponent<Ativa_desativa>().ativado = true;
        armaAtual = armas[Arma_Selecionada];
    }
}
