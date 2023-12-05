using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kuntilanak : Enemy
{
    private Rigidbody2D myRigidbody;

    public float teleportCooldown = 5f;
    private float lastTeleportTime;
    public Transform target;

    [Header("kondisi Serang")]
    public Collider2D boundary;

    [Header("kondisi kelar")]
    public BoolValue kuntilanak;
    public GameObject portalBack;
    public GameObject Peti;

    [Header("Teleportation Effects")]
    public GameObject teleportEffectPrefab;
    public AudioClip teleportSound;
    private AudioSource audioSource;

    private Animator animator;
    private bool isTeleporting = false;

    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        lastTeleportTime = Time.time; // Initialize lastTeleportTime
    }

    // Update is called once per frame
    void Update()
    {
        if (boundary.OverlapPoint(target.transform.position) && !isTeleporting)
        {
            Vector2 direction = (target.position - transform.position).normalized;

            // Check if it's time to teleport
            if (Time.time >= lastTeleportTime + teleportCooldown)
            {
                StartCoroutine(TeleportRoutine(direction));
            }
            else
            {
                MoveTowardsPlayer(direction);
                UpdateAnimator(direction);
            }
        }
    }

    private IEnumerator TeleportRoutine(Vector2 direction)
    {
        isTeleporting = true;
        UpdateAnimator(Vector2.zero); // Stop movement animation

        // Start the teleportation coroutine
        yield return StartCoroutine(TeleportBehindPlayer(direction));

        yield return new WaitForSeconds(1); // Additional wait after teleporting, if needed

        lastTeleportTime = Time.time; // Update lastTeleportTime after teleportation
        isTeleporting = false;
    }

    private void UpdateAnimator(Vector2 direction)
    {
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", direction.sqrMagnitude);
    }

    private bool HasLineOfSightToPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, target.position - transform.position, Vector3.Distance(transform.position, target.position));
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            return true;
        }
        return false;
    }

    private IEnumerator TeleportBehindPlayer(Vector2 direction)
    {
        Vector3 teleportPosition = target.position - new Vector3(direction.x, direction.y, 0) * 2; // Teleport 2 units behind player

        // Move Kuntilanak to the new position immediately
        transform.position = teleportPosition;

        // Wait for 0.5 seconds before playing the effects and sound
        yield return new WaitForSeconds(0.5f);

        // Play teleportation effects and sound
        Instantiate(teleportEffectPrefab, teleportPosition, Quaternion.identity);
        audioSource.PlayOneShot(teleportSound);
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
    private void MoveTowardsPlayer(Vector2 direction)
    {
        if (currentState == EnemyState.walk)
        {
            myRigidbody.MovePosition(myRigidbody.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            StartCoroutine(RecoverFromCollision());
        }
    }

    IEnumerator RecoverFromCollision()
    {
        currentState = EnemyState.idle;
        yield return new WaitForSeconds(0.1f);
        currentState = EnemyState.walk;
    }

    public override void Die()
    {
        DeathEffect();
        MakeLoot();
        portalBack.SetActive(true);
        Peti.SetActive(true);
        kuntilanak.initialValue = true;
        Destroy(gameObject);

    }

    /* void TransitionToMoveState()
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

     }*/
}
