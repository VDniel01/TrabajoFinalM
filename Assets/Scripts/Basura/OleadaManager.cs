using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OleadaManager : MonoBehaviour
{
    public Transform[] puntosDeSpawn;
    public GameObject enemigoNormalPrefab;
    public GameObject enemigoTanquePrefab;
    public GameObject bossPrefab;
    public int oleada = 0;

    void Start()
    {
        StartCoroutine(IniciarOleadas());
    }

    IEnumerator IniciarOleadas()
    {
        while (true)
        {
            oleada++;
            for (int i = 0; i < oleada * 10; i++)
            {
                SpawnEnemigo(enemigoNormalPrefab);
                yield return new WaitForSeconds(0.5f);
            }
            for (int i = 0; i < oleada * 2; i++)
            {
                SpawnEnemigo(enemigoTanquePrefab);
                yield return new WaitForSeconds(1f);
            }
            SpawnEnemigo(bossPrefab);
            yield return new WaitForSeconds(5f);
        }
    }

    void SpawnEnemigo(GameObject enemigoPrefab)
    {
        Transform puntoDeSpawn = puntosDeSpawn[Random.Range(0, puntosDeSpawn.Length)];
        Instantiate(enemigoPrefab, puntoDeSpawn.position, puntoDeSpawn.rotation);
    }
}
