using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kuntilanak : Enemy
{
    public Rigidbody2D rb;

    public float teleportCooldown = 8f; // Updated teleport cooldown
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
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        lastTeleportTime = Time.time; // Initialize lastTeleportTime
    }

    // Update is called once per frame
    void Update()
    {
        if (boundary.OverlapPoint(target.transform.position))
        {
            Vector2 direction = (target.position - transform.position).normalized;

            // Check if it's time to teleport
            if (!isTeleporting && Time.time >= lastTeleportTime + teleportCooldown)
            {
                StartCoroutine(TeleportRoutine(direction));
            }
            else if (!isTeleporting) // Ensure it can move only when not teleporting
            {
                SetMovement(direction); // Update the movement based on direction

            }
        }
    }

    private IEnumerator TeleportRoutine(Vector2 direction)
    {
        isTeleporting = true;
        animator.SetBool("isTeleporting", true); // Set the isTeleporting animator parameter
        currentState = EnemyState.teleport; // Set state to a special teleport state
        SetMovement(Vector2.zero); // Stop movement
        Vector2 teleportPosition = rb.position;
        GameObject teleportEffect = Instantiate(teleportEffectPrefab, teleportPosition, Quaternion.identity);

        // Start the teleportation coroutine
        yield return StartCoroutine(TeleportBehindPlayer(direction, teleportEffect));

        yield return new WaitForSeconds(1); // Additional wait after teleporting

        lastTeleportTime = Time.time; // Update lastTeleportTime after teleportation
        isTeleporting = false;
        animator.SetBool("isTeleporting", false); // Reset the isTeleporting animator parameter
        currentState = EnemyState.walk; // Reset state to walk
    }

    private IEnumerator TeleportBehindPlayer(Vector2 direction,GameObject teleportEffect)
    {
        yield return new WaitForSeconds(2f);
        Vector3 teleportPosition = target.position - new Vector3(direction.x, direction.y, 0) * 2; // Teleport 2 units behind player

        // Move Kuntilanak to the new position immediately
        transform.position = teleportPosition;

        Destroy(teleportEffect, 0.5f);

       
    }

    void SetMovement(Vector2 direction)
    {
        if (currentState != EnemyState.stagger) // Ensure not to move if staggered
        {
            movement = direction;
            if (movement != Vector2.zero && !isTeleporting)
            {
                Vector2 newPosition = rb.position + movement * moveSpeed * Time.fixedDeltaTime;
                rb.MovePosition(newPosition);
                currentState = EnemyState.walk;
                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);
                animator.SetFloat("Speed", movement.sqrMagnitude);
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetFloat("Speed", 0);
                animator.SetBool("isWalking", false);
            }
        }
    }






}
