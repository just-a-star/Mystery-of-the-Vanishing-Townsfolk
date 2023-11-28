using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocong : Enemy
{
    private Rigidbody2D myRigidbody;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    Vector2 movement;
    public Animator animator;

    public float jumpHeight = 5f; // Max height of the jump
    public float jumpDuration = 3f; // Duration of the jump
    public float jumpCooldown = 5f; // Time in seconds between jumps

    private bool isJumping = false; // To track if Pocong is currently jumping

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
        Vector2 direction = target.position - transform.position;
        direction.Normalize();

        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        float speed = movement.sqrMagnitude;
        animator.SetFloat("Speed", speed);
    }

    void CheckDistance()
    {
        float distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (distanceToTarget <= chaseRadius && distanceToTarget > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                ChangeAnimationState(EnemyState.walk);
                myRigidbody.MovePosition(temp);
                movement = (temp - transform.position).normalized;
                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);
                animator.SetFloat("Speed", movement.sqrMagnitude);
            }
        }
        else if (distanceToTarget <= attackRadius)
        {
            PerformAttack();
        }
        else
        {
            if (currentState != EnemyState.idle)
            {
                ChangeAnimationState(EnemyState.idle);
                animator.SetFloat("Speed", 0);
            }
        }
    }

    void ChangeAnimationState(EnemyState newState)
    {
        if (currentState == newState) return;
        currentState = newState;
    }

    void PerformAttack()
    {
        // Attack logic here
    }

    IEnumerator PerformJump()
    {
        isJumping = true;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = target.position;
        endPosition.y = transform.position.y;

        float elapsedTime = 0;
        while (elapsedTime < jumpDuration)
        {
            float progress = elapsedTime / jumpDuration;
            float verticalPosition = 4 * jumpHeight * progress * (1 - progress);
            Vector3 horizontalPosition = Vector3.Lerp(startPosition, endPosition, progress);
            transform.position = new Vector3(horizontalPosition.x, startPosition.y + verticalPosition, horizontalPosition.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

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
