using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject gameOverIU;
    public GameObject levelComplete;
    public GameObject uiStats;

    public static bool GAMEISOVER;
	// Use this for initialization
	void Start () {
        GAMEISOVER = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (GAMEISOVER)
            return;

		if (PlayerStats.LIVES <= 0)
        {
            EndGame();
        }
	}

    private void EndGame ()
    {
        uiStats.SetActive(false);
        GAMEISOVER = true;
        gameOverIU.SetActive(true);
    }

    public void WinLevel ()
    {
        uiStats.SetActive(false);
        GAMEISOVER = true;
        levelComplete.SetActive(true);
    }
}
