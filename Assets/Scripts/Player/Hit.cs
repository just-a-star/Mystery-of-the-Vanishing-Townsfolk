using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public static Hit Instance;

    [Header("knockback")]
    public float dorongan;
    public float knockTime;
    public int damage;

    [Header("dps")]
    float dps = 1f;
    bool isTouching;

    //bool stay = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (isTouching)
        {
            dps -= Time.deltaTime;
            if( dps <= 0 )
            {
                PlayerHealth.singleton.TakeDamage(damage);
                dps = 1f;

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D hit = collision.GetComponent<Rigidbody2D>();

            if (hit != null)
            {
                
                    Vector2 difference = hit.transform.position - transform.position;
                    difference = difference.normalized * dorongan;

                Vector2 raycastOrigin = new Vector2(transform.position.x, transform.position.y);


                RaycastHit2D dinding = Physics2D.Raycast(raycastOrigin, difference, 0, LayerMask.GetMask("Obstacle"));

                    hit.AddForce(difference, ForceMode2D.Impulse);
                


                if (collision.gameObject.CompareTag("Enemy") && collision.isTrigger)
                {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    collision.GetComponent<Enemy>().Knock(hit, knockTime, damage);
                }

                if (collision.gameObject.CompareTag("Player") && collision.isTrigger)
                {
                    PlayerHealth.singleton.TakeDamage(damage);
                    hit.GetComponent<PlayerController>().state = PlayerState.stun;
                    collision.GetComponent<PlayerController>().Knock(knockTime);
                }
            }
            

        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) { 
        isTouching = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player")) { 
        isTouching = false;
        }
    }
}
