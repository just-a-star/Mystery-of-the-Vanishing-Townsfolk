using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DashTutor : MonoBehaviour
{
    public PlayableDirector pd;
    public GameObject dash;

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
        dash.SetActive(true);
        yield return new WaitForSeconds(2f);
        dash.SetActive(false);
        
    }
}
