using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPool : MonoBehaviour
{
    public float lifeTime = 1.5f; // Tiempo antes de volver al pool

    private void OnEnable()
    {
        // Al activarse, programamos su desactivación
        Invoke("Return", lifeTime);
    }

    private void Return()
    {
        ObjectPool.Instance.ReturnObject(gameObject);
    }

    private void OnDisable()
    {
        // Cancelamos por si acaso se desactivó manualmente antes
        CancelInvoke();
    }
}