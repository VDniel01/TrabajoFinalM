using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{
    public float burnDamage = 10f;
    public float duration = 3f;
    public float speed = 10f;

    [Header("Visual & Audio")]
    public GameObject hitEffect; // Prefab de Partícula
    public AudioClip impactSound; // --- NUEVO: Sonido Impacto ---

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

        // --- NUEVO: Sonido ---
        if (impactSound != null)
        {
            AudioSource.PlayClipAtPoint(impactSound, transform.position);
        }

        // Efecto visual
        if (hitEffect != null)
        {
            GameObject effectInstance = ObjectPool.Instance.GetObject(hitEffect);
            effectInstance.transform.position = transform.position;
            effectInstance.transform.rotation = transform.rotation;
        }

        ObjectPool.Instance.ReturnObject(gameObject);
    }
}