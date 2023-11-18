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
    dash,
    interact
}

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove singleton;
    //darah
    public IntValue darah;
    

    // dash
    [SerializeField] public float dashSpeed;
    [SerializeField] public float dashDuration;
    [SerializeField] public float dashCooldown;
    private bool isDashing = false;
    private float lastDashTime = 0;
    [SerializeField] float dashDistance;
    public LayerMask obstacleLayer;
    
    
    // Components
    public PlayerState state;
    public Animator animator;
    Rigidbody2D rb;

    // received item
    public SpriteRenderer receivedItem;
    public Inventory playerInventory;

    // Movement
    public float speed;
    Vector2 moveSpeed;
    public VectorValue starPos;

    private void Awake()
    {
        singleton = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //darah
        
        
        /*playerAttack = GetComponent<PlayerAttack>();*/
        state = PlayerState.walk;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
        if (starPos != null)
        {
            transform.position = starPos.initialValue;
            
        } else
        {
            Debug.Log("make sure to set the starting position");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
        inputJalan();

        if (Input.GetButtonDown("attack") && state != PlayerState.attack && state != PlayerState.stun && state != PlayerState.interact)
        {
            StartCoroutine(AttackCo());
            /*playerAttack.Attack();*/
        }
        else if (!isDashing)
        {
           
            if (Input.GetButtonDown("dash") && Time.time - lastDashTime > dashCooldown && state != PlayerState.dash && state != PlayerState.interact)
            {
                Debug.Log("Dash key pressed");
                StartDash();
            }
            else if(state == PlayerState.walk || state == PlayerState.idle)
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

   /* public void Knock(float knockTime)
    {
        StartCoroutine(KnockCo(knockTime));
    }*/

    // Dash abilty
    void StartDash()
    {
        isDashing = true;
        lastDashTime = Time.time;
        state = PlayerState.dash;

        int playerLayer = LayerMask.NameToLayer("Player");
        int enemyLayer = LayerMask.NameToLayer("Musuh");

        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, true);

        Vector2 dashDirection = moveSpeed.normalized;
        float finalDashSpeed = dashSpeed * dashDistance;
        

        // pke raycast untuk memeriksa ada collider gk di depan player
        RaycastHit2D hit = Physics2D.Raycast(rb.position, dashDirection, finalDashSpeed * Time.fixedDeltaTime, obstacleLayer);

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
            StartCoroutine(EndDash(playerLayer, enemyLayer));
        }
    }

    IEnumerator EndDash(int playerLayer, int enemyLayer)
    {
        yield return new WaitForSeconds(dashDuration);

        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);

        isDashing = false;
        state = PlayerState.walk;
        rb.velocity = Vector2.zero;
    }

    public void Knock(float knockTime, int damage)
    {
        darah.initialValue = darah.initialValue - damage;
        
        if (darah.initialValue > 0 )
        {
            
            StartCoroutine(KnockCo(knockTime));
        } else
        {
            this.gameObject.SetActive(false);
        }
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

    public void RaiseItem()
    {
        if(playerInventory.currentItem != null)
        {

            if(state != PlayerState.interact)
            {
                animator.SetBool("receive items", true);
                state = PlayerState.interact;
                receivedItem.sprite = playerInventory.currentItem.itemSprite;
            } else
            {
                animator.SetBool("receive items", false);
                state = PlayerState.idle;
                receivedItem.sprite = null;
            }
        }
    }

}
