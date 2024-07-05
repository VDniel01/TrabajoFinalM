using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject menuPausa;

    public void IniciarJuego()
    {
        SceneManager.LoadScene("NombreDeTuEscena");
    }

    public void PausarJuego()
    {
        Time.timeScale = 0f;
        menuPausa.SetActive(true);
    }

    public void ReanudarJuego()
    {
        Time.timeScale = 1f;
        menuPausa.SetActive(false);
    }
}
