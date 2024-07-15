using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Castillo : MonoBehaviour
{
    public float maxVida = 1000;
    public float vidaActual;
    public Text contadorVidaText;
    public GameObject gameOverPanel;

    private void Start()
    {
        vidaActual = maxVida;
        ActualizarContadorVida();
        gameOverPanel.SetActive(false);
    }

    public void RecibirDaño(float daño)
    {
        vidaActual -= daño;
        if (vidaActual <= 0)
        {
            vidaActual = 0;
            GameOver();
        }
        ActualizarContadorVida();
    }

    private void ActualizarContadorVida()
    {
        if (contadorVidaText != null)
        {
            contadorVidaText.text = vidaActual.ToString();
        }
    }

    private void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ReiniciarNivel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IrAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }
}
