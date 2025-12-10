using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerFire : Tower
{
    public GameObject bulletPrefab;

    private void Start()
    {
        // Inicializamos audio aquí también por si acaso, aunque el padre lo hace
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(ShootTimer());
    }

    // Nota: Eliminamos Update, EnemyDetection y LookRotation porque hereda de Tower

    private void OnMouseDown()
    {
        UIpanelManager.instance.OpenPanel(this);
    }

    public override IEnumerator ShootTimer()
    {
        yield return new WaitForSeconds(0.5f); // Espera inicial

        while (true)
        {
            if (currentTarget != null)
            {
                Shoot();
                yield return new WaitForSeconds(CurrendData.timeToShoot);
            }
            else
            {
                yield return null;
            }
        }
    }

    private void Shoot()
    {
        if (bulletPrefab != null && shootPosition != null)
        {
            GameObject bulletInstance = ObjectPool.Instance.GetObject(bulletPrefab);
            bulletInstance.transform.position = shootPosition.position;
            bulletInstance.transform.rotation = shootPosition.rotation;

            BulletFire bullet = bulletInstance.GetComponent<BulletFire>();
            if (bullet != null)
            {
                bullet.SeekTarget(currentTarget.transform);
            }

            // --- NUEVO: Sonido ---
            if (audioSource != null && CurrendData.shootSound != null)
            {
                audioSource.PlayOneShot(CurrendData.shootSound);
            }
        }
    }
}