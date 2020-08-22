using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    

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

    /*
     * Before the first step instance it
     */
    void Awake()
    {
        instance = this;
    }

    /*
     * Get player Stats class
     */
    void Start()
    {
        playerStats = this.GetComponent<PlayerStats>();
    }

    /*
     * Check every 1/60 seconds if the player is dead or if key down escape is pressed
     */
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

    /*
     * Get the turret builded
     */
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
    
    /*
     * Get the preshow turret build
     */
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

    /*
     * Get the turret upgraded by one level
     */
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


    /*
     * Delete turret and return 1/2 of the turret cost
     */
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

    /*
     * Select the upgrade cursor to upgrade turrets
     */
    public void SelectUpgrade()
    {
        SelectNull();
        CursorSprite.instance.ChangeCursor("upgrade");
        upgrade = true;
    }

    /*
     * Select the sell cursor to upgrade sell
     */
    public void SelectSell()
    {
        SelectNull();
        CursorSprite.instance.ChangeCursor("sell");
        sell = true;
    }

    /*
     * Select balista tower
     */
    public void SelectBalista()
    {
        SelectNull();
        toBuild = ballistaTurret[0];
        toShow = ballistaTurretShow;
    }

    /*
     * Select canon tower
     */
    public void SelectCanon()
    {
        SelectNull();
        toBuild = canonTurret[0];
        toShow = canonTurretShow;
    }

    /*
     * Deselect turrets or cursors
     */
    public void SelectNull()
    {
        CursorSprite.instance.ChangeCursor("");
        toBuild = null;
        toShow = null;
        upgrade = false;
        sell = false;
    }
}
