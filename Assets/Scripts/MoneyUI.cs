using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour {

    // Use this for initialization
    public Text money;
    
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        money.text = PlayerStats.MONEY.ToString();
    }
}
