using System.Collections.Generic;
using UnityEngine;


/*
 * Class to generate stats of an enemy
 */
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

    /*
     * Set up of basics stats
     */
    void Start()
    {
        speed = speedInit;
        health = maxHealth;

        target = Waypoints.points[waypointIndex + 1];
    }

    /*
     * Check every 1/60 sec
     */
    void Update()
    {
        Movement();
        ChekEffects();
    }

    /*
     * Check effects and applied it
     */
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

        /*
         * Check speed multiplier like speed boost or slow
         * or
         * Destroy particules if not
         */
        if (speedMultiplier > 0)
        {
            speed = speedInit * speedMultiplier;

            if (slowEffect != null && !instanceSlowEffect)
            {
                instanceSlowEffect = Instantiate(slowEffect, this.transform);
            } else if (slowEffect == null)
            {
                Debug.Log("slowEffect null");
            }
        } 
        else
        {
            speed = speedInit;

            if (instanceSlowEffect)
            {
                Destroy(instanceSlowEffect);
                instanceSlowEffect = null;
            }
        }

        timerPoison -= Time.deltaTime;

        /*
         * Check every X seconds poison and apply dmg
         * or
         * Destroy particules if not
         */
        if (timerPoison < 0)
        {
            timerPoison = 1;
            if (dmgByPoison > -1)
            {
                GetDmg(dmgByPoison);

                if (poisonEffect != null && !instancePoisonEffect)
                {
                    instancePoisonEffect = Instantiate(poisonEffect, this.transform);
                } else if (poisonEffect == null)
                {
                    Debug.Log("poisonEffect null");
                }
            } else
            {
                if (instancePoisonEffect)
                {
                    Destroy(instancePoisonEffect);
                    instancePoisonEffect = null;
                }
            }
        }
    }

    /*
     * Add entity effect
     */
    public void AddEffect(EntityEffect effect)
    {
        effects.Add(effect);
    }


    /*
     * Go the waypoint and go the next if the actual is touched
     */
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

    /*
     * Get the next waypoint to go
     */
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

    /*
     * Get damage
     */
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

/*
 * Effects class
 */
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
