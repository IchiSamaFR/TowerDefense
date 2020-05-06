using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    public WavesController      wavesController;

    public PlayerStats          playerToGive;

    List<EntityEffect>          effects = new List<EntityEffect>();

    [Header("stats")]
    public float                speedInit = 10;
    float                       speed;

    public int                  dmg = 1;

    public float                maxHealth = 10;
    float                       health;

    Transform                   target;
    int                         waypointIndex = 0;

    public int                  moneyGive;
    float                       timerPoison;

    [Header("effects")]
    public GameObject           slowEffect;
    public GameObject           poisonEffect;

    GameObject                  instanceSlowEffect;
    GameObject                  instancePoisonEffect;

    void Start()
    {
        speed = speedInit;
        health = maxHealth;

        target = Waypoints.points[waypointIndex + 1];
    }
    
    void Update()
    {
        Movement();
        ChekEffects();
    }

    void ChekEffects()
    {
        float speedMultiplier = -1;
        float dmgByPoison = -1;

        for (int i = 0; i < effects.Count; i++)
        {
            if(effects[i].timeBeforeDelet < 0)
            {
                effects.Remove(effects[i]);
                return;
            }
            if(effects[i].type == "slow" && (effects[i].value < speedMultiplier || speedMultiplier == -1))
            {
                speedMultiplier = effects[i].value;
            }
            if (effects[i].type == "poison" && (effects[i].value < dmgByPoison || dmgByPoison == -1))
            {
                dmgByPoison = effects[i].value;
            }
            effects[i].timeBeforeDelet -= Time.deltaTime;
        }
        if(speedMultiplier > 0)
        {
            speed = speedInit * speedMultiplier;

            //S'il n'y a pas de particules
            if (slowEffect != null && !instanceSlowEffect)
            {
                instanceSlowEffect = Instantiate(slowEffect, this.transform);
            } else if (slowEffect == null)
            {
                Debug.Log("slowEffect null");
            }
        } else
        {
            speed = speedInit;

            //S'il n'y a pas de slow verifier qu'il existe des particules, les detruires
            if (instanceSlowEffect)
            {
                Destroy(instanceSlowEffect);
                instanceSlowEffect = null;
            }
        }

        timerPoison -= Time.deltaTime;
        if (timerPoison < 0)
        {
            timerPoison = 1;
            if (dmgByPoison > -1)
            {
                GetDmg(dmgByPoison);

                //S'il n'y a pas de particules
                if (poisonEffect != null && !instancePoisonEffect)
                {
                    instancePoisonEffect = Instantiate(poisonEffect, this.transform);
                } else if (poisonEffect == null)
                {
                    Debug.Log("poisonEffect null");
                }
            } else
            {
                //S'il n'y a pas de poison et qu'il existe des particules, les detruires
                if (instancePoisonEffect)
                {
                    Destroy(instancePoisonEffect);
                    instancePoisonEffect = null;
                }
            }
        }
    }
    public void AddEffect(EntityEffect effect)
    {
        effects.Add(effect);
    }


    void Movement()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.position, target.position) <= 0.1f)
        {
            NextWaypoint();
        }

        if(direction.x > 0.2f)
        {
            this.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
        }
        else if (direction.x < -0.2f)
        {
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }
        else if(direction.z > 0.2f)
        {
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else if (direction.z < -0.2f)
        {
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

    }

    void NextWaypoint()
    {
        if(waypointIndex + 2 >= Waypoints.points.Length)
        {
            Waypoints.playerStats.GetDmg(dmg);
            wavesController.enemiesAlive--;
            Destroy(gameObject);
            return;
        }

        waypointIndex++;
        target = Waypoints.points[waypointIndex + 1];
    }

    public void GetDmg(float dmgDeal)
    {
        if(health <= 0)
        {
            return;
        }
        health -= dmgDeal;
        if(health <= 0)
        {
            wavesController.enemiesAlive--;
            Waypoints.playerStats.GetMoney(moneyGive);
            Destroy(gameObject);
        }
    }
}

public class EntityEffect
{
    public string   type;
    public float    value;
    public float    timeBeforeDelet;

    public EntityEffect(string type, float value, float timeBeforeDelet)
    {
        this.type = type;
        this.value = value;
        this.timeBeforeDelet = timeBeforeDelet;
    }

}
