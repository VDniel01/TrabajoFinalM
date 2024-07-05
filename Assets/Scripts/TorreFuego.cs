using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreFuego : MonoBehaviour
{
    public float rango = 15f;
    public float frecuenciaDisparo = 1f;
    public GameObject proyectilPrefab;
    public Transform puntoDisparo;
    public float dañoPorSegundo = 10f;
    public float duración = 5f;

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
            proyectil.dañoPorSegundo = 10f; // Puedes ajustar este valor según sea necesario
            proyectil.duración = 3f; // Puedes ajustar este valor según sea necesario
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rango);
    }
}
