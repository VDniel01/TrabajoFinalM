using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance; // Instancia estática para acceso global

    [Header("Pool Settings")]
    // Ya no necesitamos un "objectPrefab" único aquí, porque lo pediremos dinámicamente.
    // Usamos un Diccionario: Clave (Nombre del prefab) -> Valor (Cola de objetos)
    private Dictionary<string, Queue<GameObject>> pools = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        // Implementar el patrón Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Modificado: Ahora recibe el prefab que queremos spawnear
    public GameObject GetObject(GameObject prefab)
    {
        string key = prefab.name; // La clave es el nombre del prefab

        // Si no existe el pool para este objeto, lo creamos
        if (!pools.ContainsKey(key))
        {
            pools[key] = new Queue<GameObject>();
        }

        if (pools[key].Count > 0)
        {
            GameObject obj = pools[key].Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            // Si no hay objetos disponibles, instanciamos uno nuevo
            GameObject obj = Instantiate(prefab);
            obj.name = key; // IMPORTANTE: Le quitamos el "(Clone)" para que el nombre coincida con la clave al devolverlo
            return obj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        string key = obj.name; // Usamos el nombre del objeto para saber a qué cola devolverlo

        obj.SetActive(false);

        if (pools.ContainsKey(key))
        {
            pools[key].Enqueue(obj);
        }
        else
        {
            // Si intentamos devolver un objeto que no se creó mediante el pool, lo destruimos por seguridad
            Debug.LogWarning("El objeto " + key + " no pertenece a ningún pool registrado.");
            Destroy(obj);
        }
    }
}