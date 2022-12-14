using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float speed;
    public float damage;
    public bool enemyBullet;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !enemyBullet)
        {
            collision.GetComponent<HealthManager>().takeDamage(damage);
        }else if (collision.CompareTag("Player") && enemyBullet)
        {
            collision.GetComponent<HealthManager>().takeDamage(damage);
        }

        Destroy(gameObject);
    }

}
