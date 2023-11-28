using UnityEngine;

public class SihirGenderuwo : MonoBehaviour
{
    public float speed = 5f; // Speed of the bullet
    public Vector2 direction; // Direction of the bullet
    public int damage = 2;

    // Initialize the bullet's direction.
    public void Initialize(Vector2 direction)
    {
        this.direction = direction.normalized;
        this.GetComponent<Rigidbody2D>().velocity = this.direction * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);

            // Add damage to the player here if needed
            Destroy(gameObject); // Destroy the bullet
        }
        else if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject); // Destroy the bullet if it hits a wall
        }
    }
}

