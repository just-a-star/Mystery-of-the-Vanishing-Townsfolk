using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LogShooter : Enemy
{
    [Header("Target Enemy")]
    public Transform target;

    [Header("Posisi Awal")]
    public Transform homePosition;


    [Header("Projectile")]
    public GameObject projectile;
    public float fireDelay;
    float fireDelaySeconds;
    public bool canFire = true;
    Animator anim;

    public Collider2D boundary;

    private void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        
        CheckDistance();
        fireDelaySeconds -= Time.deltaTime;
        if (fireDelaySeconds <= 0)
        {
            canFire = true;
            fireDelaySeconds = fireDelay;
        }
    }

    public void CheckDistance()
    {
        if (boundary.OverlapPoint(target.transform.position))
        {
                if (canFire)
                {
                    Vector3 tempVector = target.transform.position - transform.position;
                    GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);

                anim.SetBool("attacking", true);
                anim.SetFloat("moveX", (target.position.x - transform.position.x));
                anim.SetFloat("moveY", (target.position.y - transform.position.y));
                current.GetComponent<Batu>().Launch(tempVector);
                    canFire = false;
                }

            
        }
        
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, homePosition.position, moveSpeed * Time.deltaTime);
            anim.SetBool("attacking", false);
        }
    }

    

}
