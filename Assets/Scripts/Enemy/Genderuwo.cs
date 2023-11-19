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

    // jump
    public float jumpHeight = 5f; // Max height of the jump
    public float jumpDuration = 3f; // Duration of the jump
    public float jumpCooldown = 5f; // Time in seconds between jumps

    private bool isJumping = false; // To track if Genderuwo is currently jumping

    private int originalLayer; // To store the original layer of the Genderuwo
    private int jumpLayer; // Layer that doesn't collide with other entities

    void Start()
    {
        currentState = EnemyState.idle;
        myRigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        myRigidbody.freezeRotation = true;

        StartCoroutine(JumpRoutine());


    }

    void FixedUpdate()
    {
        if (!isJumping)
        {
            CheckDistance();
        }
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

    // Jump

    IEnumerator PerformJump()
    {
        isJumping = true;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = target.position;
        endPosition.y = transform.position.y; // Keep the y-coordinate consistent

        float elapsedTime = 0;
        while (elapsedTime < jumpDuration)
        {
            // Calculate progress in the range of 0 to 1
            float progress = elapsedTime / jumpDuration;

            // Calculate vertical position using a parabolic trajectory formula
            float verticalPosition = 4 * jumpHeight * progress * (1 - progress);

            // Interpolate horizontal position only, vertical position is calculated separately
            Vector3 horizontalPosition = Vector3.Lerp(startPosition, endPosition, progress);
            transform.position = new Vector3(horizontalPosition.x, startPosition.y + verticalPosition, horizontalPosition.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure Genderuwo lands at the player's position
        transform.position = new Vector3(endPosition.x, transform.position.y, endPosition.z);
        isJumping = false;
    }




    IEnumerator JumpRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(jumpCooldown);
            if (!isJumping)
            {
                StartCoroutine(PerformJump());
            }
        }
    }







}

