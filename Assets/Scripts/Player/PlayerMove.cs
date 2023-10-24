using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;


public enum PlayerState
{
    stun,
    idle,
    walk,
    attack,
    dash
}

public class PlayerMove : MonoBehaviour
{
    // dash
    [SerializeField] public float dashSpeed;
    [SerializeField] public float dashDuration;
    [SerializeField] public float dashCooldown;
    private bool isDashing = false;
    private float lastDashTime = 0;
    public LayerMask obstacleLayer;


    public PlayerState state;
    Animator animator;
    Rigidbody2D rb;
    public float speed;
    Vector2 moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        state = PlayerState.walk;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
    }

    // Update is called once per frame
    void Update()
    {
        inputJalan();

        if (Input.GetButtonDown("attack") && state != PlayerState.attack && state != PlayerState.stun)
        {
            StartCoroutine(AttackCo());
        }
        else if (!isDashing)
        {
           
            if (Input.GetButtonDown("dash") && Time.time - lastDashTime > dashCooldown && state != PlayerState.dash)
            {
                Debug.Log("Dash key pressed");
                StartDash();
            }
            else
            {
                animasi();
            }
        }
    }


    IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        state = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        state = PlayerState.walk;
    }

    void inputJalan()
    {
        moveSpeed = Vector2.zero;
        moveSpeed.x = Input.GetAxisRaw("Horizontal");
        moveSpeed.y = Input.GetAxisRaw("Vertical");

    }

    void animasi()
    {
        if(moveSpeed != Vector2.zero)
        {
            move();
            animator.SetFloat("moveX", moveSpeed.x);
            animator.SetFloat("moveY", moveSpeed.y);
            animator.SetBool("moving", true);
        } else
        {
            animator.SetBool("moving", false);
        }
    }

    void move()
    {
        rb.MovePosition(rb.position + moveSpeed.normalized * speed * Time.fixedDeltaTime);
    }

    public void Knock(float knockTime)
    {
        StartCoroutine(KnockCo(knockTime));
    }

    // Dash abilty
    void StartDash()
    {
        isDashing = true;
        lastDashTime = Time.time;
        state = PlayerState.dash;

        Vector2 dashDirection = moveSpeed.normalized;
        

        // pke raycast untuk memeriksa ada collider gk di depan player
        RaycastHit2D hit = Physics2D.Raycast(rb.position, dashDirection, dashSpeed, obstacleLayer);

        if (hit.collider != null)
        {
            // Ada collider yang menghalangi, jadi kita tidak melakukan dash
            isDashing = false;
            state = PlayerState.walk;
        }
        else
        {
            // Tidak ada collider yang menghalangi, jadi kita melakukan dash
            rb.velocity = dashDirection * dashSpeed;
            Debug.Log("Dash Velocity: " + rb.velocity);
            StartCoroutine(EndDash());
        }
    }

    IEnumerator EndDash()
    {
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        state = PlayerState.walk;
        rb.velocity = Vector2.zero;
    }

    private IEnumerator KnockCo(float knockTime)
    {
        if (rb != null)
        {
            yield return new WaitForSeconds(knockTime);
            rb.velocity = Vector2.zero;
            state = PlayerState.idle;
            rb.velocity = Vector2.zero;
        }
    }

}
