using UnityEngine;
using System.Threading.Tasks;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private float DelayTime;
    [SerializeField] private float speed;
    [SerializeField] private float stopTime;

    public bool isReturn;

    private MeshRenderer meshRenderer;
    private Collider thisCollider;

    private float delayTimer = 0;
    private float timer = 0;
    private bool Trigger;
    private bool isTimerFinish;
    private bool isTimer;
    private bool isMove = false;
    private bool isTimerStop;
    private bool isStart;
    private int moveIndex = 0;
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        thisCollider = GetComponent<Collider>();
    }
    private void Update()
    {
        if(isStart)
        {
            MoveSystem();
            stopTimerSystem();
        }else
        {
            delayTimer += Time.deltaTime;
            delayTimerCheck();
        }
    }
    private void delayTimerCheck()
    {
        if(delayTimer > DelayTime)
        {
            isStart = true;
        }
    }
    private void MoveSystem()
    {
        MoveToPoint(points[moveIndex]);
    }
    private void MoveToPoint(Transform point)
    {
        Vector3 TargetPoint = point.position;
        Vector3 Direction = (TargetPoint - this.transform.position).normalized;

        isMove = MoveCheck(point);

        if (isMove)
        {
            this.transform.position += Direction * speed * Time.deltaTime;
            SetTrigger(false);
        }

        if(!isMove)
        {
            if(!Trigger)
            {
                SetTrigger(true);
                SetIsTimer(true);
            }

            if(!isMove && isTimerFinish)
            {
                MoveIndexAdd();
                SetIsTimerFinish(false);
            }
        }
    }
    private bool MoveCheck(Transform point)
    {
        Vector3 length = point.position - this.transform.position;

        if (length.magnitude < 0.25f)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    private void stopTimerSystem()
    {
        if(isTimer)
        {
            timer += Time.deltaTime;
        }

        if(timer > stopTime)
        {
            timer = 0;
            SetIsTimer(false);
            SetIsTimerFinish(true);
        }
    }
    private void MoveIndexAdd()
    {
        if(moveIndex == 0)
        {
            SetisReturn(false);
        }

        moveIndex++;

        if (moveIndex >= points.Length)
        {
            SetMoveIndex(0);
        }
    }
    private void SetMoveIndex(int value)
    {
        moveIndex = value;

        if(value ==0)
        {
            SetisReturn(true);
        }
    }
    private void SetisReturn(bool active)
    {
          isReturn = active;
    }
    private void SetIsTimer(bool active)
    {
        isTimer = active;
    }
    private void SetIsTimerFinish(bool active)
    {
        isTimerFinish = active;
    }
    private void SetTrigger(bool active)
    {
        Trigger = active;
    }
    private void SetIsTimerStop(bool active)
    {
        isTimerStop = active;
    }
    public void SetActivePlatform(bool active)
    {
        meshRenderer.enabled = active;
        thisCollider.enabled = active;
    }
    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < points.Length; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(points[i].position, 0.5f);
        }
    }

}
