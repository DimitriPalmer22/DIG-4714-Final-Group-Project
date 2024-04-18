using System;
using UnityEngine;

public class CharacterProjectile : MonoBehaviour
{
    private Vector2 _direction;
    [SerializeField] private float speed;
    
    /// <summary>
    /// A flag to check if the projectile has hit something
    /// </summary>
    private bool _hitSomething;

    // Start is called before the first frame update
    private void Start()
    {
        // destroy the projectile after 10 seconds
        Destroy(gameObject, 10);
    }

    // Update is called once per frame
    private void Update()
    {
        // Move the projectile
        Move();
    }

    public void Shoot(Vector2 direction, float speed)
    {
        _direction = direction;
        this.speed = speed;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the projectile has already hit something, return
        if (_hitSomething)
            return;
                
        // If the projectile hits anything other than an enemy, return
        if (!other.CompareTag("Enemy"))
            return;
        
        // Set the hit something variable to true
        _hitSomething = true;

        // get the enemy script
        var enemy = other.GetComponent<EnemyScript>();

        // Make the enemy take damage
        enemy.ChangeHealth(-1);

        // Destroy the projectile
        Destroy(gameObject);
    }

    private void Move()
    {
        transform.Translate(_direction * (speed * Time.deltaTime));
    }
}