using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// --- NUEVO: Obliga a que la torre tenga AudioSource ---
[RequireComponent(typeof(AudioSource))]
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

    protected AudioSource audioSource; // --- NUEVO: Referencia al audio ---

    private void Start()
    {
        // Configuración de audio
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0.8f; // Sonido 3D

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
        if (CurrendData == null) return;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, CurrendData.range);
        currentTargets = hitColliders
            .Select(hitCollider => hitCollider.GetComponent<Enemigo>())
            .Where(enemy => enemy != null && !enemy.isDead)
            .ToList();

        if (currentTargets.Count > 0)
        {
            currentTarget = currentTargets[0];
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
        // Corrección: Espera inicial
        yield return new WaitForSeconds(0.5f);

        while (true)
        {
            if (CurrendData == null)
            {
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
            // CAMBIO: Usamos el Pool en lugar de Instantiate
            GameObject bulletObj = ObjectPool.Instance.GetObject(bullet.gameObject);
            bulletObj.transform.position = shootPosition.position;
            bulletObj.transform.rotation = shootPosition.rotation;

            Bullet bulletScript = bulletObj.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.SetBullet(currentTarget, CurrendData.dmg);
            }
        }

        // Sonido de disparo (Ya lo tenías)
        if (audioSource != null && CurrendData != null && CurrendData.shootSound != null)
        {
            audioSource.PlayOneShot(CurrendData.shootSound);
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