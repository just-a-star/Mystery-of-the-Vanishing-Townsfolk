using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batu : MonoBehaviour
{

    [Header("move")]
    public float moveSpeed;
    public Vector2 directionToMove;

    [Header("lifetime")]
    public float lifetime;
    float lifetimeSeconds;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lifetimeSeconds = lifetime;
    }

    // Update is called once per frame
    void Update()
    {
        lifetimeSeconds -= Time.deltaTime;
        if (lifetimeSeconds <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Launch(Vector2 initialVel)
    {
        rb.velocity = initialVel * moveSpeed;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
