using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance; // Instancia est�tica para acceso global

    [Header("Pool Settings")]
    public GameObject objectPrefab; // Prefab del objeto a ser "pooling"
    public int poolSize = 10; // Tama�o inicial del pool

    private Queue<GameObject> poolQueue = new Queue<GameObject>();

    private void Awake()
    {
        // Implementar el patr�n Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Inicializar el pool
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectPrefab);
            obj.SetActive(false); // El objeto no est� activo al inicio
            poolQueue.Enqueue(obj);
        }
    }

    public GameObject GetObject()
    {
        if (poolQueue.Count > 0)
        {
            GameObject obj = poolQueue.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            // Opcionalmente puedes crear m�s objetos si el pool est� vac�o
            GameObject obj = Instantiate(objectPrefab);
            return obj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        poolQueue.Enqueue(obj);
    }
}
