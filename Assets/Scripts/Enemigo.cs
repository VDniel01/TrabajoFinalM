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

    public void RecibirDaño(float cantidad)
    {
        vida -= cantidad;
        if (vida <= 0)
        {
            Matar();
        }
    }

    public void RecibirDañoPorSegundo(float cantidad, float duración)
    {
        StartCoroutine(DañoPorSegundo(cantidad, duración));
    }

    private IEnumerator DañoPorSegundo(float cantidad, float duración)
    {
        float tiempo = 0;
        while (tiempo < duración)
        {
            RecibirDaño(cantidad * Time.deltaTime);
            tiempo += Time.deltaTime;
            yield return null;
        }
    }

    public void Ralentizar(float factor, float duración)
    {
        StartCoroutine(Ralentización(factor, duración));
    }

    private IEnumerator Ralentización(float factor, float duración)
    {
        float velocidadOriginal = agente.speed;
        agente.speed *= factor;
        yield return new WaitForSeconds(duración);
        agente.speed = velocidadOriginal;
    }

    void Matar()
    {
        GameManager.instancia.AñadirPuntos(puntos);
        Destroy(gameObject);
    }
}
