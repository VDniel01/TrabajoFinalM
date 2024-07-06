using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public List<WaveObject> waves = new List<WaveObject>();
    public bool isWaitingForNextWave;
    public bool wavesFinish;
    public int currentWave;
    public Transform intPositon;

    public TextMeshProUGUI counterText;
    public GameObject buttonNextWave;

    private void Start()
    {
        StartCoroutine(ProcesWave());
    }
    private void Update()
    {
        CheckCounterAndShowButton();
        CheckCounterForNExtWave();
    }


    private void CheckCounterForNExtWave()
    {
        if (isWaitingForNextWave && !wavesFinish)
        {
            waves[currentWave].counterToNextWavve -= 1 * Time.deltaTime;
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
        waves[currentWave].counterToNextWavve = waves[currentWave].timerForNextWave;
        for (int i = 0; i < waves[currentWave].enemigos.Count; i++)
        {
            var eneMyGo = Instantiate(waves[currentWave].enemigos[i], intPositon.position, intPositon.rotation);
            yield return new WaitForSeconds(waves[currentWave].timerPerCreation);
        }
        isWaitingForNextWave = true;
        if (currentWave >= waves.Count -1)
        {
            Debug.Log("Nivel Terminado");
            wavesFinish = true;
        }
    }

   
    private void CheckCounterAndShowButton()
    {
        if (!wavesFinish)
        {
            buttonNextWave.SetActive(isWaitingForNextWave);
            counterText.gameObject.SetActive(isWaitingForNextWave);

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
