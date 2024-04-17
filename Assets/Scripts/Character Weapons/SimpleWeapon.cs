using UnityEngine;

public class SimpleWeapon : CharacterWeapon
{
    protected override void CustomFire()
    {
        // Instantiate the projectile
        var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        
        // Get the projectile script
        var projectileScript = projectile.GetComponent<CharacterProjectile>();
        
        projectileScript.Shoot(fireDirection, projectileVelocity + character.Speed);
    }
}