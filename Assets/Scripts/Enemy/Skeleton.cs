using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    Animator anim;
    Transform target;
    public Transform homePosition;
    [SerializeField] float maxRange;
    [SerializeField] float minRange;

    public Collider2D boundary;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (boundary.OverlapPoint(target.transform.position))
        {
            FollowPlayer();
        } else
        {
            GoHome();
        }
        
    }

    public void FollowPlayer()
    {
        anim.SetBool("isMoving", true);
        anim.SetFloat("moveX", (target.position.x - transform.position.x));
        anim.SetFloat("moveY", (target.position.y - transform.position.y));
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
    }

    public void GoHome()
    {
        anim.SetFloat("moveX", (homePosition.position.x - transform.position.x));
        anim.SetFloat("moveY", (homePosition.position.y - transform.position.y));
        transform.position = Vector3.MoveTowards(transform.position, homePosition.position, moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, homePosition.position) == 0)
        {
            anim.SetBool("isMoving", false);
        }
    }
}
