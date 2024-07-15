using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCold : MonoBehaviour
{
    public float slowAmount = 0.5f;
    public float duration = 3f;
    public float speed = 10f;
    private Transform target;

    public void SeekTarget(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
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
            enemigo.ApplyColdEffect(slowAmount, duration);
        }
        Destroy(gameObject);
    }
}
