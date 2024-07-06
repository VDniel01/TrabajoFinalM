using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>();
    private int targetIndex = 1;
    public float movementSpeed = 4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[targetIndex].position, movementSpeed * Time.deltaTime);
    }
}
