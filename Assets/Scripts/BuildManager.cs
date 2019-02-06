using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;
    public GameObject buildEffect;
    public NodeUI nodeUI;
    public Shop shop;

    private GameObject turret;
    private TurretBluePrint turretToBuild;

    //Property
    public bool CanBuild { get { return turretToBuild != null; } } // mai es podrà fer un Set d'aquesta varibale, nomes Get
    public bool HasMoney { get { return PlayerStats.MONEY >= turretToBuild.cost; } }

    void Awake () {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager instance in the scene");
            return;
        }
        instance = this;
	}

    public void SelectTurretToBuild (TurretBluePrint turret)
    {
        turretToBuild = turret;
    }

    public TurretBluePrint GetTurretToBuild ()
    {
        return turretToBuild;
    }

    //prueba
    public void SelectTurret (GameObject _turret, Vector3 hitPosition)
    {
        turretToBuild = Turret(_turret);

        nodeUI.SetTarget(_turret, turretToBuild, hitPosition);
    }

    private TurretBluePrint Turret (GameObject _turret)
    {
        if (_turret.tag == "StandardTurret")
        {
            return shop.standardTurret;
        }
        else if (_turret.tag == "MissileTurret")
        {
            return shop.missileLauncher;
        }
        else if (_turret.tag == "LaserTurret")
        {
            return shop.laserBeamer;
        }
        else
        {
            Debug.Log("turret is null");
            return null;
        }
    }
}
