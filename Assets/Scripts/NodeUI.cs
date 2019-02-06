using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour {

    public GameObject ui;

    Selector selector;
    GameObject turret;

    [HideInInspector]
    public TurretBluePrint turretBluePrint;

    public BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void HideUI ()
    {
        ui.SetActive(false);
    }

    public void UpgradeDamage ()
    {
        UpgradeDamage(turret.transform.position);
        HideUI();
    }

    public void UpgradeFireRate()
    {
        UpgradeFireRate(turret.transform.position);
        HideUI();
    }

    public void UpgradeRange()
    {
        UpgradeRange(turret.transform.position);
        HideUI();
    }

    //prueba

    public void BuildTurret(TurretBluePrint bluePrint, Vector3 position)
    {
        if (bluePrint == null)
            return;

        if (PlayerStats.MONEY < bluePrint.cost)
        {
            Debug.Log("Not enough money to buid that!");
            return;
        }

        PlayerStats.MONEY -= bluePrint.cost;

        turret = Instantiate(bluePrint.prefab, position, Quaternion.identity);
        turretBluePrint = bluePrint;

        FindObjectOfType<AudioManager>().Play("turret");

        GameObject effect = Instantiate(buildManager.buildEffect, position, Quaternion.identity);
        Destroy(effect, 2f);
    }

    public void SetTarget(GameObject _turret, TurretBluePrint _turretBluePrint, Vector3 hitPosition)
    {
        turret = _turret;
        turretBluePrint = _turretBluePrint;
        transform.position = hitPosition;
        ui.SetActive(true);
    }

    public void UpgradeDamage(Vector3 position)
    {
        if (PlayerStats.MONEY < turretBluePrint.upgradeDamageCost)
        {
            return;
        }

        PlayerStats.MONEY -= turretBluePrint.upgradeDamageCost;
        FindObjectOfType<AudioManager>().Play("upgrade");

        // Get rid of the old torret
        Destroy(turret);

        //Build a new turret
        GameObject _turret = Instantiate(turretBluePrint.upgradedDamagePrefab, position, Quaternion.identity);
        turret = _turret;

        GameObject effect = Instantiate(buildManager.buildEffect, position, Quaternion.identity);
        Destroy(effect, 2f);
    }

    public void UpgradeFireRate(Vector3 position)
    {
        if (PlayerStats.MONEY < turretBluePrint.upgradeFireRateCost)
        {
            return;
        }

        PlayerStats.MONEY -= turretBluePrint.upgradeFireRateCost;
        FindObjectOfType<AudioManager>().Play("upgrade");

        // Get rid of the old torret
        Destroy(turret);

        // Build a new turret
        GameObject _turret = Instantiate(turretBluePrint.upgradedFireRatePrefab, position, Quaternion.identity);
        turret = _turret;

        GameObject effect = Instantiate(buildManager.buildEffect, position, Quaternion.identity);
        Destroy(effect, 2f);
    }

    public void UpgradeRange(Vector3 position)
    {

        if (PlayerStats.MONEY < turretBluePrint.upgradeRangeCost)
        {
            return;
        }

        PlayerStats.MONEY -= turretBluePrint.upgradeRangeCost;
        FindObjectOfType<AudioManager>().Play("upgrade");

        // Get rid of the old torret
        Destroy(turret);

        // Build a new turret
        GameObject _turret = Instantiate(turretBluePrint.upgradedRangePrefab, position, Quaternion.identity);
        turret = _turret;

        GameObject effect = Instantiate(buildManager.buildEffect, position, Quaternion.identity);
        Destroy(effect, 2f);
    }
}
