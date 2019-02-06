using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public static int MONEY, LIVES, ROUNDS;
    public int startMoney = 400, startLives = 5;
	// Use this for initialization
	void Start () {
        MONEY = startMoney;
        LIVES = startLives;
        ROUNDS = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
