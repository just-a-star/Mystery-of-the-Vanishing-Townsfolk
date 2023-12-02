using System.Collections;
using UnityEngine;

public class Genderuwo : Enemy
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
        if (distanceToTarget <= chaseRadius && distanceToTarget > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk)
            {
                SetMovement(target.position - transform.position);
            }
        }
        else if (distanceToTarget <= attackRadius && currentState != EnemyState.attack)
        {
            SetMovement(Vector2.zero);
            PerformAttack();

        }
        else
        {
            SetMovement(Vector2.zero);
        }
    }

    void SetMovement(Vector2 direction)
    {
        movement = direction.normalized;
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
        currentState = EnemyState.attack;
        animator.SetTrigger("Attack");
        // Attack logic (e.g., instantiate bullets, apply damage to the player, etc.)
        shotCounter -= Time.deltaTime;
    if (shotCounter <= 0)
    {
        ShootBullet();
        shotCounter = Random.Range(3f, 5f); // Reset the shot counter
        currentState = EnemyState.idle; // Reset the state to allow movement again
        }
    }

    void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        SihirGenderuwo bulletComponent = bullet.GetComponent<SihirGenderuwo>();
        if (bulletComponent != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            bulletComponent.Initialize(direction);
        }
    }

}
