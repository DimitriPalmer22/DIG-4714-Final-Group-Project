using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemyPrefabs;

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

    /// <summary>
    /// The list of things enemies can potentially drop when they die.
    /// </summary>
    [SerializeField] private GameObject[] enemyDrops;

    /// <summary>
    /// The chance that an enemy will drop something when they die.
    /// </summary>
    [SerializeField] [Range(0, 1)] private float dropChance;

    // Start is called before the first frame update
    void Awake()
    {
        SetTimeUntilSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        spawnInterval -= Time.deltaTime;

        if (spawnInterval <= 0 && !GlobalLevelScript.Instance.IsPlayerDead)
            SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        RandomLocation();

        var enemyPrefab = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Length)];

        var enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

        // set the enemy's parent transform to the enemy spawner
        enemy.transform.SetParent(transform);

        SetTimeUntilSpawn();

        // Modify the enemy's health
        healthModifier = GlobalLevelScript.Instance.XpBar.Level / 5;
        var enemyScript = enemy.GetComponent<EnemyScript>();
        enemyScript.ModifyHealth(healthModifier);

        // Subscribe to the enemy's OnDeath event
        enemyScript.OnDeath += DropOnDeath;
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

        spawnPos = Camera.main!.ViewportToWorldPoint(spawnPos);

        // Make sure that enemies are spawned at the same z position as the player
        spawnPos = new Vector3(spawnPos.x, spawnPos.y, 0);
    }

    private void DropOnDeath(Actor sender)
    {
        // Check if the enemy will drop something
        if (Random.value > dropChance)
            return;

        // if the enemy drops are empty, return
        if (enemyDrops.Length == 0)
            return;

        // Get a random drop from the list of possible drops
        var randomDrop = enemyDrops[Random.Range(0, enemyDrops.Length)];

        // Instantiate the drop at the enemy's position
        var drop = Instantiate(randomDrop, sender.transform.position, Quaternion.identity);

        // Destroy the drop after 10 seconds
        Destroy(drop, 10);
    }
}