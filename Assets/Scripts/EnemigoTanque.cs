using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemigoTanque : Enemigo
{
    // Puedes personalizar las estadísticas del Enemigo Tanque aquí
    void Start()
    {
        vida = 200f; // más vida
        velocidad = 3f; // velocidad lenta
        puntos = 20; // puntos al matar
        agente = GetComponent<NavMeshAgent>();
        agente.speed = velocidad;
    }
}
