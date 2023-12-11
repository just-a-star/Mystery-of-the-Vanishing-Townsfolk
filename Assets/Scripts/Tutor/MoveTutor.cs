using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MoveTutor : MonoBehaviour
{

    public PlayableDirector pd;
    public GameObject atas;
    public GameObject bawah;
    public GameObject kanan;
    public GameObject kiri;

    public PlayerController pc;

    bool tlEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        pd.stopped += OnTimeLineFinished;
    }

    private void Update()
    {

        if(tlEnd == true)
        {
            pc.state = PlayerState.walk;
            
        } else
        {
            pc.state = PlayerState.stun;
        }
        
    }

    void OnTimeLineFinished(PlayableDirector director)
    {
        tlEnd = true;
        StartCoroutine(gerak());
        
    }

    IEnumerator gerak()
    {
        atas.SetActive(true);
        kanan.SetActive(true);
        kiri.SetActive(true);
        bawah.SetActive(true);
        yield return new WaitForSeconds(2f);
        atas.SetActive(false);
        kanan.SetActive(false);
        kiri.SetActive(false);
        bawah.SetActive(false);
    }
}
