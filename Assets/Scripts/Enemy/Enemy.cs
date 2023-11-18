using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public GameObject deathEffect;
    public LootItem lutingan;

  /*  public float Health
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
    }*/

 

    // Call this function to apply damage to the enemy
    void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void MakeLoot()
    {
        if (lutingan != null)
        {
            GameObject current = lutingan.lot();
            if (current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }

    private void Die()
    {
        // Code to handle the enemy's death, such as playing an animation or directly destroying the GameObject

        // Handle the enemy's death
        DeathEffect();
        MakeLoot();
        Destroy(gameObject);
    }

    void DeathEffect()
    {
        if(deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }
    }


   /* public void Defeated()
    {
        // Trigger the defeated animation, animasi ini belum ada sih jadi kumatiin dlu
        // animator.SetTrigger("Defeated");

        // Optionally, disable the enemy's collider, movement, etc.
        // GetComponent<Collider2D>().enabled = false;

        // Destroy or deactivate the enemy GameObject after a short delay to allow the animation to play
        Invoke(nameof(RemoveEnemy), 1f); // Adjust the delay as needed for your animation
    }*/

    public void RemoveEnemy()
    {
        // Destroy the enemy GameObject
        Destroy(gameObject);
    }

    public void Knock(Rigidbody2D rb, float knockTime, int damage)
    {
        StartCoroutine(KnockCo(rb, knockTime));
        TakeDamage(damage);
    }

    private IEnumerator KnockCo(Rigidbody2D rb, float knockTime)
    {
        if (rb != null)
        {
            yield return new WaitForSeconds(knockTime);
            rb.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            rb.velocity = Vector2.zero;
        }
    }
}
