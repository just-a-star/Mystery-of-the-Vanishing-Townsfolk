using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerMove : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    public float speed;
    Vector2 moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        inputJalan();

        animasi();
    }

    void inputJalan()
    {
        moveSpeed = Vector2.zero;
        moveSpeed.x = Input.GetAxisRaw("Horizontal");
        moveSpeed.y = Input.GetAxisRaw("Vertical");

        if (moveSpeed != Vector2.zero)
        {
            rb.MovePosition(rb.position + moveSpeed.normalized * speed * Time.fixedDeltaTime);
        }
    }

    void animasi()
    {
        if(moveSpeed != Vector2.zero)
        {
            animator.SetFloat("moveX", moveSpeed.x);
            animator.SetFloat("moveY", moveSpeed.y);
            animator.SetBool("moving", true);
        } else
        {
            animator.SetBool("moving", false);
        }
    }


}
