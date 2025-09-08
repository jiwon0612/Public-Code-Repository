using UnityEngine;
using UnityEngine.Playables;

public class TimeLineController : MonoBehaviour
{
    public PlayableDirector playableDirector;


    private void Start()
    {
        playableDirector.playOnAwake = false;
    }

    public void PlayTimeLine()
    {
        playableDirector.gameObject.SetActive(true);
        playableDirector.Play();
    }

    public void StopTimeLine()
    {
        playableDirector.Stop();
        playableDirector.gameObject.SetActive(false);
    }
}
