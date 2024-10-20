using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject[] zombiePrefabs; 
    public Transform spawnAreaCenter; 
    public float spawnAreaRadius = 10f; 
    private static int activeZombieCount = 0;

    void Start()
    {
        SpawnZombies(5); 
    }

    void Update()
    {
        if (activeZombieCount <= 0)
        {
            SpawnZombies(5); 
        }
    }

    void SpawnZombies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = spawnAreaCenter.position + Random.insideUnitSphere * spawnAreaRadius;
            spawnPosition.y = spawnAreaCenter.position.y; 
            int randomIndex = Random.Range(0, zombiePrefabs.Length);
            Instantiate(zombiePrefabs[randomIndex], spawnPosition, Quaternion.identity);
            activeZombieCount++;
        }
    }

    public static void ZombieDied()
    {
        activeZombieCount--;
    }
}
