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
    [SerializeField] public float fireCooldown;
    [SerializeField] public float projectileVelocity;
    
    #endregion Fields

    #region Methods

    public abstract void CustomFire();

    public void CopyInformationTo(CharacterWeaponInfo weaponInfo)
    {
        weaponInfo.projectilePrefab = projectilePrefab;
        weaponInfo.fireCooldown = fireCooldown;
        weaponInfo.projectileVelocity = projectileVelocity;
        
        CustomCopy(weaponInfo);
    }

    protected abstract void CustomCopy(CharacterWeaponInfo weaponInfo);

    #endregion Methods
}