using System;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class WeaponPickup : MonoBehaviour
{
    /// <summary>
    /// The information that is transferred to the player's weapon when it is picked up
    /// </summary>
    private CharacterWeaponInfo _weaponInfo;

    /// <summary>
    /// The collider for this object to detect if the player is in range
    /// </summary>
    private Collider2D _collider;

    private void Start()
    {
        // Get the collider component
        _collider = GetComponent<Collider2D>();
        
        // set the collider to be a trigger
        _collider.isTrigger = true;
        
        // Get the weapon script
        _weaponInfo = GetComponent<CharacterWeaponInfo>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the other object is not the player, return
        if (!other.CompareTag("Player"))
            return;

        // Get the player's weapon script
        var weapon = other.GetComponent<CharacterWeapon>();

        // Set the player's weapon info to this pickup's weapon info
        weapon.ChangeWeaponInfo(_weaponInfo);
        
        // Destroy the weapon pickup
        Destroy(gameObject);
        
    }
}