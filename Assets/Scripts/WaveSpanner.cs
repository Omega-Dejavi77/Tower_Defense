using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpanner : MonoBehaviour {

    public static int ENEMIESALIVE = 0;
    public Wave [] waves;
    public GameManager gameManager;

    public Transform spawnPoint;
    public float timeBetweenWaves; //maneja cada cuanto vuelven a salir enemigos
    private float countdown = 3f;
    private int waveIndex = 0;
    public Text waveCountDownTimer;

    public GameObject spawn;

    [Header("Window Editor")]
    public GameObject standardEnemy_Prefab;
    public GameObject fasterEnemy_Prefab;
    public GameObject toughEnemy_Prefab;

    [HideInInspector]
    public int waveNumber;

    [HideInInspector]
    public int count;

    [HideInInspector]
    public int rate;

    [HideInInspector]
    public bool standardEnemy;

    [HideInInspector]
    public bool fasterEnemy;

    [HideInInspector]
    public bool toughEnemy;


    // Comenti tot el metode Start per utilitzar les oleades ja pre-establertes
    /*void Start()
    {
        rate = 1;
        waves = new Wave[waveNumber];
        Debug.Log("waveNumber: " + waveNumber);
        Debug.Log("count: " + count);
        Debug.Log("rate: " + rate);

        GameObject enemy = ChoseEnemy();

        for (int i = 0; i < waves.Length; i++)
        {
            waves[i] = new Wave(enemy, count, rate);
        }
    }*/ //--> descomentar el void Start per fer-ne us del window -> GameOptions

    // Update is called once per frame
    void Update () {

        if (ENEMIESALIVE > 0)
            return;

        if (waveIndex == waves.Length)
        {
            gameManager.WinLevel();
            this.enabled = false;
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }
        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        waveCountDownTimer.text = string.Format("{0:00}", countdown); //--> para hacer el contador de aspecto diferente (se ha de comentar la parte de arriba entonces)
    }

    IEnumerator SpawnWave () // cada x segundos va spawneando grupos de enemigos cada vez mas grandes
    {
        PlayerStats.ROUNDS++;

        FindObjectOfType<AudioManager>().Play("new_wave");

        Wave wave = waves[waveIndex];

        ENEMIESALIVE = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        waveIndex++;
    }

    void SpawnEnemy (GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        GameObject effect = Instantiate(spawn, spawnPoint.position, spawnPoint.rotation);
        Destroy(effect, 2f);
        
    }

    //prueba
    private GameObject ChoseEnemy ()
    {
        if (standardEnemy)
        {
            return standardEnemy_Prefab;
        }
        else if (fasterEnemy)
        {
            return fasterEnemy_Prefab;
        }
        else if (toughEnemy)
        {
            return toughEnemy_Prefab;
        }
        else
        {
            Debug.Log("You must choose one enemy");
            return null;
        }
    }
}
