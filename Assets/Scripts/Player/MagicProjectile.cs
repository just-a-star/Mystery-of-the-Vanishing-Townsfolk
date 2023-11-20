using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicProjectile : MonoBehaviour
{
    public float speed, destroyTime;
    public Rigidbody2D rb;
    private void Awake()
    {
        Destroy(gameObject, destroyTime);
    }

    public void gerak(Vector2 velocity, Vector3 direction)
    {
        rb.velocity = velocity.normalized * speed;
        transform.rotation = Quaternion.Euler(direction);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
