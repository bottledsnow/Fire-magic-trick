using MoreMountains.Feedbacks;
using UnityEditor.Animations;
using UnityEngine;

public class NGP_Basic_UI : MonoBehaviour
{
    //Script
    [SerializeField] protected Animator animator_State;

    //Feedbacks
    protected MMF_Player UI_State;

    protected virtual void Start()
    {
        UI_State = GameManager.singleton.Feedbacks_List.GetComponent<Feedbacks_List>().UI_State;
    }
    protected virtual void Update()
    {
        
    }
    public void ToNone()
    {
        animator_State.SetBool("isNone", true);
        animator_State.SetBool("isFire", false);
        animator_State.SetBool("isWind", false);
    }
    public void ToFire()
    {
        UI_State.PlayFeedbacks();
        animator_State.SetBool("isNone", false);
        animator_State.SetBool("isFire", true);
        animator_State.SetBool("isWind", false);
    }
    public void ToWind()
    {
        UI_State.PlayFeedbacks();
        animator_State.SetBool("isNone", false);
        animator_State.SetBool("isFire", false);
        animator_State.SetBool("isWind", true);
    }
}
