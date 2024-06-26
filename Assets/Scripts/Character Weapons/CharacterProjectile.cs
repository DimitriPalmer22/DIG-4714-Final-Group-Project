using System;
using UnityEngine;

public class CharacterProjectile : MonoBehaviour
{
    private const float LIFE_TIME = 5;
    
    private Vector2 _direction;
    [SerializeField] private float speed;
    
    /// <summary>
    /// A flag to check if the projectile has hit something
    /// </summary>
    private bool _hitSomething;
    
    /// <summary>
    /// Used to keep track of how long the projectile has been alive
    /// </summary>
    private float _timePassed;

    /// <summary>
    /// How much damage the projectile does to the enemies.
    /// </summary>
    private float _damage;

    /// <summary>
    /// How far the projectile can travel before it is destroyed
    /// </summary>
    private float _range;
    
    /// <summary>
    /// Starting position of the projectile
    /// </summary>
    private Vector3 _startPosition;

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
        
        // Destroy the projectile after {LIFE_TIME} seconds
        _timePassed += Time.deltaTime;
        if(_timePassed >= LIFE_TIME)
            Destroy(this);
        
        // Destroy the projectile if it has traveled too far
        if (Vector3.Distance(_startPosition, transform.position) >= _range)
            Destroy(gameObject);
    }

    public void Shoot(Vector2 direction, CharacterWeaponInfo weaponInfo)
    {
        _direction = direction;
        speed = weaponInfo.projectileVelocity + weaponInfo.weapon.Character.Speed;
        _damage = weaponInfo.damage * weaponInfo.weapon.DamageMultiplier;
        _range = weaponInfo.range;
        
        _startPosition = transform.position;
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
        enemy.ChangeHealth(-_damage);

        // Destroy the projectile
        Destroy(gameObject);
    }

    private void Move()
    {
        transform.Translate(_direction * (speed * Time.deltaTime));
    }
}