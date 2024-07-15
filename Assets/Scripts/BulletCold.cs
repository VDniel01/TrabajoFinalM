using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCold : Bullet
{
    public float slowDuration = 3f;
    public float slowAmount = 0.5f; // Reduce speed to 50%

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, velocity * Time.deltaTime);
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance <= 0.1f)
            {
                target.TakeDamege(dmg);
                target.ApplyColdEffect(slowAmount, slowDuration);
                Destroy(gameObject);
            }
        }
    }
}
