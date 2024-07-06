using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemigoNormal : Enemigo
{
    void Start()
    {
        vida = 100f; // vida normal
        velocidad = 5f; // velocidad normal
        puntos = 10; // puntos al matar
        agente = GetComponent<NavMeshAgent>();
        agente.speed = velocidad;
    }
}
