using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretBuild
{
    public GameObject   prefab;
    public int          cost;
    public int          level = 0;
    public string       type;

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
