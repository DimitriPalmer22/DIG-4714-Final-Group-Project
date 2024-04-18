using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;

    [SerializeField] private float minSpawnTime;
    [SerializeField] private float maxSpawnTime;

    private float spawnInterval;

    private Vector3 spawnPos;
    private float spawnX;
    private float spawnY;

    /// <summary>
    /// How much the enemy's health will be modified when they are spawned.
    /// </summary>
    [SerializeField] private int healthModifier;

    // Start is called before the first frame update
    void Awake()
    {
        SetTimeUntilSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        spawnInterval -= Time.deltaTime;

        if (spawnInterval <= 0)
            SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        RandomLocation();
        var enemy = Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
        SetTimeUntilSpawn();
        
        // Modify the enemy's health
        healthModifier = GlobalLevelScript.Instance.XpBar.Level / 5;
        enemy.GetComponent<Actor>().ModifyHealth(healthModifier);
    }

    private void SetTimeUntilSpawn()
    {
        spawnInterval = Random.Range(minSpawnTime, maxSpawnTime);
    }

    private void RandomLocation()
    {
        spawnX = Random.Range(-0.1f, 0.1f);
        spawnY = Random.Range(-0.1f, 0.1f);

        if (spawnX < 0)
        {
            spawnX -= 1;
        }
        else if (spawnX >= 0)
        {
            spawnX += 1;
        }


        if (spawnY < 0)
        {
            spawnY -= 1;
        }
        else if (spawnY >= 0)
        {
            spawnY += 1;
        }

        spawnPos = new Vector3(spawnX, spawnY, 0);

        spawnPos = Camera.main.ViewportToWorldPoint(spawnPos);

        // Make sure that enemies are spawned at the same z position as the player
        spawnPos = new Vector3(spawnPos.x, spawnPos.y, 0);
    }
}