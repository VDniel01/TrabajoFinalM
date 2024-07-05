using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreBase : MonoBehaviour
{
    public Transform objetivo;
    public float rango = 15f;
    public float velocidadDisparo = 1f;
    private float tiempoDisparo = 0f;

    void Update()
    {
        if (objetivo == null)
        {
            BuscarObjetivo();
            return;
        }

        if (Vector3.Distance(transform.position, objetivo.position) > rango)
        {
            objetivo = null;
            return;
        }

        if (tiempoDisparo <= 0f)
        {
            Disparar();
            tiempoDisparo = 1f / velocidadDisparo;
        }

        tiempoDisparo -= Time.deltaTime;
    }

    void BuscarObjetivo()
    {
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemigo");
        float distanciaMasCercana = Mathf.Infinity;
        GameObject enemigoMasCercano = null;

        foreach (GameObject enemigo in enemigos)
        {
            float distancia = Vector3.Distance(transform.position, enemigo.transform.position);
            if (distancia < distanciaMasCercana)
            {
                distanciaMasCercana = distancia;
                enemigoMasCercano = enemigo;
            }
        }

        if (enemigoMasCercano != null && distanciaMasCercana <= rango)
        {
            objetivo = enemigoMasCercano.transform;
        }
    }

    void Disparar()
    {
        // Implementación específica en las subclases
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rango);
    }
}
