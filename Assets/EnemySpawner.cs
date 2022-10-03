using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnpoints;

    [SerializeField] private int startSpawnNumber = 3;

    [SerializeField] private float A = 1;
    
    [SerializeField] private float printUntilRound = 10;
    
    [SerializeField] private float secondsPerRound = 10f;

    [SerializeField] private float percentageFollowers = 0.75f;
    
    [SerializeField] private GameObject chargerPrefab;

    [SerializeField] private GameObject followerPrefab;
    
    [SerializeField] private Transform enemiesParent;

    
    [SerializeField] private bool printTest = false;
    
    [SerializeField] private Transform player;
    
    private float spawnRate;

    private float spawnTimer;

    private SceneSwitcher _sceneSwitcher;

    private int sceneCount = 1;
    
    private int testSceneCount = 1;

    private int spawnIndex = 0;

    [SerializeField] private int spawnNumber;
    void Start()
    {
        _sceneSwitcher = FindObjectOfType<SceneSwitcher>();
        _sceneSwitcher.OnSwitchSceneEvent += SceneSwitcherOnOnSwitchSceneEvent;
        spawnNumber = startSpawnNumber;

        spawnRate = spawnNumber / secondsPerRound;
        spawnTimer = spawnRate;

        string str = "Spawn num: ";
        for (int i = 0; i < printUntilRound; i++)
        {
            testSceneCount++;
            string test = Mathf.RoundToInt(A * Mathf.Log(testSceneCount) + startSpawnNumber) + ", ";
            str += test;
        }
        Debug.Log(str);
    }

    private void SceneSwitcherOnOnSwitchSceneEvent(SceneState state)
    {
        DetermineNewSpawnRate();
    }

    private void DetermineNewSpawnRate()
    {
        sceneCount++;
        spawnNumber = Mathf.RoundToInt(A * Mathf.Log(sceneCount) + startSpawnNumber);
        spawnRate = spawnNumber / secondsPerRound;
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer < 0)
        {
            spawnTimer = spawnRate;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {

        if (spawnIndex == spawnpoints.Length)
        {
            spawnIndex = 0;
        }

        if (spawnpoints.Length == 0)
        {
            return;
            
        }
        Vector3 spawnPos = spawnpoints[spawnIndex].position;

        
        GameObject enemyToSpawn;
        float random = Random.Range(0f, 1.0f);
        if (random < percentageFollowers)
        {
            enemyToSpawn = followerPrefab;
            FollowerEnemy enemy = Instantiate(enemyToSpawn, spawnPos, Quaternion.identity, enemiesParent).GetComponent<FollowerEnemy>();
            enemy.SetTarget(player);
        }
        else
        {
            enemyToSpawn = chargerPrefab;
            Charger enemy = Instantiate(enemyToSpawn, spawnPos, Quaternion.identity, enemiesParent).GetComponent<Charger>();
            enemy.SetTarget(player);
        }
        
    }
}