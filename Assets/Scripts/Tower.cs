using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public string towerName;
    public string towerDescription;
    public TowerData CurrendData;
    public Enemigo currentTarget;
    public List<Enemigo> currentTargets = new List<Enemigo>();

    public Transform rotationPart;
    public Transform shootPosition;

    public Bullet bullet;

    [Header("Tower Upgrade")]
    public List<TowerData> towerUpgradeData = new List<TowerData>();
    public int currentIndexUpgrade = 0;

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
        UIpanelManager.instance.OpenPanel(this);

    }

    public virtual void EnemyDetection()
    {
        // Protección: Si no hay datos, no hacemos nada para evitar errores
        if (CurrendData == null) return;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, CurrendData.range);
        currentTargets = hitColliders
            .Select(hitCollider => hitCollider.GetComponent<Enemigo>())
            .Where(enemy => enemy != null && !enemy.isDead)
            .ToList();

        if (currentTargets.Count > 0)
        {
            currentTarget = currentTargets[0];
            // Debug.Log("Enemigo detectado: " + currentTarget.name); // Comentado para limpiar la consola
        }
        else
        {
            currentTarget = null;
        }
    }

    public virtual void LookRotation()
    {
        if (currentTarget != null)
        {
            Vector3 direction = currentTarget.transform.position - rotationPart.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            Vector3 rotation = Quaternion.Lerp(rotationPart.rotation, lookRotation, Time.deltaTime * 10).eulerAngles;
            rotationPart.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
    }

    public virtual IEnumerator ShootTimer()
    {
        // --- CORRECCIÓN: Espera inicial de 0.5 segundos ---
        // Esto evita el disparo instantáneo al colocar la torre
        yield return new WaitForSeconds(0.5f);
        // --------------------------------------------------

        while (true)
        {
            if (CurrendData == null)
            {
                Debug.LogWarning("La torre " + gameObject.name + " no tiene Datos (ScriptableObject) asignados.");
                yield return null;
            }
            else if (currentTarget != null)
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
        if (bullet != null && shootPosition != null)
        {
            var bulletInstance = Instantiate(bullet, shootPosition.position, shootPosition.rotation);
            // Aseguramos que la bala sepa cuánto daño hacer según los datos actuales de la torre
            bulletInstance.SetBullet(currentTarget, CurrendData.dmg);
        }
    }

    private void OnDrawGizmos()
    {
        if (CurrendData != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, CurrendData.range);
        }
    }
}