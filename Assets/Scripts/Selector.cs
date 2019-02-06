using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Selector : MonoBehaviour {

    public Camera cam;
    public NodeUI nodeUI;
    BuildManager buildManager;

    public int turretLayer;
    public int upgradedLayer;

    [HideInInspector]
    public GameObject turret;

    [HideInInspector]
    public TurretBluePrint turretBluePrint;

    [HideInInspector]
    public bool isUpgraded = false;

    [HideInInspector]
    public Vector3 positionOffSet;

    [HideInInspector]
    public Vector3 hitPosition;

    public Material greenProjector;
    public Material redProjector;

    public ParticleSystem particle;

    // Use this for initialization
    void Start () {
        buildManager = BuildManager.instance;
    }
	
	// Update is called once per frame
	void Update () {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (!particle.gameObject.activeSelf)
                particle.gameObject.SetActive(true);

            particle.transform.position = hit.point + Vector3.up;
            NavMeshHit navhit;

            if (NavMesh.SamplePosition(hit.point, out navhit, 1.0f, NavMesh.AllAreas)) // em si un punt forma part de la navigation mesh
            {
                int mask = 1 << NavMesh.GetAreaFromName("turret"); //operacio de moure bits ,nom del layer on puc colocar torretes

                if (navhit.mask == mask)
                {
                    particle.GetComponent<Renderer>().material = greenProjector;

                    GameObject anyTurret = hit.collider.gameObject;

                    if (anyTurret.layer == turretLayer || anyTurret.layer == upgradedLayer)
                    {
                        particle.GetComponent<Renderer>().material = redProjector;
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        if (EventSystem.current.IsPointerOverGameObject())
                            return;

                        GameObject turret = hit.collider.gameObject;

                        if (turret.layer == turretLayer)
                        {
                            FindObjectOfType<AudioManager>().Play("click2");
                            hitPosition = hit.point;
                            //nodeUI.SetTargert(turret);
                            buildManager.SelectTurret(turret, hitPosition);
                        }

                        else if (turret.layer != upgradedLayer)
                        {
                            hitPosition = hit.point;
                            nodeUI.BuildTurret(buildManager.GetTurretToBuild(), hitPosition);
                        }
                    }
                }

                else
                {
                    particle.GetComponent<Renderer>().material = redProjector;
                }
            }
        }
    }
}
