using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCold : Tower
{
    public BulletCold bulletCold;

    private void Start()
    {
        StartCoroutine(ShootTimer());
    }

    private void OnMouseDown()
    {
        UIpanelManager.instance.OpenPanel(this);
    }

    public override IEnumerator ShootTimer()
    {
        // --- CORRECCIÓN: Espera inicial para evitar disparo instantáneo ---
        yield return new WaitForSeconds(0.5f);
        // ---------------------------------------------------------------

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
            // Usamos ObjectPool pasando el gameObject del script bulletCold
            GameObject bulletInstance = ObjectPool.Instance.GetObject(bulletCold.gameObject);

            bulletInstance.transform.position = shootPosition.position;
            bulletInstance.transform.rotation = shootPosition.rotation;

            BulletCold bulletScript = bulletInstance.GetComponent<BulletCold>();
            if (bulletScript != null)
            {
                bulletScript.SeekTarget(currentTarget.transform);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, CurrendData.range);
    }
}