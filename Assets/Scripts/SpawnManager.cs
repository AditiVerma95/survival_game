using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour {
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private List<GameObject> enemyPrefabs;
    
    [SerializeField] private float spawnTime = 30f;
    private float currentTime = 30f;
    

    private void Update() {
        currentTime += Time.deltaTime;
        if (currentTime >= spawnTime) {
            Instantiate(RandomEnemyPrefab(), RandomSpawnPoint().position, Quaternion.identity);
            currentTime = 0;
        }
    }

    private Transform RandomSpawnPoint() {
        int randomIndex = Random.Range(0, spawnPoints.Count);
        return spawnPoints[randomIndex];
    }
    
    private GameObject RandomEnemyPrefab() {
        int randomIndex = Random.Range(0, enemyPrefabs.Count);
        return enemyPrefabs[randomIndex];
    }
}
