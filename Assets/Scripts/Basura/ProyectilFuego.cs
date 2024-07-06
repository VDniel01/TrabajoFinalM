using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilFuego : MonoBehaviour
{
    public float da�oPorSegundo = 10f;
    public float duraci�n = 3f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemigo"))
        {
            Enemigo enemigo = other.GetComponent<Enemigo>();
            if (enemigo != null)
            {
                enemigo.RecibirDa�oPorSegundo(da�oPorSegundo, duraci�n);
                Destroy(gameObject); // Destruir el proyectil despu�s de impactar
            }
        }
    }
}
