using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject pausePanel;
    public GameObject victoryPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowPauseMenu()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void HidePauseMenu()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ShowVictoryScreen()
    {
        victoryPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene"); // Asegúrate de que este es el nombre correcto de tu menú principal
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
