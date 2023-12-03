using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed; // Speed at which the bullet moves
    private Rigidbody2D rb;

    [Header("Lifetime")]
    public float lifetime; // Total time before the bullet gets destroyed
    private float lifetimeSeconds; // Countdown timer for the bullet's lifetime

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Initialize the Rigidbody2D component
        lifetimeSeconds = lifetime; // Set the lifetimeSeconds to the specified lifetime
    }

    void Update()
    {
        lifetimeSeconds -= Time.deltaTime; // Decrease the lifetime timer each frame
        if (lifetimeSeconds <= 0)
        {
            Destroy(gameObject); // Destroy the bullet when its lifetime ends
        }
    }

    // This method is called to launch the bullet in a specific direction
    public void Launch(Vector2 direction)
    {
        rb.velocity = direction.normalized * moveSpeed; // Set the velocity of the bullet
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Specify what happens when the bullet collides with different objects
        if (collision.CompareTag("Player"))
        {
            // Add logic here if you want to do something when hitting the player
            Destroy(gameObject); // Destroy the bullet on collision with the player
        }
        else if (collision.gameObject.layer == 3)
        {
            // Add logic here for collision with obstacles, if needed
            Destroy(gameObject); // Destroy the bullet on collision with an obstacle
        }
        // You can add more conditions for other collision tags if necessary
    }
}
