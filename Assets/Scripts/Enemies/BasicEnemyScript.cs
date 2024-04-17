using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyScript : EnemyScript
{
    private const float StopWalkingDistance = 2f;

    // Update is called once per frame
    private void Update()
    {
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        // Move towards the player
        MoveTowardPlayer();
    }

    protected override void MoveTowardPlayer()
    {
        Vector2 direction = Player.transform.position - transform.position;

        direction.Normalize();
        
        // Get the distance between the player and the enemy
        var distance = Vector2.Distance(transform.position, Player.transform.position);

        var newPosition = Vector2.MoveTowards(transform.position, Player.transform.position, Speed * Time.deltaTime);
        
        rb.MovePosition(newPosition);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, StopWalkingDistance);
    }
}