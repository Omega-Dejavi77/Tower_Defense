using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    BuildManager buildManager;
    public TurretBluePrint standardTurret, missileLauncher, laserBeamer; // Amb el [System.Serializable] a la classe TurretBluePrint, podem veure en l'inspector del Script Shop, els atributs de la classe TurretBluePrint

    void Start () {
        buildManager = BuildManager.instance;
	}

    public void SelectStandardTurret ()
    {
        FindObjectOfType<AudioManager>().Play("click1");
        Debug.Log("Standard turret selected");
        buildManager.SelectTurretToBuild(standardTurret);
    }

    public void SelectMisileLauncher()
    {
        FindObjectOfType<AudioManager>().Play("click1");
        Debug.Log("Misile launcher selected");
        buildManager.SelectTurretToBuild(missileLauncher);
    }

    public void SelectlaserBeamer()
    {
        FindObjectOfType<AudioManager>().Play("click1");
        Debug.Log("Laser Turret selected");
        buildManager.SelectTurretToBuild(laserBeamer);
    }
}
