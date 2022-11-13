using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyType
{
    drone = 0,
    grabbingGunner = 1
}
[System.Serializable]
public class Wave
{
    public EnemyType[] enemies;
}
[System.Serializable]
public class EnemyPrefab
{
    public string name;
    public GameObject prefab;
    public EnemyType type;
}
public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    public EnemyPrefab[] enemyTypes;

    public Vector2 minSpawnPos, maxSpawnPos;

    public Wave[] waves;
    public int currentWave = 0;
    public int aliveEnemies;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (aliveEnemies <= 0)
        {
            SpawnWave();
        }
    }
    public void SpawnWave()
    {
        SpawnWave(currentWave);
        currentWave++;
    }
    public void SpawnWave(int wave)
    {
        foreach (EnemyType enemy in waves[wave].enemies)
        {
            spawnEnemy(enemy);
            aliveEnemies++;
        }
    }
    public void spawnEnemy(EnemyType enemyType)
    {
        Instantiate(System.Array.Find<EnemyPrefab>(enemyTypes, m => m.type == enemyType).prefab, new Vector2(Random.Range(minSpawnPos.x, maxSpawnPos.x), Random.Range(minSpawnPos.y, maxSpawnPos.y)), Quaternion.identity);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube((minSpawnPos + maxSpawnPos) / 2, maxSpawnPos - minSpawnPos);
    }
}
