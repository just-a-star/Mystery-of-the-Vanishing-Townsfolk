using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour
{
    [Header("state Enemy")]
    public EnemyState currentState;

    [Header("Health")]
    float maxHealth;
    public float health;
    public GameObject holder;
    public Image healthUI;

    [Header("Speed")]
    public float moveSpeed;

    [Header("Efek & dropItem")]
    public GameObject deathEffect;
    public LootItem lutingan;

    [Header("flash")]
    bool flashActive;
    [SerializeField] float flashLength = 0f;
    float flashCounter = 0f;
    public SpriteRenderer enemySprite;

    private void Awake()
    {
        maxHealth = health;
    }

    private void Start()
    {
        enemySprite = GetComponent<SpriteRenderer>();
        healthUI = GetComponent<Image>();
        
    }

    private void FixedUpdate()
    {
        if(flashActive == true)
        {
            if (flashCounter > flashLength * .99f)
            {
                
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            } else if (flashCounter > flashLength * .82f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }else if (flashCounter > flashLength * .66f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }else if (flashCounter > flashLength * .49f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }else if (flashCounter > flashLength * .33f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }else if (flashCounter > flashLength * .16f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }else if (flashCounter > 0)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }else
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
                flashActive = false;
            }
            flashCounter -= Time.deltaTime;
        }

    }
    void TakeDamage(float damage)
    {

        holder.SetActive(true);
        if( health > 0 ) {
        health -= damage;
            float fillAmount = health / maxHealth;
            healthUI.fillAmount = fillAmount;


            flashActive = true;
        flashCounter = flashLength;

            if (health <= 0)
        {
            Die();
        }
        }
    }

    public void MakeLoot()
    {
        if (lutingan != null)
        {
            GameObject current = lutingan.lot();
            if (current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }

    public virtual void Die()
    {
        DeathEffect();
        MakeLoot();
        Destroy(gameObject);
    }

    public void DeathEffect()
    {
        if(deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }
    }


    public void RemoveEnemy()
    {
        Destroy(gameObject);
    }

    public void Knock(Rigidbody2D rb, float knockTime, int damage)
    {
        StartCoroutine(KnockCo(rb, knockTime));
        TakeDamage(damage);
    }

    private IEnumerator KnockCo(Rigidbody2D rb, float knockTime)
    {
        if (rb != null)
        {
            yield return new WaitForSeconds(knockTime);
            rb.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            rb.velocity = Vector2.zero;
        }
    }
}
