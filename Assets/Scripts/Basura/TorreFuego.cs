using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreFuego : TorreBase
{
    public GameObject proyectilPrefab;
    public Transform puntoDisparo;

    void Update()
    {
        if (objetivo != null)
        {
            Disparar();
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
}
