using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTutor : MonoBehaviour
{
    public GameObject interact;

    IEnumerator serang()
    {

        interact.SetActive(true);

        yield return new WaitForSeconds(3f);

        interact.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(serang());
        }
    }
}
