using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave {

    public GameObject enemy;
    public int count;
    public int rate;

    public Wave (GameObject _enemy, int _count, int _rate)
    {
        enemy = _enemy;
        count = _count;
        rate = _rate;
    }
}
