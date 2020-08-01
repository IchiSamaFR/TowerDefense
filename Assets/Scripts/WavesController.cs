using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WavesController : MonoBehaviour
{
    PlayerStats playerStats;
    [Header("Waves")]
    [SerializeField]
    List<Wave>          lstWaves = new List<Wave>();


    [Header("Others")]
    [SerializeField]
    List<GameObject>    lstEnemy = new List<GameObject>();

    public int          enemiesAlive = 0;

    [SerializeField]
    float               timeBetweenWaves = 5f;
    
    public Text         txtCountdown;
    public Text         txtWave;

    Transform           spawn;
    int                 enemies = 1;
    int                 wave = 1;
    bool                isWon = false;
    float               countdown;

    void Start()
    {
        countdown = timeBetweenWaves;
        playerStats = this.GetComponent<PlayerStats>();
        spawn = Waypoints.points[0];
    }

    void Update()
    {

        if (wave == lstWaves.Count + 1 && enemiesAlive == 0)
        {
            wave--;
            isWon = true;
        }
        if (isWon)
        {
            this.GetComponent<CursorSprite>().ChangeCursor("normal");
            this.GetComponent<Pause>().Victory();
            return;
        }


        if (this.GetComponent<PlayerStats>().isDead || enemiesAlive != 0)
        {
            if(enemiesAlive < 0)
            {
                enemiesAlive = 0;
            }
            return;
        }

        if (countdown <= 0f)
        {
            StartCoroutine(NewSpawnWave());
            countdown = timeBetweenWaves;
            //enemies++;
            playerStats.GetIncome();
        }
        countdown -= Time.deltaTime;
        txtCountdown.text = ((int)countdown + 1).ToString();
        txtWave.text = wave.ToString();
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < enemies; i++)
        {
            GameObject enemy = Instantiate(lstEnemy[0]);
            enemy.transform.position = spawn.position;
            enemy.GetComponent<EnemyEntity>().wavesController = this;
            enemiesAlive += 1;
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator NewSpawnWave()
    {
        for (int i = 0; i < lstWaves[wave - 1].enemies.Count; i++)
        {
            EnemyToSpawn toSpawn = lstWaves[wave - 1].enemies[i];

            for (int x = 0; x < toSpawn.amount; x++)
            {
                GameObject enemy = Instantiate(toSpawn.prefab);
                enemy.transform.position = spawn.position;
                enemy.GetComponent<EnemyEntity>().wavesController = this;
                enemiesAlive += 1;
                yield return new WaitForSeconds(lstWaves[wave - 1].rate);
            }
        }
        wave++;
    }
}
