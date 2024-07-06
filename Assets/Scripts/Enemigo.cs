using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>();
    private int targetIndex = 1;
    public float movementSpeed = 4;
    public float rotationSpeed = 6;
    private Animator anim;
    private bool isTakeDamage;
    private bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Movement", true);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        LookAt();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isTakeDamage = !isTakeDamage;
            anim.SetBool("TakeDamage", isTakeDamage);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            isDead = true;
            anim.SetBool("Die", true);
        }
    }

    private void Movement()
    {
        if (isDead)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[targetIndex].position, movementSpeed * Time.deltaTime);
        var distance = Vector3.Distance(transform.position, waypoints[targetIndex].position);
        if (distance <= 0.1f)
        {
            if (targetIndex >= waypoints.Count -1)
            {
                Debug.Log("subir targetindex");
                return;
            }
            targetIndex++;
        }
    }
    private void LookAt()
    {
        if (isDead)
        {
            return;
        }
        //transform.LookAt(waypoints[targetIndex]);
        var dir = waypoints[targetIndex].position - transform.position;
        var rootTarget = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rootTarget, rotationSpeed * Time.deltaTime);
    }
}
