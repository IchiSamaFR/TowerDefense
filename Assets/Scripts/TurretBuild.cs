using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Turret build, class used to stock the turret prefabs and informations
 */
[System.Serializable]
public class TurretBuild
{
    public GameObject   prefab;
    public int          cost;
    public int          level = 0;
    public string       type;

    /*
     * Like a "public className{}" to set up informations
     */
    public void Set(int cost, int level, string type)
    {
        this.level = level;
        this.type = type;
        this.cost = cost;
    }
    public void Set(TurretBuild turret)
    {
        this.level = turret.level;
        this.type = turret.type;
        this.cost = turret.cost;
    }
}
