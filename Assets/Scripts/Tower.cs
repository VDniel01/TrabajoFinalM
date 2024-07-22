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
        if (bullet != null && shootPosition != null)
        {
            var bulletInstance = Instantiate(bullet, shootPosition.position, shootPosition.rotation);
            bulletInstance.SetBullet(currentTarget, CurrendData.dmg);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, CurrendData.range);
    }
}
