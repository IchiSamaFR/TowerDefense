using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Target")]
    public Transform    target;
    public float        maxRange = 10f;
    public float        range;
    public string       tagEnemy = "enemy";

    [Header("Rotation")]
    public Transform    toRotate;
    public float        speedRotation = 2f;
    
    [Header("Shoot")]
    public float        maxDmg = 5;
    public float        dmg;
    public float        maxBulletSpeed = 5;
    public float        bulletSpeed; 
    public float        fireRate = 2;
    public float        fireCountdown;
    public float        maxExplodeRadius;
    public float        explodeRadius;
    public Transform    bulletSpawn;
    public GameObject   bullet;


    [Header("Effects")]
    public float slowMultpl = -1;
    public float slowTime = 0;
    public float poisonDmg = -1;
    public float poisonTime = 0;

    public AudioSource audioSource;

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();

        dmg = maxDmg;
        bulletSpeed = maxBulletSpeed;
        explodeRadius = maxExplodeRadius;
        range = maxRange;

        InvokeRepeating("UpdateTarget", 0f, 0.1f);
    }

    void Update()
    {
        LookTarget();
        CanShoot();
    }


    public void UpdateTarget()
    {
        GameObject[]    enemies = GameObject.FindGameObjectsWithTag(tagEnemy);
        float           shortestDistance = Mathf.Infinity;
        GameObject      nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if(distanceEnemy < shortestDistance)
            {
                nearestEnemy = enemy;
                shortestDistance = distanceEnemy;
            }
        }


        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        } else
        {
            target = null;
        }
    }

    public void LookTarget()
    {
        if(!target)
        {
            return;
        }

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(toRotate.rotation, lookRotation, Time.deltaTime * speedRotation).eulerAngles;
        toRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

    }

    public void CanShoot()
    {

        fireCountdown -= Time.deltaTime;

        if (!target)
        {
            return;
        }

        if (fireCountdown <= 0)
        {
            Shoot();
            fireCountdown = 1 / fireRate;
        }

    }

    public void Shoot()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
            audioSource.Play();
        }
        var rota = Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z);
        GameObject bullObj = Instantiate(bullet, new Vector3(bulletSpawn.transform.position.x, this.transform.position.y + 0.2f, bulletSpawn.transform.position.z), rota);
        if (explodeRadius > 0)
        {
            bullObj.GetComponent<Bullet>().Set(target, dmg, bulletSpeed, explodeRadius);
        }
        else
        {
            bullObj.GetComponent<Bullet>().Set(target, dmg, bulletSpeed);
        }
        if (slowMultpl > -1)
        {
            bullObj.GetComponent<Bullet>().SetSlow(slowMultpl, slowTime);
        }
        if (poisonDmg > -1)
        {
            bullObj.GetComponent<Bullet>().SetPoison(poisonDmg, poisonTime);
        }
    }
}
