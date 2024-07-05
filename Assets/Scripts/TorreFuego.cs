using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreFuego : MonoBehaviour
{
    public float rango = 15f;
    public float frecuenciaDisparo = 1f;
    public GameObject proyectilPrefab;
    public Transform puntoDisparo;
    public float da�oPorSegundo = 10f;
    public float duraci�n = 5f;

    private Transform objetivo;
    private float tiempoDisparo = 0f;

    void Update()
    {
        BuscarObjetivo();

        if (objetivo != null)
        {
            Disparar();
        }

        tiempoDisparo -= Time.deltaTime;
    }

    void BuscarObjetivo()
    {
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemigo");
        float distanciaCercana = Mathf.Infinity;
        GameObject enemigoCercano = null;

        foreach (GameObject enemigo in enemigos)
        {
            float distancia = Vector3.Distance(transform.position, enemigo.transform.position);
            if (distancia < distanciaCercana)
            {
                distanciaCercana = distancia;
                enemigoCercano = enemigo;
            }
        }

        if (enemigoCercano != null && distanciaCercana <= rango)
        {
            objetivo = enemigoCercano.transform;
        }
        else
        {
            objetivo = null;
        }
    }

    void Disparar()
    {
        GameObject proyectilGO = Instantiate(proyectilPrefab, puntoDisparo.position, puntoDisparo.rotation);
        ProyectilFuego proyectil = proyectilGO.GetComponent<ProyectilFuego>();
        if (proyectil != null)
        {
            proyectil.da�oPorSegundo = 10f; // Puedes ajustar este valor seg�n sea necesario
            proyectil.duraci�n = 3f; // Puedes ajustar este valor seg�n sea necesario
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rango);
    }
}
