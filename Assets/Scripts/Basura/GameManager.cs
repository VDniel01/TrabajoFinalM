using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;
    public Text puntosText;
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
        puntosText.text = "Puntos: " + puntos.ToString();
    }

    public bool UsarPuntos(int cantidad)
    {
        if (puntos >= cantidad)
        {
            puntos -= cantidad;
            puntosText.text = "Puntos: " + puntos.ToString();
            return true;
        }
        return false;
    }
}
