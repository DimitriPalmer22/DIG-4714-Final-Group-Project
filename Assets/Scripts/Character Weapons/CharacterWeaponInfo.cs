using System;
using UnityEngine;

/// <summary>
/// Attach this script to pickups.
/// Reference this script within the character weapon class.
/// </summary>
public abstract class CharacterWeaponInfo : MonoBehaviour
{
    #region Fields

    [HideInInspector] public CharacterWeapon weapon;
    
    [SerializeField] public GameObject projectilePrefab;
    
    [Header("Basic Weapon Stats")]
    [SerializeField] public float damage;
    [SerializeField] public float fireCooldown;
    [SerializeField] public float projectileVelocity;
    [SerializeField] public float range;
    
    #endregion Fields

    #region Methods

    public abstract void CustomFire();

    public void CopyInformationTo(CharacterWeaponInfo weaponInfo)
    {
        weaponInfo.projectilePrefab = projectilePrefab;
        weaponInfo.damage = damage;
        weaponInfo.fireCooldown = fireCooldown;
        weaponInfo.projectileVelocity = projectileVelocity;
        weaponInfo.range = range;
        
        CustomCopy(weaponInfo);
    }

    protected abstract void CustomCopy(CharacterWeaponInfo weaponInfo);

    #endregion Methods
}