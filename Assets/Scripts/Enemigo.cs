using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemigo : MonoBehaviour
{
    [Header("Movimiento")]
    public List<Transform> waypoints = new List<Transform>();
    private int targetIndex = 1;
    public float movementSpeed = 4;
    public float rotationSpeed = 6;
    private Animator anim;

    [Header("Vida")]
    public bool isDead = false;
    public float maxLife = 100;
    public float currentLife = 0;
    public Image barraVidaimage;
    private Transform canvasRoot;
    private Quaternion intilLifeRotatoin;

    [Header("Muerto")]
    public int MoneyOnDead = 10;

    [Header("Daño al castillo")]
    public float dañoAlCastillo = 50;
    private Castillo castillo;

    private Coroutine coldEffectCoroutine;
    private Coroutine burnEffectCoroutine;

    private void Awake()
    {
        canvasRoot = barraVidaimage?.transform.parent.parent;
        intilLifeRotatoin = canvasRoot?.rotation ?? Quaternion.identity;
        anim = GetComponent<Animator>();
        if (GetComponent<Collider>() == null)
        {
            gameObject.AddComponent<BoxCollider>();
        }
        if (anim == null)
        {
            Debug.LogError("Animator component is missing on " + gameObject.name);
        }
        else
        {
            anim.SetBool("Movement", true);
        }
        maxWaypoint();
    }

    private void Start()
    {
        isDead = false; // Asegúrate de que el enemigo no esté muerto al inicio
        currentLife = maxLife;
        castillo = GameObject.FindObjectOfType<Castillo>(); // Encuentra el castillo en la escena
    }

    private void maxWaypoint()
    {
        waypoints.Clear();
        var rootWaypoint = GameObject.Find("WaypointContainer")?.transform;
        if (rootWaypoint != null)
        {
            for (int i = 0; i < rootWaypoint.childCount; i++)
            {
                waypoints.Add(rootWaypoint.GetChild(i));
            }
        }
        else
        {
            Debug.LogError("WaypointContainer not found in the scene.");
        }
    }

    void Update()
    {
        if (canvasRoot != null)
        {
            canvasRoot.transform.rotation = intilLifeRotatoin;
        }
        if (!isDead) // Solo mover si no está muerto
        {
            Movement();
            LookAt();
        }
    }

    #region Movimiento y Rotaciones
    private void Movement()
    {
        if (isDead)
        {
            return;
        }
        if (waypoints.Count > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[targetIndex].position, movementSpeed * Time.deltaTime);
            var distance = Vector3.Distance(transform.position, waypoints[targetIndex].position);
            if (distance <= 0.1f)
            {
                if (targetIndex >= waypoints.Count - 1)
                {
                    AtacarCastillo(); // Atacar al castillo al llegar al final del recorrido
                    return;
                }
                targetIndex++;
            }
        }
    }

    private void LookAt()
    {
        if (isDead)
        {
            return;
        }
        if (waypoints.Count > 0)
        {
            var dir = waypoints[targetIndex].position - transform.position;
            var rootTarget = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rootTarget, rotationSpeed * Time.deltaTime);
        }
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
        PlayerData.instance.AddMoney(MoneyOnDead);
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
        Destroy(gameObject);
    }

    private void AtacarCastillo()
    {
        if (castillo != null)
        {
            castillo.RecibirDaño(dañoAlCastillo);
        }
        Destroy(gameObject);
    }

    public void ApplyColdEffect(float slowAmount, float duration)
    {
        if (coldEffectCoroutine != null)
        {
            StopCoroutine(coldEffectCoroutine);
        }
        coldEffectCoroutine = StartCoroutine(ColdEffectCoroutine(slowAmount, duration));
    }

    private IEnumerator ColdEffectCoroutine(float slowAmount, float duration)
    {
        float originalSpeed = movementSpeed;
        movementSpeed *= slowAmount;
        yield return new WaitForSeconds(duration);
        movementSpeed = originalSpeed;
        coldEffectCoroutine = null;
    }

    public void ApplyBurnEffect(float burnDamage, float duration)
    {
        if (burnEffectCoroutine != null)
        {
            StopCoroutine(burnEffectCoroutine);
        }
        burnEffectCoroutine = StartCoroutine(BurnEffectCoroutine(burnDamage, duration));
    }

    private IEnumerator BurnEffectCoroutine(float burnDamage, float duration)
    {
        float elapsed = 0;
        while (elapsed < duration)
        {
            TakeDamege(burnDamage * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
        burnEffectCoroutine = null;
    }
}