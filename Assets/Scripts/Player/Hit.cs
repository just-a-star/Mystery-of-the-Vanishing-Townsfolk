using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{

    public float dorongan;
    public float knockTime;
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && collision.isTrigger)
        {
            collision.GetComponent<Musuh>().TakeDamage(damage);
        }
    }
}
