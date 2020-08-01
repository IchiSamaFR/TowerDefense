﻿using System.Collections;
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
    void Update()
    {

    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

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
        else if (buildManager.upgrade)
        {
            TurretBuild turretToBuild = buildManager.GetTurretUpgrade(this);

            if (turretToBuild == null)
            { return; }
            Destroy(turret.prefab);
            turret.Set(turretToBuild);
            turret.prefab = Instantiate(turretToBuild.prefab, this.transform.position + new Vector3(0, 0.2f, 0), this.transform.rotation);
        }
        else if (buildManager.sell)
        {
            buildManager.SellTurret(this);
        }



        if (showToBuild != null)
        {   Destroy(showToBuild);   }
    }

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
