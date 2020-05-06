using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public List<EnemyToSpawn>  enemies;
    public float        rate = 0.2f;
}

[System.Serializable]
public class EnemyToSpawn
{
    public GameObject   prefab;
    public int          amount;
}
