using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}

public class Musuh : MonoBehaviour
{

    public EnemyState currentState;
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;

   

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
