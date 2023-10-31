using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class RoomMove : MonoBehaviour
{
    public Vector2 cameraChange;
    public Vector3 playerChange;
    private cameraMove cam;
    public bool needText;
    public string placeName;
    public GameObject text;

    //next tmbh text tpi blom ditambah
    /*public Text placeText;*/

    void Start()
    {
        cam = Camera.main.GetComponent<cameraMove>();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        cam.minPosition += cameraChange;
        cam.maxPosition += cameraChange;
        collision.transform.position += playerChange;

        //blom nmbh teks
        /*if(needText)
        {
            StartCoroutine(placeNameCo());
        }*/
    }

    //blom nambah teks nama
   /* IEnumerator placeNameCo()
    {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(4f);
        text.SetActive(false);
    }*/

}


