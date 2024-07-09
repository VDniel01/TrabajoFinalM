using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Enemigo target;
    private float dmg;
    public float velocity = 90;

    public void SetBullet(Enemigo target, float dmg)
    {
        this.target = target;
        this.dmg = dmg;
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, velocity * Time.deltaTime);
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance <= 0.1f)
            {
                target.TakeDamege(dmg);
                Destroy(gameObject);
            }
        }
    }
}
