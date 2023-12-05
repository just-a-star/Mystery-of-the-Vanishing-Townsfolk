using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenderuwoHell : Enemy
{
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Animator animator;
    public GameObject bulletPrefab; // Assign the bullet prefab in the Inspector
    public Transform bulletSpawnPoint; // Assign the spawn point for the bullets in the Inspector
    public float bulletSpeed = 5f; // Speed at which the bullet will travel

    private Rigidbody2D myRigidbody;
    private Vector2 movement;
    private float timeBetweenShots;
    private float shotCounter;
    public float meleeAttackRadius;


    public Collider2D boundary;

    [Header("kondisi kelar")]
    public string end;



    void Start()
    {
        currentState = EnemyState.idle;
        myRigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        myRigidbody.freezeRotation = true;
        timeBetweenShots = Random.Range(3f, 5f);
    }

    void Update()
    {
        if (target != null && currentState != EnemyState.stagger)
        {
            CheckDistance();
        }
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    void CheckDistance()
    {
        float distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (boundary.OverlapPoint(target.transform.position))
        {

            if (distanceToTarget > attackRadius) // Outside attack radius, move towards player
            {
                Vector2 direction = (target.position - transform.position).normalized;
                SetMovement(direction);
            }
            else if (distanceToTarget > meleeAttackRadius) // Within ranged attack radius, but outside melee attack radius
            {
                if (currentState != EnemyState.attack)
                {
                    SetMovement(Vector2.zero); // Stop moving when attacking
                    PerformRangedAttack();
                }
            }
            else // Within melee attack radius
            {
                if (currentState != EnemyState.attack)
                {
                    SetMovement(Vector2.zero); // Stop moving when attacking
                    PerformMeleeAttack();
                }
            }
        }
    }

    void PerformMeleeAttack()
    {
        animator.SetTrigger("Attack"); // Ensure you have a MeleeAttack trigger in your Animator
                                       // Melee attack logic here
                                       // For example, apply damage directly to the player if they are within melee range
        TransitionToMoveState();
    }
    void PerformRangedAttack()
    {
        animator.SetTrigger("RangedAttack"); // Ensure you have a RangedAttack trigger in your Animator
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            ShootBullet();
            shotCounter = timeBetweenShots; // Reset the shot counter
        }
        // Continue moving while performing ranged attack
        if (currentState != EnemyState.walk)
        {
            SetMovement((target.position - transform.position).normalized);
            currentState = EnemyState.walk;
        }
    }

    void SetMovement(Vector2 direction)
    {
        movement = direction;
        if (movement != Vector2.zero)
        {
            currentState = EnemyState.walk;
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
            animator.SetBool("isWalking", true);
        }
        else
        {
            currentState = EnemyState.idle;
            animator.SetFloat("Speed", 0);
            animator.SetBool("isWalking", false);
        }
    }

    void MoveCharacter()
    {
        if (currentState == EnemyState.walk)
        {
            myRigidbody.MovePosition(myRigidbody.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void PerformAttack()
    {
        animator.SetTrigger("Attack");
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            ShootBullet();
            shotCounter = timeBetweenShots; // Reset the shot counter
                                            // Transition back to walk state after shooting
            TransitionToMoveState();
        }
    }

    void ShootBullet()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

        SihirGenderuwo bulletComponent = bullet.GetComponent<SihirGenderuwo>();
        if (bulletComponent != null)
        {
            bulletComponent.Initialize(direction, bulletSpeed); // Pass direction and speed
        }
    }


    void TransitionToMoveState()
    {
        float distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (distanceToTarget > attackRadius)
        {
            SetMovement((target.position - transform.position).normalized);
            currentState = EnemyState.walk;
        }
        else
        {
            SetMovement(Vector2.zero);
            currentState = EnemyState.walk;
        }
    }

    public override void Die()
    {
        DeathEffect();
        MakeLoot();
        Destroy(gameObject);
        SceneManager.LoadScene(end);
    }









    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3) // Assuming obstacleLayer is defined
        {
            // Implement logic for when Genderuwo collides with an obstacle
            // For example, wait, change direction, etc.
            StartCoroutine(RecoverFromCollision());
        }
    }
    IEnumerator RecoverFromCollision()
    {
        currentState = EnemyState.idle; // Temporarily stop the enemy
        yield return new WaitForSeconds(0.1f); // Wait for a moment
        currentState = EnemyState.walk; // Resume following the player
    }
}
