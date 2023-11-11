using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{

    public float dorongan;
    public float knockTime;
    public int damage;

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
                        collision.GetComponent<PlayerHealth>().TakeDamage();
                    
                    if(collision.GetComponent<PlayerMove>().state != PlayerState.stun)
                    {
                        hit.GetComponent<PlayerMove>().state = PlayerState.stun;
                        collision.GetComponent<PlayerMove>().Knock(knockTime, damage);

                    }
                }
            }
            
        }
    }
}
