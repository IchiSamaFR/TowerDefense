using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Bullet class of towers
 */
public class Bullet : MonoBehaviour
{
    public GameObject   effectParticle;
    Transform           target;
    float               dmg;
    float               speed;
    float               explodeRadius;
    float               slowMultipl = -1;
    float               slowTime = 0;
    float               poisonDmg = -1;
    float               poisonTime = 0;

    [Header("effects")]
    public GameObject slowEffect;
    public GameObject poisonEffect;


    /*
     * Called in first step after "public class{}"
     * Set of particles (Slow / Poison) speed to have a smooth effect
     */
    void Start()
    {
        if (slowEffect && slowMultipl > -1)
        {
            slowEffect.GetComponent<ParticleSystem>().startSpeed += speed;
            slowEffect.SetActive(true);
        } 
        else if (!slowEffect)
        {
            print("slowEffect null");
        }

        if (poisonEffect && poisonDmg > -1)
        {
            poisonEffect.GetComponent<ParticleSystem>().startSpeed += speed;
            poisonEffect.SetActive(true);
        }
        else if (!poisonEffect)
        {
            print("poisonEffect null");
        }
    }

    /*
     * Call every 1/60 sec movement
     */
    void Update()
    {
        Movement();
    }

    /*
     * Follow the target until target is touched or dispawn
     */
    public void Movement()
    {
        if(target == null)
        {
            Destroy(this.gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position + new Vector3(0, 0.3f, 0);

        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;

        this.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

    }


    /*
     * Like a "public className{}" to set up informations
     */
    public void Set(Transform target, float dmg, float speed)
    {
        this.target = target;
        this.dmg = dmg;
        this.speed = speed;
    }


    /*
     * Like a "public className{}" to set up informations
     */
    public void Set(Transform target, float dmg, float speed, float explodeRadius)
    {
        this.target = target;
        this.dmg = dmg;
        this.speed = speed;
        this.explodeRadius = explodeRadius;
    }


    /*
     * Add a slow effect to the arrow
     */
    public void SetSlow(float multiplier, float Time)
    {
        slowMultipl = multiplier;
        this.slowTime = Time;
    }

    /*
     * Add a poson effect to the arrow
     */
    public void SetPoison(float dmg, float Time)
    {
        poisonDmg = dmg;
        this.poisonTime = Time;
    }


    /*
     * Detect if he touched the target, call Hit()
     */
    void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject == target.gameObject)
        {
            Hit();
        }
    }

    /*
     * If he touch the target, apply dmg to the enemy or enemies in range if it have an explode radius
     */
    public void Hit()
    {
        GameObject effect = Instantiate(effectParticle, transform.position, transform.rotation);
        Destroy(effect, 0.5f);

        if (explodeRadius > 0)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, explodeRadius);

            foreach (Collider collider in colliders)
            {
                if(collider.gameObject.tag == "enemy")
                {
                    collider.gameObject.GetComponent<EnemyEntity>().GetDmg(dmg);
                    if(slowMultipl > -1)
                    {
                        collider.gameObject.GetComponent<EnemyEntity>().AddEffect(new EntityEffect("slow", slowMultipl, slowTime));
                        SetEffects(collider.gameObject);
                    }
                }
            }

        } else
        {
            target.gameObject.GetComponent<EnemyEntity>().GetDmg(dmg);
            SetEffects(target.gameObject);
        }
        Destroy(this.gameObject);
    }

    /*
     * Set effects to the target if it have
     */
    void SetEffects(GameObject obj)
    {
        if (slowMultipl > -1)
        {
            obj.GetComponent<EnemyEntity>().AddEffect(new EntityEffect("slow", slowMultipl, slowTime));
        }
        if (poisonDmg > -1)
        {
            obj.GetComponent<EnemyEntity>().AddEffect(new EntityEffect("poison", poisonDmg, poisonTime));
        }
    }
}
