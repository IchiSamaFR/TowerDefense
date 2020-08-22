using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Create a Wave with parameters
 */
[System.Serializable]
public class Wave
{
    public List<EnemyToSpawn>  enemies;
    public float        rate = 0.2f;
}

/*
 * Create a type of ennemy and the amount to spawn
 */
[System.Serializable]
public class EnemyToSpawn
{
    public GameObject   prefab;
    public int          amount;
}
