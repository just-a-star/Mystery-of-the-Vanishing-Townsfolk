using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DashTutor : MonoBehaviour
{
    public PlayableDirector pd;
    public GameObject dash;
    public GameObject atk;

    // Start is called before the first frame update
    void Start()
    {
        pd.stopped += OnTimeLineFinished;
    }

    void OnTimeLineFinished(PlayableDirector director)
    {
        StartCoroutine(gerak());
        StartCoroutine(serang());
    }

    IEnumerator gerak()
    {
        dash.SetActive(true);
        yield return new WaitForSeconds(4f);
        dash.SetActive(false);
        
    }

    IEnumerator serang()
    {
        yield return new WaitForSeconds(5f);
        atk.SetActive(true) ;
        yield return new WaitForSeconds(4f);
        atk.SetActive(false) ;
    }
}
