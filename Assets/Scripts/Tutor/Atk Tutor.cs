using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkTutor : MonoBehaviour
{
    public GameObject atk;
 
    IEnumerator serang()
    {

        atk.SetActive(true);

        yield return new WaitForSeconds(3f);

        atk.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        { 
            StartCoroutine(serang());
        }
    }
}
