using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemigo : MonoBehaviour
{
    [Header("Movvimiento")]
    public List<Transform> waypoints = new List<Transform>();
    private int targetIndex = 1;
    public float movementSpeed = 4;
    public float rotationSpeed = 6;
    private Animator anim;

    [Header("Life")]
    private bool isDead;
    public float maxLife = 100;
    public float currentLife = 0;
    public Image barraVidaimage;
    private Transform canvasRoot;
    private Quaternion intilLifeRotatoin;

    private void Awake()
    {
        canvasRoot = barraVidaimage.transform.parent.parent;
        intilLifeRotatoin = canvasRoot.rotation;
        anim = GetComponent<Animator>();
        anim.SetBool("Movement", true);
        maxWaypoint();
    }

    private void Start()
    {
        currentLife = maxLife;
    }
    private void maxWaypoint()
    {
        waypoints.Clear();
        var rootWaypoint = GameObject.Find("WaypointContainer").transform;
        for (int i = 0; i < rootWaypoint.childCount; i++)
        {
            waypoints.Add(rootWaypoint.GetChild(i));
        }
    }

    void Update()
    {
        canvasRoot.transform.rotation = intilLifeRotatoin;
        Movement();
        LookAt();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamege(10);
        }

    }

    #region Moviment & Rotations
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
    #endregion

    public void TakeDamege(float dmg)
    {
        var newLife = currentLife - dmg;
        if (isDead)
        {
            return;
        }
        if (newLife <= 0)
        {
            OnDead();

        }
        currentLife = newLife;
        var fillValue = currentLife * 1 / 100;
        barraVidaimage.fillAmount = fillValue;
        currentLife = newLife;
        StartCoroutine(AnimationDamge());
    }

    private IEnumerator AnimationDamge()
    {
        anim.SetBool("TakeDamage", true);
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("TakeDamage", false);
    }

    private void OnDead()
    {
        isDead = true;
        anim.SetBool("TakeDamage", false);
        anim.SetBool("Die", true);
        currentLife = 0;
        barraVidaimage.fillAmount = 0;
        StartCoroutine(OnDaedEffect());
    }

    private IEnumerator OnDaedEffect()
    {
        yield return new WaitForSeconds(1f);
        var FinalPositionY = transform.position.y - 5;
        Vector3 target = new Vector3(transform.position.x, FinalPositionY, transform.position.z);
        while (transform.position.y != FinalPositionY)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, 1.5f * Time.deltaTime);
            yield return null;
           
        }
        Destroy(this);
    }


}
