using System;
using UnityEngine;

public class CharacterProjectile : MonoBehaviour
{
    private Vector3 _direction;
    [SerializeField] private float speed;
    
    public void Shoot(Vector3 direction, float speed)
    {
        _direction = direction;
        this.speed = speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the projectile hits anything other than an enemy, return
        if (!other.CompareTag("Enemy"))
            return;

        // get 
        
    }
}