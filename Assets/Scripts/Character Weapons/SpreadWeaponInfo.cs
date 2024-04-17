using UnityEngine;

public class SpreadWeaponInfo : CharacterWeaponInfo
{
    /// <summary>
    /// How many projectiles to shoot during each shot.
    /// </summary>
    [SerializeField] private int projectileCount;
    
    /// <summary>
    /// The total angle of the spread.
    /// </summary>
    [SerializeField] private float spreadAngle;
    
    public override void CustomFire()
    {
        // Calculate the angle between each projectile
        var angleBetweenProjectiles = spreadAngle / projectileCount;
        
        // Get the angle of the weapon's fire direction
        var fireAngle = Vector2.SignedAngle(Vector2.right, weapon.FireDirection);
        
        // Calculate the starting angle
        var startAngle = fireAngle - spreadAngle / 2;
        
        // Loop through each projectile
        for (var i = 0; i < projectileCount; i++)
        {
            // Calculate the angle of the current projectile
            var angle = startAngle + angleBetweenProjectiles * i;
            
            // Calculate the direction of the current projectile
            var direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            
            // Instantiate the projectile
            var projectile = Instantiate(projectilePrefab, weapon.transform.position, Quaternion.identity);
            
            // Get the projectile script
            var projectileScript = projectile.GetComponent<CharacterProjectile>();
            
            // Shoot the projectile
            projectileScript.Shoot(direction, projectileVelocity + weapon.Character.Speed);
        }
    }

    protected override void CustomCopy(CharacterWeaponInfo weaponInfo)
    {
        var cInfo = weaponInfo as SpreadWeaponInfo;
        
        cInfo.projectileCount = projectileCount;
        cInfo.spreadAngle = spreadAngle;
    }
}