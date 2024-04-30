using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;

    private float timeAlive;

    // Update is called once per frame
    void Update()
    {
        if (timeAlive > 2f)
        {
            Destroy(this.gameObject);
        }

        timeAlive += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        other.GetComponent<Character>().ChangeHealth(-1);
        Destroy(this.gameObject);
    }
}