using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject uiStats;

    public GameObject ui;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
	}

    public void Toggle ()
    {
        ui.SetActive(!ui.activeSelf);

        if (ui.activeSelf)
        {
            uiStats.SetActive(false);

            Time.timeScale = 0f;
        }
        else
        {
            uiStats.SetActive(true);
            Time.timeScale = 1f;
        }
    }

    public void Retry ()
    {
        Toggle();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu ()
    {
        Toggle();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
