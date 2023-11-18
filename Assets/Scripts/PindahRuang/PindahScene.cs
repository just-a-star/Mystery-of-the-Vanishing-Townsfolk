using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PindahScene : MonoBehaviour
{

    public string sceneToLoad;
    public bool playerInRange;
    public Vector2 playerPosition;
    public VectorValue playerStorage;

    public Vector2 cameraNewMax;
    public Vector2 cameraNewMin;
    public VectorValue cameraMin;
    public VectorValue cameraMax;

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
        ResetCamera();
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

    public void ResetCamera()
    {
        cameraMax.initialValue = cameraNewMax;
        cameraMin.initialValue = cameraNewMin;
    }
}
