using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OleadaManager : MonoBehaviour
{
    public GameObject enemigoNormalPrefab;
    public GameObject enemigoTanquePrefab;
    public GameObject bossPrefab;
    public Transform[] puntosDeSpawn;

    private int oleadaActual = 0;

    void Start()
    {
        IniciarOleada();
    }

    void IniciarOleada()
    {
        oleadaActual++;
        StartCoroutine(SpawnOleada());
    }

    IEnumerator SpawnOleada()
    {
        for (int i = 0; i < oleadaActual * 5; i++)
        {
            SpawnEnemigo(enemigoNormalPrefab);
            yield return new WaitForSeconds(1f);
        }

        SpawnEnemigo(bossPrefab);
        yield return new WaitForSeconds(1f);

        yield return new WaitForSeconds(5f); // Tiempo de descanso entre oleadas
        IniciarOleada();
    }

    void SpawnEnemigo(GameObject enemigoPrefab)
    {
        int spawnIndex = Random.Range(0, puntosDeSpawn.Length);
        Instantiate(enemigoPrefab, puntosDeSpawn[spawnIndex].position, Quaternion.identity);
    }
}
