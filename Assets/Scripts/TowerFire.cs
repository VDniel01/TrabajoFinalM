using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerFire : Tower
{
    public BulletFire bulletFire; // Usamos BulletFire en lugar de Bullet

    private void Start()
    {
        StartCoroutine(ShootTimer());
    }

    private void Update()
    {
        EnemyDetection();
        LookRotation();
    }

    private void OnMouseDown()
    {
        UIpanelManager.instance.OpenPanel(this); // Ahora this es de tipo TowerFire, que hereda de Tower
    }

    public override void EnemyDetection()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, CurrendData.range);
        currentTargets = hitColliders
            .Select(hitCollider => hitCollider.GetComponent<Enemigo>())
            .Where(enemy => enemy != null && !enemy.isDead)
            .ToList();

        if (currentTargets.Count > 0)
        {
            currentTarget = currentTargets[0];
            Debug.Log("Enemigo detectado: " + currentTarget.name);
        }
        else
        {
            currentTarget = null;
            Debug.Log("No se detectaron enemigos.");
        }
    }

    public override void LookRotation()
    {
        if (currentTarget != null)
        {
            Vector3 direction = currentTarget.transform.position - rotationPart.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            Vector3 rotation = Quaternion.Lerp(rotationPart.rotation, lookRotation, Time.deltaTime * 10).eulerAngles;
            rotationPart.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
    }

    public override IEnumerator ShootTimer()
    {
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
        if (bulletFire != null && shootPosition != null)
        {
            var bulletInstance = Instantiate(bulletFire, shootPosition.position, shootPosition.rotation);
            bulletInstance.SeekTarget(currentTarget.transform);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, CurrendData.range);
    }
}
