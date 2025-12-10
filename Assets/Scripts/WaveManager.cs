using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    [Header("Configuración de Oleadas")]
    public List<WaveObject> waves = new List<WaveObject>();
    public Transform intPositon; // Punto de spawn

    [Header("Estado del Juego")]
    public bool isWaitingForNextWave;
    public bool wavesFinish;
    public int currentWave;
    private bool levelHasStarted = false; // Nueva variable para saber si ya dimos "Start"

    [Header("UI References")]
    public TextMeshProUGUI counterText;
    public GameObject buttonNextWave;
    public GameObject startLevelButton; // ¡Nuevo botón para asignar en el Inspector!

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
        // Al inicio, aseguramos que el botón de Start esté activo y el de Next Wave oculto
        if (startLevelButton != null)
            startLevelButton.SetActive(true);

        if (buttonNextWave != null)
            buttonNextWave.SetActive(false);

        if (counterText != null)
            counterText.gameObject.SetActive(false);
    }

    private void Update()
    {
        CheckCounterAndShowButton();
        CheckCounterForNextWave();
        Castillo.instance.CheckForVictory();
    }

    // --- NUEVA FUNCIÓN: Conectar esto al botón "Start Level" en Unity ---
    public void StartLevel()
    {
        if (levelHasStarted) return; // Evitar doble click

        levelHasStarted = true;

        if (startLevelButton != null)
            startLevelButton.SetActive(false); // Ocultamos el botón de inicio

        StartCoroutine(ProcesWave()); // Iniciamos la primera oleada
    }

    private void CheckCounterForNextWave()
    {
        // Solo contamos si el nivel ya empezó
        if (levelHasStarted && isWaitingForNextWave && !wavesFinish)
        {
            waves[currentWave].counterToNextWavve -= 1 * Time.deltaTime;

            if (counterText != null)
                counterText.text = waves[currentWave].counterToNextWavve.ToString("00");

            if (waves[currentWave].counterToNextWavve <= 0)
            {
                ChangeWave();
                Debug.Log("set next wave");
            }
        }
    }

    public void ChangeWave()
    {
        if (wavesFinish)
            return;
        currentWave++;
        StartCoroutine(ProcesWave());
    }

    private IEnumerator ProcesWave()
    {
        if (wavesFinish)
            yield break;

        isWaitingForNextWave = false;

        // Ocultamos texto y botón mientras salen enemigos
        if (counterText != null) counterText.gameObject.SetActive(false);
        if (buttonNextWave != null) buttonNextWave.SetActive(false);

        waves[currentWave].counterToNextWavve = waves[currentWave].timerForNextWave;

        for (int i = 0; i < waves[currentWave].enemigos.Count; i++)
        {
            var eneMyGo = Instantiate(waves[currentWave].enemigos[i], intPositon.position, intPositon.rotation);
            yield return new WaitForSeconds(waves[currentWave].timerPerCreation);
        }

        isWaitingForNextWave = true;

        if (currentWave >= waves.Count - 1)
        {
            Debug.Log("Nivel Terminado");
            wavesFinish = true;
            Castillo.instance.CheckForVictory();
        }
    }

    private void CheckCounterAndShowButton()
    {
        // Solo mostramos la UI de "Siguiente Oleada" si el nivel YA empezó
        if (levelHasStarted && !wavesFinish)
        {
            if (buttonNextWave != null) buttonNextWave.SetActive(isWaitingForNextWave);
            if (counterText != null) counterText.gameObject.SetActive(isWaitingForNextWave);
        }
    }

    [System.Serializable]
    public class WaveObject
    {
        public float timerPerCreation = 1;
        public float timerForNextWave = 10;
        [HideInInspector] public float counterToNextWavve = 0;
        public List<Enemigo> enemigos = new List<Enemigo>();
    }
}