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
    // Start is called before the first frame update
    void Awake()
    {
        SetTimeUntilSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        spawnInterval -= Time.deltaTime;

        if(spawnInterval <= 0)
        {
            RandomLocation();
            Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
            SetTimeUntilSpawn();
        }
    }

    private void SetTimeUntilSpawn()
    {
        spawnInterval = Random.Range(minSpawnTime, maxSpawnTime);
    }

    private void RandomLocation()
    {
        spawnX = Random.Range(-0.1f, 0.1f);
        spawnY = Random.Range(-0.1f, 0.1f);

        if(spawnX < 0)
        {
            spawnX -= 1;
        }
        else if(spawnX >= 0)
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
