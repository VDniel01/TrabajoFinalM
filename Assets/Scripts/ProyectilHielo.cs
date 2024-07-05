using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilHielo : MonoBehaviour
{
    private Transform objetivo;
    public float velocidad = 70f;
    public float tiempoRalentización = 2f;
    public float factorRalentización = 0.5f;

    public void Buscar(Transform _objetivo)
    {
        objetivo = _objetivo;
    }

    void Update()
    {
        if (objetivo == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direccion = objetivo.position - transform.position;
        float distanciaEsteFrame = velocidad * Time.deltaTime;

        if (direccion.magnitude <= distanciaEsteFrame)
        {
            Impacto();
            return;
        }

        transform.Translate(direccion.normalized * distanciaEsteFrame, Space.World);
        transform.LookAt(objetivo);
    }

    void Impacto()
    {
        Enemigo enemigo = objetivo.GetComponent<Enemigo>();

        if (enemigo != null)
        {
            enemigo.Ralentizar(factorRalentización, tiempoRalentización);
        }

        Destroy(gameObject);
    }
}
