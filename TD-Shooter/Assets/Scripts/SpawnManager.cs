using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    public List<GameObject> enemies = new List<GameObject>();
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    public int maxEnemies = 10;
    public float spawnInterval = 2;
    private float lastSpawnTime;

    public bool enableSpawnEnemy;

    private void Start()
    {
        enableSpawnEnemy = true;
        enemies.Clear(); // clear enemy list
    }

    private void FixedUpdate()
    {
        StopSpawnEnemies(); // check for spawn enemy

        if (enableSpawnEnemy && Time.time >= lastSpawnTime + spawnInterval)
        {
            SpawnEnemies();
            lastSpawnTime = Time.time;
        }
    }

    private void SpawnEnemies()
    {
        if (spawnPoints.Length > 0 && enemyPrefab != null)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            GameObject newEnemy = Instantiate(
                enemyPrefab,
                spawnPoints[spawnPointIndex].position,
                spawnPoints[spawnPointIndex].rotation
            );
            enemies.Add(newEnemy);
        }
        else
        {
            Debug.LogWarning("Spawn points not set or enemyPrefab is null!");
        }
    }

    private void StopSpawnEnemies()
    {
        enableSpawnEnemy = enemies.Count < maxEnemies;
    }

    public void RemoveEnemy(GameObject enemy)
    {
        // Delete dead enemy from list (null or object miss)
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
        }
    }
}
