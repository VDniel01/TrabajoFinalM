using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casillero : MonoBehaviour
{
    public GameObject torrePrefab;
    public int costoTorre = 50;
    private GameObject torre;
    private UIManager uiManager;

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    void OnMouseDown()
    {
        uiManager.MostrarMenuColocarTorre(this);
    }

    public void ColocarTorre()
    {
        if (torre == null && GameManager.instancia.UsarPuntos(costoTorre))
        {
            torre = Instantiate(torrePrefab, transform.position, transform.rotation);
        }
    }
}
