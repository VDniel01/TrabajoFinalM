using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : Bullet
{
    public float burnDuration = 3f;
    public float burnDamagePerSecond = 5f;

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, velocity * Time.deltaTime);
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance <= 0.1f)
            {
                target.TakeDamege(dmg);
                target.ApplyBurnEffect(burnDamagePerSecond, burnDuration);
                Destroy(gameObject);
            }
        }
    }
}
