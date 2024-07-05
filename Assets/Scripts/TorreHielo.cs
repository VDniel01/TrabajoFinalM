using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreHielo : MonoBehaviour
{
    public float rango = 15f;
    public float frecuenciaDisparo = 1f;
    public GameObject proyectilPrefab;
    public Transform puntoDisparo;
    public float tiempoRalentización = 2f;
    public float factorRalentización = 0.5f;

    private Transform objetivo;
    private float tiempoDisparo = 0f;

    void Update()
    {
        BuscarObjetivo();

        if (objetivo != null && tiempoDisparo <= 0f)
        {
            Disparar();
            tiempoDisparo = 1f / frecuenciaDisparo;
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
        ProyectilHielo proyectil = proyectilGO.GetComponent<ProyectilHielo>();

        if (proyectil != null)
        {
            proyectil.Buscar(objetivo);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rango);
    }
}
