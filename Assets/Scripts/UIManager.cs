using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instancia;

    public GameObject panelDeTorres;
    private Casillero casilleroSeleccionado;

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

    public void MostrarPanelDeTorres(Casillero casillero)
    {
        casilleroSeleccionado = casillero;
        panelDeTorres.SetActive(true);
    }

    public void OcultarPanelDeTorres()
    {
        panelDeTorres.SetActive(false);
    }

    public void SeleccionarTorre(GameObject torrePrefab)
    {
        casilleroSeleccionado.ColocarTorre(torrePrefab);
        OcultarPanelDeTorres();
    }
}
