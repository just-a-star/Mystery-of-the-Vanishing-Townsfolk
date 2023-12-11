using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PortalBoss : MonoBehaviour
{
    [Header("load Scene")]
    public string sceneToLoad;
    public bool playerInRange;

    [Header("posisi Player")]
    public Vector2 playerPosition;
    public VectorValue playerStorage;

    [Header("Fading")]
    FadeInOut fade;

    [Header("key")]
    public BoolValue key;

    [Header("dialog")]
    public GameObject dialogBox;
    public Text dialogText;

    // Start is called before the first frame update
    void Start()
    {
        fade = FindObjectOfType<FadeInOut>();

        fade.FadeOut();
    }

    public IEnumerator ubahScene()
    {
        fade.FadeIn();
        yield return new WaitForSeconds(1);
        playerStorage.initialValue = playerPosition;
        SceneManager.LoadScene(sceneToLoad);
    }

    // Update is called once per frame
    void Update()
    {
        buka();
    }

    void buka()
    {
        if (Input.GetButtonDown("interact") && playerInRange)
        {
            if (key.initialValue == true)
            {
                Debug.Log("keubah g");
                StartCoroutine(ubahScene());

            } else
            {
                StartCoroutine(pesan());

            }
        }
    }

    IEnumerator pesan()
    {
        dialogBox.SetActive(true);
        dialogText.text = "Ambil Kunci Dahulu Sebelum Pergi";
        yield return new WaitForSeconds(2);
        dialogBox.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
