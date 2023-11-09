using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;

    [SerializeField] private float health; // Make this private to encapsulate it
    public float Health
    {
        set
        {
            health = value;
            if (health <= 0)
            {
                Defeated(); // Call Defeated when health reaches 0 or below
            }
        }
        get { return health; }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Call this function to apply damage to the enemy
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Code to handle the enemy's death, such as playing an animation or directly destroying the GameObject

        // Handle the enemy's death
        Debug.Log(name + " is dead.");
        Destroy(gameObject);
    }


    public void Defeated()
    {
        // Trigger the defeated animation, animasi ini belum ada sih jadi kumatiin dlu
        // animator.SetTrigger("Defeated");

        // Optionally, disable the enemy's collider, movement, etc.
        // GetComponent<Collider2D>().enabled = false;

        // Destroy or deactivate the enemy GameObject after a short delay to allow the animation to play
        Invoke(nameof(RemoveEnemy), 1f); // Adjust the delay as needed for your animation
    }

    public void RemoveEnemy()
    {
        // Destroy the enemy GameObject
        Destroy(gameObject);
    }
}
