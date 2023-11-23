using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class TreasureChest : Interactable
{
    /*public GameObject gambar;*/
    public Item contents;
    public Inventory playerInventory;
    public bool isOpen;
    public BoolValue Kebuka;
    public GameObject dialogBox;
    public Text dialogText;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isOpen = Kebuka.initialValue;

        if(isOpen)
        {
            anim.SetBool("opened", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetButtonDown("interact") && playerInRange)
        {
            if( !isOpen )
            {
                OpenChest();
            } else
            {
                ChestOpened();
            }
        }
        
    }

    public void OpenChest()
    {
        dialogBox.SetActive(true);

        dialogText.text = contents.itemDescription;

        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;

        //player anim
        PlayerController.singleton.RaiseItem();

        context.SetActive(false);

        /*gambar.SetActive(true);*/

        isOpen = true;
        
        anim.SetBool("opened", true);
        Kebuka.initialValue = isOpen;
    }

    public void ChestOpened()
    {
        dialogBox.SetActive(false );
        /*gambar.SetActive(false);*/

        //player anim stop
        PlayerController.singleton.RaiseItem();

        playerInRange = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger && !isOpen)
        {
            context.SetActive(true);
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") &&  !collision.isTrigger && !isOpen)
        {
            context.SetActive(false);
        }
    }
}
