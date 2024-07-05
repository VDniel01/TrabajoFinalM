using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void IniciarJuego()
    {
        SceneManager.LoadScene("MainScene"); // Asegúrate de que la escena principal esté nombrada correctamente
    }

    public void SalirJuego()
    {
        Application.Quit();
    }
}
