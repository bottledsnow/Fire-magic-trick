using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class Teach_TimelineTrigger : MonoBehaviour
{
    [SerializeField] private PlayableDirector playableDirector;
    private ProgressSystem _progressSystem;
    private bool Trigger;
    private PlayableDirector _playableDirector;
    private void Start()
    {
        _progressSystem = GameManager.singleton.GetComponent<ProgressSystem>();
        _playableDirector = GameManager.singleton.GetComponent<TimelineSystem>().PlayableDirector;
        _progressSystem.OnPlayerDeath += ResetTrigger;
    }
    private void OnTriggerEnter(Collider other)
    {   
        if(other.CompareTag("Player"))
        {
            useThisTimeline();
        }
    }
    private void useThisTimeline()
    {
        if(!Trigger)
        {
            playableDirector.Stop();
            playableDirector.Play();
            Trigger = true;
        }
    }
    private void ResetTrigger()
    {
        Trigger = false;
    }
}
