using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{
    public float burnDamage = 10f;
    public float duration = 3f;
    public float speed = 10f;

    [Header("Visual Effects")]
    public GameObject hitEffect; // ARRASTRA AQUÍ TU PREFAB DE EXPLOSIÓN

    private Transform target;

    public void SeekTarget(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        if (target == null)
        {
            ObjectPool.Instance.ReturnObject(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    void HitTarget()
    {
        Enemigo enemigo = target.GetComponent<Enemigo>();
        if (enemigo != null)
        {
            enemigo.ApplyBurnEffect(burnDamage, duration);
        }

        // --- NUEVO: Spawnear Efecto Visual ---
        if (hitEffect != null)
        {
            GameObject effectInstance = ObjectPool.Instance.GetObject(hitEffect);
            effectInstance.transform.position = transform.position;
            effectInstance.transform.rotation = transform.rotation;
        }
        // -------------------------------------

        ObjectPool.Instance.ReturnObject(gameObject);
    }
}