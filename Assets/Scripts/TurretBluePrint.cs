using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //podem mostrar a l'inspector els atributs de la classe
public class TurretBluePrint {

    [Header("General Atributtes")]
    public GameObject prefab;
    public int cost;

    [Header("UpgradeDamage")]
    public GameObject upgradedDamagePrefab;
    public int upgradeDamageCost;

    [Header("UpgradeFireRate")]
    public GameObject upgradedFireRatePrefab;
    public int upgradeFireRateCost;

    [Header("UpgradeRange")]
    public GameObject upgradedRangePrefab;
    public int upgradeRangeCost;
}
