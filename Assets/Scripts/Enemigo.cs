using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    public float vida;
    public float velocidad;
    public int puntos;
    protected NavMeshAgent agente;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        agente.speed = velocidad;
    }

    public void RecibirDa�o(float cantidad)
    {
        vida -= cantidad;
        if (vida <= 0)
        {
            Matar();
        }
    }

    public void RecibirDa�oPorSegundo(float cantidad, float duraci�n)
    {
        StartCoroutine(Da�oPorSegundo(cantidad, duraci�n));
    }

    private IEnumerator Da�oPorSegundo(float cantidad, float duraci�n)
    {
        float tiempo = 0;
        while (tiempo < duraci�n)
        {
            RecibirDa�o(cantidad * Time.deltaTime);
            tiempo += Time.deltaTime;
            yield return null;
        }
    }

    public void Ralentizar(float factor, float duraci�n)
    {
        StartCoroutine(Ralentizaci�n(factor, duraci�n));
    }

    private IEnumerator Ralentizaci�n(float factor, float duraci�n)
    {
        float velocidadOriginal = agente.speed;
        agente.speed *= factor;
        yield return new WaitForSeconds(duraci�n);
        agente.speed = velocidadOriginal;
    }

    void Matar()
    {
        GameManager.instancia.A�adirPuntos(puntos);
        Destroy(gameObject);
    }
}
