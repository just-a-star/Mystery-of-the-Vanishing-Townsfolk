using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{

    public static Hit Instance;

    public float dorongan;
    public float knockTime;
    public int damage;

    private void Awake()
    {
        Instance = this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D hit = collision.GetComponent<Rigidbody2D>();
            
            if(hit != null)
            {
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * dorongan;
                hit.AddForce(difference, ForceMode2D.Impulse);

                if(collision.gameObject.CompareTag("Enemy") && collision.isTrigger )
                {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    collision.GetComponent<Enemy>().Knock(hit, knockTime, damage);
                    Debug.Log("Hit " + collision.GetComponent<Enemy>().enemyName + ". HP left: " + collision.GetComponent<Enemy>().health);
                }

                if(collision.gameObject.CompareTag("Player") && collision.isTrigger)
                {
                    PlayerHealth.singleton.TakeDamage(damage);
                    
                    /*if(collision.GetComponent<PlayerMove>().state != PlayerState.stun)
                    {*/
                        hit.GetComponent<PlayerController>().state = PlayerState.stun;
                        collision.GetComponent<PlayerController>().Knock(knockTime);

                    /*}*/
                }
            }
            
        }
    }
}
