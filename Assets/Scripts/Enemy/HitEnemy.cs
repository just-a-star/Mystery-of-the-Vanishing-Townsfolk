using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemy : MonoBehaviour
{
    public static HitEnemy singleton;

    [Header("knockback")]
    public float dorongan;
    public float knockTime;
    public int damage;

    private void Awake()
    {
        singleton = this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D hit = collision.GetComponent<Rigidbody2D>();

        if (hit != null)
        {
            Vector2 difference = hit.transform.position - transform.position;
            difference = difference.normalized * dorongan;
            hit.AddForce(difference, ForceMode2D.Impulse);

            if (collision.gameObject.CompareTag("Enemy") && collision.isTrigger)
            {
                hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                collision.GetComponent<Enemy>().Knock(hit, knockTime, damage);
            }
        }

    }
}
