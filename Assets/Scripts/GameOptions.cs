using UnityEngine;
using UnityEditor;


public class GameOptions : EditorWindow {

    public  int waveNumber;
    public  int numberOfEnemiesPerRound;
    public  int rate;

    public  bool standardEnemy;
    public  bool fasterEnemy;
    public  bool toughEnemy;

    public WaveSpanner waveSpanner;


    [MenuItem("Window/GameOptions")]
	public static void ShowWindow ()
    {
        GetWindow<GameOptions>("GameOptions");
    }

    void OnEnable()
    {
        waveSpanner = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<WaveSpanner>();
    }

    void OnGUI ()
    {
        if (waveSpanner == null)
            return;
        // Window code

        waveNumber = EditorGUILayout.IntField("Number of waves", waveNumber);
        waveSpanner.waveNumber = waveNumber;

        numberOfEnemiesPerRound = EditorGUILayout.IntField("Enemies per round", numberOfEnemiesPerRound);
        waveSpanner.count = numberOfEnemiesPerRound;

        standardEnemy = EditorGUILayout.Toggle("StandardEnemy", standardEnemy);
        waveSpanner.standardEnemy = standardEnemy;

        fasterEnemy = EditorGUILayout.Toggle("fasterEnemy", fasterEnemy);
        waveSpanner.fasterEnemy = fasterEnemy;

        toughEnemy = EditorGUILayout.Toggle("toughEnemy", toughEnemy);
        waveSpanner.toughEnemy = toughEnemy;

        SelectOne();
    }

    private void SelectOne ()
    {
        if (standardEnemy)
        {
            fasterEnemy = false;
            toughEnemy = false;
        }
        else if (fasterEnemy)
        {
            standardEnemy = false;
            toughEnemy = false;
        }
        else if (toughEnemy)
        {
            fasterEnemy = false;
            standardEnemy = false;
        }
    }
}