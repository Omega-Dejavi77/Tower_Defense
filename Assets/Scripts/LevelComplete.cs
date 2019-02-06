using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour {

    public string mainMenuName = "MainMenu";
    public string nextLevel = "Level02";
    public static int levelToUnlock = 2;
    public LevelSelector levelSelector;

    void Awake()
    {
        FindObjectOfType<AudioManager>().Play("fanfare");
    }

    public void Menu ()
    {
        levelSelector.SelectLevel(mainMenuName);
    }

    public void Continue ()
    {
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        Debug.Log("Level WON");
        levelSelector.SelectLevel(nextLevel);
    }
}
