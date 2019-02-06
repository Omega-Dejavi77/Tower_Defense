using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public GameObject bulletImpactEffect;
    private Transform target;
    public float speed;
    public int damage;

	// Use this for initialization
	void Start () {
        if (gameObject.tag == "Missile")
            FindObjectOfType<AudioManager>().Play("missile");
        
        else
            FindObjectOfType<AudioManager>().Play("shot"); 
    }
	
	// Update is called once per frame
	void Update () {
		if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceToThisFrame = speed * Time.deltaTime;
        if (dir.magnitude <= distanceToThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceToThisFrame, Space.World);
        transform.LookAt(target);
	}

    public void Seek (Transform _target)
    {
        target = _target;
    }

    void HitTarget ()
    {
        GameObject effectIns = Instantiate(bulletImpactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 5f);
        Damage(target); 
        Destroy(gameObject);
    }

    void Damage (Transform enemy)
    {
        EnemyAgent e = enemy.GetComponent<EnemyAgent>();
        if (e != null)
        {
            e.TakeDamage(damage);
        }
    }
}
