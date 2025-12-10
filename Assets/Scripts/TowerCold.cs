using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCold : Tower
{
    public BulletCold bulletCold;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(ShootTimer());
    }

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
        if (bulletCold != null && shootPosition != null)
        {
            GameObject bulletInstance = ObjectPool.Instance.GetObject(bulletCold.gameObject);

            bulletInstance.transform.position = shootPosition.position;
            bulletInstance.transform.rotation = shootPosition.rotation;

            BulletCold bulletScript = bulletInstance.GetComponent<BulletCold>();
            if (bulletScript != null)
            {
                bulletScript.SeekTarget(currentTarget.transform);
            }

            // --- NUEVO: Sonido ---
            if (audioSource != null && CurrendData.shootSound != null)
            {
                audioSource.PlayOneShot(CurrendData.shootSound);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (CurrendData != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, CurrendData.range);
        }
    }
}