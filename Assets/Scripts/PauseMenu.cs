using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseButton;
    public GameObject pauseMenuUI;

    private bool isPaused = false;

    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    public void Pause()
    {
        isPaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Detiene el tiempo del juego
    }

    public void Resume()
    {
        isPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Reanuda el tiempo del juego
    }

    public void Restart()
    {
        Time.timeScale = 1f; // Asegura que el tiempo esté corriendo al reiniciar
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reinicia la escena actual
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f; // Asegura que el tiempo esté corriendo al volver al menú
        SceneManager.LoadScene("SampleScene"); // Cambia "MenuScene" por el nombre de tu escena de menú
    }
}
