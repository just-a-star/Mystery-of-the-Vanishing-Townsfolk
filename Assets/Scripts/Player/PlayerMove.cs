using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;


public enum PlayerState
{
    stun,
    idle,
    walk,
    attack
}

public class PlayerMove : MonoBehaviour
{

 
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
        
        else if (state == PlayerState.walk || state == PlayerState.idle)
        {
            animasi();
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
