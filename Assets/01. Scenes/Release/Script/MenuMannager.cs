using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class MenuMannager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayableDirector timeline;
    private ControllerInput _input;

    private bool trigger;
    private void Start()
    {
        _input = GetComponent<ControllerInput>();
    }
    private void Update()
    {
        CheckInput();
    }
    private void CheckInput()
    {
        if(_input.anyKey && !trigger)
        {
            InputEvent();
            trigger = true;
        }
    }
    private void InputEvent()
    {
        animator.Play("Menu");
        timeline.Play();
    }
    public void LoadStartSence()
    {
        SceneManager.LoadScene("StarBall");
    }
}
