using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RangedEnemy : EnemyScript
{
    [SerializeField] private float fireRate;

    [SerializeField] private float distanceToShoot;
    [SerializeField] private float distanceToStop;

    [SerializeField] private Transform shootingPoint;
    [SerializeField] private GameObject projectile;

    private float distance;
    private float timeToFire;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
     
        // Move towards the player
        MoveTowardPlayer();
    }


    protected override void MoveTowardPlayer()
    {
        // Get the distance between the player and the enemy
        distance = Vector2.Distance(transform.position, Player.transform.position);

        // Get the direction to the player
        Vector2 direction = Player.transform.position - transform.position;
        direction.Normalize();

        // Get the angle to the player
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Move towards the player if the player is outside the distance to stop
        if (distance >= distanceToStop)
        {
            var newPosition = Vector2.MoveTowards(transform.position, Player.transform.position, Speed * Time.deltaTime);
            
            rb.MovePosition(newPosition);
        }

        // Shoot if the player is within the distance to shoot
        else
            Shoot();

        shootingPoint.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void Shoot()
    {
        if (timeToFire <= 0f)
        {
            Debug.Log("would shoot now");
            timeToFire = fireRate;
            Instantiate(projectile, shootingPoint.position, shootingPoint.transform.rotation);
        }
        else
        {
            timeToFire -= Time.deltaTime;
        }
    }
}