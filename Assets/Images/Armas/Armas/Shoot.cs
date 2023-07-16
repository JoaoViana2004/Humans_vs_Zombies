using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject Ponta;
    public GameObject Bala;
    public GameObject effect;

    public bool atirar;
    public int municao = 30;
    public float fireRate = 10f; // Taxa de disparo (tiros por segundo)
    private float nextFireTime; // Próximo momento em que é permitido disparar

    public float p = .1f;
    public float speed_bullet;


    private void Start()
    {
        nextFireTime = Time.time;
    }
    public void Atirar(Vector2 direcao)
    {
        if (municao >= 1 && PodeAtirar())
        {
            GameObject Bala_Criada = Instantiate(Bala, Ponta.transform.position, Ponta.transform.rotation);

            Bala_Criada.GetComponent<Bullet>().direct = new Vector3(speed_bullet, 0, 0);

            Instantiate(effect, Ponta.transform.position, Ponta.transform.rotation);
            municao -= 1;
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    public bool PodeAtirar()
    {
        return Time.time >= nextFireTime; // Verifica se já passou do momento de disparo
    }
}
