/*using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Collider2D swordCollider;
    [SerializeField] private float swordDamage;
    private Animator animator;
    private PlayerMove playerMove;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
        swordCollider.enabled = false; // Ensure the sword collider is disabled by default
    }

    public void Attack()
    {
        if (playerMove.state != PlayerState.attack && playerMove.state != PlayerState.stun)
        {
            swordCollider.enabled = true; // Enable the sword collider when attacking
            playerMove.DisableMovement(); // Disable player movement during attack
            animator.SetBool("attacking", true);
            playerMove.state = PlayerState.attack;
            StartCoroutine(AttackCo());
        }
    }

    public void StopAttack()
    {
        swordCollider.enabled = false; // Disable the sword collider after attack
        playerMove.EnableMovement(); // Re-enable player movement
        animator.SetBool("attacking", false);
        playerMove.state = PlayerState.walk;
    }

    IEnumerator AttackCo()
    {
        yield return new WaitForSeconds(0.3f); // Wait for the duration of the attack animation
        StopAttack();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && swordCollider.enabled)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(swordDamage);
                // Print the enemy's remaining health to the console
                Debug.Log("Hit " + other.name + ". HP left: " + enemy.Health);
            }
            else
            {
                // For debugging: print a message if the enemy component is not found
                Debug.Log("Enemy component not found on: " + other.name);
            }
        }
    }
    }
*/