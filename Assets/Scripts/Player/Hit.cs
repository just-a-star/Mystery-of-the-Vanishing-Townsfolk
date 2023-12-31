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
        Debug.Log("OnTriggerEnter2D: Collision detected with " + collision.gameObject.name);

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
                Debug.Log("Force applied to " + collision.gameObject.name);

                if (collision.gameObject.CompareTag("Enemy"))
                {
                    Debug.Log("Knocking back enemy: " + collision.gameObject.name);
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    collision.GetComponent<Enemy>().Knock(hit, knockTime, damage);
                }

                if (collision.gameObject.CompareTag("Player"))
                {
                    Debug.Log("Player hit: " + collision.gameObject.name);
                    PlayerHealth.singleton.TakeDamage(damage);
                    hit.GetComponent<PlayerController>().state = PlayerState.stun;
                    collision.GetComponent<PlayerController>().Knock(knockTime);
                }
            }
            else
            {
                Debug.Log("Rigidbody2D not found on " + collision.gameObject.name);
            }
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouching = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouching = false;
        }
    }

    /*private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
       
    }*/
}
