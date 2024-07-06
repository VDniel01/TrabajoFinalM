using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : Enemigo
{
    void Start()
    {
        vida = 500f; // mucha vida
        velocidad = 7f; // velocidad rápida
        puntos = 50; // puntos al matar
        agente = GetComponent<NavMeshAgent>();
        agente.speed = velocidad;
    }
}
