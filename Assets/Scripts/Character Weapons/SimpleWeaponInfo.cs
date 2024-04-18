using System;
using UnityEngine;

[Serializable]
public class SimpleWeaponInfo : CharacterWeaponInfo
{
    public override void CustomFire()
    {
        // Instantiate the projectile
        var projectile = Instantiate(projectilePrefab, weapon.transform.position, Quaternion.identity);

        // Get the projectile script
        var projectileScript = projectile.GetComponent<CharacterProjectile>();

        projectileScript.Shoot(weapon.FireDirection, projectileVelocity + weapon.Character.Speed, weapon.Damage);
    }

    protected override void CustomCopy(CharacterWeaponInfo weaponInfo)
    {
    }
}