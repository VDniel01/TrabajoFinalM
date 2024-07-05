using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casillero : MonoBehaviour
{
    private GameObject torre;

    void OnMouseDown()
    {
        UIManager.instancia.MostrarPanelDeTorres(this);
    }

    public void ColocarTorre(GameObject torrePrefab)
    {
        if (torre == null && GameManager.instancia.UsarPuntos(10)) // Ajusta el costo de las torres según sea necesario
        {
            torre = Instantiate(torrePrefab, transform.position, Quaternion.identity);
        }
    }
}
