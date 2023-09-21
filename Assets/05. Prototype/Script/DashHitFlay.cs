using MoreMountains.Feedbacks;
using UnityEngine;

public class DashHitFlay : MonoBehaviour
{

    [SerializeField] private float speedForward;
    [SerializeField] private float speedHeigh;
    [SerializeField] private float speeFall;

    private ControllerInput _input;
    public bool isFlay;
    public bool isfalling;

    private void Start()
    {
        _input = GameManager.singleton._input;
    }
    private void Update()
    {
        HitCheck();
        HitFlay();
        isFalling();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            isFlay = true;
        }
    }
    private void HitFlay()
    {
        if(isFlay)
        {
            transform.position += (speedForward * this.transform.forward + speedHeigh * this.transform.up) * Time.deltaTime;
        }
    }
    private void isFalling()
    {
        if(isfalling)
        {
            transform.position += this.transform.up * -speeFall * Time.deltaTime;
        }
    }
    private void HitCheck()
    {
        if(_input.ButtonY)
        {
            isFlay = false;
            isfalling = true;
        }
    }
}
