using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject menuColocarTorre;
    private Casillero casilleroSeleccionado;

    public void MostrarMenuColocarTorre(Casillero casillero)
    {
        casilleroSeleccionado = casillero;
        menuColocarTorre.SetActive(true);
    }

    public void OcultarMenuColocarTorre()
    {
        menuColocarTorre.SetActive(false);
    }

    public void ColocarTorreSeleccionada()
    {
        if (casilleroSeleccionado != null)
        {
            casilleroSeleccionado.ColocarTorre();
            OcultarMenuColocarTorre();
        }
    }
}
