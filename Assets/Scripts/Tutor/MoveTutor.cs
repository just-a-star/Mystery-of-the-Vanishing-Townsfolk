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

    // Start is called before the first frame update
    void Start()
    {
        pd.stopped += OnTimeLineFinished;
    }

    void OnTimeLineFinished(PlayableDirector director)
    {
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
