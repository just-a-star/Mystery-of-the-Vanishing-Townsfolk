using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogShooter : Enemy
{
    [Header("ampun log")]
    public Transform target;
    public float chaseRadius;
    public float attackRadius;


    [Header("asli")]
    public GameObject projectile;
    public float fireDelay;
    float fireDelaySeconds;
    public bool canFire = true;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        fireDelaySeconds -= Time.deltaTime;
        if (fireDelaySeconds <= 0)
        {
            canFire = true;
            fireDelaySeconds = fireDelay;
        }
    }

    private void FixedUpdate()
    {
        CheckDistance();
    }
    public void CheckDistance()
    {
        if (Vector3.Distance(target.position,
                            transform.position) <= chaseRadius
           && Vector3.Distance(target.position,
                               transform.position) > attackRadius)
        {
                if (canFire)
                {
                    Vector3 tempVector = target.transform.position - transform.position;
                    GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);

                current.GetComponent<Batu>().Launch(tempVector);
                    canFire = false;
                anim.SetBool("attacking", true);
                }

            
        } else
        {
            anim.SetBool("attacking", false);
        }
    }

}
