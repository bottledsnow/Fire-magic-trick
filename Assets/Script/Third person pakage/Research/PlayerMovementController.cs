using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovementController : MonoBehaviour
{
    private PlayerInputManager playerInput;
    private NavMeshAgent _agent;
    public Vector2 _move;
    public Vector2 _look;
    public float aimValue;
    public float fireValue;

    public Vector3 nextPosition;
    public Quaternion nextRotation;

    public float rotationPower = 3f;
    public float rotationLerp = 0.5f;

    public float speed = 1f;
    public Camera camera;

    private Vector3 angles;
    private float angle;
    [Header("旋轉上下限")]
    public float maxAngle = 40;
    public float minAngle = 340;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        playerInput = GetComponent<PlayerInputManager>();
    }

    private void Start()
    {
        SetAangle();
    }
    private void getPlayerInput()
    {
        GetMovement();
        GetLook();
    }
    public void GetMovement()
    {
        _move.x = playerInput.Horizontal;
        _move.y = playerInput.Vertical;
    }
    
    public void GetLook()
    {
        _look.x = playerInput.MouseX;
        _look.y = playerInput.MouseY;
    }
    /*
    public void OnAim(InputValue value)
    {
        aimValue = value.Get<float>();
    }
    
    public void OnFire(InputValue value)
    {
        fireValue = value.Get<float>();
    }
    */
    public GameObject followTransform;

    private void Update()
    {
        getPlayerInput();
        
        followTransform.transform.rotation *= Quaternion.AngleAxis(_look.x * rotationPower, Vector3.up);

        #region Vertical Rotation
        followTransform.transform.rotation *= Quaternion.AngleAxis(_look.y * rotationPower, Vector3.right);

        var angles = followTransform.transform.localEulerAngles;
        angles.z = 0;

        var angle = followTransform.transform.localEulerAngles.x;

        //Clamp the Up/Down rotation
        if (angle > 180 && angle < minAngle)
        {
            angles.x = minAngle;
        }
        else if (angle < 180 && angle > maxAngle)
        {
            angles.x = maxAngle;
        }


        followTransform.transform.localEulerAngles = angles;
        #endregion


        nextRotation = Quaternion.Lerp(followTransform.transform.rotation, nextRotation, Time.deltaTime * rotationLerp);

        if (_move.x == 0 && _move.y == 0)
        {
            nextPosition = transform.position;

            if (aimValue == 1)
            {
                //Set the player rotation based on the look transform
                transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
                //reset the y rotation of the look transform
                followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
            }

            return;
        }
        float moveSpeed = speed / 100f;
        Vector3 position = (transform.forward * _move.y * moveSpeed) + (transform.right * _move.x * moveSpeed);
        nextPosition = transform.position + position;


        //Set the player rotation based on the look transform
        transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
        //reset the y rotation of the look transform
        followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
    }

    private void HorizonRotation()
    {
        followTransform.transform.rotation *= Quaternion.AngleAxis(_look.x * rotationPower, Vector3.up);
    }
    private void VerticalRotation()
    {
        followTransform.transform.rotation *= Quaternion.AngleAxis(_look.y * rotationPower, Vector3.right);
        angles.z = 0;

        if (angle > 180 && angle < minAngle)
        {
            angles.x = minAngle;
        }
        else if (angle < 180 && angle > maxAngle)
        {
            angles.x = maxAngle;
        }

        followTransform.transform.localEulerAngles = angles;
    }
    private void SetAangle()
    {
        angles = followTransform.transform.localEulerAngles;
        angle = followTransform.transform.localEulerAngles.x;
    }

}
