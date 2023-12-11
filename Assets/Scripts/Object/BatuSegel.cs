using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class BatuSegel : Interactable
{
    [Header("Inventori")]
    public Inventory playerInventory;
    public int keyPerlu;

    [Header("gambar & collider")]
    public SpriteRenderer gambar;
    public PolygonCollider2D kolider;
    BoxCollider2D pemicunya;

    [Header("Dialog Box")]
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;

    [Header("ShakeCam")]
    public CinemachineImpulseSource impulseSource;
    public float shakeForce = 1f;

    private void Start()
    {
        pemicunya = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("interact") && playerInRange && Time.timeScale == 1)
        {
            if(playerInventory.numberOfKeys >= keyPerlu)
            {
                playerInventory.numberOfKeys -= keyPerlu;
                buka();
            } else if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
            } else
            {
                dialogBox.SetActive(true);
                dialogText.text = dialog + $" ({playerInventory.numberOfKeys} / {keyPerlu})";
            }
        }
    }

    public void buka()
    {
        impulseSource.GenerateImpulseWithForce(shakeForce);
        gambar.enabled = false;
        kolider.enabled = false;
        pemicunya.enabled = false;
        context.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !collision.isTrigger)
        {
            context.SetActive(true);
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            context.SetActive(false);
            playerInRange = false;
            dialogBox.SetActive(false);
        }
    }
}
