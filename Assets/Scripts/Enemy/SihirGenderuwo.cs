using UnityEngine;

public class SihirGenderuwo : MonoBehaviour
{
    public float speed = 5f; // Speed of the bullet
    public Vector2 direction; // Direction of the bullet
    public int damage = 2;
    public Rigidbody2D rb;
    public float stunTime = 2.0f;

    // Initialize the bullet's direction.
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Initialize the bullet with direction and speed
    public void Initialize(Vector2 direction, float speed)
    {
        rb.velocity = direction.normalized * speed;

        // Calculate the angle in degrees and rotate the bullet
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Debugging information
        Debug.Log("Bullet Direction: " + direction + ", Angle: " + angle + ", Velocity: " + rb.velocity);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().StunPlayer(stunTime); // Stun for 2 seconds
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject); // Destroy the bullet
        }

    }
}

