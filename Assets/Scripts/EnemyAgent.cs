using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAgent : MonoBehaviour {

    private Transform destination;
    public float startHealth = 100;
    private float currentHealth;
    public int moneyReward = 50;
    private bool isDead = false;

    public NavMeshAgent agent;
    public Canvas canvas;

    [HideInInspector]
    public float startSpeed;

    [HideInInspector]
    public float currentSpeed;

    [Header("Particle Effects")]
    public GameObject deathEffect;
    public GameObject endPathEffect;

    [Header("Unity Staff")]
    public Image healthBar;
    private Quaternion initialRotation;
    public GameObject parent;

    // Use this for initialization
    void Start () {
        startSpeed = agent.speed;
        currentSpeed = startSpeed;

        currentHealth = startHealth;

        initialRotation = canvas.transform.rotation;
        
        transform.position += Vector3.up;

        destination = GameObject.FindGameObjectWithTag("Finish").transform;
        agent.destination = destination.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        
        agent.speed = currentSpeed;
        
        canvas.transform.rotation = initialRotation;

        float distanceToEnd = Vector3.Distance(transform.position, destination.transform.position);
        if (distanceToEnd <= 1f)
        {
            EndPath();
            return;
        }
        
    }

    void EndPath ()
    {
        PlayerStats.LIVES--;
        WaveSpanner.ENEMIESALIVE--;

        GameObject effect = Instantiate(endPathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);

        Destroy(parent);
    }

    public void TakeDamage (float amount)
    {
        currentHealth -= amount;
        healthBar.fillAmount = currentHealth / startHealth;
        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }
    
    private void Die ()
    {
        FindObjectOfType<AudioManager>().Play("explode");
        isDead = true;
        PlayerStats.MONEY += moneyReward;

        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);

        WaveSpanner.ENEMIESALIVE--;
        Destroy(parent);
    }

    public void SlowSpeed (float slowSpeedPercentatge)
    {
        agent.speed = startSpeed * (1f - slowSpeedPercentatge);
    }
}
