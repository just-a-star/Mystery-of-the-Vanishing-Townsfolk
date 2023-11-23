using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSave : SaveData
{
    [Header("Inpo Save")]
    public float jarakTarget;
    public GameObject DialogBox;
    public Transform target;

    [Header("Notif Save")]
    public GameObject NotifSave;
    [SerializeField] float WaktuMuncul;

    private void Update()
    {
        if (Vector3.Distance(target.position, transform.position) <= jarakTarget)
        {
            DialogBox.SetActive(true);
        }
        else
        {
            DialogBox.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !collision.isTrigger)
        {
            StartCoroutine(notifSave());
            SaveGame();
        }
    }

    IEnumerator notifSave()
    {
        NotifSave.SetActive(true);
        yield return new WaitForSeconds(WaktuMuncul);
        NotifSave.SetActive(false );
    }
}
