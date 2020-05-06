using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    
    void Awake()
    {
        instance = this;
    }

    [Header("Options")]
    public bool     upgrade = false;
    public bool     sell = false;
    TurretBuild     toBuild = null;
    GameObject      toShow = null;
    
    public List<TurretBuild> ballistaTurret;
    public List<TurretBuild> canonTurret;


    public GameObject ballistaTurretShow;
    public GameObject canonTurretShow;

    PlayerStats playerStats;

    void Start()
    {
        playerStats = this.GetComponent<PlayerStats>();
    }

    void Update()
    {
        if (this.GetComponent<PlayerStats>().isDead)
        {
            SelectNull();
            return;
        }
        if (Input.GetKeyDown("escape"))
        {
            SelectNull();
        }
    }

    public TurretBuild GetTurretBuild(Tile tile)
    {
        if(toBuild != null && playerStats.money >= toBuild.cost && tile.turret.type == "")
        {
            playerStats.Paid(toBuild.cost);
            return toBuild;
        } else
        {
            return null;
        }
    }
    public GameObject GetTurretToBuild(Tile tile)
    {
        if (toShow != null && tile.turret.type == "")
        {
            return toShow;
        }
        else
        {
            return null;
        }
    }
    public TurretBuild GetTurretUpgrade(Tile tile)
    {
        if (tile.turret.type == "balista" && tile.turret.level < ballistaTurret.Count - 1 
            && upgrade && playerStats.money >= ballistaTurret[tile.turret.level + 1].cost)
        {

            playerStats.Paid(ballistaTurret[tile.turret.level + 1].cost);


            return ballistaTurret[tile.turret.level + 1];
        }
        else if(tile.turret.type == "canon" && tile.turret.level < canonTurret.Count - 1
            && upgrade && playerStats.money >= canonTurret[tile.turret.level + 1].cost)
        {

            playerStats.Paid(canonTurret[tile.turret.level + 1].cost);


            return canonTurret[tile.turret.level + 1];
        }
        else
        {
            return null;
        }
    }

    public void SellTurret(Tile tile)
    {
        TurretBuild turret = tile.turret;
        Destroy(turret.prefab);
        playerStats.GetMoney(turret.cost / 2);
        turret.prefab = null;
        turret.cost = 0;
        turret.level = 0;
        turret.type = "";
    }


    public void SelectUpgrade()
    {
        SelectNull();
        CursorSprite.instance.ChangeCursor("upgrade");
        upgrade = true;
    }
    public void SelectSell()
    {
        SelectNull();
        CursorSprite.instance.ChangeCursor("sell");
        sell = true;
    }
    public void SelectBalista()
    {
        SelectNull();
        toBuild = ballistaTurret[0];
        toShow = ballistaTurretShow;
    }
    public void SelectCanon()
    {
        SelectNull();
        toBuild = canonTurret[0];
        toShow = canonTurretShow;
    }
    public void SelectNull()
    {
        CursorSprite.instance.ChangeCursor("");
        toBuild = null;
        toShow = null;
        upgrade = false;
        sell = false;
    }
}
