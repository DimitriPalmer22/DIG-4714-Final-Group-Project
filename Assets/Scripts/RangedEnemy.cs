using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RangedEnemy : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float fireRate;
    
    [SerializeField] private float distanceToShoot;
    [SerializeField] private float distanceToStop;

    [SerializeField] private Transform shootingPoint;
    [SerializeField] private GameObject projectile;

    private float distance;
    private float timeToFire;

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;

        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if(distance >= distanceToStop)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, 
            chaseSpeed * Time.deltaTime);
        }

        if(distance <= distanceToStop)
        {
            Shoot();
        }

        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        shootingPoint.rotation = Quaternion.Euler(0f, 0f, angle);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Character>().ChangeHealth(-1);
        }
  
    }

    private void Shoot()
    {
        if(timeToFire <= 0f)
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
