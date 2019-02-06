using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    private Transform target;
    private EnemyAgent enemyTarget;
    public WaveSpanner waveSpanner;

    [Header("General Atributtes")]
    public float range;


    [Header("Using Bullets")]
    public GameObject bullet;
    public float fireRate = 1f; //cadencia
    private float fireCountdown = 0f;


    [Header("Using Laser")]
    public bool usingLaser = false;
    public LineRenderer lineRenderer;
    public ParticleSystem particleImpactEffect;
    public Light impactLight;
    public int damageOverTime = 30;
    public float slowSpeed = 0.5f;


    [Header("Unity SetUp Fields")]
    public Transform partToRotate;
    public string enemytag;
    public float turnSpeed;
    public Transform firePoint;

    void Awake()
    {
        waveSpanner = FindObjectOfType<WaveSpanner>();
    }
    // Use this for initialization
    void Start () {
        InvokeRepeating("UpdateTarget", waveSpanner.count, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemytag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            //Debug.Log(enemy.transform.position);
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            enemyTarget = target.GetComponent<EnemyAgent>();
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update () {
		if (target == null)
        {

            if (usingLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    particleImpactEffect.Stop();
                    impactLight.enabled = false;
                }      
            }
            return;
        }
        
        // rotatcion torreta
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (usingLaser)
        {
            Laser();
        }
        else // using Bullets
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }
	}

    void Shoot ()
    {
        GameObject bulletGO = Instantiate(bullet, firePoint.position, firePoint.rotation);
        Bullet b = bulletGO.GetComponent<Bullet>();
        if (b != null)
        {
            b.Seek(target);
        }
    }

    void Laser ()
    {
        FindObjectOfType<AudioManager>().Play("laser");
        enemyTarget.TakeDamage(damageOverTime * Time.deltaTime);
        enemyTarget.SlowSpeed(slowSpeed);

        if (!lineRenderer.enabled)
        {
            FindObjectOfType<AudioManager>().Stop("laser");
            lineRenderer.enabled = true;
            particleImpactEffect.Play();
            impactLight.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 direction = firePoint.position - transform.position;

        particleImpactEffect.transform.position = target.position + direction.normalized * 0.5f;

        particleImpactEffect.transform.rotation = Quaternion.LookRotation(direction);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
