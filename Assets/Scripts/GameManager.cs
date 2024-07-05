using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;
    public Text puntosTexto;
    private int puntos;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AñadirPuntos(int cantidad)
    {
        puntos += cantidad;
        puntosTexto.text = "Puntos: " + puntos;
    }

    public bool UsarPuntos(int cantidad)
    {
        if (puntos >= cantidad)
        {
            puntos -= cantidad;
            puntosTexto.text = "Puntos: " + puntos;
            return true;
        }
        return false;
    }
}
