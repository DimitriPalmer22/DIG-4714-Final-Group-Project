using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Once the player levels up, there is a chance that 
/// </summary>
public class WeaponSpawner : MonoBehaviour
{
    private const float SPAWN_CHANCE = 0.75f;

    /// <summary>
    /// An array of weapon prefabs that can be spawned.
    /// </summary>
    [SerializeField] private GameObject[] weaponPrefabs;

    /// <summary>
    /// The particle system that will be used to spawn the weapon.
    /// </summary>
    private ParticleSystem _particleSystem;

    private Dictionary<Type, Dictionary<string, float>> _levelModifiers = new();

    private Dictionary<Type, Dictionary<string, (float min, float max)>> _modifierRanges = new();

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the level modifiers
        _particleSystem = GetComponent<ParticleSystem>();

        // Initialize the level modifiers
        InitializeLevelModifiers();

        // Initialize the modifier ranges
        InitializeModifierRanges();

        // Subscribe to the level up event
        XP_Bar.Instance.OnLevelUp += SpawnWeapon;
    }

    private void InitializeLevelModifiers()
    {
        _levelModifiers[typeof(SimpleWeaponInfo)] = new Dictionary<string, float>
        {
            { "damage", 0 },
            { "fireCooldown", 1 / 64f },
            { "projectileVelocity", 1 / 16f },
            { "range", 1 / 8f }
        };

        _levelModifiers[typeof(BurstWeaponInfo)] = new Dictionary<string, float>
        {
            { "damage", 0 },
            { "fireCooldown", 1 / 64f },
            { "projectileVelocity", 1 / 16f },
            { "range", 1 / 8f },

            { "projectileCount", 0 },
            { "burstDuration", .1f / 32f }
        };

        _levelModifiers[typeof(SpreadWeaponInfo)] = new Dictionary<string, float>
        {
            { "damage", 0 },
            { "fireCooldown", 1 / 32f },
            { "projectileVelocity", 1 / 16f },
            { "range", 1 / 8f },

            { "projectileCount", 0 },
            { "spreadAngle", 1 / 16f }
        };
    }

    private void InitializeModifierRanges()
    {
        _modifierRanges[typeof(SimpleWeaponInfo)] = new Dictionary<string, (float min, float max)>
        {
            { "damage", (0, 1000) },
            { "fireCooldown", (1 / 8f, 1000) },
            { "projectileVelocity", (1 / 16f, 1000) },
            { "range", (1 / 8f, 1000) }
        };

        _modifierRanges[typeof(BurstWeaponInfo)] = new Dictionary<string, (float min, float max)>
        {
            { "damage", (0, 1000) },
            { "fireCooldown", (1 / 2f, 2) },
            { "projectileVelocity", (1 / 16f, 1000) },
            { "range", (1 / 8f, 1000) },

            { "projectileCount", (3, 10) },
            { "burstDuration", (1 / 16f, 5) }
        };

        _modifierRanges[typeof(SpreadWeaponInfo)] = new Dictionary<string, (float min, float max)>
        {
            { "damage", (0, 1000) },
            { "fireCooldown", (1 / 4f, 2) },
            { "projectileVelocity", (1 / 16f, 1000) },
            { "range", (1 / 8f, 1000) },

            { "projectileCount", (4, 16) },
            { "spreadAngle", (15, 150) }
        };
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

        // Modify the weapon's stats based on the player's level
        ModifyWeaponStats(spawnedWeapon);

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

    /// <summary>
    /// Change the spawned weapon's stats based on the player's level.
    /// </summary>
    private void ModifyWeaponStats(GameObject spawnedWeapon)
    {
        // Get the weapon's weapon info
        var weaponInfo = spawnedWeapon.GetComponent<CharacterWeaponInfo>();

        // return if the weapon info is null
        if (weaponInfo == null)
            return;

        // Increase the weapon's rate of fire based on the player's level
        weaponInfo.fireCooldown -= XP_Bar.Instance.Level * _levelModifiers[weaponInfo.GetType()]["fireCooldown"];
        weaponInfo.fireCooldown = Mathf.Clamp(
            weaponInfo.fireCooldown,
            _modifierRanges[weaponInfo.GetType()]["fireCooldown"].min,
            _modifierRanges[weaponInfo.GetType()]["fireCooldown"].max
        );

        // Increase the weapon's projectile velocity based on the player's level
        weaponInfo.projectileVelocity +=
            XP_Bar.Instance.Level * _levelModifiers[weaponInfo.GetType()]["projectileVelocity"];
        weaponInfo.projectileVelocity = Mathf.Clamp(
            weaponInfo.projectileVelocity,
            _modifierRanges[weaponInfo.GetType()]["projectileVelocity"].min,
            _modifierRanges[weaponInfo.GetType()]["projectileVelocity"].max
        );

        // Increase the weapon's range based on the player's level
        weaponInfo.range += XP_Bar.Instance.Level * _levelModifiers[weaponInfo.GetType()]["range"];
        weaponInfo.range = Mathf.Clamp(
            weaponInfo.range,
            _modifierRanges[weaponInfo.GetType()]["range"].min,
            _modifierRanges[weaponInfo.GetType()]["range"].max
        );

        // If the weapon is a burst weapon, decrease the burst duration
        if (weaponInfo is BurstWeaponInfo burstWeaponInfo)
        {
            burstWeaponInfo.burstDuration -=
                XP_Bar.Instance.Level * _levelModifiers[weaponInfo.GetType()]["burstDuration"];
            burstWeaponInfo.burstDuration = Mathf.Clamp(
                burstWeaponInfo.burstDuration,
                _modifierRanges[weaponInfo.GetType()]["burstDuration"].min,
                _modifierRanges[weaponInfo.GetType()]["burstDuration"].max
            );
        }
    }
}