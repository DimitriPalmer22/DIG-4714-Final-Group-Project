using System.Collections;
using UnityEngine;

public class BurstWeaponInfo : CharacterWeaponInfo
{
    /// <summary>
    /// How many projectiles to shoot during each burst.
    /// </summary>
    [SerializeField] private int projectileCount;

    /// <summary>
    /// How long does it take to fire all the projectiles in the burst.
    /// </summary>
    [SerializeField] private float burstDuration;
    
    public override void CustomFire()
    {
        // Start the coroutine to shoot the burst
        StartCoroutine(nameof(ShootBurst));
    }

    protected override void CustomCopy(CharacterWeaponInfo weaponInfo)
    {
        var cInfo = weaponInfo as BurstWeaponInfo;
        
        cInfo.projectileCount = projectileCount;
        cInfo.burstDuration = burstDuration;
    }

    private IEnumerator ShootBurst()
    {
        for (var i = 0; i < projectileCount; i++)
        {
            // Instantiate the projectile
            var projectile = Instantiate(projectilePrefab, weapon.transform.position, Quaternion.identity);

            // Get the projectile script
            var projectileScript = projectile.GetComponent<CharacterProjectile>();

            projectileScript.Shoot(weapon.FireDirection, projectileVelocity + weapon.Character.Speed);

            yield return new WaitForSeconds(burstDuration / projectileCount);
        }
    }
}