using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CobaPocong : Enemy
{

    public BoolValue pocong;
    public GameObject portalBack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        DeathEffect();
        MakeLoot();
        portalBack.SetActive(true);
        pocong.initialValue = true;
        Destroy(gameObject);

    }
}
