using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public string towerName;
    public string towerDescription;
    public float buyPrice;
    public float sellPrice;

    public float range;
    public float dmg = 20;
    public float timeToShoot = 1;
    public Enemigo currentTargert;
    public List<Enemigo> currentTargets = new List<Enemigo>();

    public Transform rotationPart;

    private void Start()
    {
        StartCoroutine(ShootTimer());
    }
    private void Update()
    {
        EnemyDetection();
        LookRotation();
    }

    private void EnemyDetection()
    {
        var enemys = Physics.OverlapSphere(transform.position, range).Where(currentEnemys => currentEnemys.GetComponent<Enemigo>()).Select(currentEnemys => currentEnemys.GetComponent<Enemigo>()).ToList();
        if (currentTargets.Count > 0)
            currentTargert = currentTargets[0];
        else if (currentTargets.Count == 0)
            currentTargert = null;
    }

    private void LookRotation()
    {
        if (currentTargert)
        {
            rotationPart.LookAt(currentTargert.transform);
        }
    }

    private IEnumerator ShootTimer()
    {
        while (true)
        {
            if (currentTargert)
            {
                Shoot();
                yield return new WaitForSeconds(timeToShoot);
            }

            yield return null;
        }
    }

    private void Shoot()
    {
        currentTargert.TakeDamege(dmg);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
