using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilFuego : MonoBehaviour
{
    public float dañoPorSegundo = 10f;
    public float duración = 3f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemigo"))
        {
            Enemigo enemigo = other.GetComponent<Enemigo>();
            if (enemigo != null)
            {
                enemigo.RecibirDañoPorSegundo(dañoPorSegundo, duración);
                Destroy(gameObject); // Destruir el proyectil después de impactar
            }
        }
    }
}
