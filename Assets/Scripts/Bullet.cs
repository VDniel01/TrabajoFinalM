using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected Enemigo target;
    protected float dmg;
    public float velocity = 90;

    [Header("Visual & Audio")]
    public GameObject hitEffect;  // Arrastra el prefab de explosión aquí
    public AudioClip impactSound; // Arrastra el sonido de impacto aquí

    public void SetBullet(Enemigo target, float dmg)
    {
        this.target = target;
        this.dmg = dmg;
    }

    void Update()
    {
        // Si el objetivo muere o desaparece mientras la bala viaja
        if (target == null)
        {
            ObjectPool.Instance.ReturnObject(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, velocity * Time.deltaTime);
        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance <= 0.1f)
        {
            HitTarget();
        }
    }

    protected virtual void HitTarget()
    {
        // Aplicar daño
        if (target != null)
        {
            target.TakeDamege(dmg);
        }

        // --- Sonido de Impacto ---
        if (impactSound != null)
        {
            AudioSource.PlayClipAtPoint(impactSound, transform.position);
        }

        // --- Efecto Visual (Partículas) ---
        if (hitEffect != null)
        {
            GameObject effectInstance = ObjectPool.Instance.GetObject(hitEffect);
            effectInstance.transform.position = transform.position;
            effectInstance.transform.rotation = transform.rotation;
        }

        // Volver al Pool
        ObjectPool.Instance.ReturnObject(gameObject);
    }
}