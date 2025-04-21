using System;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject wolfPrefab;
    [SerializeField] private GameObject bearPrefab;
    private void Start() {
        Instantiate(wolfPrefab, spawnPoint.position, Quaternion.identity);
    }
}
