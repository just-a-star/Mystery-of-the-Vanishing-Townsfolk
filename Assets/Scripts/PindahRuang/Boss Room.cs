using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class BossRoom : MonoBehaviour
{
    public GameObject batu;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !collision.isTrigger)
        {
            batu.SetActive(true);
        }
    }

}
