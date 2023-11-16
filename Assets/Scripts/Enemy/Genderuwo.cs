using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genderuwo : Enemy
{
    // Start is called before the first frame update
    private Rigidbody2D myRigidbody;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    Vector2 movement;
    public Animator animator;

    void Start()
    {
        currentState = EnemyState.idle;
        myRigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        myRigidbody.freezeRotation = true;
    }
    void Update()
    {
        // Calculate the direction vector from the enemy to the target (player).
        Vector2 direction = target.position - transform.position;
        direction.Normalize(); // Normalize the direction to have a magnitude of 1.

        // Set the animator parameters.
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);

        // Calculate the speed as the magnitude of the movement vector.
        // This will be zero when idle and positive when moving.
        float speed = movement.sqrMagnitude;
        animator.SetFloat("Speed", speed);
    }


    void FixedUpdate()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        // Calculate the distance from the enemy to the target (player).
        float distanceToTarget = Vector3.Distance(target.position, transform.position);

        // If the player is within the chase radius but outside the attack radius.
        if (distanceToTarget <= chaseRadius && distanceToTarget > attackRadius)
        {
            // Check if the enemy is currently idle or walking but not staggered.
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                // Move towards the player.
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                // Change the animation state to walk.
                ChangeAnimationState(EnemyState.walk);

                // Move the enemy towards the target.
                myRigidbody.MovePosition(temp);

                // Since the enemy is moving, update the movement vector.
                movement = (temp - transform.position).normalized;

                // Update the animation parameters.
                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);
                animator.SetFloat("Speed", movement.sqrMagnitude);
            }
        }
        else if (distanceToTarget <= attackRadius)
        {
            // If the player is within attack range, stop moving and perform attack.
            // You would need to implement this based on your game's logic.
            PerformAttack();
        }
        else
        {
            // If the player is outside the chase radius, return to idle state.
            if (currentState != EnemyState.idle)
            {
                ChangeAnimationState(EnemyState.idle);
                animator.SetFloat("Speed", 0);
            }
        }
    }

    void ChangeAnimationState(EnemyState newState)
    {
        // If the current state is the same as the new state, don't interrupt the animation.
        if (currentState == newState) return;

        // Update the current state.
        currentState = newState;

        // Here you might want to change the animation based on the state.
        // This will depend on how your animations are set up in Unity.
        // For example:
        // animator.SetBool("isWalking", newState == EnemyState.walk);
        // animator.SetBool("isAttacking", newState == EnemyState.attack);
    }

    void PerformAttack()
    {
        // Here you would implement the attack logic.
        // This could involve setting the currentState to attack,
        // triggering an attack animation, and dealing damage to the player.
    }

}

