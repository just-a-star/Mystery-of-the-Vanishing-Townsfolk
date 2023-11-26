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

public class PlayerController : MonoBehaviour
{
    public static PlayerController singleton;
    
    [Header("Darah")]
    public FloatValue darah;

    [Header("Dash")]
    [SerializeField] public float dashSpeed;
    [SerializeField] public float dashDuration;
    [SerializeField] public float dashCooldown;
    private bool isDashing = false;
    private float lastDashTime = 0;
    [SerializeField] float dashDistance;
    public LayerMask obstacleLayer;


    [Header("Komponen")]
    public PlayerState state;
    public Animator animator;
    Rigidbody2D rb;


    [Header("Dapat Item")]
    public SpriteRenderer receivedItem;
    public Inventory playerInventory;


    [Header("Move")]
    public float speed;
    Vector2 moveSpeed;
    public VectorValue starPos;

    [Header("Tembakan")]
    public GameObject magicShot;
    public Inventory tes;
    public FloatValue mana;

    private void Awake()
    {
        singleton = this;
    }

    // Start is called before the first frame update
    void Start()
    {
       
        
        
        /*playerAttack = GetComponent<PlayerAttack>();*/
        state = PlayerState.walk;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
        transform.position = starPos.initialValue;

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
        else if (Input.GetButtonDown("Nembak") && state != PlayerState.attack && state != PlayerState.stun && state !=PlayerState.interact && mana.initialValue > 0)
        {
            StartCoroutine(SecondAttackCo());
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
    
    IEnumerator DamagedCo()
    {
        animator.SetTrigger("kenaSerang");
        state = PlayerState.stun;
        yield return null;
        /*animator.SetBool("damaged", false);*/
        yield return new WaitForSeconds(.3f);
        state = PlayerState.idle;
    }
    
    IEnumerator SecondAttackCo()
    {
        animator.SetBool("tembak", true);
        state = PlayerState.attack;
        yield return null;
        MakeTembak();
        
        animator.SetBool("tembak", false);
        yield return new WaitForSeconds(.3f);
        state = PlayerState.walk;
    }

    void MakeTembak()
    {
        
            Vector2 temp = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        MagicProjectile sihir = Instantiate(magicShot, transform.position, Quaternion.identity).GetComponent<MagicProjectile>();
        sihir.gerak(temp, ArahTembakan());
        MagicManager.singleton.DecreaseMagic();


    }

    Vector3 ArahTembakan()
    {
        float temp = Mathf.Atan2(animator.GetFloat("moveY"), animator.GetFloat("moveX")) * Mathf.Rad2Deg;

        return new Vector3(0, 0, temp);
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
            moveSpeed.x = Mathf.Round(moveSpeed.x);
            moveSpeed.y = Mathf.Round(moveSpeed.y);
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
        int ProjectileLayer = LayerMask.NameToLayer("MusuhProjectile");

        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, true);
        Physics2D.IgnoreLayerCollision(playerLayer, ProjectileLayer, true);

        Vector2 dashDirection = moveSpeed.normalized;
        

        // pke raycast untuk memeriksa ada collider gk di depan player
        RaycastHit2D hit = Physics2D.Raycast(rb.position, dashDirection, dashDistance, obstacleLayer);

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
            StartCoroutine(EndDash(playerLayer, enemyLayer, ProjectileLayer));
        }
    }

    IEnumerator EndDash(int playerLayer, int enemyLayer, int ProjectileLayer)
    {
        yield return new WaitForSeconds(dashDuration);

        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
        Physics2D.IgnoreLayerCollision(playerLayer, ProjectileLayer, false);

        isDashing = false;
        state = PlayerState.walk;
        rb.velocity = Vector2.zero;
    }

    public void Knock(float knockTime)
    {
        if (darah.initialValue > 0 )
        {
            StartCoroutine(KnockCo(knockTime));
            StartCoroutine(DamagedCo());
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
