using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MskRumah : MonoBehaviour
{

    public string sceneToLoad;
    public bool playerInRange;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    FadeInOut fade;

    void Start()
    {
        fade = FindObjectOfType<FadeInOut>();

        fade.FadeOut();
    }

    public IEnumerator ubahScene()
    {
        fade.FadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneToLoad);
    }

    private void Update()
    {
        if (Input.GetButtonDown("interact") && playerInRange)
        {

            StartCoroutine(ubahScene());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerStorage.initialValue = playerPosition;
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
