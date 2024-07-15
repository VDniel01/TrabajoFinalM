using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Castillo : MonoBehaviour
{
    public static Castillo instance; // Instancia estática

    public float maxVida = 1000;
    public float vidaActual;
    public Text contadorVidaText;
    public GameObject gameOverPanel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

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

    private void Victory()
    {
        UIManager.Instance.ShowVictoryScreen();
        Time.timeScale = 0f;
    }

    private bool NoMoreEnemies()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
    }

    public void CheckForVictory()
    {
        if (WaveManager.instance.wavesFinish && NoMoreEnemies())
        {
            Victory();
        }
    }

    public void ReiniciarNivel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IrAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene"); // Cambia esto al nombre de tu menú principal
    }
}
