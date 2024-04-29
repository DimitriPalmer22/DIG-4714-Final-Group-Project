using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Once the player levels up, there is a chance that 
/// </summary>
public class WeaponSpawner : MonoBehaviour
{
    private const float SPAWN_CHANCE = 0.5f;

    /// <summary>
    /// An array of weapon prefabs that can be spawned.
    /// </summary>
    [SerializeField] private GameObject[] weaponPrefabs;

    /// <summary>
    /// The particle system that will be used to spawn the weapon.
    /// </summary>
    private ParticleSystem _particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();

        XP_Bar.Instance.OnLevelUp += SpawnWeapon;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void SpawnWeapon()
    {
        // Check if the player has a chance to spawn a weapon
        if (Random.value > SPAWN_CHANCE)
            return;

        // Choose a random weapon prefab from the array
        var randomWeapon = weaponPrefabs[Random.Range(0, weaponPrefabs.Length)];

        // Choose a random location to spawn the weapon
        const float randLocationMax = 0.4f;
        const float randLocationMin = 0.2f;
        var randX = new[]
        {
            Random.Range(.5f - randLocationMax, .5f - randLocationMin),
            Random.Range(.5f + randLocationMin, .5f + randLocationMax)
        }[Random.Range(0, 2)];
        var randY = new[]
        {
            Random.Range(.5f - randLocationMax, .5f - randLocationMin),
            Random.Range(.5f + randLocationMin, .5f + randLocationMax)
        }[Random.Range(0, 2)];

        var spawnLocation = Camera.main!.ViewportToWorldPoint(new Vector3(randX, randY, 0));

        // Spawn a weapon
        var spawnedWeapon = Instantiate(randomWeapon, spawnLocation, Quaternion.identity);
        spawnedWeapon.transform.parent = transform;
        spawnedWeapon.transform.position = new Vector3(
            spawnedWeapon.transform.position.x,
            spawnedWeapon.transform.position.y,
            0
        );

        // Emit particles when the weapon is spawned
        var emitParams = new ParticleSystem.EmitParams
        {
            position = spawnLocation,
            applyShapeToPosition = true
        };
        _particleSystem.Emit(emitParams, 200);
        
        // Destroy the weapon after 10 seconds
        Destroy(spawnedWeapon, 10);
    }
}