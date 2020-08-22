using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    public TurretBuild  turret = null;
    public GameObject   showToBuild = null;
    BuildManager        buildManager;


    void Start()
    {
        buildManager = BuildManager.instance;
    }

    /*
     * On mouse down check if the mouse is in or out and make actions
     * Place a turret
     * Upgrade the turret inside
     * Sell the turret
     */
    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        /*
         * if Turret null create a new turret
         */
        if (turret.type == "")
        {
            TurretBuild turretToBuild = buildManager.GetTurretBuild(this);

            if (turretToBuild == null)
            {   return; }

            foreach (GameObject child in GetChilds())
            {
                Destroy(child);
            }

            turret.Set(turretToBuild);
            turret.prefab = Instantiate(turretToBuild.prefab, this.transform.position + new Vector3(0, 0.2f, 0), this.transform.rotation);
        }
        /*
         * if upgrade request
         */
        else if (buildManager.upgrade)
        {
            TurretBuild turretToBuild = buildManager.GetTurretUpgrade(this);

            if (turretToBuild == null)
            { return; }
            Destroy(turret.prefab);
            turret.Set(turretToBuild);
            turret.prefab = Instantiate(turretToBuild.prefab, this.transform.position + new Vector3(0, 0.2f, 0), this.transform.rotation);
        }
        /*
         * if sell request
         */
        else if (buildManager.sell)
        {
            buildManager.SellTurret(this);
        }



        if (showToBuild != null)
        {   Destroy(showToBuild);   }
    }

    /*
     * Check the mouse enter in the tile pos and show the shadowBuild if there is a build to show
     */
    void OnMouseEnter()
    {
        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild(this);
        if (turretToBuild == null)
        {
            return;
        }
        foreach (GameObject child in GetChilds())
        {
            child.SetActive(false);
        }
        showToBuild = Instantiate(turretToBuild, this.transform.position + new Vector3(0, 0.2f, 0), this.transform.rotation);
    }

    /*
     * Check if the mouse is out Destroy shadow build
     */
    void OnMouseExit()
    {
        if (showToBuild != null)
        {
            Destroy(showToBuild);
            foreach(GameObject child in GetChilds())
            {
                child.SetActive(true);
            }
            return;
        }
    }

    /*
     * Check if a turret is already build in the tile
     */
    List<GameObject> GetChilds()
    {
        List<GameObject> toReturn = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            toReturn.Add(transform.GetChild(i).gameObject);
        }

        return toReturn;
    }
}
