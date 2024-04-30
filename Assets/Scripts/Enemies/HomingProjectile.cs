using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    private const float LIFE_SPAN = 3f;

    [SerializeField] private float speed;


    [Range(0, 1)] [SerializeField] private float homingStrength = 1f;

    private float timeAlive;

    private GameObject target;

    // Update is called once per frame
    void Update()
    {
        // Find the target
        FindTarget();

        // If the projectile has been alive for too long, destroy it
        if (timeAlive >= LIFE_SPAN)
            Destroy(gameObject);

        timeAlive += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (target == null)
            return;

        transform.right = Vector2.Lerp(
            transform.right,
            (target.transform.position - transform.position).normalized,
            homingStrength
        );

        transform.Translate(Vector2.right * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        other.GetComponent<Character>().ChangeHealth(-1);
        Destroy(gameObject);
    }

    private void FindTarget()
    {
        if (target != null)
            return;

        var possibleTargets = GameObject.FindGameObjectsWithTag("Player");

        if (possibleTargets.Length == 0)
            return;

        // Find the closest target
        target = possibleTargets.OrderBy(n => Vector3.Distance(n.transform.position, transform.position)).First();
    }
}