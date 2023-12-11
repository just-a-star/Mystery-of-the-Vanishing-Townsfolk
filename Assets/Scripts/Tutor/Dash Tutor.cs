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

    bool tlEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        pd.stopped += OnTimeLineFinished;
    }

    private void Update()
    {

        if (tlEnd == true)
        {
            pc.state = PlayerState.walk;

        }
        else
        {
            pc.state = PlayerState.stun;
        }

    }

    void OnTimeLineFinished(PlayableDirector director)
    {
        tlEnd = true;
        StartCoroutine(gerak());
        StartCoroutine(serang());
    }

    IEnumerator gerak()
    {
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
