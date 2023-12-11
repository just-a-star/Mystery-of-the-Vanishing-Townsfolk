using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DashTutor : MonoBehaviour
{
    public PlayableDirector pd;
    public GameObject dash;
    public GameObject atk;

    public PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        pc.state = PlayerState.stun;
        pd.stopped += OnTimeLineFinished;
    }

    void OnTimeLineFinished(PlayableDirector director)
    {
        StartCoroutine(gerak());
        StartCoroutine(serang());
    }

    IEnumerator gerak()
    {
        pc.state = PlayerState.idle;
        dash.SetActive(true);
        yield return new WaitForSeconds(3f);
        dash.SetActive(false);
        
    }

    IEnumerator serang()
    {
        yield return new WaitForSeconds(4f);
        atk.SetActive(true) ;
        yield return new WaitForSeconds(3f);
        atk.SetActive(false) ;
    }
}
