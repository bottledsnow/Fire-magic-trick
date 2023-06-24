using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class PokerBullet : MonoBehaviour
{
    private bool Hit;
    [SerializeField] private float speed;
    [SerializeField] private float rotate_Vertical;
    [SerializeField] private float rotate_Horizontal;
    [SerializeField] private AnimationCurve Curve;

    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        CurveTest();
        Destroy(this, 3f);
    }
    private void FixedUpdate()
    {
        GiveSpeed();
        GiveRotate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Hit = true;
    }

    private void GiveSpeed()
    {
        if(!Hit)
        {
            rb.velocity = this.transform.forward * speed;
        }else
        {
            rb.velocity = Vector3.zero;
        }
    }
    private void GiveRotate()
    {
        if(Hit)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }else
        {
            this.transform.Rotate(new Vector3(rotate_Vertical, rotate_Horizontal, 0));
        }
    }
    private async void CurveTest()
    {
        float t = 0;
        for(int i = 0; i < 10;i++) 
        {
            await Task.Delay(100);
            t += 0.1f;
            rotate_Horizontal = rotate_Horizontal * (1-Curve.Evaluate(t));
            rotate_Vertical = rotate_Vertical * (1-Curve.Evaluate(t));
        }
    }
    public void initialization_Hori(float horizontal)
    {
        rotate_Horizontal = horizontal;
    }
    public void initialization_Vert(float Vertical)
    {
        rotate_Vertical = Vertical;
    }
}
